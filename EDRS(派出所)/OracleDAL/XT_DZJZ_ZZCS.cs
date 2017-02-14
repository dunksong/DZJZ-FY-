
using System;
using System.Data;
using System.Text;
using System.Data.OracleClient;
using EDRS.IDAL;
using Maticsoft.DBUtility;
using EDRS.Common;
using System.Collections.Generic;
using System.Collections;//Please add references
namespace EDRS.OracleDAL
{
	/// <summary>
	/// 数据访问类:XT_DZJZ_ZZCS
	/// </summary>
    public partial class XT_DZJZ_ZZCS : IXT_DZJZ_ZZCS
	{
        public System.Web.HttpRequest context = null;
        public void SetHttpContext(System.Web.HttpRequest _context)
        {
            this.context = _context;
        }
        public XT_DZJZ_ZZCS() { }
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string MBJZBH)
		{
            //StringBuilder strSql=new StringBuilder();
            //strSql.Append("select count(1) from XT_DZJZ_ZZCS");
            //strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
            //strSql.Append(" where MBJZBH=:MBJZBH ");
            //OracleParameter[] parameters = {
            //        new OracleParameter(":MBJZBH", OracleType.Char,14)			};
            //parameters[0].Value = MBJZBH;

            //try
            //{
            //    return DbHelperOra.Exists(strSql.ToString(), parameters);
            //}
            //catch (Exception ex)
            //{
            //    EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Exists(string MBJZBH)", "EDRS.OracleDAL.XT_DZJZ_ZZCS", strSql.ToString(), parameters);
            //}
            return false;
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EDRS.Model.XT_DZJZ_ZZCS model)
		{
            //StringBuilder strSql=new StringBuilder();
            //strSql.Append("insert into XT_DZJZ_ZZCS(");
            //strSql.Append("MBJZBH,AJLB,SYCH)");
            //strSql.Append(" values (");
            //strSql.Append(":MBJZBH,:AJLB,:SYCH)");
            //OracleParameter[] parameters = {
            //        new OracleParameter(":MBJZBH", OracleType.Char,14),
            //        new OracleParameter(":AJLB", OracleType.Char,4),
            //        new OracleParameter(":SYCH", OracleType.Char,1)};
            //parameters[0].Value = model.MBJZBH;
            //parameters[1].Value = model.AJLB;
            //parameters[2].Value = model.SYCH;

            //int rows = 0;
            //try
            //{
            //    rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            //}
            //catch (Exception ex)
            //{
            //    EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Add(EDRS.Model.XT_DZJZ_ZZCS model)", "EDRS.OracleDAL.XT_DZJZ_ZZCS", strSql.ToString(), parameters);
            //}
            //if (rows > 0)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}           

            return false;

		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(EDRS.Model.XT_DZJZ_ZZCS model)
		{
            //StringBuilder strSql=new StringBuilder();
            //strSql.Append("update XT_DZJZ_ZZCS set ");
            //strSql.Append("AJLB=:AJLB,");
            //strSql.Append("SYCH=:SYCH");
            //strSql.Append(" where MBJZBH=:MBJZBH ");
            //OracleParameter[] parameters = {
            //        new OracleParameter(":AJLB", OracleType.Char,4),
            //        new OracleParameter(":SYCH", OracleType.Char,1),
            //        new OracleParameter(":MBJZBH", OracleType.Char,14)};
            //parameters[0].Value = model.AJLB;
            //parameters[1].Value = model.SYCH;
            //parameters[2].Value = model.MBJZBH;

            //int rows = 0;
            //try
            //{
            //    rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            //}
            //catch (Exception ex)
            //{
            //    EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Update(EDRS.Model.XT_DZJZ_ZZCS model)", "EDRS.OracleDAL.XT_DZJZ_ZZCS", strSql.ToString(), parameters);
            //}
            //if (rows > 0)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            return false;
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string MBJZBH)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from XT_DZJZ_ZZCS ");
            strSql.Append(" where FZBS=:FZBS ");
			OracleParameter[] parameters = {
					new OracleParameter(":FZBS", OracleType.VarChar,50)			};
			parameters[0].Value = MBJZBH;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Delete(string MBJZBH)", "EDRS.OracleDAL.XT_DZJZ_ZZCS", strSql.ToString(), parameters);
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
		/// 批量删除数据
		/// </summary>
        public bool DeleteList(string FZBSlist)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from XT_DZJZ_ZZCS ");
            strSql.Append(" where FZBS in (" + FZBSlist + ")  ");
            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString());
            }
            catch (Exception ex)
            {
                System.Collections.Generic.Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
                parameters.Add("FZBS", FZBSlist);
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool DeleteList(string FZBSlist )", "EDRS.OracleDAL.XT_DZJZ_ZZCS", strSql.ToString(), parameters);
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
		/// 得到一个对象实体
		/// </summary>
		public EDRS.Model.XT_DZJZ_ZZCS GetModel(string MBJZBH)
		{
			
            //StringBuilder strSql=new StringBuilder();
            //strSql.Append("select MBJZBH,AJLB,SYCH from XT_DZJZ_ZZCS ");
            //strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
            //strSql.Append(" where MBJZBH=:MBJZBH ");
            //OracleParameter[] parameters = {
            //        new OracleParameter(":MBJZBH", OracleType.Char,14)			};
            //parameters[0].Value = MBJZBH;

            //EDRS.Model.XT_DZJZ_ZZCS model=new EDRS.Model.XT_DZJZ_ZZCS();
            //DataSet ds = null;
            //try
            //{
            //    ds = DbHelperOra.Query(strSql.ToString(), parameters);
            //}
            //catch (Exception ex)
            //{
            //    EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public EDRS.Model.XT_DZJZ_ZZCS GetModel(string MBJZBH)", "EDRS.OracleDAL.XT_DZJZ_ZZCS", strSql.ToString(), parameters);
            //}
            //if(ds.Tables[0].Rows.Count>0)
            //{
            //    return DataRowToModel(ds.Tables[0].Rows[0]);
            //}
            //else
            //{
            //    return null;
            //}
            return null;
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public EDRS.Model.XT_DZJZ_ZZCS DataRowToModel(DataRow row)
		{
			EDRS.Model.XT_DZJZ_ZZCS model=new EDRS.Model.XT_DZJZ_ZZCS();
			if (row != null)
			{
                if (row["CSBH"] != null)
				{
                    model.CSBH = row["CSBH"].ToString();
				}
                if (row["CSKEY"] != null)
				{
                    model.CSKEY = row["CSKEY"].ToString();
				}
                if (row["CSVALUE"] != null)
				{
                    model.CSVALUE = row["CSVALUE"].ToString();
				}
                if (row["DYSJ"] != null)
                {
                    model.DYSJ = Convert.ToDateTime(row["DYSJ"].ToString());
                }
                if (row["FZBS"] != null)
                {
                    model.FZBS = row["FZBS"].ToString();
                }
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
        public DataSet GetList(string strWhere, params object[] objValues)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select CSBH,CSKEY,CSVALUE,DYSJ,FZBS ");            
			strSql.Append(" FROM XT_DZJZ_ZZCS ");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where 1=1 "+strWhere);
			}
            try
            {
                return DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetList(string strWhere, params object[] objValues)", "EDRS.OracleDAL.XT_DZJZ_ZZCS", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
        public int GetRecordCount(string strWhere, params object[] objValues)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM XT_DZJZ_ZZCS ");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where 1=1 "+strWhere);
			}
            object obj = 0;
            try
            {
                obj = DbHelperOra.GetSingle(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public int GetRecordCount(string strWhere, params object[] objValues)", "EDRS.OracleDAL.XT_DZJZ_ZZCS", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.MBJZBH desc");
			}
			strSql.AppendFormat(")AS Ro, T.*  from XT_DZJZ_ZZCS{0}T ",ConfigHelper.GetConfigString("OrcDBLinq"));
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)", "EDRS.OracleDAL.XT_DZJZ_ZZCS", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			OracleParameter[] parameters = {
					new OracleParameter(":tblName", OracleType.VarChar, 255),
					new OracleParameter(":fldName", OracleType.VarChar, 255),
					new OracleParameter(":PageSize", OracleType.Number),
					new OracleParameter(":PageIndex", OracleType.Number),
					new OracleParameter(":IsReCount", OracleType.Clob),
					new OracleParameter(":OrderType", OracleType.Clob),
					new OracleParameter(":strWhere", OracleType.VarChar,1000),
					};
			parameters[0].Value = "XT_DZJZ_ZZCS";
			parameters[1].Value = "MBJZBH";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperOra.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod
        /// <summary>
        /// 增加多条数据
        /// </summary>
        public bool AddList(List<EDRS.Model.XT_DZJZ_ZZCS> modelList)
        {
            int count = 0;
            Hashtable hash = new Hashtable();
            foreach (EDRS.Model.XT_DZJZ_ZZCS model in modelList)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into XT_DZJZ_ZZCS(");
                strSql.Append("cskey, csvalue, dysj, fzbs)");
                strSql.Append(" values (");
                strSql.Append(":cskey" + count + ",:csvalue" + count + ",:dysj" + count + ",:fzbs" + count + ")");
                OracleParameter[] parameters = {				
					new OracleParameter(":cskey" + count, OracleType.VarChar,100),
					new OracleParameter(":csvalue" + count, OracleType.VarChar,1000),
					new OracleParameter(":dysj" + count, OracleType.DateTime),
					new OracleParameter(":fzbs" + count, OracleType.VarChar,50)
					};
                parameters[0].Value = model.CSKEY;
                parameters[1].Value = model.CSVALUE;
                parameters[2].Value = model.DYSJ;
                parameters[3].Value = model.FZBS;
                hash.Add(strSql.ToString(), parameters);
                count++;
            }

            try
            {
                return DbHelperOra.ExecuteSqlTran(hash);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Add(EDRS.Model.XT_DZJZ_ZZCS model)", "EDRS.OracleDAL.XT_DZJZ_ZZCS", hash);
            }
            return false;
        }
		#endregion  ExtensionMethod
	}
}

