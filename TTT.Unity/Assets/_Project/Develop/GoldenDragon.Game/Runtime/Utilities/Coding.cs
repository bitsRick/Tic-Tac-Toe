using System;
using System.Text;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities
{
    public static class Coding
    {
        public static string GetCodingBase64(string data)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            return Convert.ToBase64String(bytes);
        }

        public static string GetEncodingBase64(string data)
        {
            byte[] decodedBytes = Convert.FromBase64String(data);
            return Encoding.UTF8.GetString(decodedBytes);
        }
    }
}