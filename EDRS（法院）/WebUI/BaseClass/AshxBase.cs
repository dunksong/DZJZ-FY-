using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Cyvation.CCQE.Common;

namespace Cyvation.CCQE.Web
{
    public class AshxBase : IHttpHandler, IRequiresSessionState
    {
        private EDRS.Model.XT_ZZJG_RYBM _userInfo = null;

        public EDRS.Model.XT_ZZJG_RYBM UserInfo
        {
            get { return _userInfo; }
        }
        private EDRS.Model.XT_ZZJG_DWBM _userDwbm;
        /// <summary>
        /// 用户登录单位编码
        /// </summary>
        public EDRS.Model.XT_ZZJG_DWBM UserDwbm
        {
            get { return _userDwbm; }
            set { _userDwbm = value; }
        }
        private List<EDRS.Model.XT_QX_JSBM> _userRole = null;

        public List<EDRS.Model.XT_QX_JSBM> UserRole
        {
            get { return _userRole; }
        }
        private string jsbms;
        /// <summary>
        /// 角色编码字符串
        /// </summary>
        public string Jsbms
        {
            get { return jsbms; }
            set { jsbms = value; }
        }
        private string bmbms;
        /// <summary>
        /// 部门编码字符串
        /// </summary>
        public string Bmbms
        {
            get { return bmbms; }
            set { bmbms = value; }
        }

        // 日志记录
        private Lazy<ILogExtention> _logger;

        /// <summary>
        /// Log4net日志操作方法
        /// </summary>
        public ILogExtention Logger
        {
            get
            {
                if (null == _logger)
                {

                    _logger = new Lazy<ILogExtention>(() => LogManagerExtention.GetLogger(this.GetType()), isThreadSafe: true);
                }

                return _logger.Value;
            }
        }

        public virtual void ProcessRequest(HttpContext context)
        {
            if (null == context.Session["user"]) { return; }
            _userInfo = context.Session["user"] as EDRS.Model.XT_ZZJG_RYBM;

            if (null == context.Session["userDwbm"]) { return; }
            _userDwbm = context.Session["userDwbm"] as EDRS.Model.XT_ZZJG_DWBM;

            if (null == context.Session["userRole"]) { return; }
            _userRole = context.Session["userRole"] as List<EDRS.Model.XT_QX_JSBM>;

            string jsbms = "";
            string bmbms = "";
            for (int i = 0; i < UserRole.Count; i++)
            {
                jsbms += "'" + UserRole[i].JSBM + "'";
                bmbms += "'" + UserRole[i].BMBM + "'";
                if (i < UserRole.Count - 1)
                {
                    jsbms += ",";
                    bmbms += ",";
                }
            }
            this.jsbms = jsbms;
            this.bmbms = bmbms;

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}