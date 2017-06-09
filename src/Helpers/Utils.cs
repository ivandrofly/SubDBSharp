﻿using System.IO;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;

namespace SubDBSharp
{
    public static class Utils
    {
        public static string FormatUserAgent(ProductHeaderValue procInfo)
        {
            return string.Format("{0} ({1} {2})", "SubDB/1.0", procInfo, "http://github.com/ivandrofly");
        }

        public static string GetMovieHash(string file)
        {
            int bytesToRead = 64 * 1024;
            using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var md5 = MD5.Create())
            {
                int len = (int)fs.Length;
                byte[] buffer = new byte[bytesToRead * 2];

                // Read first 64kb.
                fs.Read(buffer, 0, bytesToRead);

                // Read last 64kb.
                fs.Seek(-bytesToRead, SeekOrigin.End);
                fs.Read(buffer, bytesToRead, bytesToRead);
                byte[] computedBuffer = md5.ComputeHash(buffer);
                return ToHexadecimal(computedBuffer);
            }
        }

        public static string ToHexadecimal(byte[] bytes)
        {
            var _hexBuilder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                _hexBuilder.Append(bytes[i].ToString("x2"));
            }
            return _hexBuilder.ToString();
        }

    }
}