using System.IO;
using System.Text;
using System;

namespace Cyvation.CCQE.Common
{
    public static class SerializeObjectExtensions
    {
        public static string SerializeObject<T>(this ISerializeObject serializeObject, T instance)
        {
            return SerializeObject<T>(serializeObject, instance, Encoding.Default);
        }

        public static string SerializeObject<T>(this ISerializeObject serializeObject, T instance,Encoding encoding)
        {
            if (instance == null) throw new ArgumentNullException("instance");
          
            var ms = new MemoryStream();
            serializeObject.SerializeObject(instance, ms);
            return StreamConvert.ConvertStreamToString(ms, encoding);
        }

        public static T DeserializeObject<T>(this ISerializeObject serializeObject, string text, Encoding encoding)
        {
            if (string.IsNullOrEmpty(text)) throw new ArgumentNullException("text");
            var ms = new MemoryStream(encoding.GetBytes(text));
            return serializeObject.DeserializeObject<T>(ms);
        }
    }
}