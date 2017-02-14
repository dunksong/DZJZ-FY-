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
	/// 数据访问类:XY_DZJZ_MBPZB
	/// </summary>
	public partial class XY_DZJZ_MBPZB:IXY_DZJZ_MBPZB
    {
        public System.Web.HttpRequest context = null;
        public void SetHttpContext(System.Web.HttpRequest _context)
        {
            this.context = _context;
        }
        public XY_DZJZ_MBPZB()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
        public bool Exists(string DossierTypeValueMember)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from XY_DZJZ_MBPZB");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
            strSql.Append(" where SFSC='N' AND DossierTypeValueMember=:DossierTypeValueMember ");
			OracleParameter[] parameters = {
					new OracleParameter(":DossierTypeValueMember", OracleType.VarChar,20)};
            parameters[0].Value = DossierTypeValueMember;
            try
            {
                return DbHelperOra.Exists(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Exists(string DossierTypeValueMember)", "EDRS.OracleDAL.XY_DZJZ_MBPZB", strSql.ToString(), parameters);
            }
            return false;
		}

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool ExistsChildren(string DossierTypeValueMember)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from XY_DZJZ_MBPZB");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
            strSql.Append(" where SFSC='N' AND DossierParentMember=:DossierParentMember ");
            OracleParameter[] parameters = {
					new OracleParameter(":DossierParentMember", OracleType.VarChar,20)};
            parameters[0].Value = DossierTypeValueMember;
            try
            {
                return DbHelperOra.Exists(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool ExistsChildren(string DossierTypeValueMember)", "EDRS.OracleDAL.XY_DZJZ_MBPZB", strSql.ToString(), parameters);
            }
            return false;
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EDRS.Model.XY_DZJZ_MBPZB model)
		{
			StringBuilder strSql=new StringBuilder();
            //先删除数据
            strSql.Append("DELETE XY_DZJZ_MBPZB WHERE UnitID = :UnitID AND SSLBBM = :SSLBBM");
            OracleParameter[] parameters = new OracleParameter[]{
					new OracleParameter(":UnitID", OracleType.VarChar,20),
					new OracleParameter(":SSLBBM", OracleType.VarChar,20)
            };
            parameters[0].Value = model.UnitID;
            parameters[1].Value = model.SSLBBM;
            int rows = 0;
            try
            {
                DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Add(EDRS.Model.XY_DZJZ_MBPZB model)", "EDRS.OracleDAL.XY_DZJZ_MBPZB", strSql.ToString(), parameters);
            }


            strSql.Clear();
			strSql.Append("insert into XY_DZJZ_MBPZB(");
            strSql.Append("DossierTypeValueMember,CaseInfoTypeID,CaseInfoTypeName,UnitID,DossierTypeDisplayMember,DossierParentMember,DossierEvidenceValueMember,SortIndex,Category,SSLBBM,SSLBMC,Auto)");
			strSql.Append(" values (");
            strSql.Append(":DossierTypeValueMember,:CaseInfoTypeID,:CaseInfoTypeName,:UnitID,:DossierTypeDisplayMember,:DossierParentMember,:DossierEvidenceValueMember,:SortIndex,:Category,:SSLBBM,:SSLBMC,:Auto)");
            parameters = new OracleParameter[]{
					new OracleParameter(":DossierTypeValueMember", OracleType.VarChar,20),
					new OracleParameter(":CaseInfoTypeID", OracleType.VarChar,20),
					new OracleParameter(":CaseInfoTypeName", OracleType.VarChar,100),
					new OracleParameter(":UnitID", OracleType.VarChar,20),
					new OracleParameter(":DossierTypeDisplayMember", OracleType.VarChar,100),
					new OracleParameter(":DossierParentMember", OracleType.VarChar,20),
                    new OracleParameter(":DossierEvidenceValueMember", OracleType.VarChar,100),
                    new OracleParameter(":SortIndex", OracleType.Number,18),
                    new OracleParameter(":Category", OracleType.Char,1),
					new OracleParameter(":SSLBBM", OracleType.VarChar,20),
					new OracleParameter(":SSLBMC", OracleType.VarChar,100),
                    new OracleParameter(":Auto",OracleType.Char,1)};
            parameters[0].Value = model.DossierTypeValueMember;
            parameters[1].Value = model.CaseInfoTypeID;
            parameters[2].Value = model.CaseInfoTypeName;
            parameters[3].Value = model.UnitID;
            parameters[4].Value = model.DossierTypeDisplayMember;
            parameters[5].Value = model.DossierParentMember;
            parameters[6].Value = model.DossierEvidenceValueMember;
            parameters[7].Value = model.SortIndex;
            parameters[8].Value = model.Category;
            parameters[9].Value = model.SSLBBM;
            parameters[10].Value = model.SSLBMC;
            parameters[11].Value = model.Auto;
            rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Add(EDRS.Model.XY_DZJZ_MBPZB model)", "EDRS.OracleDAL.XY_DZJZ_MBPZB", strSql.ToString(), parameters);
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
		/// 更新一条数据
		/// </summary>
		public bool Update(EDRS.Model.XY_DZJZ_MBPZB model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update XY_DZJZ_MBPZB set ");
            strSql.Append("CaseInfoTypeID=:CaseInfoTypeID,CaseInfoTypeName=:CaseInfoTypeName,UnitID=:UnitID,DossierTypeDisplayMember=:DossierTypeDisplayMember,DossierParentMember=:DossierParentMember,DossierEvidenceValueMember=:DossierEvidenceValueMember,SortIndex=:SortIndex,Category=:Category,SSLBBM=:SSLBBM,SSLBMC=:SSLBMC,Auto=:Auto");
            strSql.Append(" where DossierTypeValueMember=:DossierTypeValueMember");
			OracleParameter[] parameters = {
					new OracleParameter(":DossierTypeValueMember", OracleType.VarChar,20),
					new OracleParameter(":CaseInfoTypeID", OracleType.VarChar,20),
					new OracleParameter(":CaseInfoTypeName", OracleType.VarChar,100),
					new OracleParameter(":UnitID", OracleType.VarChar,20),
					new OracleParameter(":DossierTypeDisplayMember", OracleType.VarChar,100),
					new OracleParameter(":DossierParentMember", OracleType.VarChar,20),
                    new OracleParameter(":DossierEvidenceValueMember", OracleType.VarChar,100),
                    new OracleParameter(":SortIndex", OracleType.Number,18), 
                    new OracleParameter(":Category", OracleType.Char,1),
					new OracleParameter(":SSLBBM", OracleType.VarChar,20),
					new OracleParameter(":SSLBMC", OracleType.VarChar,100),
                    new OracleParameter(":Auto",OracleType.Char,1)};
            parameters[0].Value = model.DossierTypeValueMember;
            parameters[1].Value = model.CaseInfoTypeID;
            parameters[2].Value = model.CaseInfoTypeName;
            parameters[3].Value = model.UnitID;
            parameters[4].Value = model.DossierTypeDisplayMember;
            parameters[5].Value = model.DossierParentMember;
            parameters[6].Value = model.DossierEvidenceValueMember;
            parameters[7].Value = model.SortIndex;
            parameters[8].Value = model.Category;
            parameters[9].Value = model.SSLBBM;
            parameters[10].Value = model.SSLBMC;
            parameters[11].Value = model.Auto;
            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Update(EDRS.Model.XY_DZJZ_MBPZB model)", "EDRS.OracleDAL.XY_DZJZ_MBPZB", strSql.ToString(), parameters);
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
		/// 删除一条数据
		/// </summary>
        public bool Delete(string DossierTypeValueMember)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from XY_DZJZ_MBPZB ");
            strSql.Append(" where DossierTypeValueMember=:DossierTypeValueMember ");
            OracleParameter[] parameters = {
					new OracleParameter(":DossierTypeValueMember", OracleType.VarChar,20)};
            parameters[0].Value = DossierTypeValueMember;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Delete(string DossierTypeValueMember)", "EDRS.OracleDAL.XY_DZJZ_MBPZB", strSql.ToString(), parameters);
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
        public bool DeleteList(string DossierTypeValueMember)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from XY_DZJZ_MBPZB ");
            strSql.Append(" where DossierTypeValueMember in (" + DossierTypeValueMember + ")  ");
            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString());
            }
            catch (Exception ex)
            {
                System.Collections.Generic.Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
                parameters.Add("DossierTypeValueMember", DossierTypeValueMember);
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool DeleteList(string DossierTypeValueMember)", "EDRS.OracleDAL.XY_DZJZ_MBPZB", strSql.ToString(), parameters);
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
        public EDRS.Model.XY_DZJZ_MBPZB GetModel(string DossierTypeValueMember)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select DossierTypeValueMember,CaseInfoTypeID,CaseInfoTypeName,UnitID,DossierTypeDisplayMember,DossierParentMember,DossierEvidenceValueMember,SortIndex,Category,Sslbbm,Sslbmc,Auto,(SELECT SSLBBM FROM XY_DZJZ_MBPZB T2 WHERE T1.Dossierparentmember = T2.Dossiertypevaluemember) FSSLBBM from XY_DZJZ_MBPZB T1");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
            strSql.Append(" where DossierTypeValueMember=:DossierTypeValueMember ");
            OracleParameter[] parameters = {
					new OracleParameter(":DossierTypeValueMember", OracleType.VarChar,20)};
            parameters[0].Value = DossierTypeValueMember;

			EDRS.Model.XY_DZJZ_MBPZB model=new EDRS.Model.XY_DZJZ_MBPZB();
            DataSet ds = null;
            try
            {
                ds = DbHelperOra.Query(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public EDRS.Model.XY_DZJZ_MBPZB GetModel(string DossierTypeValueMember)", "EDRS.OracleDAL.XY_DZJZ_MBPZB", strSql.ToString(), parameters);
            }
			if(ds!= null && ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public EDRS.Model.XY_DZJZ_MBPZB DataRowToModel(DataRow row)
		{
			EDRS.Model.XY_DZJZ_MBPZB model=new EDRS.Model.XY_DZJZ_MBPZB();
			if (row != null)
			{
                if (row["DossierTypeValueMember"] != null)
				{
                    model.DossierTypeValueMember = row["DossierTypeValueMember"].ToString();
				}
                if (row["CaseInfoTypeID"] != null)
				{
                    model.CaseInfoTypeID = row["CaseInfoTypeID"].ToString();
				}
                if (row["CaseInfoTypeName"] != null)
				{
                    model.CaseInfoTypeName = row["CaseInfoTypeName"].ToString();
				}
                if (row["UnitID"] != null)
				{
                    model.UnitID = row["UnitID"].ToString();
				}
                if (row["DossierTypeDisplayMember"] != null && row["DossierTypeDisplayMember"].ToString() != "")
				{
                    model.DossierTypeDisplayMember = row["DossierTypeDisplayMember"].ToString();
				}
                if (row["DossierParentMember"] != null)
				{
                    model.DossierParentMember = row["DossierParentMember"].ToString();
				}
                if (row["DossierEvidenceValueMember"] != null)
                {
                    model.DossierEvidenceValueMember = row["DossierEvidenceValueMember"].ToString();
                }
                if (row["SortIndex"] != null)
                {
                    model.SortIndex = int.Parse(row["SortIndex"].ToString());
                }
                if (row["Category"] != null)
                {
                    model.Category = row["Category"].ToString();
                }
                if (row["Sslbbm"] != null)
                {
                    model.SSLBBM = row["Sslbbm"].ToString();
                }
                if (row["Sslbmc"] != null)
                {
                    model.SSLBMC = row["Sslbmc"].ToString();
                }
                if (row["FSSLBBM"] != null)
                {
                    model.FSslbBM = row["FSSLBBM"].ToString();
                }
                if (row["Auto"] != null)
                {
                    model.Auto = row["Auto"].ToString();
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
            strSql.Append("select DossierTypeValueMember,CaseInfoTypeID,CaseInfoTypeName,UnitID,DossierTypeDisplayMember,DossierParentMember,DossierEvidenceValueMember,SortIndex,Category,Auto ");
			strSql.Append(" FROM XY_DZJZ_MBPZB ");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where SFSC='N' "+strWhere);
			}
            try
            {
                return DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetList(string strWhere, params object[] objValues)", "EDRS.OracleDAL.XY_DZJZ_MBPZB", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
        public int GetRecordCount(string strWhere, params object[] objValues)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM XY_DZJZ_MBPZB ");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
			if(strWhere.Trim()!="")
			{
                strSql.Append(" where  SFSC='N' AND " + strWhere);
			}
            object obj = 0;
            try
            {
                obj = DbHelperOra.GetSingle(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public int GetRecordCount(string strWhere, params object[] objValues)", "EDRS.OracleDAL.XY_DZJZ_MBPZB", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
                strSql.Append("order by T.DOSSIERTYPEVALUEMEMBER desc");
			}
            strSql.AppendFormat(")AS Ro, T.*  from XY_DZJZ_MBPZB{0} T ", ConfigHelper.GetConfigString("OrcDBLinq"));
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
                strSql.Append(" WHERE  SFSC='N' AND " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Ro between {0} and {1}", startIndex, endIndex);
            try
            {
                return DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)", "EDRS.OracleDAL.XY_DZJZ_MBPZB", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
			parameters[0].Value = "XY_DZJZ_MBPZB";
			parameters[1].Value = "SPJSBM";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperOra.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		
	}
}

