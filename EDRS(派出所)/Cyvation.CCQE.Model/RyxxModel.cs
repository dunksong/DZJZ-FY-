using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cyvation.CCQE.Model
{
    /// <summary>
    /// 查询人员的实体类
    /// </summary>
    [Serializable]
    public class RyxxModel
    {
        /// <summary>
        /// 工号
        /// </summary>
        private string _gh;

        public string Gh
        {
            get { return _gh; }
            set { _gh = value; }
        }
        /// <summary>
        /// 名称
        /// </summary>
        private string _mc;

        public string Mc
        {
            get { return _mc; }
            set { _mc = value; }
        }

        /// <summary>
        /// 登录别名
        /// </summary>
        private string _dlbm;

        public string Dlbm
        {
            get { return _dlbm; }
            set { _dlbm = value; }
        }

        /// <summary>
        /// 工作证号
        /// </summary>
        private string _gzzh;

        public string Gzzh
        {
            get { return _gzzh; }
            set { _gzzh = value; }
        }

        /// <summary>
        /// 角色名称
        /// </summary>
        private string _jsmc;

        public string Jsmc
        {
            get { return _jsmc; }
            set { _jsmc = value; }
        }

        /// <summary>
        /// 移动电话号码
        /// </summary>
        private string _yddhhm;

        public string Yddhhm
        {
            get { return _yddhhm; }
            set { _yddhhm = value; }
        }

        /// <summary>
        /// 性别
        /// </summary>
        private string _xb;

        public string Xb
        {
            get { return _xb; }
            set { _xb = value; }
        }

        /// <summary>
        /// 是否停职
        /// </summary>
        private string _sftz;

        public string Sftz
        {
            get { return _sftz; }
            set { _sftz = value; }
        }

        /// <summary>
        /// 是否删除
        /// </summary>
        private string _sfsc;

        public string Sfsc
        {
            get { return _sfsc; }
            set { _sfsc = value; }
        }

        /// <summary>
        /// 部门编码
        /// </summary>
        private string _bmbm;

        public string Bmbm
        {
            get { return _bmbm; }
            set { _bmbm = value; }
        }

        /// <summary>
        /// 角色编码
        /// </summary>
        private string _jsbm;

        public string Jsbm
        {
            get { return _jsbm; }
            set { _jsbm = value; }
        }
    }
}