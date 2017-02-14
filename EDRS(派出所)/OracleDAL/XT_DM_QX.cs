
using System;
using System.Data;
using System.Text;
using System.Data.OracleClient;
using EDRS.IDAL;
using Maticsoft.DBUtility;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using EDRS.Common;//Please add references


namespace EDRS.OracleDAL
{
	/// <summary>
	/// 数据访问类:XT_DZJZ_JZMB
	/// </summary>
	public partial class XT_DM_QX:IXT_DM_QX
	{
        public HttpRequest context = null;//客户端对象，用于记录日志，客户端信息
        public void SetHttpContext(System.Web.HttpRequest _context)
        {
            this.context = _context;
        }
        public XT_DM_QX() { }
        #region  BasicMethod

        public DataSet GetTreeList(string strWhere,string strQxWhere, params object[] objValues)
        {
            
            StringBuilder strSql = new StringBuilder();
            if (strWhere.IndexOf(":DWBM") == -1)//根据名称筛选
            {
                strSql.AppendFormat("select distinct t1.*");
                strSql.AppendFormat(",(select COUNT(1) from xt_dm_qx qx WHERE QXLX = 0 AND TRIM(qx.Qxbm) = TRIM(t1.dwbm) ");
                strSql.AppendFormat("{0}", strQxWhere);
                strSql.AppendFormat(") qx ");
                strSql.AppendFormat(" from XT_ZZJG_DWBM{0} t1 left join XT_ZZJG_DWBM{0} t2 on(t2.dwbm = t1.fdwbm) ", ConfigHelper.GetConfigString("OrcDBLinq"));
            }
            else
            {
                strSql.AppendFormat("select distinct t1.*");
                strSql.AppendFormat(",(select COUNT(1) from xt_dm_qx qx WHERE QXLX = 0 AND TRIM(qx.Qxbm) = TRIM(t1.dwbm)");
                strSql.AppendFormat("{0}", strQxWhere);
                strSql.AppendFormat(") qx ");
                strSql.AppendFormat(" from XT_ZZJG_DWBM{0} t1", ConfigHelper.GetConfigString("OrcDBLinq"));
            }
            strSql.AppendFormat(" where 1=1 {0}", strWhere);
            strSql.Append(" order by t1.DWBM");
            try
            {
                return DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strQxWhere + strWhere, objValues));
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetTreeList(string strWhere, params object[] objValues)", "EDRS.OracleDAL.XT_DM_QX", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
        } 
        /// <summary>
        /// 获取案件分类
        /// </summary>
        /// <returns></returns>
        public DataTable GetAJLBList(string jsbm, string _dwbm, string _bmbm,string key)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AJLBBM,AJLBMC from XT_DM_AJLBBM");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
            //过滤已存在权限的案件分类记录
            strSql.Append(" where SFSC = 'N' and TRIM(AJLBBM) Not In (select distinct TRIM(QXBM) from XT_DM_QX WHERE trim(JSBM)=:JSBM AND trim(DWBM)=:DWBM AND Trim(BMBM)=:BMBM) AND AJLBMC LIKE :AJLBMC");
            OracleParameter[] parameters = {
					new OracleParameter(":JSBM", OracleType.VarChar,10)	,
					new OracleParameter(":DWBM", OracleType.VarChar,50)	,
					new OracleParameter(":BMBM", OracleType.VarChar,10),
					new OracleParameter(":AJLBMC", OracleType.VarChar,50)
            };
            parameters[0].Value = jsbm.Trim();
            parameters[1].Value = _dwbm.Trim();
            parameters[2].Value = _bmbm.Trim();
            parameters[3].Value = "%" + key + "%";
            DataSet ds = null;
            try
            {
                ds = DbHelperOra.Query(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataTable GetAJLBList(string jsbm, string _dwbm, string _bmbm)", "EDRS.OracleDAL.XT_DM_QX", strSql.ToString(), parameters);
            }
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }

        public DataSet GetQXList(string jsbm, string qxType, string _dwbm, string _bmbm,string key)
        {
            string strWhere = " JSBM=:JSBM AND QXLX=:QXLX AND trim(DWBM)=:DWBM AND BMBM=:BMBM";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct QXBM,QXMC,JSBM,QXLX ");
            strSql.Append(" FROM XT_DM_QX ");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
            strSql.Append(" where 1=1 AND " + strWhere);
            object[] objValues = new object[4];
            objValues[0] = jsbm.Trim();
            objValues[1] = qxType;
            objValues[2] = _dwbm.Trim();
            objValues[3] = _bmbm.Trim();
            try
            {
                return DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetQXList(string jsbm, string qxType, string _dwbm, string _bmbm)", "EDRS.OracleDAL.XT_DM_QX", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
        }
        public DataSet GetDwList(string jsbm, string _dwbm, string _bmbm,string key)
        {
            string strWhere = " JSBM IN (" + jsbm + ") AND QXLX=:QXLX AND trim(DWBM) = :DWBM AND BMBM in (" + _bmbm + ") ";

            object[] objValues = new object[5];
            objValues[0] = 0;//权限类型
            objValues[1] = _dwbm;

            if (!string.IsNullOrEmpty(key))
            {
                strWhere += " and QXMC like :QXMC ";
                objValues[2] = "%" + key + "%";
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct QXBM,QXMC");
            strSql.Append(" FROM XT_DM_QX ");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
            strSql.Append(" where 1=1 AND " + strWhere);
           
            try
            {
                return DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetDwList(string jsbm, string _dwbm, string _bmbm)", "EDRS.OracleDAL.XT_DM_QX", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
        }

        public DataSet GetLBList(string jsbm, string _dwbm, string _bmbm,string key)
        {
            string strWhere = " JSBM IN (" + jsbm + ") AND QXLX=:QXLX AND trim(DWBM)=:DWBM AND BMBM IN (" + _bmbm + ") ";

            object[] objValues = new object[5];
            objValues[0] = 1;//权限类型
            objValues[1] = _dwbm.Trim();

            if (!string.IsNullOrEmpty(key))
            {
                strWhere += " and QXMC like :QXMC ";
                objValues[2] = "%" + key + "%";
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct QXBM,QXMC");
            strSql.Append(" FROM XT_DM_QX ");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
            strSql.Append(" where 1=1 AND " + strWhere);
            
            try
            {
                return DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetLBList(string jsbm, string _dwbm, string _bmbm)", "EDRS.OracleDAL.XT_DM_QX", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
        }
        /// <summary>
        /// 添加部门权限
        /// </summary>
        /// <param name="bmbm">部门编码</param>
        /// <param name="jsbm">角色编码</param>
        /// <returns>返回空时，成功</returns>
        public bool AddDW(List<EDRS.Model.XT_DM_QX> list, string jsbm, string dwbm, string bmbm)
        {
            Hashtable sqlList = new Hashtable();
            foreach (EDRS.Model.XT_DM_QX model in list)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into XT_DM_QX(");
                strSql.Append("QXBM,QXMC,JSBM,QXLX,DWBM,BMBM)");
                strSql.Append(" values (");
                strSql.Append(":QXBM,:QXMC,:JSBM,:QXLX,:DWBM,:BMBM)");
                OracleParameter[] parameters = {
                        new OracleParameter(":QXBM", OracleType.VarChar,50),
                        new OracleParameter(":QXMC", OracleType.VarChar,200),
                        new OracleParameter(":JSBM", OracleType.VarChar,10),
                        new OracleParameter(":QXLX", OracleType.Number),
                        new OracleParameter(":DWBM", OracleType.VarChar,50),
                        new OracleParameter(":BMBM", OracleType.VarChar,10),
                                               };
                parameters[0].Value = model.ID.Trim();
                parameters[1].Value = model.Text.Trim();
                parameters[2].Value = jsbm.Trim();
                parameters[3].Value = 0;
                parameters[4].Value = dwbm.Trim();
                parameters[5].Value = bmbm.Trim();
                sqlList.Add(strSql, parameters);

            }
            try
            {
                return DbHelperOra.ExecuteSqlTran(sqlList);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool AddDW(List<EDRS.Model.XT_DM_QX> list, string jsbm, string dwbm, string bmbm)", "EDRS.OracleDAL.XT_DM_QX", sqlList);
            }
            return false;
        }
        /// <summary>
        /// 删除部门权限
        /// </summary>
        /// <param name="bmbm">部门编码</param>
        /// <param name="jsbm">角色编码</param>
        /// <returns>返回空时，成功</returns>
        public bool DelDW(List<EDRS.Model.XT_DM_QX> list, string jsbm, string dwbm, string bmbm)
        {
            Hashtable sqlList = new Hashtable();
            foreach (EDRS.Model.XT_DM_QX model in list)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from XT_DM_QX");
                strSql.Append(" where");
                strSql.Append(" TRIM(QXBM)=:QXBM AND TRIM(JSBM)=:JSBM AND TRIM(QXLX)=:QXLX AND TRIM(DWBM)=:DWBM AND TRIM(BMBM)=:BMBM");
                OracleParameter[] parameters = {
					new OracleParameter(":QXBM", OracleType.VarChar,50),
					new OracleParameter(":JSBM", OracleType.VarChar,10),
					new OracleParameter(":QXLX", OracleType.Number),
					new OracleParameter(":DWBM", OracleType.VarChar,50),
					new OracleParameter(":BMBM", OracleType.VarChar,10),
                                           };
                parameters[0].Value = model.ID.Trim();
                parameters[1].Value = jsbm.Trim();
                parameters[2].Value = 0;
                parameters[3].Value = dwbm.Trim();
                parameters[4].Value = bmbm.Trim();
                sqlList.Add(strSql, parameters);
            }
            try
            {
                //throw new Exception("测试事务提交错误日志记录");
                return DbHelperOra.ExecuteSqlTran(sqlList);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool DelDW(List<EDRS.Model.XT_DM_QX> list, string jsbm, string dwbm, string bmbm)", "EDRS.OracleDAL.XT_DM_QX", sqlList);
            }
            return false;
        }
        /// <summary>
        /// 添加类别权限
        /// </summary>
        /// <param name="lbbm">案件类别编码</param>
        /// <param name="jsbm">角色编码</param>
        /// <returns>返回空时，成功</returns>
        public bool AddLB(List<EDRS.Model.XT_DM_QX> list, string jsbm, string dwbm, string bmbm)
        {
            Hashtable sqlList = new Hashtable();
            foreach (EDRS.Model.XT_DM_QX model in list)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into XT_DM_QX(");
                strSql.Append("QXBM,QXMC,JSBM,QXLX,DWBM,BMBM)");
                strSql.Append(" values (");
                strSql.Append(":QXBM,:QXMC,:JSBM,:QXLX,:DWBM,:BMBM)");
                OracleParameter[] parameters = {
					new OracleParameter(":QXBM", OracleType.VarChar,50),
					new OracleParameter(":QXMC", OracleType.VarChar,50),
					new OracleParameter(":JSBM", OracleType.VarChar,10),
					new OracleParameter(":QXLX", OracleType.Number),
					new OracleParameter(":DWBM", OracleType.VarChar,50),
					new OracleParameter(":BMBM", OracleType.VarChar,10)
                                           };
                parameters[0].Value = model.ID.Trim();
                parameters[1].Value = model.Text.Trim();
                parameters[2].Value = jsbm.Trim();
                parameters[3].Value = 1;
                parameters[4].Value = dwbm.Trim();
                parameters[5].Value = bmbm.Trim();
                sqlList.Add(strSql, parameters);
            }
            try
            {
                return DbHelperOra.ExecuteSqlTran(sqlList);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool AddLB(List<EDRS.Model.XT_DM_QX> list, string jsbm, string dwbm, string bmbm)", "EDRS.OracleDAL.XT_DM_QX", sqlList);
            }
            return false;
        }
        /// <summary>
        /// 删除类别权限
        /// </summary>
        /// <param name="lbbm">案件类别编码</param>
        /// <param name="jsbm">角色编码</param>
        /// <returns>返回空时，成功</returns>
        public bool DelLB(List<EDRS.Model.XT_DM_QX> list, string jsbm, string dwbm, string bmbm)
        {
            Hashtable sqlList = new Hashtable();
            foreach (EDRS.Model.XT_DM_QX model in list)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from XT_DM_QX");
                strSql.Append(" where");
                strSql.Append(" TRIM(QXBM)=:QXBM AND TRIM(JSBM)=:JSBM AND TRIM(QXLX)=:QXLX AND TRIM(DWBM)=:DWBM AND TRIM(BMBM)=:BMBM");
                OracleParameter[] parameters = {
					new OracleParameter(":QXBM", OracleType.VarChar,50),
					new OracleParameter(":JSBM", OracleType.VarChar,10),
					new OracleParameter(":QXLX", OracleType.Number),
					new OracleParameter(":DWBM", OracleType.VarChar,50),
					new OracleParameter(":BMBM", OracleType.VarChar,10)
                                           };
                parameters[0].Value = model.ID.Trim();
                parameters[1].Value = jsbm.Trim();
                parameters[2].Value = 1;
                parameters[3].Value = dwbm.Trim();
                parameters[4].Value = bmbm.Trim();
                sqlList.Add(strSql, parameters);
            }
            try
            {
                return DbHelperOra.ExecuteSqlTran(sqlList);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool DelLB(List<EDRS.Model.XT_DM_QX> list, string jsbm, string dwbm, string bmbm)", "EDRS.OracleDAL.XT_DM_QX", sqlList);
            }
            return false;
        }

        #region 获取用户所有角色权限
        /// <summary>
        /// 获取用户所有角色权限
        /// </summary>
        /// <param name="dwbm">单位编码</param>
        /// <param name="bmbm">部门编码</param>
        /// <param name="gh">工号</param>
        /// <param name="type">部门权限还是类型权限（0，1）</param>
        /// <returns></returns>
        public DataSet GetQxListByRole(string dwbm, string bmbm, string gh, int type, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select distinct QXBM,QXMC,JSBM,QXLX  FROM XT_DM_QX{0}", ConfigHelper.GetConfigString("OrcDBLinq"));
            strSql.Append(" where jsbm in ( select jsbm from xt_qx_ryjsfp where trim(DWBM)= :dwbm and gh=:gh )");
            strSql.AppendFormat(" and trim(DWBM)= :dwbm2 and qxlx=:qxlx {0}", strWhere);
            strSql.Append(" group by QXBM,QXMC,JSBM,QXLX ");

            OracleParameter[] parameters = {
                    new OracleParameter(":dwbm", OracleType.VarChar, 50),
                    new OracleParameter(":gh", OracleType.VarChar,10),
                    new OracleParameter(":dwbm2", OracleType.VarChar,50),
                    new OracleParameter(":qxlx", OracleType.Number)
                    };
            parameters[0].Value = dwbm;
            parameters[1].Value = gh;
            parameters[2].Value = dwbm;
            parameters[3].Value = type;
         
            try
            {
                return DbHelperOra.Query(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetQxListByType(string dwbm,string bmbm,string gh,int type)", "EDRS.OracleDAL.XT_DM_QX");
            }
            return null;
        }

        public DataSet GetQxListByRole(string strWhere ,params object[] objValues)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select distinct QXBM,QXMC,JSBM,QXLX  FROM XT_DM_QX{0}", ConfigHelper.GetConfigString("OrcDBLinq"));
          //  strSql.Append(" where jsbm in ( select jsbm from xt_qx_ryjsfp where dwbm= :dwbm and gh=:gh )");
            if(!string.IsNullOrEmpty(strWhere))
            strSql.AppendFormat(" where 1=1 {0}", strWhere);
            strSql.Append(" group by QXBM,QXMC,JSBM,QXLX ");

            // string sql = string.Format("select QXBM,QXMC,JSBM,QXLX  FROM XT_DM_QX where jsbm in ( select jsbm from xt_qx_ryjsfp where dwbm= '370000' and gh='0000' ) and dwbm= '370000' and qxlx=1  and QXMC ='侦监适时介入侦查' ");

            //string sql = " select QXBM,QXMC,JSBM,QXLX  FROM XT_DM_QX where QXMC ='侦监适时介入侦查'";
            // OracleParameter[] parameters = { new OracleParameter(":qxmc", OracleType.VarChar, 10)};

            //OracleParameter[] parameters = {
            //        new OracleParameter(":dwbm", OracleType.VarChar, 50),
            //        new OracleParameter(":gh", OracleType.VarChar,10),
            //        new OracleParameter(":dwbm2", OracleType.VarChar,50),
            //        new OracleParameter(":qxlx", OracleType.Number)
            //        };
            //parameters[0].Value = dwbm;
            //parameters[1].Value = gh;
            //parameters[2].Value = dwbm;
            //parameters[3].Value = type;
            //parameters[4].Value="%反%";
            //obj = DbHelperOra.GetSingle(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            try
            {
                return DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetQxListByType(string dwbm,string bmbm,string gh,int type)", "EDRS.OracleDAL.XT_DM_QX");
            }
            return null;
        } 
        #endregion


        /// <summary>
        /// 获取单位权限编码，组装树形结构数据
        /// </summary>
        /// <param name="dwbm"></param>
        /// <param name="jsbms"></param>
        /// <returns></returns>
        public DataSet GetDwQxList(string dwbm, string jsbms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select distinct dw.dwbm,dw.dwmc,dw.dwjc,case when qx1.qxbm is null then '' else dw.Fdwbm end Fdwbm  from xt_dm_qx  qx");
            strSql.AppendFormat(" right join xt_zzjg_dwbm dw on(TRIM(qx.Qxbm) = TRIM(dw.dwbm))");
            strSql.AppendFormat(" left join (select * from xt_dm_qx qx where qx.qxlx = 0 and qx.Jsbm in (" + jsbms + ") AND trim(qx.DWBM) = :dwbm) qx1 on (dw.Fdwbm = trim(qx1.qxbm))");
            strSql.Append(" where qx.qxlx = 0 and qx.Jsbm in (" + jsbms + ") AND trim(qx.DWBM) = :dwbm");
            OracleParameter[] parameters = {
					new OracleParameter(":dwbm", OracleType.VarChar, 50),
					};
            parameters[0].Value = dwbm;
            try
            {
                return DbHelperOra.Query(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetQxListByType(string dwbm,string bmbm,string gh,int type)", "EDRS.OracleDAL.XT_DM_QX");
            }
            return null;
        }
		#endregion  BasicMethod

        #region  获取权限列表，包含单位编码和案件类别编号
        /// <summary>
        /// 获取权限列表，包含单位编码和案件类别编号
        /// </summary>
        /// <param name="dwbm"></param>
        /// <param name="gh"></param>
        /// <returns></returns>
        public DataSet GetQxList(string dwbm, string gh)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select distinct qx.* from xt_dm_qx qx");
            strSql.Append(" join xt_qx_ryjsfp rj on trim(qx.dwbm) = trim(rj.dwbm) and qx.jsbm=rj.jsbm");
            strSql.Append(" where trim(rj.dwbm)=:dwbm and rj.gh=:gh");

            OracleParameter[] parameters = {
					new OracleParameter(":dwbm", OracleType.VarChar, 50),
                    new OracleParameter(":gh", OracleType.VarChar, 10)
					};
            parameters[0].Value = dwbm;
            parameters[1].Value = gh;

            try
            {
                return DbHelperOra.Query(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetQxList(string dwbm,string gh)", "EDRS.OracleDAL.XT_DM_QX");
            }
            return null;
        } 
        #endregion
	}
}

