using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Cyvation.CCQE.Model
{
    /// <summary>
    /// 流程节点
    /// </summary>
    public enum EnumWorkFlowNode
    {
        [EnumDescription("001")]
        考评 = 1,

        [EnumDescription("002")]
        生成通知书 = 2,

        [EnumDescription("003")]
        待接收 = 3,

        [EnumDescription("004")]
        已接收 = 4,

        [EnumDescription("005")]
        生成反馈书 = 5,

        [EnumDescription("006")]
        已反馈 = 6,

        [EnumDescription("007")]
        确认 = 7
    }

    public enum EnumDocType
    {
        通知书 = 1,
        反馈书 = 2        
    }
    
    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field, AllowMultiple = false)]
    public class EnumDescriptionAttribute : Attribute
    {
        private string description;

        public string Description
        {
            get
            {
                return this.description;
            }
        }

        public EnumDescriptionAttribute(string description)
            : base()
        {
            this.description = description;
        }
    }

    /// <summary>
    /// 获取枚举相关描述
    /// </summary>
    public class Cls_EnumHandle
    {
        public static string GetDescription(Enum value)
        {
            if (value == null) throw new ArgumentNullException("value");
            string description = value.ToString();
            FieldInfo fieldInfo = value.GetType().GetField(description);
            EnumDescriptionAttribute[] attr = (EnumDescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);
            if (attr != null && attr.Length > 0)
            {
                description = attr[0].Description;
            }
            return description;
        }

        /// <summary>
        /// 获取枚举对象指定值的描述信息
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <param name="Iflag">枚举对象指定值</param>
        /// <returns></returns>
        public static string GetDeInfo(Type type, string Iflag)
        {
            string RetInfo = string.Empty;
            if (!string.IsNullOrEmpty(Iflag))
            {
                foreach (Enum value in Enum.GetValues(type))
                {
                    if (value.GetHashCode() == int.Parse(Iflag))
                    {
                        RetInfo = GetDescription(value);
                    }
                }
            }
            return RetInfo;
        }

        /// <summary>
        /// 获取枚举HashCode的中文值
        /// </summary>
        /// <param name="type"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static string GetValueString(Type type, int flag)
        {
            string RetInfo = string.Empty;
            foreach (Enum value in Enum.GetValues(type))
            {
                if (value.GetHashCode() == flag)
                {
                    RetInfo = value.ToString();
                }
            }
            return RetInfo;
        }       
    }
}
