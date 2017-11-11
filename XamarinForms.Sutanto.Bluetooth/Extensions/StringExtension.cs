using System;
using System.Text;

namespace XamarinForms.Sutanto.Bluetooth.Extensions
{
    public static class StringExtension
    {
        public static byte[] ToBytes(this String text)
        {
            return Encoding.Unicode.GetBytes(text);
        }
        public static byte ToBytes(this char character)
        {
            return Convert.ToByte(character);
        }
    }
}
