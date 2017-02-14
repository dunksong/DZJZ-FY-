using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cyvation.CCQE.Model;
using System.Data;
using Cyvation.CCQE.Common;

namespace Cyvation.CCQE.BLL
{
    public class DocManage
    {
        /// <summary>
        /// 获取文书模板信息
        /// </summary>
        /// <param name="wsmbbh"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static DocTemplateModel GetDocmbInfo(string wsmbbh, out string errmsg)
        {
            errmsg = string.Empty;
            DocTemplateModel docInfo = null;
            DataTable dt = null;
            ParamConvert pc = new ParamConvert();
            pc.Basic(true);
            pc.Add("p_wsmbbh", wsmbbh);
            try
            {
                dt = pc.DoExecuteDataTable("PKG_DOC_MANAGE.proc_get_mbifo");
                errmsg += Convert.ToString(pc.GetValueByKey("p_errmsg"));
            }
            catch (Exception e)
            {
                errmsg += e.Message;
            }
            if (string.IsNullOrEmpty(errmsg) && dt != null && dt.Rows.Count == 1)
            {
                docInfo = ModelHandler.FillModel<DocTemplateModel>(dt.Rows[0]);
            }
            return docInfo;
        }
        /// <summary>
        /// 创建通知书
        /// </summary>
        /// <param name="docInfo"></param>
        /// <param name="lc"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static string CreateDoc(DocInfoModel docInfo, LcsljdModel lc, out string errmsg)
        {
            string wjbh = string.Empty;
            errmsg = string.Empty;
            ParamConvert paramConvert = new ParamConvert();
            paramConvert.Basic(false);
            paramConvert.Add("p_ajbh", docInfo.AJBH); // 案件编号
            paramConvert.Add("p_dwbm", docInfo.DWBM); // 单位编码
            paramConvert.Add("p_fwjbh", docInfo.FWJBH); // 父文件编号
            paramConvert.Add("p_wjmc", docInfo.WJMC); // 文件名称
            paramConvert.Add("p_wjlj", docInfo.WJLJ); // 文件路径
            paramConvert.Add("p_wjwh", docInfo.WJWH); // 文书文号
            paramConvert.Add("p_wjlx", docInfo.WJLX); // 文件类型
            paramConvert.Add("p_wsmbbh", docInfo.DocTemplate.WSMBBH); // 文件类型
            paramConvert.Add("p_lcmbbm", lc.LCMBBM); // 文件类型
            paramConvert.Add("p_lcslbh", lc.LCSLBH); // 文件类型
            paramConvert.Add("p_lcjdbm", lc.LCJDBM); // 文件类型
            paramConvert.Add("p_jdzxzgh", lc.JDZXZGH); // 文件类型
            paramConvert.Add("p_jdzxz", lc.JDZXZ); // 文件类型
            paramConvert.Add("p_wjbh", DBNull.Value); // 文件类型
            try
            {
                paramConvert.DoExecuteNonQuery("PKG_DOC_MANAGE.proc_newdoc");
                wjbh = Convert.ToString(paramConvert.GetValueByKey("p_wjbh"));
                errmsg = Convert.ToString(paramConvert.GetValueByKey("p_errmsg"));
            }
            catch (Exception e)
            {
                errmsg += e.Message;
            }
            return wjbh;
        }

        /// <summary>
        /// 创建通知书
        /// </summary>
        /// <param name="docInfo"></param>
        /// <param name="lc"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static List<string> AddFile(List<DocInfoModel> docInfoList, string ajbh, string fwjbh, out string errmsg)
        {
            List<string> wsbhList = new List<string>();
            string strErr = string.Empty;
            errmsg = string.Empty;
            ParamConvert pc = new ParamConvert();
            try
            {
                pc.DoExecuteWork(() =>
                {
                    foreach (DocInfoModel docInfo in docInfoList)
                    {
                        pc.Basic(false);
                        pc.Add("p_ajbh", ajbh); // 案件编号
                        pc.Add("p_dwbm", docInfo.DWBM); // 单位编码
                        pc.Add("p_fwjbh", fwjbh); // 父文件编号
                        pc.Add("p_wjmc", docInfo.WJMC); // 文件名称
                        pc.Add("p_wjlj", docInfo.WJLJ); // 文件路径           
                        pc.Add("p_wjlx", docInfo.WJLX); // 文件类型           
                        pc.Add("p_wjbh", DBNull.Value); // 文件类型
                        pc.DoWork("PKG_DOC_MANAGE.proc_addfile");
                        string wjbh = Convert.ToString(pc.GetValueByKey("p_wjbh"));
                        strErr = Convert.ToString(pc.GetValueByKey("p_errmsg"));
                        if (!string.IsNullOrEmpty(strErr))
                        {
                            throw new Exception(strErr);
                        }
                        wsbhList.Add(wjbh);
                    }
                });
            }
            catch (Exception ex)
            {
                errmsg = ex.Message;
                wsbhList = null;
            }
            return wsbhList;
        }

        /// <summary>
        /// 获取通知书或反馈书
        /// </summary>
        /// <param name="ajbh">案件编号</param>
        /// <param name="tzfk">通知/反馈</param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static DataTable GetDocInfo(string ajbh, string tzfk, out string errmsg)
        {
            errmsg = string.Empty;
            DataTable dt = null;
            ParamConvert pc = new ParamConvert();
            pc.Basic(true);
            pc.Add("p_ajbh", ajbh);
            pc.Add("p_tzfk", tzfk);
            try
            {
                dt = pc.DoExecuteDataTable("PKG_DOC_MANAGE.proc_get_docinfo");
                errmsg += Convert.ToString(pc.GetValueByKey("p_errmsg"));
            }
            catch (Exception e)
            {
                errmsg += e.Message;
            }
            return dt;
        }

        public static DataTable GetFileList(string ajbh, string fwjbh, out string errmsg)
        {
            errmsg = string.Empty;
            DataTable dt = null;
            ParamConvert pc = new ParamConvert();
            pc.Basic(true);
            pc.Add("p_ajbh", ajbh);
            pc.Add("p_fwsbh", fwjbh);
            try
            {
                dt = pc.DoExecuteDataTable("PKG_DOC_MANAGE.proc_get_filelist");
                errmsg += Convert.ToString(pc.GetValueByKey("p_errmsg"));
            }
            catch (Exception e)
            {
                errmsg += e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 获取通知书或反馈书
        /// </summary>
        /// <param name="ajbh">案件编号</param>
        /// <param name="tzfk">通知/反馈</param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static DataTable GetTagData(string dwbm, string ajbh, string wsmbbh, out string errmsg)
        {
            errmsg = string.Empty;
            DataTable dt = null;
            ParamConvert pc = new ParamConvert();
            pc.Basic(true);
            pc.Add("p_dwbm", dwbm);
            pc.Add("p_ajbh", ajbh);
            pc.Add("p_wsmbbh", wsmbbh);
            try
            {
                dt = pc.DoExecuteDataTable("PKG_DOC_TAGFUNC.proc_gettagdata");
                errmsg += Convert.ToString(pc.GetValueByKey("p_errmsg"));
            }
            catch (Exception e)
            {
                errmsg += e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 获取通知书或反馈书
        /// </summary>
        /// <param name="ajbh">案件编号</param>
        /// <param name="tzfk">通知/反馈</param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static string GetWH(string dwbm, string bmbm, string wsmbbh, string wsjc, out string errmsg)
        {
            errmsg = string.Empty;
            string wh = string.Empty;
            ParamConvert pc = new ParamConvert();
            pc.Basic(false);
            pc.Add("p_dwbm", dwbm);
            pc.Add("p_bmbm", bmbm);
            pc.Add("p_wsmbbh", wsmbbh);
            pc.Add("p_wsjc", wsjc);
            pc.Add("p_wh", DBNull.Value);
            try
            {
                pc.DoExecuteNonQuery("pkg_no_manage.proc_get_wh");
                wh = Convert.ToString(pc.GetValueByKey("p_wh"));
                errmsg = Convert.ToString(pc.GetValueByKey("p_errmsg"));
            }
            catch (Exception e)
            {
                errmsg += e.Message;
            }
            return wh;
        }

        public static void DeleteDoc(string ajbh, string wjbh, out string errmsg)
        {
            errmsg = string.Empty;
            ParamConvert paramConvert = new ParamConvert();
            paramConvert.Basic(false);
            paramConvert.Add("p_ajbh", ajbh); // 案件编号
            paramConvert.Add("p_wjbh", wjbh); // 文件编号            
            try
            {
                paramConvert.DoExecuteNonQuery("PKG_DOC_MANAGE.proc_del_doc");
                errmsg = Convert.ToString(paramConvert.GetValueByKey("p_errmsg"));
            }
            catch (Exception e)
            {
                errmsg += e.Message;
            }
        }
    }
}
