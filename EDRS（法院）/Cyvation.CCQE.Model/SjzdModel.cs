using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cyvation.CCQE.Model
{
    public class SjzdModel
    {
        /// <summary>
        /// 编码
        /// </summary>
        public string DM { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string MC { get; set; }

        /// <summary>
        /// 父编码
        /// </summary>
        public string FDM { get; set; }        

        /// <summary>
        /// 所属类别代码
        /// </summary>
        public string SSLBDM { get; set; }

        /// <summary>
        /// 所属类别名称
        /// </summary>
        public string SSLBMC { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string SM { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public string XH { get; set; }
    }
}
