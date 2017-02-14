using System;
using System.Data;
using System.Text;
using System.Data.OracleClient;
using EDRS.IDAL;
using Maticsoft.DBUtility;
using EDRS.Common;
using System.Collections.Generic;//Please add references
namespace EDRS.OracleDAL
{
	/// <summary>
	/// 数据访问类:XY_DZJZ_XTPZ
	/// </summary>
	public partial class XY_DZJZ_XTPZ:IXY_DZJZ_XTPZ
    {
        public System.Web.HttpRequest context = null;
        public void SetHttpContext(System.Web.HttpRequest _context)
        {
            this.context = _context;
        }
		public XY_DZJZ_XTPZ()
		{}
        #region  成员方法

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string PZBM)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from XY_DZJZ_XTPZ");
            strSql.Append(" where PZBM=:PZBM ");
            OracleParameter[] parameters = {
					new OracleParameter(":PZBM", OracleType.VarChar,50)};
            parameters[0].Value = PZBM;

            return DbHelperOra.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(EDRS.Model.XY_DZJZ_XTPZ model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into XY_DZJZ_XTPZ(");
            strSql.Append("PZBM,SYSTEMMARK,CONFIGID,CONFIGNAME,CONFIGVALUE)");
            strSql.Append(" values (");
            strSql.Append("sys_guid(),:SYSTEMMARK,:CONFIGID,:CONFIGNAME,:CONFIGVALUE)");
            OracleParameter[] parameters = {
					new OracleParameter(":SYSTEMMARK", OracleType.VarChar,50),
					new OracleParameter(":CONFIGID", OracleType.Number,4),
					new OracleParameter(":CONFIGNAME", OracleType.VarChar,200),
					new OracleParameter(":CONFIGVALUE", OracleType.VarChar,500)};
            parameters[0].Value = model.SYSTEMMARK;
            parameters[1].Value = model.CONFIGID;
            parameters[2].Value = model.CONFIGNAME;
            parameters[3].Value = model.CONFIGVALUE;

            if (DbHelperOra.ExecuteSql(strSql.ToString(), parameters) > 0)
                return true;
            return false;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(EDRS.Model.XY_DZJZ_XTPZ model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update XY_DZJZ_XTPZ set ");
            strSql.Append("SYSTEMMARK=:SYSTEMMARK,");
            strSql.Append("CONFIGID=:CONFIGID,");
            strSql.Append("CONFIGNAME=:CONFIGNAME,");
            strSql.Append("CONFIGVALUE=:CONFIGVALUE");
            strSql.Append(" where PZBM=:PZBM ");
            OracleParameter[] parameters = {
					new OracleParameter(":PZBM", OracleType.VarChar,50),
					new OracleParameter(":SYSTEMMARK", OracleType.VarChar,50),
					new OracleParameter(":CONFIGID", OracleType.Number,4),
					new OracleParameter(":CONFIGNAME", OracleType.VarChar,200),
					new OracleParameter(":CONFIGVALUE", OracleType.VarChar,500)};
            parameters[0].Value = model.PZBM;
            parameters[1].Value = model.SYSTEMMARK;
            parameters[2].Value = model.CONFIGID;
            parameters[3].Value = model.CONFIGNAME;
            parameters[4].Value = model.CONFIGVALUE;

            if (DbHelperOra.ExecuteSql(strSql.ToString(), parameters) > 0)
                return true;
            return false;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string PZBM)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from XY_DZJZ_XTPZ ");
            strSql.Append(" where PZBM=:PZBM ");
            OracleParameter[] parameters = {
					new OracleParameter(":PZBM", OracleType.VarChar,50)};
            parameters[0].Value = PZBM;

            if (DbHelperOra.ExecuteSql(strSql.ToString(), parameters) > 0)
                return true;
            return false;
        }

        /// <summary>
        /// 删除多条数据
        /// </summary>
        public bool DeleteList(string PZBMlist)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from XY_DZJZ_XTPZ ");
            strSql.Append(" where PZBM in (" + PZBMlist + ")    ");
            if (DbHelperOra.ExecuteSql(strSql.ToString()) > 0)
                return true;
            return false;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public EDRS.Model.XY_DZJZ_XTPZ GetModel(string PZBM)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select PZBM,SYSTEMMARK,CONFIGID,CONFIGNAME,CONFIGVALUE from XY_DZJZ_XTPZ ");
            strSql.Append(" where PZBM=:PZBM ");
            OracleParameter[] parameters = {
					new OracleParameter(":PZBM", OracleType.VarChar,50)};
            parameters[0].Value = PZBM;

            EDRS.Model.XY_DZJZ_XTPZ model = new EDRS.Model.XY_DZJZ_XTPZ();
            DataSet ds = DbHelperOra.Query(strSql.ToString(), parameters);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
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
        public EDRS.Model.XY_DZJZ_XTPZ DataRowToModel(DataRow row)
        {
            EDRS.Model.XY_DZJZ_XTPZ model = new EDRS.Model.XY_DZJZ_XTPZ();
            if (row != null)
            {
                if (row["PZBM"] != null)
                {
                    model.PZBM = row["PZBM"].ToString();
                }
                if (row["SYSTEMMARK"] != null)
                {
                    model.SYSTEMMARK = row["SYSTEMMARK"].ToString();
                }
                if (row["CONFIGID"] != null && !string.IsNullOrEmpty(row["CONFIGID"].ToString()))
                {
                    model.CONFIGID = int.Parse(row["CONFIGID"].ToString());
                }
                if (row["CONFIGNAME"] != null)
                {
                    model.CONFIGNAME = row["CONFIGNAME"].ToString();
                }
                if (row["CONFIGVALUE"] != null && row["CONFIGVALUE"].ToString() != "")
                {
                    model.CONFIGVALUE = row["CONFIGVALUE"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere, params object[] objValues)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select PZBM,SYSTEMMARK,CONFIGID,CONFIGNAME,CONFIGVALUE ");
            strSql.Append(" FROM XY_DZJZ_XTPZ ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1 " + strWhere);
            }
            return DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public int GetRecordCount(string strWhere, params object[] objValues)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM XY_DZJZ_XTPZ"); if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1 " + strWhere);
            }
            object obj = DbHelperOra.GetSingle(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            if (obj == null)
                return 0;
            return Convert.ToInt32(obj);
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)
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
                strSql.Append("order by T.CONFIGID desc");
            }
            strSql.Append(")AS Ro, T.*  from XY_DZJZ_XTPZ T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE 1=1 " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Ro between {0} and {1}", startIndex, endIndex);
            return DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
            parameters[0].Value = "XY_DZJZ_XTPZ";
            parameters[1].Value = "ID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperOra.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        public bool AddList(List<EDRS.Model.XY_DZJZ_XTPZ> modelList)
        {
            return false;
        }
        #endregion  成员方法
	}
}

