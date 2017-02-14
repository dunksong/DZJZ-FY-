using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Cyvation.CCQE.Common
{
    public static class BinarySerializeClone
    {
        public static T Clone<T>(T objInstance)
        {
            Stream ms = new MemoryStream();
            IFormatter ff = new BinaryFormatter();//以二进制序列化类
            T obj = objInstance;
            try
            {
                ff.Serialize(ms, objInstance);
                ms.Seek(0, SeekOrigin.Begin);
                obj = (T)ff.Deserialize(ms);
            }
            catch //(Exception ex)
            {
                throw;
            }
            finally
            {
                ms.Close();
            }

            return obj;
        }
    }
}
