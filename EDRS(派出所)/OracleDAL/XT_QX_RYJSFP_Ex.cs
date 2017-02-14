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
	/// 数据访问类:XT_QX_RYJSFP
	/// </summary>
	public partial class XT_QX_RYJSFP
    {     
		
		/// <summary>
		/// 获取所有下级部门的人员
		/// </summary>
        public DataSet GetListChildrenBmAll(string strWhere, params object[] objValues)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * from xt_qx_ryjsfp where bmbm in(select bmbm from xt_zzjg_bmbm connect by prior bmbm=fbmbm start with ");
			
			if(strWhere.Trim()!="")
			{
				strSql.Append(" 1=1 "+strWhere);
			}
            strSql.Append(" )");
            try
            {
                return DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetList(string strWhere, params object[] objValues)", "EDRS.OracleDAL.XT_QX_RYJSFP", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;

		}

	}
}

