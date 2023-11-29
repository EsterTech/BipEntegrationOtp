namespace Bip.Entegration.Otp.Extensions;

/// <summary>
/// 2 MSSDN
/// 0 HASH
/// 1 Tüm Aboneler
/// </summary>
public static class ReceiverTypeFinder
{
    public static int Convert(string str)
    {
        if (str.Length == 12 && long.TryParse(str, out long lng))
        {
            return 2;
        }
        return 0;
    }
}
