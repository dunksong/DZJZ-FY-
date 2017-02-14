using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cyvation.CCQE.Model
{
    public class LcsljdModel
    {
        /// <summary>
        /// 案件编号
        /// </summary>
        public string AJBH { get; set; }

        /// <summary>
        /// 流程实例节点编号
        /// </summary>
        public string LCSLJDBH { get; set; }

        /// <summary>
        /// 单位编码
        /// </summary>
        public string DWBM { get; set; }

        /// <summary>
        /// 模板编码
        /// </summary>
        public string LCMBBM { get; set; }

        /// <summary>
        /// 流程节点编号
        /// </summary>
        public string LCJDBM { get; set; }

        /// <summary>
        /// 流程节点名称
        /// </summary>
        public string LCJDMC { get; set; }

        /// <summary>
        /// "节点类型‘1’开始节点‘2’结束节点‘3’业务节点"
        /// </summary>
        public string JDLX { get; set; }

        /// <summary>
        /// 父节点编号(为空表示根)
        /// </summary>
        public string FJDBH { get; set; }

        /// <summary>
        /// 节点进入时间
        /// </summary>
        public DateTime? JDJRSJ { get; set; }

        /// <summary>
        /// 节点离开时间
        /// </summary>
        public DateTime? JDLKSJ { get; set; }

        /// <summary>
        /// 节点执行者工号
        /// </summary>
        public string JDZXZGH { get; set; }

        /// <summary>
        /// 节点执行者名称
        /// </summary>
        public string JDZXZ { get; set; }

        /// <summary>
        /// 节点执行状态(‘1’:正常运行‘2’:正常结束)
        /// </summary>
        public string JDZXZT { get; set; }

        /// <summary>
        /// 节点进入原因(‘1’:正常进入‘2’:回退)
        /// </summary>
        public string JDJRYY { get; set; }

        /// <summary>
        /// 是否允许离开
        /// </summary>
        public string SFYXLK { get; set; }

        /// <summary>
        /// 流程实例编号
        /// </summary>
        public string LCSLBH { get; set; }
    }
}
