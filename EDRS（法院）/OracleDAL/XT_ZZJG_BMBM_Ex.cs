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
	/// 数据访问类:XT_ZZJG_BMBM
	/// </summary>
	public partial class XT_ZZJG_BMBM
	{
        /// <summary>
        /// 批量逻辑删除数据
        /// </summary>
        public bool DeleteListLogic(string BMBMlist,string DWBMlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update XT_ZZJG_BMBM set SFSC='Y'");
            strSql.Append(" where DWBM in (" + DWBMlist + ") and BMBM in (" + BMBMlist + ")  ");
            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString());
            }
            catch (Exception ex)
            {
                System.Collections.Generic.Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
                parameters.Add("BMBM", BMBMlist);
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool DeleteListLogic(string BMBMlist)", "EDRS.OracleDAL.XT_ZZJG_BMBM", strSql.ToString(), parameters);
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
        /// 获得数据列表排序
        /// </summary>
        public DataSet GetListOrBCount(string strWhere, string order, params object[] objValues)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from (select BMBM,DWBM,FBMBM,BMMC,BMJC,BMAHJC,BMWHJC,SFLSJG,SFCBBM,BMXH,BZ,SFSC,BMYS ");
            strSql.Append(" FROM XT_ZZJG_BMBM");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
            if (string.IsNullOrEmpty(order))
                order = "BMBM asc";
            strSql.AppendFormat(" order by {0})", order);           
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetListOrBCount(string strWhere, string order, params object[] objValues)", "EDRS.OracleDAL.XT_ZZJG_BMBM", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;


        } 

       
        #region 获取树形结构当前节点所有父节点
        /// <summary>
        /// 获取树形结构当前节点所有父节点
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="objValues"></param>
        /// <returns></returns>
        public DataSet GetTreeList(string strWhere, string withWhere, params object[] objValues)
        {
            //StringBuilder strSql = new StringBuilder();
            //strSql.AppendFormat("select distinct * from XT_ZZJG_BMBM{0} ",ConfigHelper.GetConfigString("OrcDBLinq"));
            //strSql.AppendFormat(" where 1=1 {0}", strWhere);
            //strSql.Append(" start with FBMBM is null ");            
            //strSql.Append(" connect by prior FBMBM=BMBM order by BMXH desc");
            //return DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select t.*,b.DWMC from (");
            strSql.Append("select * from XT_ZZJG_BMBM");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
            strSql.AppendFormat(" where 1=1 {0}", strWhere);
            strSql.Append(") t left join xt_zzjg_dwbm b on t.dwbm=b.dwbm and b.sfsc='N' ");
            strSql.AppendFormat(" start with {0} connect by nocycle FBMBM= prior BMBM ", withWhere);
            try
            {
                return DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetTreeList(string strWhere, string withWhere, params object[] objValues)", "EDRS.OracleDAL.XT_ZZJG_BMBM", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
        }
        #endregion

        #region 获取单位部门视图
        /// <summary>
        /// 获取单位部门视图
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="objValues"></param>
        /// <returns></returns>
        public DataSet GetOrganization(string strWhere, params object[] objValues)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  * from view_dw_bm start with ");
            strSql.AppendFormat(" 1=1 {0}", strWhere);
            strSql.Append(" connect by nocycle prior PARENTID=ID order by PARENTID desc,ID asc");
            try
            {
                return DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetOrganization(string strWhere, params object[] objValues)", "EDRS.OracleDAL.XT_ZZJG_BMBM", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
        } 
        #endregion

        #region 根据父级编号获取1级子集
        /// <summary>
        /// 根据父级编号获取1级子集
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="objValues"></param>
        /// <returns></returns>
        public DataSet GetTreeChildren(string strWhere, params object[] objValues)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.*,(select count(*) from view_dw_bm b where parentid=a.id) as count from view_dw_bm a ");
            strSql.AppendFormat(" where 1=1 {0}", strWhere);
            try
            {
                return DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetTreeChildren(string strWhere, params object[] objValues)", "EDRS.OracleDAL.XT_ZZJG_BMBM", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;

        }
        #endregion

        /// <summary>
        /// 获取部门编码的使用数量
        /// </summary>
        /// <param name="dwbm"></param>
        /// <returns></returns>
        public int GetBmbmCount(string dwbm,string bmbm)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select sum(s) s from(");
            strSql.Append(" select count(1) s from xt_zzjg_bmbm where dwbm='"+dwbm+"' and fbmbm="+bmbm+" and sfsc='N' and rownum =1 union ");
            strSql.Append(" select count(1) from xt_qx_jsbm where dwbm='" + dwbm + "' and bmbm=" + bmbm + " and rownum=1) t");
            int rows = -1;
            try
            {
                object count = DbHelperOra.GetSingle(strSql.ToString());
                rows = Convert.ToInt32(count);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetBmbmCount(string dwbm,string bmbm)", "EDRS.OracleDAL.XT_ZZJG_BMBM", strSql.ToString());
            }

            return rows;
        }

	}
}

