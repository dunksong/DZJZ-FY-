using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Cyvation.CCQE.Common
{
    /// <summary>
    /// 引用类型的键值对
    /// </summary>
    /// <typeparam name="TKey">键</typeparam>
    /// <typeparam name="TValue">值</typeparam>
    [Serializable]
    [DataContract]
    public class KeyValueItem<TKey,TValue>
    {
        TKey key;
        TValue value;       
        
        /// <summary>
        /// 键
        /// </summary>
       [DataMember]
        public TKey Key
        {
            get
            {
                return key;
            }
            set
            {
                key = value;
            }
        }
        /// <summary>
        /// 值
        /// </summary>
       [DataMember]
       public TValue Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
            }
        }
        public KeyValueItem()
        {

        }
        public KeyValueItem(TKey key, TValue value)
        {
            this.key = key;
            this.value = value;
        }      

        public  override string ToString()
        {
            return key == null || string.IsNullOrEmpty(key.ToString()) ? base.ToString() : key.ToString();           
        }
    }

    [Serializable]
    [DataContract]
    public class KeyValueItem :KeyValueItem<string,object>
    {
        public KeyValueItem():base()
        { 
        }

        public KeyValueItem(string key, object value):base(key,value)
        {
        }
    }


}
