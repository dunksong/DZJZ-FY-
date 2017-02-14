using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cyvation.CCQE.Model
{
    public partial class JdAjModel : PagerModel
    {
        /// <summary>
        /// 提请批捕时间查询（开始时间）
        /// </summary>
        public string dtPbBeg { get; set; }
        /// <summary>
        /// 提请批捕时间查询（结束时间）
        /// </summary>
        public string dtPbEnd { get; set; }
        /// <summary>
        /// 整改时间查询（开始时间）
        /// </summary>
        public string dtZgBeg { get; set; }
        /// <summary>
        /// 整改时间查询（结束时间）
        /// </summary>
        public string dtZgEnd { get; set; }

        /// <summary>
        /// 提请批捕时间
        /// </summary>
        public string TQPBSJ_CN
        {
            get
            {
                return TQPBSJ.ToString("yyyy年MM月dd日");
            }
        }
    }
}
