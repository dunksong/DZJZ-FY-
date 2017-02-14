using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cyvation.CCQE.Model
{
    /// <summary>
    /// 单位编码表
    /// </summary>
    [Serializable]
    public class DwbmModel
    {
        #region private
        /// <summary>
        /// 单位编码
        /// </summary>
        public string DWBM { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>	
        public string DWMC { get; set; }

        /// <summary>
        /// 父单位编码
        /// </summary>	
        public string FDWBM { get; set; }
        /// <summary>
        /// 父单位名称
        /// </summary>	
        public string FDWMC { get; set; }
        /// <summary>
        /// 单位级别
        /// </summary>	
        public string DWJB { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>	
        public string SFSC { get; set; }
        /// <summary>
        /// 单位简称
        /// </summary>	
        public string DWJC { get; set; }
        
        /// <summary>
        /// 单位属性
        /// </summary>
        public string DWSX { get; set; }

        #endregion
    }
}
