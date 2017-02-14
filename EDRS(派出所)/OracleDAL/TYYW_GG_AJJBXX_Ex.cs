
using System;
using System.Data;
using System.Text;
using System.Data.OracleClient;
using System.Web;
using EDRS.IDAL;
using Maticsoft.DBUtility;
using EDRS.Common;//Please add references
namespace EDRS.OracleDAL
{
	/// <summary>
	/// 数据访问类:TYYW_GG_AJJBXX
	/// </summary>
	public partial class TYYW_GG_AJJBXX
    {

        /// <summary>
        /// 扩展分页获取数据列表
        /// </summary>
        public DataSet GetListByPageUnite(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)
        {
            string tableName = string.Format("(select JZ.JZSCRXM,JZ.JZSCRGH,JZ.CJSJ,d.CJSJ GGCJSJ,JZ.AJBH,JZ.WSBH,JZ.WSMC,JZ.ZZZT,(select count(1) from yx_dzjz_jzjbxx{0} y where (to_char(y.jzxgh) <> '0' and y.jzxgh is not null ) and y.Bmsah=d.Bmsah and y.sfsc='N') IsRegard,", ConfigHelper.GetConfigString("OrcDBLinq"));
            tableName += "AJMC,d.BMSAH,AJLB_MC,CBDW_MC,CBBM_MC,CBR,DQJD,SLRQ,AJZT,DQRQ,BJRQ,WCRQ,GDRQ,AJLB_BM,CBDW_BM,d.SFSC,XYR,SFZH,TARYXX,SHR,ZJS,DJJ,ZYS,JZPZ,JZSHRBH,JZSHR,JZSHSJ,JZBH ";
            tableName += string.Format(" from TYYW_GG_AJJBXX{0} d  LEFT JOIN yx_dzjz_jzjbxx JZ ON (d.bmsah = JZ.BMSAH) where d.SFSC='N' and JZ.SFSC = 'N')", ConfigHelper.GetConfigString("OrcDBLinq"));

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT Ro,JZSCRXM,JZSCRGH,CJSJ,GGCJSJ,AJBH,WSBH,WSMC,ZZZT,IsRegard,AJMC,BMSAH,AJLB_MC,CBDW_MC,CBBM_MC,CBR,DQJD,SLRQ,AJZT,DQRQ,BJRQ,WCRQ,GDRQ,AJLB_BM,CBDW_BM,XYR,SFZH,TARYXX,SHR,ZJS,DJJ,ZYS,JZPZ,JZSHRBH,JZSHR,JZSHSJ,JZBH  FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.BMSAH desc");
            }
            strSql.AppendFormat(")AS Ro,JZSCRXM,JZSCRGH,CJSJ,GGCJSJ,AJBH,WSBH,WSMC,ZZZT,IsRegard,T.AJMC,T.BMSAH,T.AJLB_MC,T.CBDW_MC,T.CBBM_MC,T.CBR,T.DQJD,T.SLRQ,T.AJZT,T.DQRQ,T.BJRQ,T.WCRQ,T.GDRQ,T.AJLB_BM,T.CBDW_BM,T.SFSC,T.XYR,T.SFZH,T.TARYXX,T.SHR,T.ZJS,T.DJJ,T.ZYS,T.JZPZ,T.JZSHRBH,T.JZSHR,T.JZSHSJ,T.JZBH  from {0} T ", tableName);
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetListByPageUnite(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)", "EDRS.OracleDAL.TYYW_GG_AJJBXX_Ex", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
        }



        /// <summary>
        /// 扩展获取记录总数
        /// </summary>
        public int GetRecordCountUnite(string strWhere, params object[] objValues)
        {
            string tableName = "(select JZ.JZSCRXM,JZ.JZSCRGH,JZ.CJSJ,JZ.AJBH,JZ.WSBH,JZ.WSMC,JZ.ZZZT,AJMC,d.BMSAH,AJLB_MC,CBDW_MC,CBBM_MC,CBR,DQJD,SLRQ,AJZT,DQRQ,BJRQ,WCRQ,GDRQ,AJLB_BM,CBDW_BM,d.SFSC,XYR,SFZH,TARYXX,SHR,ZJS,DJJ,ZYS,JZPZ,JZSHRBH,JZSHR,JZSHSJ,JZBH  from TYYW_GG_AJJBXX d  LEFT JOIN yx_dzjz_jzjbxx JZ ON (d.bmsah = JZ.BMSAH) where d.SFSC='N' and JZ.SFSC = 'N') T ";            

            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select count(1) FROM {0} ", tableName);
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1 " + strWhere);
            }
            object obj = null;
            try
            {
                obj = DbHelperOra.GetSingle(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public int GetRecordCountUnite(string strWhere, params object[] objValues)", "EDRS.OracleDAL.TYYW_GG_AJJBXX_Ex", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
	}
}

