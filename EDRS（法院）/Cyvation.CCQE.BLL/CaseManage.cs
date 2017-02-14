using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cyvation.CCQE.Model;
using System.Data;

namespace Cyvation.CCQE.BLL
{
    public class CaseManage
    {
        /// <summary>
        /// 获取案由
        /// </summary>
        /// <param name="strErr"></param>
        /// <returns></returns>
        public static DataTable GetAy(out string strErr)
        {
            strErr = string.Empty;
            DataTable dt = new DataTable();
            ParamConvert pc = new ParamConvert();
            pc.Basic(true);
            try
            {
                dt = pc.DoExecuteDataTable("pkg_fldm_manage.proc_get_sjzdbygd");
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
            }
            return dt;
        }

        /// <summary>
        /// 查询监督案件
        /// </summary>
        /// <param name="param"></param>
        /// <param name="strErr"></param>
        /// <returns></returns>
        public static DataTable GetCaseList(JdAjModel param, out string strErr)
        {
            DataTable dt = null;
            strErr = string.Empty;
            ParamConvert pc = new ParamConvert();
            pc.ConvertParam<JdAjModel>(param);
            try
            {
                dt = pc.DoExecuteDataTable("pkg_case_manage.proc_get_jdaj");
                param.Count = Convert.ToInt32(pc.GetValueByKey("p_count"));
            }
            catch (Exception e)
            {
                strErr = e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 获取监督案件信息
        /// </summary>
        /// <param name="ajbh"></param>
        /// <param name="strErr"></param>
        /// <returns></returns>
        public static JdAjModel GetJdAjByAjbm(string ajbh, out string strErr)
        {
            strErr = string.Empty;
            JdAjModel jdaj = new JdAjModel();
            ParamConvert pc = new ParamConvert();
            pc.Basic(true);
            pc.Add("p_ajbh", ajbh);
            try
            {
                jdaj = pc.DoExecuteSprocAccessor<JdAjModel>("pkg_case_manage.proc_get_jdajbyid").FirstOrDefault();
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
            }
            return jdaj;
        }

        /// <summary>
        /// 添加/修改监督案件
        /// </summary>
        /// <param name="param"></param>
        /// <param name="strErr"></param>
        public static void AddOrUpdateCase(JdAjModel param, out string strErr)
        {
            strErr = string.Empty;
            if (param == null) strErr = "监督数据错误！";
            ParamConvert paramConvert = new ParamConvert();
            paramConvert.ConvertParam<JdAjModel>(param, false);
            try
            {
                if (string.IsNullOrEmpty(param.AJBH))
                {
                    paramConvert.DoExecuteNonQuery("pkg_case_manage.proc_add_jdaj");
                    param.AJBH = Convert.ToString(paramConvert.GetValueByKey("p_ajbh"));
                }
                else
                {
                    paramConvert.DoExecuteNonQuery("pkg_case_manage.proc_update_jdaj");
                }
            }
            catch (Exception e)
            {
                strErr = e.Message;
            }
        }

        /// <summary>
        /// 新增监督案件
        /// </summary>
        public static void AddCaseInfo(JdAjModel param, out string strErr)
        {
            strErr = string.Empty;
            ParamConvert paramConvert = new ParamConvert();
            paramConvert.ConvertParam<JdAjModel>(param, false);
            try
            {
                paramConvert.DoExecuteNonQuery("pkg_case_manage.proc_add_jdaj");
            }
            catch (Exception e)
            {
                strErr = e.Message;
            }
        }

        /// <summary>
        /// 修改监督案件
        /// </summary>
        public static void UpdateCaseInfo(JdAjModel param, out string strErr)
        {
            strErr = string.Empty;
            ParamConvert paramConvert = new ParamConvert();
            paramConvert.ConvertParam<JdAjModel>(param, false);
            try
            {
                paramConvert.DoExecuteNonQuery("pkg_case_manage.proc_update_jdaj");
            }
            catch (Exception e)
            {
                strErr = e.Message;
            }
        }

        /// <summary>
        /// 监督案件删除
        /// </summary>
        public static void DeleteCaseInfo(string ajbhj, out string strErr)
        {
            strErr = string.Empty;
            ParamConvert paramConvert = new ParamConvert();
            paramConvert.Basic(false);
            paramConvert.Add("p_ajbhj", ajbhj); // 案件名称            
            try
            {
                paramConvert.DoExecuteNonQuery("pkg_case_manage.proc_delete_jdaj");
            }
            catch (Exception e)
            {
                strErr = e.Message;
            }
        }

        /// <summary>
        /// 查询监督案件（发送）
        /// </summary>
        public static DataTable GetCaseListForSend(JdAjModel param, out string strErr)
        {
            DataTable dt = null;
            strErr = string.Empty;
            ParamConvert pc = new ParamConvert();
            pc.Basic(false);
            pc.Add("p_dwbm", param.CBDW_BM);
            pc.Add("p_ajmc", param.AJMC);
            pc.Add("p_cjrgh", param.CJRGH);
            pc.Add("p_dtbegin", param.dtPbBeg);
            pc.Add("p_dtend", param.dtPbEnd);
            pc.Add("p_pagesize", param.PageSize);
            pc.Add("p_pageindex", param.PageIndex);
            pc.Add("p_count", param.Count);
            try
            {
                dt = pc.DoExecuteDataTable("pkg_case_manage.proc_get_jdajforsend");
                param.Count = Convert.ToInt32(pc.GetValueByKey("p_count"));
            }
            catch (Exception e)
            {
                strErr = e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 查询监督案件(反馈)
        /// </summary>
        public static DataTable GetCaseListForResponse(JdAjModel param, out string strErr)
        {
            DataTable dt = null;
            strErr = string.Empty;
            ParamConvert pc = new ParamConvert();
            pc.Basic(false);
            pc.Add("p_dwbm", param.ZBDW_BM);
            pc.Add("p_ajmc", param.AJMC);
            pc.Add("p_dtbegin", param.dtPbBeg);
            pc.Add("p_dtend", param.dtPbEnd);
            pc.Add("p_pagesize", param.PageSize);
            pc.Add("p_pageindex", param.PageIndex);
            pc.Add("p_count", param.Count);
            try
            {
                dt = pc.DoExecuteDataTable("pkg_case_manage.proc_get_jdajforresponse");
                param.Count = Convert.ToInt32(pc.GetValueByKey("p_count"));
            }
            catch (Exception e)
            {
                strErr = e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 查询监督案件（检察院查询由公安反馈回来的案件）
        /// </summary>
        public static DataTable GetCaseListForBack(JdAjModel param, out string strErr)
        {
            DataTable dt = null;
            strErr = string.Empty;
            ParamConvert pc = new ParamConvert();
            pc.Basic(false);
            pc.Add("p_dwbm", param.CBDW_BM);
            pc.Add("p_ajmc", param.AJMC);
            pc.Add("p_cjrgh", param.CJRGH);
            pc.Add("p_dtbegin", param.dtPbBeg);
            pc.Add("p_dtend", param.dtPbEnd);
            pc.Add("p_pagesize", param.PageSize);
            pc.Add("p_pageindex", param.PageIndex);
            pc.Add("p_count", param.Count);
            try
            {
                dt = pc.DoExecuteDataTable("pkg_case_manage.proc_get_jdajforback");
                param.Count = Convert.ToInt32(pc.GetValueByKey("p_count"));
            }
            catch (Exception e)
            {
                strErr = e.Message;
            }
            return dt;
        }
        /// <summary>
        /// 查询已整改监督案件
        /// </summary>
        public static DataTable GetYzgCase(JdAjModel param, out string strErr)
        {
            DataTable dt = null;
            strErr = string.Empty;
            ParamConvert pc = new ParamConvert();
            pc.ConvertParam<JdAjModel>(param);
            try
            {
                dt = pc.DoExecuteDataTable("pkg_case_manage.proc_get_yzgaj");
                param.Count = Convert.ToInt32(pc.GetValueByKey("p_count"));
            }
            catch (Exception e)
            {
                strErr = e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 查询已整改监督案件
        /// </summary>
        public static DataTable GetYJDCase(JdAjModel param, out string strErr)
        {
            DataTable dt = null;
            strErr = string.Empty;
            ParamConvert pc = new ParamConvert();
            pc.ConvertParam<JdAjModel>(param);
            try
            {
                dt = pc.DoExecuteDataTable("pkg_case_manage.proc_get_yjdaj");
                param.Count = Convert.ToInt32(pc.GetValueByKey("p_count"));
            }
            catch (Exception e)
            {
                strErr = e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 查询已监督案件所经历的状态（检察院）
        /// </summary>
        public static DataTable GetYJDCaseZt(JdAjModel param, out string strErr)
        {
            DataTable dt = null;
            strErr = string.Empty;
            ParamConvert pc = new ParamConvert();
            pc.ConvertParam<JdAjModel>(param);
            try
            {
                dt = pc.DoExecuteDataTable("pkg_case_manage.proc_get_yjdajzt");
                param.Count = Convert.ToInt32(pc.GetValueByKey("p_count"));
            }
            catch (Exception e)
            {
                strErr = e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 获取监督案件考评信息
        /// </summary>
        public static DataTable GetKpInfo(JdAjModel param, out string strErr)
        {
            DataTable dt = null;
            strErr = string.Empty;
            ParamConvert pc = new ParamConvert();
            pc.ConvertParam<JdAjModel>(param);
            try
            {
                dt = pc.DoExecuteDataTable("pkg_case_manage.proc_get_jdajkp");
                param.Count = Convert.ToInt32(pc.GetValueByKey("p_count"));
            }
            catch (Exception e)
            {
                strErr = e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 查询考评指标分类
        /// </summary>
        public static DataTable GetAjKpzbfl(string ajbh, out string strErr)
        {
            DataTable dt = null;
            strErr = string.Empty;
            ParamConvert pc = new ParamConvert();
            pc.Basic(true);
            pc.Add("p_ajbh", ajbh);
            try
            {
                dt = pc.DoExecuteDataTable("pkg_case_manage.proc_get_zbflbyajbm");
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
            }
            return dt;
        }

        /// <summary>
        /// 获取案件对应分类指标的质量
        /// </summary>
        /// <param name="ajbh"></param>
        /// <param name="flbh"></param>
        /// <param name="strErr"></param>
        /// <returns></returns>
        public static DataTable GetAjFlzbZl(string ajbh, string flbh, out string strErr)
        {
            DataTable dt = null;
            strErr = string.Empty;
            ParamConvert pc = new ParamConvert();
            pc.Basic(true);
            pc.Add("p_ajbh", ajbh);
            pc.Add("p_flbh", flbh);
            try
            {
                dt = pc.DoExecuteDataTable("pkg_case_manage.proc_get_ajzlbyajandfl");
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
            }
            return dt;
        }

        /// <summary>
        /// 执行流程并更新案件状态
        /// </summary>
        public static void ExeLCAJ(LcsljdModel lc, out string strErr)
        {
            strErr = string.Empty;
            ParamConvert paramConvert = new ParamConvert();
            paramConvert.Basic(false);
            paramConvert.Add("p_ajbh", lc.AJBH); // 案件编号
            paramConvert.Add("p_lcslbh", lc.LCSLBH); // 流程实例编号
            paramConvert.Add("p_dwbm", lc.DWBM); // 单位编码
            paramConvert.Add("p_lcmbbm", lc.LCMBBM); // 流程模板编码
            paramConvert.Add("p_lcjdbm", lc.LCJDBM); // 流程节点编号
            paramConvert.Add("p_jdzxzgh", lc.JDZXZGH); // 节点执行者工号
            paramConvert.Add("p_jdzxz", lc.JDZXZ); // 节点执行者
            try
            {
                paramConvert.DoExecuteNonQuery("pkg_wf_instance.proc_lcsl_exeajzt");
            }
            catch (Exception e)
            {
                strErr = e.Message;
            }
        }

        /// <summary>
        /// 撤回通知书
        /// </summary>
        public static void BackAjZt(LcsljdModel lc, out string strErr)
        {
            strErr = string.Empty;
            ParamConvert paramConvert = new ParamConvert();
            paramConvert.Basic(false);
            paramConvert.Add("p_ajbh", lc.AJBH); // 案件编号
            paramConvert.Add("p_lcslbh", lc.LCSLBH); // 流程实例编号
            paramConvert.Add("p_dwbm", lc.DWBM); // 单位编码
            paramConvert.Add("p_lcmbbm", lc.LCMBBM); // 流程模板编码
            paramConvert.Add("p_lcjdbm", lc.LCJDBM); // 流程节点编号
            paramConvert.Add("p_jdzxzgh", lc.JDZXZGH); // 节点执行者工号
            paramConvert.Add("p_jdzxz", lc.JDZXZ); // 节点执行者
            try
            {
                paramConvert.DoExecuteNonQuery("pkg_wf_instance.proc_lcsl_backajzt");
            }
            catch (Exception e)
            {
                strErr = e.Message;
            }
        }

        /// <summary>
        /// 保存案件考评质量
        /// </summary>
        /// <param name="ajbh"></param>
        /// <param name="flbh"></param>
        /// <param name="ajKpzl"></param>
        /// <param name="strErr"></param>
        public static void SaveAjKpzl(string ajbh, string flbh, List<AjKpzbZlModel> ajKpzl, LcsljdModel lcsljd, out string strErr)
        {
            strErr = string.Empty;
            ParamConvert pc = new ParamConvert();
            try
            {
                pc.DoExecuteWork(() =>
                {
                    if (string.IsNullOrEmpty(lcsljd.LCSLBH))
                    {
                        pc.Basic(false);
                        pc.ConvertParam<LcsljdModel>(lcsljd);
                        pc.DoWork("pkg_wf_instance.proc_lcsl_start");
                        lcsljd.LCSLBH = Convert.ToString(pc.GetValueByKey("p_lcslbh")).Trim();
                    }

                    pc.Basic(false);
                    pc.Add("p_ajbh", ajbh);
                    pc.Add("p_flbh", flbh);
                    pc.Add("p_lcslbh", lcsljd.LCSLBH);
                    pc.DoWork("pkg_case_manage.proc_delete_ajzl");

                    foreach (AjKpzbZlModel o in ajKpzl)
                    {
                        o.LCSLBH = lcsljd.LCSLBH;
                        pc.Basic(false);
                        pc.ConvertParam<AjKpzbZlModel>(o);
                        pc.DoWork("pkg_case_manage.proc_add_ajzl");
                    }

                });
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
            }
        }

        #region 统计相关
        /// <summary>
        /// 获取部门考评案件数量
        /// </summary>
        public static DataTable GetBmAjCount(JdAjModel param, out string strErr)
        {
            DataTable dt = null;
            strErr = string.Empty;
            ParamConvert pc = new ParamConvert();
            pc.Basic(false);
            pc.Add("p_dwbm", param.CBDW_BM);
            pc.Add("p_dtbegin", param.dtPbBeg);
            pc.Add("p_dtend", param.dtPbEnd);           
            try
            {
                dt = pc.DoExecuteDataTable("pkg_case_manage.proc_get_bmajcount");
            }
            catch (Exception e)
            {
                strErr = e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 获取承办人监督案件数量
        /// </summary>
        public static DataTable GetCbrAjCount(JdAjModel param, out string strErr)
        {
            DataTable dt = null;
            strErr = string.Empty;
            ParamConvert pc = new ParamConvert();
            pc.Basic(false);
            pc.Add("p_dwbm", param.CBDW_BM);
            pc.Add("p_bmbm", param.CBBM_BM);
            pc.Add("p_dtbegin", param.dtPbBeg);
            pc.Add("p_dtend", param.dtPbEnd);
            try
            {
                dt = pc.DoExecuteDataTable("pkg_case_manage.proc_get_cbrajcount");
            }
            catch (Exception e)
            {
                strErr = e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 获取考评指标统计分析
        /// </summary>
        public static DataTable GetZbCount(JdAjModel param, out string strErr)
        {
            DataTable dt = null;
            strErr = string.Empty;
            ParamConvert pc = new ParamConvert();
            pc.Basic(false);
            pc.Add("p_dwbm", param.CBDW_BM);
            pc.Add("p_bmbm", param.CBBM_BM);
            pc.Add("p_dtbegin", param.dtPbBeg);
            pc.Add("p_dtend", param.dtPbEnd);
            try
            {
                dt = pc.DoExecuteDataTable("pkg_case_manage.proc_get_zbcount");
            }
            catch (Exception e)
            {
                strErr = e.Message;
            }
            return dt;
        }
        #endregion
    }
}
