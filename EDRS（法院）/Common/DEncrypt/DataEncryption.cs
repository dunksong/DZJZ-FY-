using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace EDRS.Common
{
    public sealed class DataEncryption
    {
        private static byte[] KEY = new byte[8] { 10, 13, 39, 27, 32, 12, 63, 55 };


        private static byte ByteXor(byte b, byte key)
        {
            b = (byte)(b ^ key);
            return b;
        }

        private static byte[] ByteXor(byte[] data, byte[] key)
        {
            var keyLen = key.Length;
            var dataLen = data.Length;
            if (dataLen == 0)
            {
                return data;
            }
            for (var i = 0; i < dataLen; i++)
            {
                data[i] = ByteXor(data[i], key[i % keyLen]);
            }
            return data;
        }

        /// <summary>
        /// 加密，key必须是2的n次方
        /// </summary>
        public static byte[] Encryption(byte[] data, byte[] key)
        {
            return ByteXor(data, key);
        }

        /// <summary>
        /// 加密, 使用内置密钥
        /// </summary>
        public static byte[] Encryption(byte[] data)
        {
            return ByteXor(data, KEY);
        }

        /// <summary>
        /// 加密文件
        /// </summary>
        /// <param name="srcFile">明文文件</param>
        /// <param name="descFile">密文文件</param>
        /// <param name="key">密钥</param>
        public static void Encryption(string srcFile, string descFile, byte[] key)
        {
            var fs = File.OpenRead(srcFile);
            var newfs = File.Create(descFile, 1024 * 512);
            var count = 0;
            var keyLen = key.Length;

            var buffer = new byte[1024 * 512];
            using (fs)
            {
                using (newfs)
                {
                    if (fs.Length == 0)
                    {
                        return;
                    }
                    while (fs.Position < fs.Length)
                    {
                        count = fs.Read(buffer, 0, 1024 * 512);
                        for (var i = 0; i < count; i++)
                        {
                            buffer[i] = ByteXor(buffer[i], key[i % keyLen]);
                        }
                        newfs.Write(buffer, 0, count);
                    }
                }
            }
        }

        /// <summary>
        /// 加密文件, 使用内置密钥
        /// </summary>
        /// <param name="srcFile">明文文件</param>
        /// <param name="descFile">密文文件</param>
        public static void Encryption(string srcFile, string descFile)
        {
            Encryption(srcFile, descFile, KEY);
        }


        /// <summary>
        /// 解密
        /// </summary>
        public static byte[] Decryption(byte[] data, byte[] key)
        {
            return ByteXor(data, key);
        }

        /// <summary>
        /// 解密, 使用内置密钥
        /// </summary>
        public static byte[] Decryption(byte[] data)
        {
            return ByteXor(data, KEY);
        }


        /// <summary>
        /// 解密文件
        /// </summary>
        /// <param name="srcFile">密文文件</param>
        /// <param name="descFile">解密后的文件</param>
        /// <param name="key">密钥</param>
        public static void Decryption(string srcFile, string descFile, byte[] key)
        {
            Encryption(srcFile, descFile, key);
        }
        /// <summary>
        /// 解密文件, 使用内置密钥 
        /// </summary>
        /// <param name="srcFile">密文文件</param>
        /// <param name="descFile">解密后的文件</param>
        public static void Decryption(string srcFile, string descFile)
        {
            Decryption(srcFile, descFile, KEY);
        }

        /// <summary>
        /// 根据文件路径判断是否加密文件
        /// </summary>
        /// <param name="srcFile">文件路径</param>
        /// <returns></returns>
        public static bool JudgeIsEncryFile(string srcFile)
        {
            return srcFile.LastIndexOf(".encry") > 0;
        }
    }
}
