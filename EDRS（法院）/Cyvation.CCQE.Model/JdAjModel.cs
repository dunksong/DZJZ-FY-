using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cyvation.CCQE.Model
{
    public partial class JdAjModel
    {
        /// <summary>
        /// 案件编码
        /// </summary>
        public string AJBH { get; set; }

        /// <summary>
        /// 案件名称
        /// </summary>
        public string AJMC { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CJSJ { get; set; }

        /// <summary>
        /// 受理日期
        /// </summary>
        public DateTime? SLRQ { get; set; }

        /// <summary>
        /// 承办单位编码
        /// </summary>
        public string CBDW_BM { get; set; }

        /// <summary>
        /// 承办单位名称
        /// </summary>
        public string CBDW_MC { get; set; }

        /// <summary>
        /// 承办部门编码
        /// </summary>
        public string CBBM_BM { get; set; }

        /// <summary>
        /// 承办部门名称
        /// </summary>
        public string CBBM_MC { get; set; }

        /// <summary>
        /// 	承办人工号
        /// </summary>
        public string CBRGH { get; set; }

        /// <summary>
        /// 	承办人
        /// </summary>
        public string CBR { get; set; }

        /// <summary>
        /// 	预审员
        /// </summary>
        public string YSY { get; set; }

        /// <summary>
        /// 	侦办单位编码
        /// </summary>
        public string ZBDW_BM { get; set; }

        /// <summary>
        /// 	侦办单位名称
        /// </summary>
        public string ZBDW_MC { get; set; }

        /// <summary>
        /// 侦办部门编码
        /// </summary>
        public string ZBBM_BM { get; set; }

        /// <summary>
        /// 	侦办部门名称
        /// </summary>
        public string ZBBM_MC { get; set; }

        /// <summary>
        /// 侦办人工号
        /// </summary>
        public string ZBMJGH { get; set; }

        /// <summary>
        /// 	侦办人
        /// </summary>
        public string ZBMJ { get; set; }

        /// <summary>
        /// 提请批捕案由代码
        /// </summary>
        public string TQPBAY_DMS { get; set; }

        /// <summary>
        /// 	提请批捕案由名称
        /// </summary>
        public string TQPBAY_MCS { get; set; }

        /// <summary>
        /// 	提请批捕时间
        /// </summary>
        public DateTime TQPBSJ { get; set; }

        /// <summary>
        /// 	提请批捕文号
        /// </summary>
        public string TQPBWH { get; set; }

        /// <summary>
        /// 	审定案由代码
        /// </summary>
        public string SDAY_DMS { get; set; }

        /// <summary>
        /// 审定案由名称
        /// </summary>
        public string SDAY_MCS { get; set; }

        /// <summary>
        /// 案情摘要
        /// </summary>
        public string AQZY { get; set; }

        /// <summary>
        /// 犯罪嫌疑人
        /// </summary>
        public string FZXYR { get; set; }

        /// <summary>
        /// 	创建人工号
        /// </summary>
        public string CJRGH { get; set; }

        /// <summary>
        /// 	创建人
        /// </summary>
        public string CJR { get; set; }

        /// <summary>
        /// 流程实例编号
        /// </summary>
        public string LCSLBH { get; set; }

        /// <summary>
        /// 流程节点编号
        /// </summary>
        public string LCJDBM { get; set; }

        /// <summary>
        /// 流程节点名称
        /// </summary>
        public string LCJDMC { get; set; }
    }
}
