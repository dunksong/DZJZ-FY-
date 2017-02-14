using System;
using System.Collections.Generic;
using System.Text;

namespace Cyvation.CCQE.Common
{
   public interface ISerializeObject
   { /// <summary>
       /// ���л�����
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <param name="instance"></param>
       /// <param name="file"></param>
       void SerializeObject<T>(T instance, string file);

       void SerializeObject<T>(T instance, System.IO.Stream s);


       /// <summary>
       /// �����л�����
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <param name="file"></param>
       /// <returns></returns>
       T DeserializeObject<T>(string file);

       T DeserializeObject<T>(System.IO.Stream s);
    }
}
