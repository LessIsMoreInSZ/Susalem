using System.Text;

namespace susalem.Communication.Share;

public static class ByteHelper
{
    public static string ToUtf8Str(this byte[] bytes)
    {
        return Encoding.UTF8.GetString(bytes);
    }

    public static string ToHexString(this byte[] bytes, string spit = " ")
    {
        return BitConverter.ToString(bytes).Replace("-", spit);
    }

    public static byte[] HexStringToArray(this string hexStr)
    {
        hexStr = hexStr.Replace(" ", "");
        var returnBytes = new byte[(int)Math.Ceiling(hexStr.Length / 2.0)];
        for (var i = 0; i < hexStr.Length / 2; i++)
        {
            returnBytes[i] = Convert.ToByte(hexStr.Substring(i * 2, 2), 16);
        }

        if (hexStr.Length % 2 != 0)
        {
            returnBytes[^1] = Convert.ToByte(hexStr.Substring(hexStr.Length - 1), 16);
        }

        return returnBytes;
    }
}

