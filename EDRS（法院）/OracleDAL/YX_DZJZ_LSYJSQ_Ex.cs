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
    /// 数据访问类:YX_DZJZ_LSYJSQ
    /// </summary>
    public partial class YX_DZJZ_LSYJSQ
    {

        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteListByY(string YJSQDHlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update YX_DZJZ_LSYJSQ ");
            strSql.Append(" set SFSC='Y' where YJSQDH in (" + YJSQDHlist + ")  ");
            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString());
            }
            catch (Exception ex)
            {
                System.Collections.Generic.Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
                parameters.Add("YJSQDH", YJSQDHlist);
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool DeleteList(string YJSQDHlist )", "EDRS.OracleDAL.YX_DZJZ_LSYJSQ", strSql.ToString(), parameters);
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
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPageBD(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)
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
                strSql.Append("order by T.YJSQDH desc");
            }
            strSql.AppendFormat(")AS Ro, T.*  from (select T.*,B.BMSAH,B.YJXH,B.AJMC,B.YJKSSJ,B.YJJSSJ,B.YJZH,B.YJMM FROM YX_DZJZ_LSYJSQ T LEFT JOIN YX_DZJZ_LSAJBD B on T.YJSQDH=B.YJSQDH AND B.SFSC='N') T ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)", "EDRS.OracleDAL.YX_DZJZ_LSYJSQ", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
        }
    }
}