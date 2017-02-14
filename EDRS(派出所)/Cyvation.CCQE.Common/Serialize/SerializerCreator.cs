using System;
using System.Collections.Generic;
using System.Text;

namespace Cyvation.CCQE.Common
{
    /// <summary>
    /// 序列化对象创建器
    /// </summary>
    public static class SerializerCreator
    {
        /// <summary>
        /// 创建XML序列化对象实例
        /// </summary>
        /// <returns></returns>
        public static ISerializeObject CreateSerializer()
        {
            return CreateSerializer(SerializeType.XML);
        }

        /// <summary>
        /// 根据指定的序列化类型，创建对应的序列化对象实例。
        /// </summary>
        /// <param name="type">序列化类型</param>
        /// <returns></returns>
        public static ISerializeObject CreateSerializer(SerializeType type) {
            switch (type)
            {
                case SerializeType.XML:
                    return new XmlSerializerObject();
                case SerializeType.Binary:
                    return new BinarySerializerObject();
                case SerializeType.JSON:
                    return new JSONSerializerObject();
                default:
                    return null;
            }
        }

        /// <summary>
        /// 根据指定的序列化类型,与序列化对象未知的类型集，创建对应的序列化对象实例。
        /// </summary>
        /// <param name="type">序列化类型</param>
        /// <param name="includeTypes">序列化对象未知的类型集合</param>
        /// <returns></returns>
        public static ISerializeObject CreateSerializer(SerializeType type, Type[] includeTypes)
        {
            switch (type)
            {
                case SerializeType.XML:
                    return new XmlSerializerObject(includeTypes);
                case SerializeType.Binary:
                    return new BinarySerializerObject();
                case SerializeType.JSON:
                    return new JSONSerializerObject();
                default:
                    return null;
            }
        }
    }
}
