using System;
using Microsoft.AspNetCore.Mvc;

namespace CsvToJsonConverter.Utils
{
    public static class Helper
    {
        public static bool IsBase64String(string base64)
        {
            Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
            return Convert.TryFromBase64String(base64, buffer, out int bytesParsed);
        }
    }
}