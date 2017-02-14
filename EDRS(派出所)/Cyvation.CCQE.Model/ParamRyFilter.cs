using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cyvation.CCQE.Model
{
    public class ParamRyFilter : PagerModel
    {
        /// <summary>
        /// 单位编码
        /// </summary>
        public string DWBM { get; set; }
        /// <summary>
        /// 部门编码
        /// </summary>
        public string BMBM { get; set; }
        /// <summary>
        /// 角色编码
        /// </summary>
        public string JSBM { get; set; }
        /// <summary>
        /// 工号
        /// </summary>
        public string GH { get; set; }
        /// <summary>
        /// 工作证号
        /// </summary>
        public string GZZH { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string XM { get; set; }
    }
}
