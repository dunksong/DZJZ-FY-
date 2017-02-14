using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Cyvation.CCQE.Common
{
    /// <summary>
    /// 事件创建者
    /// </summary>
    public class EventCreator
    { 
        public static bool ContainEvent(object objSource, string eventName)
        {
            EventInfo evClick = objSource.GetType().GetEvent(eventName);
            return evClick != null;
        }

        public void CreateEvent(object objSource, string eventName, object methodContainer, string methodName)
        {
            EventInfo evClick = objSource.GetType().GetEvent(eventName);
            if (evClick == null)
                throw new NullReferenceException(string.Format("在事件触发者里未发现名称为“{0}”的事件，将无法关联！", eventName));
            Type tDelegate = evClick.EventHandlerType;
            MethodInfo miHandler = methodContainer.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (miHandler == null)
                throw new NullReferenceException(string.Format("在方法实现者里未发现名称为“{0}”的方法，将无法关联！", methodName));
            Delegate d = Delegate.CreateDelegate(tDelegate, methodContainer, miHandler);
            evClick.AddEventHandler(objSource, d);
            //调用
            //MethodInfo addHandler = evClick.GetAddMethod();
            //Object[] addHandlerArgs = { d };
            //addHandler.Invoke(objSource, addHandlerArgs);
        }

        public void CreateEvent(object objSource, string eventName, object methodContainer, MethodInfo methodInfo)
        {
            EventInfo evClick = objSource.GetType().GetEvent(eventName);
            if (evClick == null)
                throw new NullReferenceException(string.Format("在事件触发者里未发现名称为“{0}”的事件，将无法关联！", eventName));
            Type tDelegate = evClick.EventHandlerType;
            MethodInfo miHandler = methodInfo;
            if (miHandler == null)
                throw new ArgumentNullException("methodInfo");
            Delegate d = Delegate.CreateDelegate(tDelegate, methodContainer, miHandler);
            evClick.AddEventHandler(objSource, d);
        }
    }
}
