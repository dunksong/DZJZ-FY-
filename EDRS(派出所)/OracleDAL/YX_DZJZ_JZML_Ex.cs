
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
    /// 数据访问类:YX_DZJZ_JZML
    /// </summary>
    public partial class YX_DZJZ_JZML
    {
        /// <summary>
        /// 获取树形案件目录列表
        /// </summary>
        /// <param name="strMlWhere">目录表查询条件</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="withWhere">循环条件</param>
        /// <param name="direction">查询顺序</param>
        /// <param name="yjxh">阅卷序号</param>
        /// <param name="isAll">是否只加载所有</param>
        /// <param name="objValues"></param>
        /// <returns></returns>
        public DataSet GetListByTree(string strMlWhere, string strWhere, string withWhere, bool direction, string yjxh, bool isAll, params object[] objValues)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct CONNECT_BY_ISLEAF ISLEAF,MLBH,FMLBH,MLXSMC,MLLX,MLSXH,SFSM,WJMC,WJLJ, ISEXIST  from (");
            strSql.Append(" select MLBH,FMLBH,MLXSMC,MLLX,MLSXH,SFSM,'' WJMC,'' WJLJ,NULL ISEXIST  from YX_DZJZ_JZML ");
            strSql.AppendFormat(" where 1=1 {0} ", strMlWhere);
            strSql.Append(" union all ");

            strSql.Append(" select WJXH MLBH,MLBH FMLBH,WJXSMC,'4',WJSXH,'' SFSM,WJMC,WJLJ ");
            if (!string.IsNullOrEmpty(yjxh))
            {
                strSql.Append(",(case count(fpbm) when 0 then NULL else 1 end) from ");
                strSql.AppendFormat(" (select a.*,b.fpbm,b.yjxh from YX_DZJZ_JZMLWJ a {0} join YX_DZJZ_LSAJWJFP b on a.wjxh=b.wjxh and b.sfsc='N' ", (isAll ? "left" : "right"));
                strSql.AppendFormat(" and b.yjxh= '{0}')", yjxh);
            }
            else
                strSql.Append(",NULL from YX_DZJZ_JZMLWJ ");

            strSql.AppendFormat(" where 1=1 {0}", strMlWhere);
            strSql.Append(" group by WJXH ,MLBH ,WJXSMC,WJSXH,WJMC,WJLJ ");
            strSql.Append(" ) T ");
            strSql.AppendFormat(" where 1=1 {0}", strWhere);
            strSql.AppendFormat(" start with 1=1 {0}", withWhere);
            strSql.AppendFormat(" connect by {0} order by mlsxh ", (direction ? " FMLBH= prior MLBH " : " prior FMLBH=MLBH "));

            return DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strMlWhere + strWhere + withWhere, objValues));
        }


        /// <summary>
        /// 获取树形案件目录列表
        /// </summary>
        /// <param name="strMlWhere">目录表查询条件</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="withWhere">循环条件</param>
        /// <param name="direction">查询顺序</param>
        /// <param name="yjxh">阅卷序号</param>
        /// <param name="isAll">是否只加载所有</param>
        /// <param name="objValues"></param>
        /// <returns></returns>
        public DataSet GetListByTreeEx(string strMlWhere, string strWhere, string withWhere, bool direction, string yjxh, bool isAll, params object[] objValues)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct CONNECT_BY_ISLEAF ISLEAF,MLBH,FMLBH,MLXSMC,MLLX,MLSXH,SFSM,WJMC,WJLJ, ISEXIST,wjxhjl  from (");
            strSql.Append(" select MLBH,FMLBH,MLXSMC,MLLX,MLSXH,SFSM,'' WJMC,'' WJLJ,NULL ISEXIST ,NULL wjxhjl from YX_DZJZ_JZML ");
            strSql.AppendFormat(" where 1=1 {0} ", strMlWhere);
            strSql.Append(" union all ");

            strSql.Append(" select WJXH MLBH,MLBH FMLBH,WJXSMC,'4',WJSXH,'' SFSM,WJMC,WJLJ ");
            if (!string.IsNullOrEmpty(yjxh))
            {
                strSql.Append(",(case count(fpbm) when 0 then NULL else 1 end),wjxhjl from ");
                strSql.AppendFormat(" (select a.*,b.fpbm,b.yjxh,jl.wjxh as wjxhjl from YX_DZJZ_JZMLWJ a {0} join YX_DZJZ_LSAJWJFP b on a.wjxh=b.wjxh and b.sfsc='N' ", (isAll ? "left" : "right"));
                strSql.AppendFormat(" and b.yjxh= '{0}'", yjxh);
                strSql.Append(" left join yx_dzjz_wjsqdyjl jl on b.yjxh = jl.yjxh and jl.sfsc='N' and b.wjxh=jl.wjxh)");
            }
            else
                strSql.Append(",NULL,NULL wjxhjl from YX_DZJZ_JZMLWJ ");

            strSql.AppendFormat(" where 1=1 {0}", strMlWhere);
            strSql.Append(" group by WJXH ,MLBH ,WJXSMC,WJSXH,WJMC,WJLJ,wjxhjl ");
            strSql.Append(" ) T ");
            strSql.AppendFormat(" where 1=1 {0}", strWhere);
            strSql.AppendFormat(" start with 1=1 {0}", withWhere);
            strSql.AppendFormat(" connect by {0} order by mlsxh ", (direction ? " FMLBH= prior MLBH " : " prior FMLBH=MLBH "));

            return DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strMlWhere + strWhere + withWhere, objValues));
        }




        /// <summary>
        /// 获取树形案件卷和文件
        /// </summary>
        /// <param name="strMlWhere">目录表查询条件</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="withWhere">循环条件</param>
        /// <param name="direction">查询顺序</param>
        /// <param name="yjxh">阅卷序号</param>
        /// <param name="isAll">是否只加载所有</param>
        /// <param name="objValues"></param>
        /// <returns></returns>
        public DataSet GetListByTreeJaM(string strMlWhere, string strWhere, string withWhere, bool direction, string yjxh, bool isAll, params object[] objValues)
        {

            StringBuilder strSql = new StringBuilder();

            strSql.Append(" select MLBH,FMLBH,MLXSMC,MLLX,MLSXH,SFSM,'' WJMC,'' WJLJ,NULL ISEXIST  from YX_DZJZ_JZML ");
            strSql.AppendFormat(" where 1=1 {0} {1}", strMlWhere, strWhere);

            strSql.AppendFormat(" start with 1=1 {0}", withWhere);
            strSql.AppendFormat(" connect by {0} order by mlsxh ", (direction ? " FMLBH= prior MLBH " : " prior FMLBH=MLBH "));

            return DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strMlWhere + strWhere + withWhere, objValues));
        }


        #region 获取文件
        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="strOrder"></param>
        /// <param name="objValues"></param>
        /// <returns></returns>
        public DataSet GetListByWjmc(string strWhere, string strOrder, params object[] objValues)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select WJXH MLBH,MLBH FMLBH,WJXSMC,'4',WJSXH,'' SFSM,WJMC,WJLJ ,NULL from YX_DZJZ_JZMLWJ ");
            strSql.AppendFormat(" where SFSC='N' {0}", strWhere);

            if (!string.IsNullOrEmpty(strOrder))
                strSql.AppendFormat(" order by {0}", strOrder);

            return DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
        }
        #endregion

        #region 卷宗文件获取
        /// <summary>
        /// 卷宗文件获取
        /// </summary>
        /// <param name="DWBM">单位编码</param>
        /// <param name="BMSAH">部门受案号</param>
        /// <param name="JZBH">卷宗编号</param>
        /// <returns></returns>
        public DataSet GetDossierFileInfo(string DWBM, string BMSAH, string JZBH)
        {
            
            
            OracleParameter[] parameters = {                           
                            new OracleParameter(":BMSAH",BMSAH),
                            new OracleParameter(":JZBH",JZBH)
                        };

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ml.BMSAH BH,ml.jzbh JZBH,ml.MLBH JZWJBH, ml.mlxsmc JZWJMC,count(wj.WJXH) JZWJYSL from yx_dzjz_jzml ml");
            strSql.Append(" left join yx_dzjz_jzmlwj wj on ml.jzbh = wj.jzbh and ml.mlbh = wj.mlbh");
            strSql.Append(" where ml.sfsc='N' and  ml.mllx=3 and ml.JZBH=:JZBH  and ml.fmlbh=:BMSAH");

            if (!string.IsNullOrEmpty(DWBM))
            {
                strSql.Append(" and ml.dwbm=:DWBM ");
                parameters[parameters.Length] = new OracleParameter(":DWBM", DWBM);
            }

            strSql.Append(" group by  ml.bmsah,ml.jzbh,ml.MLBH, ml.mlxsmc,ml.MLSXH  order by ml.MLSXH");
            
          
            try
            {
                //EDRS.Common.LogHelper.LogError(this.context, "测试3", "", "public DataSet GetDossierFileInfo (string DWBM, string BH, string JZBH)", "EDRS.OracleDAL.YX_DZJZ_JZJBXX", strSql.ToString());
                return DbHelperOra.Query(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetDossierFileInfo (string DWBM, string BH, string JZBH)", "EDRS.OracleDAL.YX_DZJZ_JZJBXX", strSql.ToString());
            }
            return null;

        }
        #endregion


        #region 卷宗信息获取
        /// <summary>
        /// 卷宗信息获取
        /// </summary>
        /// <param name="BH">部门受案号</param>
        /// <param name="DWBM">单位编码</param>
        /// <returns></returns>
        public DataSet GetDossierInfo(string BH, string DWBM)
        {

            OracleParameter[] parameters = {				
					new OracleParameter(":BMSAH", BH)			};

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select 
(select JZMC from yx_dzjz_jzjbxx  where BMSAH=l.bmsah)as JZMC,l.MLBH as BH,l.JZBH,l.SSLBBM as JZLBBM,l.MLXSMC as JZLBMC,
(select count(1) from yx_dzjz_jzml where FMLBH = l.MLBH and mllx = 3) as JZWJSL ,(select count(1) from yx_dzjz_jzmlwj where bmsah = l.bmsah and l.mlbh=mlbh) as JZWJSL2 
from yx_dzjz_jzml l  
left join yx_dzjz_jzjbxx jbxx on (l.BMSAH = jbxx.BMSAH) ");
            strSql.Append(" where l.BMSAH=:BMSAH and l.mllx = 1");
            if (!string.IsNullOrEmpty(DWBM))
            {
                strSql.Append(" and l.DWBM=:DWBM ");
                parameters[parameters.Length] = new OracleParameter(":DWBM", DWBM);

            }
            try
            {
                //EDRS.Common.LogHelper.LogError(this.context, "测试2", "", "public DataSet GetDossierInfo(string BH, string DWBM)", "EDRS.OracleDAL.YX_DZJZ_JZML", strSql.ToString(), parameters);
                return DbHelperOra.Query(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetDossierInfo(string BH, string DWBM)", "EDRS.OracleDAL.YX_DZJZ_JZML", strSql.ToString(), parameters);
            }
            return null;
        }
        #endregion

        #region 卷宗页获取
        /// <summary>
        /// 卷宗页获取
        /// </summary>
        /// <param name="DWBM">单位编号</param>
        /// <param name="BH">部门受案号</param>
        /// <param name="JZBH">卷宗编号</param>
        /// <param name="JZWJBH">卷宗文件编号</param>
        public DataSet GetDossierFilePageInfo(string DWBM, string BH, string JZBH, string JZWJBH)
        {

            OracleParameter[] parameters = {
					new OracleParameter(":BMSAH", BH),
                    new OracleParameter(":JZBH",JZBH),
                    new OracleParameter(":MLBH", JZWJBH)};

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select j.BMSAH as BH,j.jzbh as JZBH,j.Wjym as JZWJYYM,j.WJLX as JZWJYLX,j.Wjxsmc as JZWJYMC,j.WJXH as JZWJYBH from yx_dzjz_jzmlwj j 
where BMSAH=:BMSAH and JZBH=:JZBH and MLBH=:MLBH and SFSC='N' ");
            if (!string.IsNullOrEmpty(DWBM))
            {
                strSql.Append("  and DWBM=:DWBM");
                parameters[parameters.Length] = new OracleParameter(":DWBM", DWBM);
            }
            strSql.Append(" order by WJSXH");
         
            try
            {
                //EDRS.Common.LogHelper.LogError(this.context, "测试4", "", " public DataSet GetDossierFilePageInfo(string DWBM, string BH, string JZBH, string JZWJBH)", "EDRS.OracleDAL.YX_DZJZ_JZML", strSql.ToString(), parameters);
                return DbHelperOra.Query(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, " public DataSet GetDossierFilePageInfo(string DWBM, string BH, string JZBH, string JZWJBH)", "EDRS.OracleDAL.YX_DZJZ_JZML", strSql.ToString(), parameters);
            }
            return null;
        }
        #endregion
    }
}

