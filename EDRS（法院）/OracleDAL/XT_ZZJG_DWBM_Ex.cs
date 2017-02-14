using System;
using System.Data;
using System.Text;
using System.Data.OracleClient;
using EDRS.IDAL;
using Maticsoft.DBUtility;
using EDRS.Common;//Please add references
namespace EDRS.OracleDAL
{
	/// <summary>
	/// 数据访问类:XT_ZZJG_DWBM
	/// </summary>
	public partial class XT_ZZJG_DWBM
	{
        /// <summary>
        /// 批量逻辑删除数据
        /// </summary>
        public bool DeleteListLogic(string DWBMlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update XT_ZZJG_DWBM set SFSC='Y'");
            strSql.Append(" where DWBM in (" + DWBMlist + ")  ");
            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString());
            }
            catch (Exception ex)
            {
                System.Collections.Generic.Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
                parameters.Add("DWBM", DWBMlist);
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool DeleteListLogic(string DWBMlist)", "EDRS.OracleDAL.XT_ZZJG_DWBM", strSql.ToString(), parameters);
            }
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListOrBCount(string strWhere, params object[] objValues)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select t.*,(select count(1) from XT_ZZJG_BMBM{0} b where t.dwbm=b.dwbm and SFSC='N') as BMBM",ConfigHelper.GetConfigString("OrcDBLinq"));
            strSql.AppendFormat(" FROM XT_ZZJG_DWBM{0} t",ConfigHelper.GetConfigString("OrcDBLinq"));
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1 " + strWhere);
            }
            try
            {
                return DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetListOrBCount(string strWhere, params object[] objValues)", "EDRS.OracleDAL.XT_ZZJG_DWBM", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
        }

        #region 获取树形结构当前节点所有父节点
        /// <summary>
        /// 获取树形结构当前节点所有父节点
        /// </summary>
        /// <param name="strWhere">数据查询条件</param>
        /// <param name="withWhere">循环开始条件</param>
        /// <param name="direction">查询方向（true父级向子级查询，false子级向父级）</param>
        /// <param name="objValues">参数</param>
        /// <returns>DataSet</returns>
        public DataSet GetTreeList(string strWhere,string withWhere,bool direction, params object[] objValues)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select distinct CONNECT_BY_ISLEAF ISLEAF,DWBM,DWMC,FDWBM,DWJB,SFSC,DWJC,DWSX from XT_ZZJG_DWBM{0} ", ConfigHelper.GetConfigString("OrcDBLinq"));
            strSql.AppendFormat(" where 1=1 {0}", strWhere);
            strSql.AppendFormat(" start with {0}", withWhere);
            strSql.AppendFormat(" connect by {0} and SFSC='N'", (direction ? " FDWBM= prior DWBM " : " prior FDWBM=DWBM "));
            
            strSql.Append(" order by DWBM");
            try
            {
                return DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetTreeList(string strWhere,string withWhere,bool direction, params object[] objValues)", "EDRS.OracleDAL.XT_ZZJG_DWBM", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
        } 
        #endregion


        /// <summary>
        /// 获取树形结构当前节点所有父节点
        /// </summary>
        /// <param name="strWhere">数据查询条件</param>
        /// <param name="withWhere">循环开始条件</param>
        /// <param name="direction">查询方向（true父级向子级查询，false子级向父级）</param>
        /// <param name="objValues">参数</param>
        /// <returns>DataSet</returns>
        public DataSet GetTreeListById(string strWhere, string withWhere, string siftWhere, bool direction, params object[] objValues)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select distinct CONNECT_BY_ISLEAF ISLEAF,DWBM,DWMC,FDWBM,DWJB,SFSC,DWJC,DWSX from XT_ZZJG_DWBM{0} ", ConfigHelper.GetConfigString("OrcDBLinq"));
            strSql.AppendFormat(" where 1=1 {0}", strWhere);
            strSql.AppendFormat(" start with {0}", withWhere);
            strSql.AppendFormat(" connect by {0} and SFSC='N'", (direction ? " FDWBM= prior DWBM " : " prior FDWBM=DWBM "));
            
            strSql.Append(" order by DWBM");
            StringBuilder siftSql = new StringBuilder();
            if (!string.IsNullOrEmpty(siftWhere))
            {
                siftSql.AppendFormat("select distinct * from(");
                siftSql.AppendFormat("{0}) t start with {1}", strSql.ToString(), siftWhere);
                siftSql.AppendFormat(" connect by prior FDWBM=DWBM ");
                siftSql.Append(" order by DWBM");
                strSql.Clear();
                strSql = siftSql;
            }
            try
            {
                return DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetTreeList(string strWhere,string withWhere,bool direction, params object[] objValues)", "EDRS.OracleDAL.XT_ZZJG_DWBM", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
        }

        #region 根据单位编码和工号获取对应有权限的单位编码
        /// <summary>
        /// 根据单位编码和工号获取对应有权限的单位编码
        /// </summary>
        /// <param name="dwbm"></param>
        /// <param name="gh"></param>
        /// <returns></returns>
        public DataSet GetDwbmListByGh(string dwbm, string gh)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select * from xt_zzjg_dwbm where sfsc='N'");
            strSql.Append(" and dwbm in (select distinct trim(QXBM) from xt_dm_qx a ");
            strSql.Append(" join XT_QX_RYJSFP b on trim(a.jsbm) = b.jsbm and trim(a.bmbm)=b.bmbm and trim(a.dwbm)=b.dwbm ");
            strSql.Append("  and gh=:GH and b.dwbm=:DWBM and a.qxlx=0) order by dwbm asc ");
            OracleParameter[] parameters = {
					new OracleParameter(":DWBM", OracleType.VarChar,50),
					new OracleParameter(":GH", OracleType.Char,4)};
            parameters[0].Value = dwbm;
            parameters[1].Value = gh;
            try
            {
                return DbHelperOra.Query(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetDwbmListByGh(string string dwbm,string gh)", "EDRS.OracleDAL.XT_ZZJG_DWBM", strSql.ToString(), parameters);
            }
            return null;
        } 
        #endregion


        /// <summary>
        /// 获取单位编码的使用数量
        /// </summary>
        /// <param name="dwbm"></param>
        /// <returns></returns>
        public int GetDwbmCount(string dwbm)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select sum(s) s from(");
            strSql.Append(" select count(1) as s from xt_zzjg_dwbm where fdwbm=" + dwbm + " and sfsc='N' and rownum =1 union ");
            strSql.Append(" select count(1) from xt_zzjg_bmbm where dwbm=" + dwbm + " and sfsc='N' and rownum =1 union ");
            strSql.Append(" select count(1) from xt_zzjg_rybm where dwbm=" + dwbm + " and sfsc='N' and rownum =1 union ");
            strSql.Append(" select count(1) from tyyw_gg_ajjbxx where cbdw_bm=" + dwbm + " and sfsc='N' and rownum =1 union ");
            strSql.Append(" select count(1) from xt_qx_jsbm where dwbm=" + dwbm + " and rownum =1 ) t");
            int rows = -1;
            try
            {
                object count = DbHelperOra.GetSingle(strSql.ToString());
                rows = Convert.ToInt32(count);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetDwbmCount(string dwbm)", "EDRS.OracleDAL.XT_ZZJG_DWBM", strSql.ToString());
            }

            return rows;
        }

	}
}

