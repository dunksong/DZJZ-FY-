using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Text;
using System.IO;

namespace Cyvation.CCQE.Common
{
    internal class JSONSerializerObject : ISerializeObject
    {
        #region ISerializeObject ≥…‘±

        public void SerializeObject<T>(T instance, string file)
        {           
            using (Stream fs = new FileStream(file, FileMode.Create, FileAccess.Write))
            {
                SerializeObject<T>(instance, fs);              
            }
        }

        public void SerializeObject<T>(T instance, System.IO.Stream s)
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof(T));
            jsonSerializer.WriteObject(s, instance);
        }

        public T DeserializeObject<T>(string file)
        {          
            using (Stream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                return DeserializeObject<T>(fs);      
            }            
        }

        public T DeserializeObject<T>(System.IO.Stream s)
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof(T));
           return  (T)jsonSerializer.ReadObject(s);
        }

        #endregion
    }
}
