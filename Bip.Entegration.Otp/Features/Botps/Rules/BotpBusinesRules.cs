using Bip.Entegration.Application.Features.Clients.Queries.GetByClientId;
using Bip.Entegration.Application.Features.OutgoingMessages.Commands.Create;
using Bip.Entegration.Domain.Entities.MessageTemplates;
using Bip.Entegration.Domain.Entities.OutgoingMessages;
using Bip.Entegration.Domain.Enums;
using Bip.Entegration.Otp.BaseRules;
using Bip.Entegration.Otp.Extensions;
using Bip.Entegration.Otp.Features.BotpLogs.Commands.Create;
using Bip.Entegration.Otp.Features.Botps.Commands.UpdateStatusByTransactionId;
using Bip.Entegration.Otp.Features.Botps.Handlers.Commands.Create;
using Bip.Entegration.Otp.Models;
using Bip.Entegration.Otp.Models.Enums;
using Core.ApiHelpers.JwtHelper.Models;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.CrossCuttingConcerns.Helpers.HashHelpers;
using Core.Security.CipherHelpers;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace Bip.Entegration.Otp.Features.Botps.Rules;

public class BotpBusinesRules : BaseOtpBusinessRules
{
    IMediator _mediator;
    TokenParameters _tokenParameters;
    IConfiguration _configuration;
    CipherHelper _cipherHelper;
    public BotpBusinesRules(IMediator mediator, TokenParameters tokenParameters, IConfiguration configuration) : base(tokenParameters)
    {
        _mediator = mediator;
        _tokenParameters = tokenParameters;
        _configuration = configuration;
        _cipherHelper = new CipherHelper();
    }

    public BotpGeneratedValues GenerateOtp()
    {
        byte[] secretHash, secretSalt;
        var generatedNumber = RandomNumberGenerator.GetInt32(100000, 999999);
        HashingHelper.CreatePasswordHash(generatedNumber.ToString(), out secretHash, out secretSalt);
        return new BotpGeneratedValues
        {
            Code = generatedNumber.ToString(),
            SecretHash = secretHash,
            SecretSalt = secretSalt
        };
    }

    public T Decode<T>(string data)
        where T : class, new()
    {
        try
        {
            var decryptJson = _cipherHelper.Decrypt(data, _configuration["SecurityKey"]!);
            return JsonConvert.DeserializeObject<T>(decryptJson)!;
        }
        catch (Exception ex)
        {
            throw new BusinessException("Veri Doğrulanamadı !!!");
        }
    }

    public string Encrypt(object obj)
    {
        try
        {
            return _cipherHelper.Encrypt(JsonConvert.SerializeObject(obj), _configuration["SecurityKey"]!);
        }
        catch (Exception ex)
        {
            throw new BusinessException("Veri Doğrulanamadı !!!");
        }
    }

    public async Task<Botp> CreateWaitingBotp(string target, byte[] hash, string clientId)
    {
        var client = await _mediator.Send(new GetByClientIdClientQuery { ClientId = clientId });
        return new Botp
        {
            Id = Guid.NewGuid(),
            BotpStatus = BotpStatus.Waiting,
            ClientId = clientId,
            CreatedTime = DateTime.Now,
            TryCount = 0,
            KeyHash = hash,
            LastValidTime = DateTime.Now.AddSeconds(client.OtpExpiresIn.HasValue ? client.OtpExpiresIn.Value : 60),
            Target = target
        };
    }

    public async Task SendOtpToTarget(string otpCode, string target, string clientId)
    {
        await _mediator.Send(new CreateOutgoingMessageCommand
        {
            ClientId = clientId,
            TxnId = Guid.NewGuid().ToString(),
            Receiver = new Receiver
            {
                Address = target,
                Type = ReceiverTypeFinder.Convert(target),
            },

            Composition = new BipMessageTemplateWrapper
            {
                List = new List<BipMessageTemplate>
                    {
                        new BipMessageTemplate
                        {

                            Type = (int)OutgoingMessageType.Text,
                            Content= $"Otp Kodunuz : {otpCode}"
                        }
                    }
            },
            MessageStatus = MessageStatus.Waiting
        });
    }

    public bool ReturnFalseIfOtpNotFound(Botp? otp)
    {
        return otp != null;
    }

    public async Task<bool> ReturnFalseIfOtpInvalid(Botp otp, UpdateStatusByTransactionIdBotpCommand request)
    {
        if (HashingHelper.VerifyPasswordHash(request.Code, otp!.KeyHash, Convert.FromBase64String(request.Key)) && otp.TryCount <= 3)
        {
            return true;
        }

        await _mediator.Send(new CreateBotpLogCommand
        {
            BotpId = otp.Id,
            Description = "Geçersiz Otp Kodu",
            From = otp.BotpStatus,
            To = otp.BotpStatus,
        });
        return false;
    }
    public async Task ThrowOtpUsedLogEvent(Botp otp, BotpStatus from)
    {
        await _mediator.Send(new CreateBotpLogCommand
        {
            BotpId = otp.Id,
            Description = "Otp Kullanımı Başarılı",
            From = from,
            To = otp.BotpStatus,
        });

    }

}
