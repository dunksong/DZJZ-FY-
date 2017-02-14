using System;
using System.Collections.Generic;
using System.Text;

namespace Cyvation.CCQE.Common
{
    /// <summary>
    /// ���л����󴴽���
    /// </summary>
    public static class SerializerCreator
    {
        /// <summary>
        /// ����XML���л�����ʵ��
        /// </summary>
        /// <returns></returns>
        public static ISerializeObject CreateSerializer()
        {
            return CreateSerializer(SerializeType.XML);
        }

        /// <summary>
        /// ����ָ�������л����ͣ�������Ӧ�����л�����ʵ����
        /// </summary>
        /// <param name="type">���л�����</param>
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
        /// ����ָ�������л�����,�����л�����δ֪�����ͼ���������Ӧ�����л�����ʵ����
        /// </summary>
        /// <param name="type">���л�����</param>
        /// <param name="includeTypes">���л�����δ֪�����ͼ���</param>
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
