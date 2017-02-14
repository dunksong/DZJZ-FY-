using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Runtime.InteropServices;
using System.Net;

namespace Cyvation.CCQE.Common
{
    public static class WebClientHelper
    {
        /// <summary>
        /// 获取IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetIPAddress()
        {
            try
            {
                if (System.Web.HttpContext.Current == null || System.Web.HttpContext.Current.Request == null || System.Web.HttpContext.Current.Request.ServerVariables == null)
                {
                    return "";
                }

                string CustomerIP = string.Empty;
                //CDN加速后取到的IP
                CustomerIP = System.Web.HttpContext.Current.Request.Headers["Cdn-Src-Ip"];
                if (!string.IsNullOrEmpty(CustomerIP))
                {
                    return CustomerIP;
                }

                CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (!string.IsNullOrEmpty(CustomerIP))
                {
                    return CustomerIP;
                }

                if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                {
                    CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (CustomerIP == null)
                    {
                        CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    }
                }
                else
                {
                    CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                if (string.Compare(CustomerIP, "unknown", true) == 0)
                {
                    return System.Web.HttpContext.Current.Request.UserHostAddress;
                }
                return CustomerIP;
            }
            catch { }
            return "";
        }

        /// <summary>
        /// 获取IP地址
        /// </summary>
        /// <returns></returns>
        public static IPAddress GetIPv6Address()
        {
            IPAddress ipAddr = null;
            IPAddress[] arrIP = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress ip in arrIP)
            {
                if (System.Net.Sockets.AddressFamily.InterNetwork.Equals(ip.AddressFamily))
                {
                    ipAddr = ip;
                    break;
                }
                else if (System.Net.Sockets.AddressFamily.InterNetworkV6.Equals(ip.AddressFamily))
                {
                    ipAddr = ip;
                    break;
                }
            }
            return ipAddr;
        }

        /// <summary>
        /// 获取本机MAC地址
        /// </summary>
        /// <returns>本机的MAC地址</returns>
        public static string GetMacAddress()
        {
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc2 = mc.GetInstances();
            foreach (ManagementObject mo in moc2)
            {
                if ((bool)mo["IPEnabled"] == true)
                {
                    string mac = Convert.ToString(mo["MacAddress"]);
                    mo.Dispose();
                    return mac;
                }
            }
            return "";
        }

        [DllImport("Iphlpapi.dll")]

        static extern int SendARP(Int32 DestIP, Int32 SrcIP, ref Int64 MacAddr, ref Int32 PhyAddrLen);
        [DllImport("Ws2_32.dll")]

        static extern Int32 inet_addr(string ipaddr);
        /// <summary>
        /// SendArp获取MAC地址
        /// </summary>
        /// <param name="RemoteIP">目标机器的IP地址</param>
        /// <returns>目标机器的MAC地址</returns>
        public static string GetMacAddress(string RemoteIP)
        {
            StringBuilder macAddress = new StringBuilder();

            try
            {
                Int32 remote = inet_addr(RemoteIP);
                Int64 macInfo = new Int64();
                Int32 length = 6;
                SendARP(remote, 0, ref macInfo, ref length);
                string temp = Convert.ToString(macInfo, 16).PadLeft(12, '0').ToUpper();
                int x = 12;
                for (int i = 0; i < 6; i++)
                {
                    if (i == 5)
                    {
                        macAddress.Append(temp.Substring(x - 2, 2));
                    }
                    else
                    {
                        macAddress.Append(temp.Substring(x - 2, 2) + "-");
                    }

                    x -= 2;
                }
                return macAddress.ToString();
            }
            catch
            {
                return macAddress.ToString();
            }
        }
    }
}
