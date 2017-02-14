using System;
using System.Data;
using System.Text;
using System.Data.OracleClient;
using EDRS.IDAL;
using Maticsoft.DBUtility;
using EDRS.Common;
using System.Collections;
using System.Collections.Generic;//Please add references
namespace EDRS.OracleDAL
{
    /// <summary>
    /// 数据访问类:XY_DZJZ_MBPZB
    /// </summary>
    public partial class XY_DZJZ_MBPZB
    {
        #region 获取树形结构当前节点所有父节点
        /// <summary>
        /// 获取树形结构当前节点所有父节点
        /// </summary>
        /// <param name="strWhere">数据查询条件</param>
        /// <param name="withWhere">循环开始条件</param>
        /// <param name="direction">查询方向（true父级向子级查询，false子级向父级）</param>
        /// <param name="objValues">参数</param>
        /// <returns>DataSet</returns>
        public DataSet GetTreeList(string strWhere, string withWhere, bool direction, params object[] objValues)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select distinct CONNECT_BY_ISLEAF ISLEAF,DossierTypeValueMember,CaseInfoTypeID,CaseInfoTypeName,UnitID,(select dwmc from xt_zzjg_dwbm where dwbm=UnitID) as DWMC,DossierTypeDisplayMember,DossierParentMember,DossierEvidenceValueMember,SortIndex,Category,SSLBBM,SSLBMC,Auto,case Category when 'J' then 'tree_diver1' else case Auto when 'Y' then 'AutoIcon' else 'notAutoIcon' end end icon from XY_DZJZ_MBPZB{0} ", ConfigHelper.GetConfigString("OrcDBLinq"));
            //iconCls 配置树的图标
            strSql.AppendFormat(" where  SFSC='N' {0}", strWhere);
            strSql.AppendFormat(" start with  SFSC='N' {0}", withWhere);
            strSql.AppendFormat(" connect by {0}", (direction ? "DossierParentMember= prior DossierTypeValueMember" : " prior DossierParentMember=DossierTypeValueMember"));
            strSql.Append(" order by SortIndex");
            try
            {
                return DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetTreeList(string strWhere, string withWhere, bool direction, params object[] objValues)", "EDRS.OracleDAL.XY_DZJZ_MBPZB", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
        }
        #endregion


        #region 删除一条数据
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteNode(string DossierTypeValueMember)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE XY_DZJZ_MBPZB SET SFSC = 'Y'");
            strSql.Append(" where dossiertypevaluemember in (");
            strSql.Append(" SELECT dossiertypevaluemember FROM xy_dzjz_mbpzb");
            strSql.Append(" START WITH dossiertypevaluemember = :DossierTypeValueMember");
            strSql.Append(" CONNECT BY dossierparentmember = PRIOR dossiertypevaluemember)");

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
        #endregion

        #region 根据单位编码获取最近一个单位编码的模板
        /// <summary>
        /// 根据单位编码获取最近一个单位编码的模板
        /// </summary>
        /// <param name="dwbm">单位编码</param>
        /// <returns></returns>
        public DataSet GetListMinDwbm(string dwbm)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select ");
            strSql.Append("dossiertypevaluemember,caseinfotypeid,caseinfotypename,unitid,");
            strSql.Append("dossiertypedisplaymember,dossierparentmember,dossierevidencevaluemember,sortindex,category,auto");
            strSql.Append(" from xy_dzjz_mbpzb where unitid = ( ");
            strSql.Append(" select max(unitid) from (");
            strSql.Append(" select t1.unitid,t2.DWBM from xy_dzjz_mbpzb t1 right join ");
            strSql.Append(" (SELECT dwbm FROM xt_zzjg_dwbm START WITH dwbm = :dwbm CONNECT BY PRIOR fdwbm = dwbm) t2 ");
            strSql.Append(" on t1.unitid in t2.dwbm ");
            strSql.Append(" ) t where t.unitid is not null) ");
            OracleParameter[] parameters = {
					new OracleParameter(":DWBM", OracleType.VarChar,50)			};
            parameters[0].Value = dwbm;
            return DbHelperOra.Query(strSql.ToString(), parameters);
        } 
        #endregion

        #region 事务更新模板的顺序级归属关系
        public bool Update(Hashtable sqlList)
        {
            try
            {
                return DbHelperOra.ExecuteSqlTran(sqlList);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Update(Hashtable sqlList)", "EDRS.OracleDAL.XY_DZJZ_MBPZB_Ex", sqlList);
            }
            return false;
        }

        public DataSet GetDwAjList(out int count,string where, string orderBy, int startIndex, int endIndex, params object[] objValues)
        {
            StringBuilder strSql = new StringBuilder();
            if (string.IsNullOrEmpty(orderBy))
            {
                orderBy = "dwbm";
            }
            strSql.Append(@"SELECT * FROM (
                select row_number() over(ORDER BY qxbm) Ro,
                qxbm,qxmc,ajlbbm,ajlbmc,autonum
                FROM
                (
                select  UnitID qxbm,dw.dwmc qxmc,CaseInfoTypeID ajlbbm,CaseInfoTypeName ajlbmc,
                (select COUNT(1) from xy_dzjz_mbpzb 
                where trim(unitid) = trim(t.UnitID) 
                and trim(CaseInfoTypeID) = trim(t.CaseInfoTypeID) and Auto = 'Y') autonum 
                from XY_DZJZ_MBPZB t 
                left join xt_zzjg_dwbm dw on (t.unitid = dw.dwbm) 
                group by UnitID,CaseInfoTypeID,dw.dwmc,CaseInfoTypeName
                ) t
                 where 1=1 "+ where + @"
                ) T WHERE RO between " + startIndex + " and " + endIndex);
            try
            {
                string sql = @"select COUNT(1)
                FROM
                (
                select  UnitID qxbm,dw.dwmc qxmc,CaseInfoTypeID ajlbbm,CaseInfoTypeName ajlbmc,
                (select COUNT(1) from xy_dzjz_mbpzb 
                where trim(unitid) = trim(t.UnitID) 
                and trim(CaseInfoTypeID) = trim(t.CaseInfoTypeID) and Auto = 'Y') autonum 
                from XY_DZJZ_MBPZB t 
                left join xt_zzjg_dwbm dw on (t.unitid = dw.dwbm) 
                group by UnitID,CaseInfoTypeID,dw.dwmc,CaseInfoTypeName
                ) t
                 where 1=1 " + where + @"";
                object result = DbHelperOra.GetSingle(sql, ParameterHelp.ParameterReset(strSql.ToString(), objValues));
                count = Convert.ToInt32(result);
                return DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strSql.ToString(), objValues));
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "ppublic DataSet GetDwAjList(string where, params object[] objValues)", "XY_DZJZ_MBPZB_Ex", strSql.ToString(), ParameterHelp.ParameterReset(strSql.ToString(), objValues));
            }
            count = 0;
            return null;
        }
        /// <summary>
        /// 批量从ICE向本地数据库添加模板
        /// </summary>
        /// <param name="list"></param>
        /// <param name="dwbm"></param>
        /// <param name="ajlbmc"></param>
        /// <param name="sslbmc"></param>
        /// <returns></returns>
        public bool AddList(List<Dictionary<string, string>> list, string dwbm, string ajlbbm,string ajlbmc,string sslbbm, string sslbmc)
        {
            StringBuilder strSql0 = new StringBuilder();
            string sslbs = "";
            foreach (Dictionary<string, string> model in list)
            {
                sslbs += (sslbs == "" ? "" : ",") + "'" + model["id"] + "'";
            }
            //先删除数据
            strSql0.Append("DELETE XY_DZJZ_MBPZB WHERE UnitID = :UnitID AND CaseInfoTypeID=:CaseInfoTypeID AND SSLBBM in (" + sslbs + ")");
            OracleParameter[] parameters = new OracleParameter[]{
                    new OracleParameter(":UnitID", OracleType.VarChar,20),
                    new OracleParameter(":CaseInfoTypeID",OracleType.VarChar,20)
            };
            parameters[0].Value = dwbm;
            parameters[1].Value = ajlbbm;

            bool result = false;
            try
            {
                int row = DbHelperOra.ExecuteSql(strSql0.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Add(EDRS.Model.XY_DZJZ_MBPZB model)", "EDRS.OracleDAL.XY_DZJZ_MBPZB", strSql0.ToString(), parameters);
                return false;
            }
            //获取自增编码
            string number = "00000000";
            DataSet ds = GetListByPage("", "DOSSIERTYPEVALUEMEMBER desc", 0, 1);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                number = ds.Tables[0].Rows[0]["DOSSIERTYPEVALUEMEMBER"].ToString();
            }

            //先增加父级目录
            Hashtable sqlList = new Hashtable();
            //父级目录编码，所属类别对应关系
            Dictionary<string, string> keys = new Dictionary<string, string>();
            result = false;
            int i = 0;
            foreach (Dictionary<string,string> model in list)
            {
                StringBuilder strSql1 = new StringBuilder();
                if (model["parentId"] != "")
                {
                    continue;
                }
                //包含子级时自动设置父级菜单为自动生成
                foreach (Dictionary<string, string> modelChild in list)
                {
                    if (modelChild["parentId"] == "")
                    {
                        continue;
                    }
                    if (modelChild["auto"] == "Y")
                    {
                        model["auto"] = "Y";
                    }
                }
                strSql1.Clear();
                strSql1.Append("insert into XY_DZJZ_MBPZB(");
                strSql1.Append("DossierTypeValueMember,CaseInfoTypeID,CaseInfoTypeName,UnitID,DossierTypeDisplayMember,DossierParentMember,DossierEvidenceValueMember,SortIndex,Category,SSLBBM,SSLBMC,Auto)");
                strSql1.Append(" values (");
                strSql1.Append(":DossierTypeValueMember" + i + ",:CaseInfoTypeID" + i + ",:CaseInfoTypeName" + i + ",:UnitID" + i + ",:DossierTypeDisplayMember" + i + ",:DossierParentMember" + i + ",:DossierEvidenceValueMember" + i + ",:SortIndex" + i + ",:Category" + i + ",:SSLBBM" + i + ",:SSLBMC" + i + ",:Auto" + i + ")");
                parameters = new OracleParameter[]{
					new OracleParameter(":DossierTypeValueMember"+i, OracleType.VarChar,20),
					new OracleParameter(":CaseInfoTypeID"+i, OracleType.VarChar,20),
					new OracleParameter(":CaseInfoTypeName"+i, OracleType.VarChar,100),
					new OracleParameter(":UnitID"+i, OracleType.VarChar,20),
					new OracleParameter(":DossierTypeDisplayMember"+i, OracleType.VarChar,100),
					new OracleParameter(":DossierParentMember"+i, OracleType.VarChar,20),
                    new OracleParameter(":DossierEvidenceValueMember"+i, OracleType.VarChar,100),
                    new OracleParameter(":SortIndex"+i, OracleType.Number,18),
                    new OracleParameter(":Category"+i, OracleType.Char,1),
					new OracleParameter(":SSLBBM"+i, OracleType.VarChar,20),
					new OracleParameter(":SSLBMC"+i, OracleType.VarChar,100),
                    new OracleParameter(":Auto"+i,OracleType.Char,1)};
                number = (int.Parse(number) + 1).ToString().PadLeft(8, '0');
                //添加对应关系
                keys.Add(model["id"], number);

                parameters[0].Value = number;
                parameters[1].Value = ajlbbm;
                parameters[2].Value = ajlbmc;
                parameters[3].Value = dwbm;
                parameters[4].Value = model["text"];
                parameters[5].Value = "";
                parameters[6].Value = "";
                parameters[7].Value = model["SortIndex"];
                parameters[8].Value = "J";
                parameters[9].Value = model["id"];
                parameters[10].Value = model["text"];// model.SSLBMC;
                parameters[11].Value = model["auto"];
                sqlList.Add(strSql1.ToString(), parameters);
                i++;
            }
            //初始化子级模板
            for (int ii=0;ii<list.Count;ii++)
            {
                StringBuilder strSql2 = new StringBuilder();
                Dictionary<string, string> model = list[ii];
                if (model["parentId"] == "")
                {
                    continue;
                }
                strSql2.Clear();
                strSql2.Append("insert into XY_DZJZ_MBPZB(");
                strSql2.Append("DossierTypeValueMember,CaseInfoTypeID,CaseInfoTypeName,UnitID,DossierTypeDisplayMember,DossierParentMember,DossierEvidenceValueMember,SortIndex,Category,SSLBBM,SSLBMC,Auto)");
                strSql2.Append(" values (");
                strSql2.Append(":DossierTypeValueMember" + i + ",:CaseInfoTypeID" + i + ",:CaseInfoTypeName" + i + ",:UnitID" + i + ",:DossierTypeDisplayMember" + i + ",:DossierParentMember" + i + ",:DossierEvidenceValueMember" + i + ",:SortIndex" + i + ",:Category" + i + ",:SSLBBM" + i + ",:SSLBMC" + i + ",:Auto" + i + ")");
                parameters = new OracleParameter[]{
					new OracleParameter(":DossierTypeValueMember"+i, OracleType.VarChar,20),
					new OracleParameter(":CaseInfoTypeID"+i, OracleType.VarChar,20),
					new OracleParameter(":CaseInfoTypeName"+i, OracleType.VarChar,100),
					new OracleParameter(":UnitID"+i, OracleType.VarChar,20),
					new OracleParameter(":DossierTypeDisplayMember"+i, OracleType.VarChar,100),
					new OracleParameter(":DossierParentMember"+i, OracleType.VarChar,20),
                    new OracleParameter(":DossierEvidenceValueMember"+i, OracleType.VarChar,100),
                    new OracleParameter(":SortIndex"+i, OracleType.Number,18),
                    new OracleParameter(":Category"+i, OracleType.Char,1),
					new OracleParameter(":SSLBBM"+i, OracleType.VarChar,20),
					new OracleParameter(":SSLBMC"+i, OracleType.VarChar,100),
                    new OracleParameter(":Auto"+i,OracleType.Char,1)};
                number = (int.Parse(number) + 1).ToString().PadLeft(8, '0');
                parameters[0].Value = number;
                parameters[1].Value = ajlbbm;
                parameters[2].Value = ajlbmc;
                parameters[3].Value = dwbm;
                parameters[4].Value = model["text"];
                parameters[5].Value = keys[model["parentId"]];
                parameters[6].Value = "";
                parameters[7].Value = model["SortIndex"];
                parameters[8].Value = "W";
                parameters[9].Value = model["id"];
                parameters[10].Value = model["text"];// model.SSLBMC;
                parameters[11].Value = model["auto"];
                sqlList.Add(strSql2.ToString(), parameters);
                i++;
            }
            try
            {
                result = DbHelperOra.ExecuteSqlTran(sqlList);
                return result;
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool AddLB(List<EDRS.Model.XT_DM_QX> list, string jsbm, string dwbm, string bmbm)", "EDRS.OracleDAL.XT_DM_QX", sqlList);
            }
            return false;
        }

        public bool DeleteNodeAndChild(string dwbm, string ajlbbm, string sslbbm)
        {
            string sql = "DELETE XY_DZJZ_MBPZB ";
            sql += "WHERE DOSSIERPARENTMEMBER IN ( SELECT DOSSIERTYPEVALUEMEMBER FROM XY_DZJZ_MBPZB WHERE UNITID=:UNITID AND CASEINFOTYPEID=:CASEINFOTYPEID AND SSLBBM=:SSLBBM) ";
            sql += " OR (UNITID=:UNITID AND CASEINFOTYPEID=:CASEINFOTYPEID AND SSLBBM=:SSLBBM)";

            OracleParameter[] parameters = {
					new OracleParameter(":UNITID", OracleType.VarChar,50),
					new OracleParameter(":CASEINFOTYPEID", OracleType.VarChar,20),
					new OracleParameter(":SSLBBM", OracleType.VarChar,20)
            };
            parameters[0].Value = dwbm;
            parameters[1].Value = ajlbbm;
            parameters[2].Value = sslbbm;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(sql, parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Delete(string DossierTypeValueMember)", "EDRS.OracleDAL.XY_DZJZ_MBPZB", sql, parameters);
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
        #endregion
    }
}

