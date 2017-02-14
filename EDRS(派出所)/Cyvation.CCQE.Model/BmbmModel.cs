using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cyvation.CCQE.Model
{
    public class BmbmModel
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
        /// 父部门编码
        /// </summary>
        public string FBMBM { get; set; }

        /// <summary>
        /// 门部名称
        /// </summary>
        public string BMMC { get; set; }

        /// <summary>
        /// 部门简称
        /// </summary>
        public string BMJC { get; set; }

        /// <summary>
        /// 门部文号简称
        /// </summary>
        public string BMWHJC { get; set; }

        /// <summary>
        /// 部门案号简称
        /// </summary>
        public string BMAHJC { get; set; }

        /// <summary>
        /// 是否临时机构
        /// </summary>
        public string SFLSJG { get; set; }

        /// <summary>
        /// 是否承办部门
        /// </summary>
        public string SFCBBM { get; set; }

        /// <summary>
        /// 部门序号
        /// </summary>
        public int BMXH { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string BZ { get; set; }

        /// <summary>
        /// 否是删除
        /// </summary>
        public string SFSC { get; set; }

        /// <summary>
        /// 门部映射
        /// </summary>
        public string BMYS { get; set; }


    }
}
