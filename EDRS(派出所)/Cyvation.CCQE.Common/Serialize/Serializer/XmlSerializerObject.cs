using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace Cyvation.CCQE.Common
{
    internal class XmlSerializerObject : ISerializeObject
    {
        Type[] _types;
        public XmlSerializerObject()
        {

        }
        public XmlSerializerObject(Type[] types)
        {
            _types = types;
        }

        XmlSerializer CreateXmlSerializer(Type type)
        {
            return _types == null ? new XmlSerializer(type) : new XmlSerializer(type, _types);
        }

        #region ISerializeObject ≥…‘±

        public void SerializeObject<T>(T instance, string file)
        {
            if (instance == null) throw new ArgumentNullException("instance");
            TextWriter writer = new StreamWriter(file);

            Type type = instance.GetType();
            XmlSerializer ser = CreateXmlSerializer(type);
            try
            {             
                ser.Serialize(writer, instance);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        public void SerializeObject<T>(T instance, System.IO.Stream s)
        {
            Type type = instance.GetType();
           XmlSerializer ser = CreateXmlSerializer(type);           
            try
            {              
                ser.Serialize(s, instance);
            }
            catch (Exception ex)
            {
                throw ex;
            }          
        }
        public T DeserializeObject<T>(string file)
        {
            T obj = default(T);
            if (!File.Exists(file))
            {
                return obj;
            }
            System.IO.FileStream objFileStream = new FileStream(file, FileMode.Open,FileAccess.Read);
            StreamReader reader = null;
            try
            {
                XmlSerializer ser = CreateXmlSerializer(typeof(T));       
                reader = new StreamReader(objFileStream);
                obj = (T)ser.Deserialize(reader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objFileStream != null)
                    objFileStream.Close();
            }
            return obj;
        }

        public T DeserializeObject<T>(System.IO.Stream s)
        {
            T obj = default(T);          
            try
            {
                XmlSerializer ser = CreateXmlSerializer(typeof(T));       
                obj = (T)ser.Deserialize(s);
            }
            catch (Exception ex)
            {
                throw ex;
            }        
            return obj;
        }

        #endregion
    }
}
