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
	/// 数据访问类:YX_DZJZ_LSGL
	/// </summary>
    public partial class YX_DZJZ_LSGL
    {
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPageEx(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.XH desc");
            }
            strSql.AppendFormat(")AS Ro, T.*  from {0} T ", @"(select * from YX_DZJZ_LSGL)");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE 1=1 " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Ro between {0} and {1}", startIndex, endIndex);
            try
            {
                return DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetListByPageEx(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)", "EDRS.OracleDAL.YX_DZJZ_LSGL", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListfile(string strWhere, params object[] objValues)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select YX_DZJZ_LSGL.*,(select count(1) from yx_dzjz_lszzwj a where a.sfsc='N' and a.lszh=YX_DZJZ_LSGL.Lszh) as wjnum ");
            strSql.Append(" from  YX_DZJZ_LSGL ");

            strSql.Append(" where YX_DZJZ_LSGL.SFSC='N' " + strWhere);

            try
            {
                return DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetListfile(string strWhere, params object[] objValues)", "EDRS.OracleDAL.YX_DZJZ_LSGL", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
        }
    }
}

