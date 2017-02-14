﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net.Layout;
using log4net.Layout.Pattern;
using System.Reflection;

namespace Cyvation.CCQE.Common
{
    public class ReflectionLayout : PatternLayout
    {
        public ReflectionLayout()
        {
            this.AddConverter("property", typeof(ReflectionPatternConverter));
        }
    }

    public class ReflectionPatternConverter : PatternLayoutConverter
    {
        protected override void Convert(System.IO.TextWriter writer, log4net.Core.LoggingEvent loggingEvent)
        {

            if (Option != null)
            {
                // 写入指定键的值
                WriteObject(writer, loggingEvent.Repository, LookupProperty(Option, loggingEvent));
            }
            else
            {
                // 写入所有关键值对
                WriteDictionary(writer, loggingEvent.Repository, loggingEvent.GetProperties());
            }
        }


        ///// <summary>
        ///// 通过快速反射获取传入的日志对象的某个属性的值
        ///// </summary>
        ///// <param name="property"></param>
        ///// <returns></returns>
        //private object LookupProperty(string property, log4net.Core.LoggingEvent loggingEvent)
        //{
        //    object propertyValue = string.Empty;

        //    PropertyInfo propertyInfo = loggingEvent.MessageObject.GetType().GetProperty(property);
        //    if (propertyInfo != null)
        //    {
        //        propertyValue = propertyInfo.FastGetValue(loggingEvent.MessageObject);

        //    }
        //    return propertyValue;
        //}


        /// <summary>
        /// 通过反射获取传入的日志对象的某个属性的值
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        private object LookupProperty(string property, log4net.Core.LoggingEvent loggingEvent)
        {
            object propertyValue = string.Empty;

            PropertyInfo propertyInfo = loggingEvent.MessageObject.GetType().GetProperty(property);
            if (propertyInfo != null)
            {
                propertyValue = propertyInfo.GetValue(loggingEvent.MessageObject, null);
            }
            return propertyValue;
        }


    }
}

