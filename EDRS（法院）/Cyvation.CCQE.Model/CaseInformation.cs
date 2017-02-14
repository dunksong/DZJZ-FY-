using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cyvation.CCQE.Model
{
    public class CaseInformation
    {
        public CaseInformation()
        {
            _case = new JdAjModel();
            _doc = new DocInfoModel();
            _lc = new LcsljdModel();
        }

        /// <summary>
        /// 案件
        /// </summary>
        private JdAjModel _case;

        /// <summary>
        /// 文书
        /// </summary>
        private DocInfoModel _doc;

        /// <summary>
        /// 流程信息
        /// </summary>
        private LcsljdModel _lc;     

        /// <summary>
        /// 案件
        /// </summary>
        public JdAjModel Case
        {
            get { return _case; }
            set { _case = value; }
        }
        /// <summary>
        /// 文书
        /// </summary>
        public DocInfoModel Doc
        {
            get { return _doc; }
            set { _doc = value; }
        }

        /// <summary>
        /// 流程
        /// </summary>
        public LcsljdModel LC
        {
            get { return _lc; }
            set { _lc = value; }
        }
    }
}
