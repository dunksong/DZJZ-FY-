using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Cyvation.CCQE.DataAccess;
using Cyvation.CCQE.Common;

namespace Cyvation.CCQE.BLL
{
    public delegate void DoWork();

    public static class ParamConvertStaticHelper
    {
        /// <summary>
        /// 可空时间转字符串
        /// </summary>
        /// <param name="date"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string DateTimeHelper(this DateTime? date, string format = "yyyy-MM-dd")
        {
            return date != null ? ((DateTime)date).ToString(format) : null;
        }

        #region 模板
        ///// <summary>
        ///// 数据字典参数帮助类
        ///// </summary>
        //public static KeyValueItem[] TSJZDEntityParam(this ParamConvert para, SJZDEntity sjzd, bool bIsCursor = true)
        //{
        //    para.Basic(bIsCursor);

        //    para.Add("p_dm", sjzd.DM);
        //    para.Add("p_fdm", sjzd.FDM);
        //    para.Add("p_mc", sjzd.MC);
        //    para.Add("p_sslbdm", sjzd.SSLBDM);
        //    para.Add("p_sslbmc", sjzd.SSLBMC);
        //    para.Add("p_xh", sjzd.XH);
        //    para.Add("p_sm", sjzd.SM);

        //    return para.KeyValue;
        //}

        ///// <summary>
        ///// 添加分页信息，要求参数继承自 SearchParam 类型，并且已经构建好其他参数
        ///// </summary>
        ///// <param name="para"></param>
        ///// <param name="page"></param>
        ///// <returns></returns>
        //public static KeyValueItem[] AddPageInfo(this ParamConvert para, SearchParam page)
        //{
        //    para.Add("p_pageindex", page.PageIndex);
        //    para.Add("p_pagesize", page.PageSize);
        //    para.Add("p_needcount", page.NeedCount ? "Y" : "N");
        //    para.Add("p_count", 0);

        //    return para.KeyValue;
        //}

        ///// <summary>
        ///// 获取分页查询页码总数
        ///// </summary>
        ///// <param name="para"></param>
        ///// <returns></returns>
        //public static int GetPageCount(this ParamConvert para)
        //{
        //    int count = 0;
        //    Int32.TryParse(para.GetValueByKey("p_count").ToString(), out count);
        //    return count;
        //} 
        #endregion
    }

    /// <summary>
    ///  参数帮助类
    /// </summary>
    public class ParamConvert : IDisposable
    {
        private static ILogExtention Logger = LogManagerExtention.GetLogger("name");

        /// <summary>
        /// 操作类别
        /// </summary>
        public enum ExecType
        {
            DoExecuteNonQuery
        }

        public ParamConvert()
        {
            KeyValues = new List<KeyValueItem>();
            //KeyValues.AddRange(lstFunc);
        }

        #region 内部字段
        private List<KeyValueItem> KeyValues = null;

        /// <summary>
        /// 需要返回值的
        /// </summary>
        private readonly List<KeyValueItem> lstFunc = new List<KeyValueItem>(
            new KeyValueItem[] { 
                new KeyValueItem("p_errmsg", string.Empty) });

        /// <summary>
        /// 查询用的
        /// </summary>
        private readonly List<KeyValueItem> lstProc = new List<KeyValueItem>(
            new KeyValueItem[] { 
                new KeyValueItem("p_errmsg", string.Empty), 
                new KeyValueItem("p_cursor", null) });
        #endregion

        #region 外部操作
        /// <summary>
        /// 传入需要提取属性的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="variable">对象</param>
        /// <param name="bIsProc">默认为存储过程，false为函数</param>
        public void ConvertParam<T>(T variable, bool bIsCursor = true)
        {
            Clear();

            if (bIsCursor)
            {
                KeyValues = Param(variable, lstProc);
            }
            else
            {
                KeyValues = Param(variable, lstFunc);
            }
        }

        public void ConvertParam(DataRow dr, DataColumnCollection cols, bool bIsCursor = true)
        {
            Clear();

            if (bIsCursor)
            {
                KeyValues = Param(dr, cols, lstProc);
            }
            else
            {
                KeyValues = Param(dr, cols, lstFunc);
            }
        }

        // 更改某一值
        public void SetValue<T>(string strKey, T strValue)
        {
            foreach (KeyValueItem item in KeyValues)
            {
                if (item.Key.ToLower().Equals(strKey.ToLower()))
                {
                    item.Value = (T)strValue;
                    break;
                }
            }
        }

        /// <summary>
        /// 将某一键更改为指定的其他键
        /// </summary>
        public void SetKey(string beforeKey, string afterValue)
        {
            foreach (KeyValueItem item in KeyValues)
            {
                if (item.Key.ToLower().Equals(beforeKey.ToLower()))
                {
                    item.Key = afterValue;
                    break;
                }
            }
        }

        /// <summary>
        /// 清空对象的属性
        /// </summary>
        public void Clear()
        {
            KeyValues.Clear();
        }


        /// <summary>
        /// 获取转换后的参数数组
        /// </summary>
        public KeyValueItem[] KeyValue
        {
            get { return KeyValues.ToArray<KeyValueItem>(); }
            set { Clear(); KeyValues = value.ToList<KeyValueItem>(); }
        }

        /// <summary>
        /// 错误信息(可读、可写)
        /// </summary>
        public string Errmsg
        {
            get
            {
                if (KeyValues.Count > 0)
                {
                    return Convert.ToString(GetValueByKey("p_errmsg"));
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                if (KeyValues.Count > 0)
                {
                    SetValueByKey("p_errmsg", value);
                }
                else
                {
                    return;
                }
            }
        }

        /// <summary>
        /// 获取指定键的值（通常用于获取单个OUT返回值）
        /// </summary>
        public object GetValueByKey(string key)
        {
            if (KeyValues == null)
            {
                return null;
            }
            foreach (KeyValueItem item in KeyValues)
            {
                if (item.Key.ToLower().Equals((key).ToLower()))
                {
                    return item.Value;
                }
            }
            return null;
        }

        /// <summary>
        /// 更改指定主键key的值为指定的值value
        /// </summary>
        /// <param name="key">主键</param>
        /// <param name="value">更改值</param>
        public void SetValueByKey(string key, object value)
        {
            if (KeyValues == null)
            {
                return;
            }
            KeyValues.ForEach(item =>
            {
                if ((key).ToLower().Trim().Equals(item.Key.ToLower().Trim()))
                {
                    item.Value = value;
                }
            });
        }

        #endregion

        #region 额外添加参数的参数集合
        /// <summary>
        /// 获取或设置额外添加参数的参数数组
        /// </summary>
        public void Add(KeyValueItem[] keyvalues)
        {
            KeyValues.AddRange(keyvalues.ToList<KeyValueItem>());
        }
        public void Add(List<KeyValueItem> keyvalues)
        {
            KeyValues.AddRange(keyvalues);
        }
        public void Add(string key, object value)
        {
            key = key;
            KeyValues.Add(new KeyValueItem(key, value));
        }
        /// <summary>基本参数（错误信息，true包含游标，false不包含游标）
        /// </summary>
        public void Basic(bool bIsCursor)
        {
            Clear();
            if (bIsCursor)
            {
                KeyValues.AddRange(lstProc);
            }
            else
            {
                KeyValues.AddRange(lstFunc);
            }
        }

        #endregion

        #region 几类常用参数

        /// <summary>
        /// Dictionary^string,object^ 参数
        /// </summary>
        public KeyValueItem[] DictionaryStrObj(Dictionary<string, object> Dict, bool bIsCursor = true)
        {
            Clear();
            if (bIsCursor)
            {
                KeyValues.AddRange(lstProc);
            }
            else
            {
                KeyValues.AddRange(lstFunc);
            }
            foreach (var item in Dict)
            {
                KeyValues.Add(new KeyValueItem("p_" + item.Key.ToLower(), item.Value));
            }
            return KeyValues.ToArray<KeyValueItem>();
        }

        /// <summary>
        ///  添加参数集合
        /// </summary>
        public KeyValueItem[] AddAnyParams(List<KeyValueItem> lstKeyValue, bool bIsCursor = true)
        {
            Clear();
            if (bIsCursor)
            {
                KeyValues.AddRange(lstProc);
            }
            else
            {
                KeyValues.AddRange(lstFunc);
            }
            KeyValues.AddRange(lstKeyValue);
            return KeyValues.ToArray<KeyValueItem>();
        }

        #endregion

        #region 内部逻辑
        /// <summary>
        /// 主要转换函数，仅供对象内部调用
        /// </summary>
        private List<KeyValueItem> Param<T>(T variable, List<KeyValueItem> param)
        {
            string[] different = { "Lcstxx" };
            List<KeyValueItem> lst = new List<KeyValueItem>(param);
            Type type = typeof(T);

            string pname = string.Empty;
            object pValue = null;
            PropertyInfo[] ps = type.GetProperties();
            foreach (PropertyInfo p in ps)
            {
                pname = p.Name;
                pValue = p.GetValue(variable, null);

                if (different.Contains(pname))
                {
                    lst.Add(new KeyValueItem("p_" + pname.ToLower(), Encoding.Default.GetBytes(pValue.ToString())));
                    continue;
                }
                lst.Add(new KeyValueItem("p_" + pname.ToLower(), pValue));
            }
            return lst;
        }

        // 转换DataRow参数
        private List<KeyValueItem> Param(DataRow dr, DataColumnCollection cols, List<KeyValueItem> param)
        {
            List<KeyValueItem> lst = new List<KeyValueItem>(param);

            foreach (DataColumn clo in cols)
            {
                lst.Add(new KeyValueItem("p_" + clo.ColumnName.ToLower(), dr[clo.ColumnName]));
            }
            return lst;
        }
        #endregion

        #region 抛出错误信息
        /// <summary>
        /// 抛出错误信息
        /// </summary>
        public void ThrowException(string strProcName)
        {
            if (!string.IsNullOrEmpty(Errmsg))
            {
                AddLogError(strProcName, new Exception(Errmsg));
                throw new Exception(Errmsg);
            }
        }
        #endregion

        #region 常规数据库操作（需在参数构建完成后调用）
        /// <summary>
        /// 执行无返回值的数据库操作
        /// </summary>
        /// <param name="current">ICE连接</param>
        /// <param name="strProcName">存储过程</param>
        /// <param name="errmsgInDb">错误信息标识</param>
        /// <param name="strBMSAH">部门受案号（如果有）</param>
        /// <returns>操作结果</returns>
        public bool DoExecuteNonQuery(string strProcName)
        {
            Database db = DataAccessor.CreateDatabase();

            try
            {
                AddLogDebug(strProcName);
                db.ExecuteNonQuery<KeyValueItem>(strProcName, this.KeyValue);
            }
            catch (Exception ex)
            {
                AddLogError(strProcName, ex);
                throw new Exception("数据库处理出错：" + ex.Message);
            }
            finally
            {
                this.ThrowException(strProcName);
            }
            return string.IsNullOrEmpty(this.Errmsg);
        }

        /// <summary>
        /// 执行返回DataSet的有游标的数据库操作
        /// </summary>
        /// <param name="strProcName">存储过程</param>
        /// <returns>DataSet数据</returns>
        public DataSet DoExecuteDataSet(string strProcName)
        {
            Database db = DataAccessor.CreateDatabase();
            DataSet rtnDs = new DataSet();

            try
            {
                AddLogDebug(strProcName);
                rtnDs = db.ExecuteDataSet<KeyValueItem>(strProcName, this.KeyValue);
            }
            catch (Exception ex)
            {
                AddLogError(strProcName, ex);
                throw new Exception("数据库处理出错：" + ex.Message);
            }
            finally
            {
                this.ThrowException(strProcName);
            }
            return rtnDs;
        }

        /// <summary>
        /// 执行返回实体类的数据库操作
        /// </summary>
        /// <typeparam name="T">返回实体类型</typeparam>
        /// <param name="current">ICE连接</param>
        /// <param name="strProcName">存储过程名</param>
        /// <param name="errmsgInDb">错误信息标识</param>
        /// <param name="strError">返回错误信息</param>
        /// <param name="strBMSAH">部门受案号（如果有）</param>
        /// <returns></returns>
        public List<T> DoExecuteSprocAccessor<T>(string strProcName) where T : class, new()
        {
            Database db = DataAccessor.CreateDatabase();
            List<T> rtnLst = new List<T>();

            try
            {
                AddLogDebug(strProcName);
                IEnumerable<T> Info = db.ExecuteSprocAccessor<T, KeyValueItem>(strProcName, this.KeyValue);

                if (string.IsNullOrEmpty(this.Errmsg) && Info != null)
                {
                    rtnLst = Info.ToList<T>();
                }
            }
            catch (Exception ex)
            {
                AddLogError(strProcName, ex);
                throw new Exception("数据库处理出错：" + ex.Message);
            }
            finally
            {
                this.ThrowException(strProcName);
            }
            return rtnLst;
        }

        /// <summary>
        /// 执行返回DataTable的有游标的数据库操作
        /// </summary>
        /// <param name="strProcName">存储过程</param>
        /// <param name="errmsgInDb">日志基本信息</param>
        /// <param name="strError">返回错误信息</param>
        /// <param name="strBMSAH">部门受案号（如果有）</param>
        /// <returns>DataTable数据</returns>
        public DataTable DoExecuteDataTable(string strProcName)
        {
            Database db = DataAccessor.CreateDatabase();
            DataTable rtnDt = new DataTable();

            try
            {
                AddLogDebug(strProcName);
                DataSet ds = db.ExecuteDataSet<KeyValueItem>(strProcName, this.KeyValue);

                if (ds != null && ds.Tables.Count > 0)
                {
                    rtnDt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                AddLogError(strProcName, ex);
                throw new Exception("数据库处理出错：" + ex.Message);
            }
            finally
            {
                this.ThrowException(strProcName);
            }
            return rtnDt;
        }
        #endregion

        #region 事务数据库操作（委托中只能使用此类提供的方法与数据库进行操作）
        private DbTransaction trans = null;
        private Database dbWork = null;
        private void ThrowExceptionWork(string strProcName, string errStrPoint)
        {
            if (!string.IsNullOrEmpty(Errmsg))
            {
                if (string.IsNullOrEmpty(errStrPoint))
                {
                    this.ThrowException(strProcName);
                }
                else
                {
                    throw new Exception("[" + errStrPoint + "]" + Errmsg);
                }
            }
        }
        // ExecType.DoExecuteNonQuery 类别方法
        private void DoExecuteNonQueryInWork(string procName, string errStrPoint = "")
        {
            dbWork.ExecuteNonQuery<KeyValueItem>(trans, procName, this.KeyValue);
            this.ThrowExceptionWork(procName, errStrPoint);
        }
        /// <summary>
        /// 事务处理方法
        /// </summary>
        /// <param name="current">ICE连接</param>
        /// <param name="work">事务委托（内部请使用DoWork相关方法进行存储过程调用）</param>
        /// <param name="errmsgInDb">错误信息标识</param>
        /// <param name="strError">返回错误信息</param>
        /// <returns>事务处理信息（成功或者失败）</returns>
        public bool DoExecuteWork(DoWork work)
        {
            dbWork = DataAccessor.CreateDatabase();

            #region 保存模板事务
            using (var connection = dbWork.CreateConnection())
            {
                connection.Open();
                trans = connection.BeginTransaction();
                try
                {
                    work();
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    this.Errmsg = ex.Message;
                }
                finally
                {
                    trans.Dispose();
                    if (connection.State != ConnectionState.Closed)
                    {
                        connection.Close();
                    }
                    dbWork = null;
                    this.ThrowException("");
                }
            }
            #endregion

            return string.IsNullOrEmpty(this.Errmsg);
        }
        /// <summary>
        /// 相关存储过程调用（调用前请构建好参数）
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="errStrPoint">标识错误信息（便于定位错误，如果出错会加载在错误信息之前）</param>
        /// <param name="execType">调用存储过程类别（默认为ExecType.DoExecuteNonQuery的操作）</param>
        public void DoWork(string procName, string errStrPoint = "", ExecType execType = ExecType.DoExecuteNonQuery)
        {
            switch (execType)
            {
                case ExecType.DoExecuteNonQuery:
                    DoExecuteNonQueryInWork(procName, errStrPoint);
                    break;
            }
        }
        #endregion

        #region AddLog
        private void AddLogDebug(string strProcName)
        {
            StringBuilder sbParamInfo = new StringBuilder();
            foreach (var param in this.KeyValue)
            {
                sbParamInfo.AppendFormat("{0}:{1};", param.Key, param.Value);
            }
            string runInfo = strProcName + "(" + sbParamInfo.ToString().TrimEnd(';') + ")";
            //Logger.Debug(runInfo);
        }

        private void AddLogError(string strProcName, Exception ex)
        {
            StringBuilder sbParamInfo = new StringBuilder();
            foreach (var param in this.KeyValue)
            {
                sbParamInfo.AppendFormat("{0}:{1};", param.Key, param.Value);
            }
            string runInfo = strProcName + "(" + sbParamInfo.ToString().TrimEnd(';') + ")";
            //Logger.Error("调用存储过程" + strProcName + "异常", runInfo, ex);
        }
        #endregion



        public void Dispose()
        {
            trans = null;
            dbWork = null;
            KeyValues = null;
        }
    }
}
