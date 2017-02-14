using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cyvation.CCQE.Model
{
    /// <summary>
    /// 案件考评指标质量
    /// </summary>
    public class AjKpzbZlModel
    {
        /// <summary>
        /// 案件编号
        /// </summary>
        public string AJBH { get; set; }
        /// <summary>
        /// 考评指标编号
        /// </summary>
        public string KPZBBH { get; set; }
        /// <summary>
        /// 流程实例编号（考评批次）
        /// </summary>
        public string LCSLBH { get; set; }
        /// <summary>
        /// 考评指标内容
        /// </summary>
        public string KPZBNR { get; set; }
        /// <summary>
        /// 考评分数
        /// </summary>
        public decimal? KPFS { get; set; }
        /// <summary>
        /// 考评人工号
        /// </summary>
        public string KPRGH { get; set; }
        /// <summary>
        /// 考评人
        /// </summary>
        public string KPR { get; set; }
    }
}
