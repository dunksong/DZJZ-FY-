using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cyvation.CCQE.Model
{
    public class KpzbflModel
    {
        /// <summary>
        /// 分类编号
        /// </summary>
        public string FLBH { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string FLMC { get; set; }
        /// <summary>
        /// 父分类编号
        /// </summary>
        public string FFLBH { get; set; }
        /// <summary>
        /// 分类说明
        /// </summary>
        public string FLSM { get; set; }

        private int _xh;
        /// <summary>
        /// 序号
        /// </summary>
        public int XH
        {
            get
            {
                return _xh;
            }
            set
            {
                _xh = value;
            }
        }
    }
}
