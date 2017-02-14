using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Cyvation.CCQE.Common
{
    internal class BinarySerializerObject : ISerializeObject
    {
        #region ISerializeObject 成员

        public void SerializeObject<T>(T instance, string file)
        {
            using (Stream s = new FileStream(file, FileMode.Create, FileAccess.Write))
            {
                SerializeObject<T>(instance, s);
            }
        }

        public void SerializeObject<T>(T instance, System.IO.Stream s)
        {
            IFormatter ff = new BinaryFormatter();//以二进制序列化类           
            try
            {
                ff.Serialize(s, instance);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T DeserializeObject<T>(string file)
        {
            Stream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
            IFormatter ff = new BinaryFormatter();//以二进制序列化类
            T obj = default(T);
            try
            {
                fs.Seek(0, SeekOrigin.Begin);
                obj = (T)ff.Deserialize(fs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                fs.Close();
            }
            return obj;
        }

        public T DeserializeObject<T>(System.IO.Stream s)
        {
            IFormatter ff = new BinaryFormatter();//以二进制序列化类
            T obj = default(T);
            try
            {
                s.Seek(0, SeekOrigin.Begin);
                obj = (T)ff.Deserialize(s);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                s.Close();
            }
            return obj;
        }

        #endregion
    }
}
