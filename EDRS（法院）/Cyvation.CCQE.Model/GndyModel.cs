using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cyvation.CCQE.Model
{
    /// <summary>
    /// 角色功能
    /// </summary>
    [Serializable]
    public class GndyModel
    {
        /// <summary>
        /// 功能名称
        /// </summary>
        private string _gnmc;

        public string Gnmc
        {
            get { return _gnmc; }
            set { _gnmc = value; }
        }

        /// <summary>
        /// 功能说明
        /// </summary>
        private string _gnsm;

        public string Gnsm
        {
            get { return _gnsm; }
            set { _gnsm = value; }
        }

        /// <summary>
        /// 功能窗体
        /// </summary>
        private string _gnct;

        public string Gnct
        {
            get { return _gnct; }
            set { _gnct = value; }
        }

        /// <summary>
        /// 功能序号
        /// </summary>
        private string _gnxh;

        public string Gnxh
        {
            get { return _gnxh; }
            set { _gnxh = value; }
        }

        /// <summary>
        /// 功能参数
        /// </summary>
        private string _gncs;

        public string Gncs
        {
            get { return _gncs; }
            set { _gncs = value; }
        }

        /// <summary>
        /// 功能显示名称
        /// </summary>
        private string _gnxsmc;

        public string Gnxsmc
        {
            get { return _gnxsmc; }
            set { _gnxsmc = value; }
        }

        /// <summary>
        /// 功能编码
        /// </summary>
        private string _gnbm;

        public string Gnbm
        {
            get { return _gnbm; }
            set { _gnbm = value; }
        }

        /// <summary>
        /// 功能分类
        /// </summary>
        private string _gnfl;

        public string Gnfl
        {
            get { return _gnfl; }
            set { _gnfl = value; }
        }
    }
}
