using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cyvation.CCQE.Model
{
    /// <summary>
    /// 考评指标
    /// </summary>
    public class KpzbModel : ICloneable
    {
        /// <summary>
        /// 指标编号
        /// </summary>
        public string ZBBH { get; set; }
        /// <summary>
        /// 分类编号
        /// </summary>
        public string FLBH { get; set; }
        /// <summary>
        /// 指标名称
        /// </summary>
        public string ZBMC { get; set; }
        /// <summary>
        /// 指标说明
        /// </summary>
        public string ZBSM { get; set; }
        /// <summary>
        /// 指标相关法规
        /// </summary>
        public string ZBXGFG { get; set; }
        /// <summary>
        /// 默认分数
        /// </summary>
        public decimal MRFS { get; set; }
        /// <summary>
        /// 指标重要级别
        /// </summary>
        public string ZBZYJB { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int XH { get; set; }

        public object Clone()
        {
            KpzbModel rtnObj = new KpzbModel();
            foreach (var item in typeof(KpzbModel).GetProperties())
            {
                item.SetValue(rtnObj, item.GetValue(this, null), null);
            }
            return rtnObj;
        }
    }
}
