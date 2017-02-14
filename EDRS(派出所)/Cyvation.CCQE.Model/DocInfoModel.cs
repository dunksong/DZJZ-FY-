using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cyvation.CCQE.Model
{
    public class DocInfoModel
    {
        public DocInfoModel()
        {
            _docTemplate = new DocTemplateModel();
        }
        //文书模板
        private DocTemplateModel _docTemplate;        

        /// <summary>
        /// 案件编号
        /// </summary>
        public string AJBH { get; set; }

        /// <summary>
        /// 	文书编号
        /// </summary>
        public string WJBH { get; set; }

        /// <summary>
        /// 单位编码
        /// </summary>
        public string DWBM { get; set; }

        /// <summary>
        /// 父文件编号
        /// </summary>
        public string FWJBH { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string WJMC { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string WJLJ { get; set; }

        /// <summary>
        /// 文书文号
        /// </summary>
        public string WJWH { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public string WJLX { get; set; }

        /// <summary>
        /// 文件标识（1：通知，2：反馈）
        /// </summary>
        public string WJBZ { get; set; }
        
        /// <summary>
        /// 文书加载方式（生成还是打开）
        /// </summary>
        public string STATE { get; set; }        

        /// <summary>
        /// 标签
        /// </summary>
        public string[] TagData { get; set; }

        /// <summary>
        /// 文书模板
        /// </summary>
        public DocTemplateModel DocTemplate
        {
            get { return _docTemplate; }
            set { _docTemplate = value; }
        }
    }
}
