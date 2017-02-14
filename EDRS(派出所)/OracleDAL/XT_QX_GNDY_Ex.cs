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
	/// 数据访问类:XT_QX_GNDY
	/// </summary>
	public partial class XT_QX_GNDY
	{
        /// <summary>
        /// 获取功能分类和功能列表
        /// </summary>
        public DataSet GetListByType(string dwbm, string strWhere, params object[] objValues)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select flbm,flmc,fflbm,dwbm,flxh,gnct,sfdjy from ( ");
            strSql.AppendFormat(" select a.flbm,a.flmc,a.fflbm,a.dwbm,a.flxh,'' as gnct,'N' as sfdjy From XT_QX_GNFL {0} a where a.sfsc='N' ", ConfigHelper.GetConfigString("OrcDBLinq"));
            
            strSql.Append(" union ");
            strSql.AppendFormat(" select a.gnbm,a.gnmc,a.flbm,a.dwbm,a.gnxh,a.gnct,a.sfdjy From xt_qx_gndy {0} a where a.sfsc='N' ", ConfigHelper.GetConfigString("OrcDBLinq"));
            strSql.Append(" ) t");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1 " + strWhere);
            }
            strSql.Append(" start with fflbm Is null connect by fflbm=prior flbm group by flbm,flmc,fflbm,dwbm,flxh,gnct,sfdjy order by flxh asc ");

            try
            {
                return DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetListByType(string dwbm, string strWhere, params object[] objValues)", "EDRS.OracleDAL.XT_QX_GNDY", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
        }

        /// <summary>
        /// 获取角色对应功能
        /// </summary>
        public DataSet GetListByBound(string dwbm,string gh, string strWhere, params object[] objValues)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select distinct flbm,flmc,fflbm,dwbm,flxh,gnct from ( ");
            strSql.AppendFormat(" select a.flbm,a.flmc,a.fflbm,a.dwbm,a.flxh,'' gnct,'' jsbm, '' bmbm From XT_QX_GNFL {0} a where a.sfsc='N' ", ConfigHelper.GetConfigString("OrcDBLinq"));            
            strSql.Append(" union all ");
            strSql.Append(" select b.gnbm,b.gnmc,b.flbm,b.dwbm,b.gnxh,b.gnct,a.jsbm,a.bmbm from xt_qx_jsgnfp ");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
            strSql.Append(" a join xt_qx_gndy ");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
            //strSql.Append(" b on a.gnbm=b.gnbm and a.dwbm='"+dwbm+"' and b.dwbm='"+dwbm+"'");
            strSql.Append(" b on a.gnbm=b.gnbm ");
            strSql.Append(" right join XT_QX_RYJSFP c on (a.jsbm = c.jsbm and a.dwbm=c.dwbm and a.bmbm=c.bmbm) where c.DWBM='" + dwbm + "' and c.GH='" + gh + "'");// or a.bmbm= c.bmbm
            strSql.Append(" union all ");
            strSql.Append("select e.gnbm,e.gnmc,e.flbm,f.dwbm,e.gnxh,e.gnct,'',f.bmbm from xt_qx_gndy e ");
            strSql.Append("right join xt_qx_rygnfp f on e.gnbm = f.gnbm and f.dwbm='" + dwbm + "' and f.gh='" + gh + "'");
            strSql.Append(") t ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1 " + strWhere);
            }
            strSql.Append(" start with fflbm Is not null connect by prior fflbm= flbm order by flxh asc ");
            try
            {
                return DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetListByBound(string dwbm,string gh, string strWhere, params object[] objValues)", "EDRS.OracleDAL.XT_QX_GNDY", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
        }

	}
}

