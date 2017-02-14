using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Cyvation.CCQE.Common
{
    public class StreamConvert
    {
        static StreamConvert()
        {

        }
        public static Stream ConvertStringToStream(string context, Encoding encoding)
        {
            Stream stream = null;
            try
            {
                stream = new MemoryStream(encoding.GetBytes(context));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return stream;
        }

        public static Stream ConvertBytesToStream(byte[] context)
        {
            Stream stream = null;
            try
            {
                stream = new MemoryStream(context);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return stream;
        }

        public static string ConvertStreamToString(Stream stream, Encoding encoding)
        {
            string strContext = string.Empty;
            try
            {
                stream.Seek(0, SeekOrigin.Begin);
                byte[] buffers = new byte[stream.Length];
                stream.Read(buffers, 0, buffers.Length);
                strContext = encoding.GetString(buffers);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                stream.Close();
            }
            return strContext;
        }

        public static byte[] ConvertStreamToBytes(Stream stream)
        {
            byte[] buffers = null;
            try
            {
                stream.Seek(0, SeekOrigin.Begin);
                buffers = new byte[stream.Length];
                stream.Read(buffers, 0, buffers.Length);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                stream.Close();
            }
            return buffers;
        }
    }
}
