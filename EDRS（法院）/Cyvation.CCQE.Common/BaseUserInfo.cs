using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cyvation.CCQE.Common
{
    [Serializable]
    public class BaseUserInfo
    {
        /// <summary>
        /// 单位编码
        /// </summary>
        public string DWBM;

        /// <summary>
        /// 单位名称
        /// </summary>
        public string DWMC;
        /// <summary>
        /// 工号
        /// </summary>
        public string GH;
        /// <summary>
        /// 名称
        /// </summary>
        public string MC;
        /// <summary>
        /// 登录别名
        /// </summary>
        public string DLBM;
        /// <summary>
        /// 口令
        /// </summary>
        public string KL;
        public string GZZH;
        public string XB;
        public string CAID;
        /// <summary>
        /// 部门编码
        /// </summary>
        public string BMBM;
        /// <summary>
        /// 单位类型
        /// </summary>
        public string DWLX;
    }
}
