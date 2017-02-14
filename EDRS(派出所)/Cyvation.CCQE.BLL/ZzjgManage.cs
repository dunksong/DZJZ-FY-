using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Cyvation.CCQE.Model;

namespace Cyvation.CCQE.BLL
{
    public class ZzjgManage
    {

        /// <summary>
        /// 根据人员工号，获取人员对于功能
        /// </summary>
        public static DataTable GetGnByRy(string dwbm, string rygh, out string err)
        {
            err = string.Empty;
            DataTable dt = new DataTable();
            ParamConvert pc = new ParamConvert();
            pc.Basic(true);
            pc.Add("p_dwbm", dwbm);
            pc.Add("p_rygh", rygh);
            try
            {
                dt = pc.DoExecuteDataTable("pkg_zzjg_manage.proc_get_gnbyry");
                err = Convert.ToString(pc.GetValueByKey("p_errmsg"));
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }
            return dt;
        }

        /// <summary>
        /// 获取该单位下面的部门信息
        /// </summary>
        public static DataTable GetDwBmJsbyDwbm(string dwbm, out string err)
        {
            err = string.Empty;
            DataTable dt = null;
            ParamConvert pc = new ParamConvert();
            pc.Basic(true);
            pc.Add("p_dwbm", dwbm);
            try
            {
                dt = pc.DoExecuteDataTable("pkg_zzjg_manage.proc_get_dwbmjs");
                err = Convert.ToString(pc.GetValueByKey("p_errmsg"));
            }
            catch (Exception e)
            {
                err += e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 获取该单位下的部门角色信息
        /// </summary>
        public static DataTable GetDwBmJsInfoByDwBm(string dwbm, out string err)
        {
            err = string.Empty;
            DataTable dt = null;
            ParamConvert pc = new ParamConvert();
            pc.Basic(true);
            pc.Add("p_dwbm", dwbm);
            try
            {
                dt = pc.DoExecuteDataTable("pkg_zzjg_manage.proc_get_dwbmjs");
                err = Convert.ToString(pc.GetValueByKey("p_errmsg"));
            }
            catch (Exception e)
            {

                err += e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 获取该单位下的所有部门名称
        /// </summary>
        public static DataTable GetBmInfoByDwBm(string dwbm, out string err)
        {
            err = string.Empty;
            DataTable dt = null;
            ParamConvert pc = new ParamConvert();
            pc.Basic(true);
            pc.Add("p_dwbm", dwbm);
            try
            {
                dt = pc.DoExecuteDataTable("pkg_zzjg_manage.proc_get_bminfobydwbm");
                err = Convert.ToString(pc.GetValueByKey("p_errmsg"));
            }
            catch (Exception e)
            {
                err += e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 获取人员信息
        /// </summary>
        public static DataTable GetRyInfo(ParamRyFilter param, out string err)
        {
            DataTable dt = null;
            err = string.Empty;
            ParamConvert pc = new ParamConvert();
            pc.ConvertParam<ParamRyFilter>(param);
            try
            {
                dt = pc.DoExecuteDataTable("pkg_zzjg_manage.proc_get_ryinfobydwbm");
                param.Count = Convert.ToInt32(pc.GetValueByKey("p_count"));
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 获取单位及其子单位
        /// </summary>
        /// <param name="dwbm"></param>
        /// <param name="strErr"></param>
        /// <returns></returns>
        public static DataTable GetDwAndChildren(string dwbm, out string strErr)
        {
            strErr = string.Empty;
            DataTable dt = new DataTable();
            ParamConvert pc = new ParamConvert();
            pc.Basic(true);
            pc.Add("p_dwbm", dwbm);
            try
            {
                dt = pc.DoExecuteDataTable("pkg_zzjg_manage.proc_get_zdw");
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
            }
            return dt;
        }

        /// <summary>
        /// 获取侦办单位
        /// </summary>
        /// <param name="dwbm"></param>
        /// <param name="strErr"></param>
        /// <returns></returns>
        public static DataTable GetZbdwByCbdw(string dwbm, out string strErr)
        {
            strErr = string.Empty;
            DataTable dt = new DataTable();
            ParamConvert pc = new ParamConvert();
            pc.Basic(true);
            pc.Add("p_dwbm", dwbm);
            try
            {
                dt = pc.DoExecuteDataTable("pkg_zzjg_manage.proc_get_gxzdw");
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
            }
            return dt;
        }


        /// <summary>
        /// 获取单位信息(单位名称、单位简称、单位级别)
        /// </summary>
        public static DataTable GetDwInfo(string dwbm, out string err)
        {
            DataTable dt = null;
            err = string.Empty;
            ParamConvert pc = new ParamConvert();
            pc.Basic(false);
            pc.Add("p_dwbm", dwbm);
            try
            {
                dt = pc.DoExecuteDataTable("pkg_zzjg_manage.proc_get_dwinfo");
                err = Convert.ToString(pc.GetValueByKey("p_errmsg"));
            }
            catch (Exception e)
            {

                err += e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 添加部门
        /// </summary>
        public static bool AddBmInfo(string dwbm, string bmbm, string bmmc, string bmjc, int bmxh, string bz, string fbmbm, out string errmsg)
        {
            bool isSuc = false;
            errmsg = string.Empty;
            ParamConvert pc = new ParamConvert();
            pc.Basic(false);
            pc.Add("p_dwbm", dwbm);
            pc.Add("p_bmbm", bmbm);
            pc.Add("p_bmmc", bmmc);
            pc.Add("p_bmjc", bmjc);
            pc.Add("p_bmxh", bmxh);
            pc.Add("p_bz", bz);
            pc.Add("p_fbmbm", fbmbm);
            try
            {
                //pc.DoExecuteDataTable("proc_add_bminfo");
                pc.DoExecuteNonQuery("pkg_zzjg_manage.proc_add_bminfo");
                errmsg = Convert.ToString(pc.GetValueByKey("p_errmsg"));
            }
            catch (Exception e)
            {
                errmsg += e.Message;
            }
            if (string.IsNullOrEmpty(errmsg))
            {
                isSuc = true;
            }
            else
            {
                isSuc = false;
            }
            return isSuc;
        }


        /// <summary>
        /// 通过单位编码、部门编码获取角色信息
        /// </summary>
        public static DataTable GetJsInfoByDWBM(string dwbm, string bmbm, out string errmsg)
        {
            errmsg = string.Empty;
            DataTable dt = null;
            ParamConvert pc = new ParamConvert();
            pc.Basic(false);
            pc.Add("p_dwbm", dwbm);
            pc.Add("p_bmbm", bmbm);
            try
            {
                dt = pc.DoExecuteDataTable("pkg_zzjg_manage.proc_get_jsbydwbm");
                errmsg = Convert.ToString(pc.GetValueByKey("p_errmsg"));
            }
            catch (Exception e)
            {
                errmsg += e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 添加角色信息
        /// </summary>
        public static bool AddJsInfo(string dwbm, string bmbm, string jsbm, string jsmc, int jsxh,string qxzt, out string errmsg)
        {
            errmsg = string.Empty;
            bool isSuc = false;
            ParamConvert pc = new ParamConvert();
            pc.Basic(false);
            pc.Add("p_dwbm", dwbm);
            pc.Add("p_bmbm", bmbm);
            pc.Add("p_jsmc", jsmc);
            pc.Add("p_jsxh", jsxh);
            pc.Add("p_jsbm", jsbm);
            pc.Add("p_qxzt", qxzt);
            try
            {
                //pc.DoExecuteDataTable("proc_add_jsinfo");
                isSuc = pc.DoExecuteNonQuery("pkg_zzjg_manage.proc_add_jsinfo");
                errmsg = Convert.ToString(pc.GetValueByKey("p_errmsg"));
            }
            catch (Exception e)
            {
                errmsg += e.Message;
            }

            return isSuc;
        }

        /// <summary>
        /// 获取角色权限信息
        /// </summary>
        public static DataTable GetJsQx(string dwbm, string bmbm, string jsbm, int pagesize, int pageindex, out int count, out string errmsg)
        {
            count = 0;
            errmsg = string.Empty;
            DataTable dt = null;
            ParamConvert pc = new ParamConvert();
            pc.Basic(false);
            pc.Add("p_dwbm", dwbm);
            pc.Add("p_bmbm", bmbm);
            pc.Add("p_jsbm", jsbm);
            pc.Add("p_pagesize", pagesize);
            pc.Add("p_pageindex", pageindex);
            pc.Add("p_count", count);
            try
            {
                dt = pc.DoExecuteDataTable("pkg_zzjg_manage.proc_get_jsqx");
                errmsg = Convert.ToString(pc.GetValueByKey("p_errmsg"));
                count = Convert.ToInt32(pc.GetValueByKey("p_count").ToString());
            }
            catch (Exception e)
            {
                errmsg += e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 删除角色信息
        /// </summary>
        public static bool DeleteJsInfo(string dwbm, string bmbm, string jsbm, out string errmsg)
        {
            errmsg = string.Empty;
            bool isSuc = false;
            ParamConvert pc = new ParamConvert();
            pc.Basic(false);
            pc.Add("p_dwbm", dwbm);
            pc.Add("p_bmbm", bmbm);
            pc.Add("p_jsbm", jsbm);
            try
            {
                isSuc = pc.DoExecuteNonQuery("pkg_zzjg_manage.proc_delete_jsinfo");
                errmsg = Convert.ToString(pc.GetValueByKey("p_errmsg"));
            }
            catch (Exception e)
            {
                errmsg += e.Message;
            }
            return isSuc;
        }

        /// <summary>
        /// 获取部门序号
        /// </summary>
        public static DataTable GetBmInfo(string dwbm, string bmbm, out string errmsg)
        {
            DataTable dt = null;
            errmsg = string.Empty;
            ParamConvert pc = new ParamConvert();
            pc.Basic(false);
            pc.Add("p_dwbm", dwbm);
            pc.Add("p_bmbm", bmbm);
            try
            {
                dt = pc.DoExecuteDataTable("pkg_zzjg_manage.proc_get_bmInfo");
                errmsg = Convert.ToString(pc.GetValueByKey("p_errmsg"));
            }
            catch (Exception e)
            {
                errmsg += e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 获取角色序号
        /// </summary>
        public static DataTable GetJsxh(string dwbm, string bmbm, string jsbm, out string errmsg)
        {
            errmsg = string.Empty;
            int jsxh = -1;
            ParamConvert pc = new ParamConvert();
            pc.Basic(false);
            pc.Add("p_dwbm", dwbm);
            pc.Add("p_bmbm", bmbm);
            pc.Add("p_jsbm", jsbm);
            try
            {
                DataTable dt = pc.DoExecuteDataTable("pkg_zzjg_manage.proc_get_jsxh");
                errmsg = Convert.ToString(pc.GetValueByKey("p_errmsg"));
                //if (dt != null && string.IsNullOrEmpty(errmsg))
                //{
                //    jsxh = Convert.ToInt32(dt.Rows[0][0].ToString());
                //}
                return dt;
            }
            catch (Exception ex)
            {
                errmsg += ex.Message;
            }
            return null;
        }

        /// <summary>
        /// 获取功能分类
        /// </summary>
        public static DataTable GetGnfl(string dwbm, out string errmsg)
        {
            errmsg = string.Empty;
            DataTable dt = null;
            ParamConvert pc = new ParamConvert();
            pc.Basic(false);
            pc.Add("p_dwbm", dwbm);
            try
            {
                dt = pc.DoExecuteDataTable("pkg_zzjg_manage.proc_get_gnfl");
                errmsg += Convert.ToString(pc.GetValueByKey("p_errmsg"));
            }
            catch (Exception e)
            {
                errmsg += e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 获取功能名称，通过功能分类
        /// </summary>
        public static DataTable GetGnmcByGnfl(string dwbm, string gnfl, out string errmsg)
        {
            errmsg = string.Empty;
            DataTable dt = null;
            ParamConvert pc = new ParamConvert();
            pc.Basic(false);
            pc.Add("p_dwbm", dwbm);
            pc.Add("p_gnfl", gnfl);
            try
            {
                dt = pc.DoExecuteDataTable("pkg_zzjg_manage.proc_get_gnmcbyfbm");
                errmsg += Convert.ToString(pc.GetValueByKey("p_errmsg"));
            }
            catch (Exception e)
            {
                errmsg += e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 添加角色功能权限
        /// </summary>
        public static bool AddJsGnQx(string dwbm, string bmbm, string jsbm, string gnbm, string bz, out string errmsg)
        {
            bool isSuc = false;
            errmsg = string.Empty;
            ParamConvert pc = new ParamConvert();
            pc.Basic(false);
            pc.Add("p_dwbm", dwbm);
            pc.Add("p_bmbm", bmbm);
            pc.Add("p_jsbm", jsbm);
            pc.Add("p_gnbm", gnbm);
            pc.Add("p_bz", bz);
            try
            {
                isSuc = pc.DoExecuteNonQuery("pkg_zzjg_manage.proc_add_JSGnQx");
                errmsg += Convert.ToString(pc.GetValueByKey("p_errmsg"));
            }
            catch (Exception e)
            {
                errmsg += e.Message;
            }
            return isSuc;
        }

        /// <summary>
        /// 查询该单位下的功能权限
        /// </summary>
        public static DataTable QueryGnQx(string dwbm, string gnbm, string gnmc, string sslb, int pagesize, int pageindex, out int count, out string errmsg)
        {
            errmsg = string.Empty;
            count = 0;
            DataTable dt = null;
            ParamConvert pc = new ParamConvert();
            pc.Basic(false);
            pc.Add("p_dwbm", dwbm);
            pc.Add("p_gnbm", gnbm);
            pc.Add("p_gnmc", gnmc);
            pc.Add("p_sslb", sslb);
            pc.Add("p_pagesize", pagesize);
            pc.Add("p_pageindex", pageindex);
            pc.Add("p_count", count);
            try
            {
                dt = pc.DoExecuteDataTable("pkg_zzjg_manage.proc_get_gnqx");
                errmsg += Convert.ToString(pc.GetValueByKey("p_errmsg"));
                count = Convert.ToInt32(pc.GetValueByKey("p_count"));
            }
            catch (Exception e)
            {
                errmsg += e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 添加功能权限
        /// </summary>
        public static bool AddGnQx(string dwbm, int isExistedFlb, string gnfl, string gnbm, string gnmc, string gnct, string gnxsmc,
                                int gnxh, string gnsm, string cscs,string gjy,string sy,string sjy,string qy, out string errmsg)
        {
            bool isSuc = false;
            errmsg = string.Empty;
            ParamConvert pc = new ParamConvert();
            pc.Basic(false);
            pc.Add("p_dwbm", dwbm);
            pc.Add("p_isexistedflb", isExistedFlb);
            pc.Add("p_gnfl", gnfl);
            pc.Add("p_gnbm", gnbm);
            pc.Add("p_gnmc", gnmc);
            pc.Add("p_gnct", gnct);
            pc.Add("p_gnxsmc", gnxsmc);
            pc.Add("p_gnxh", gnxh);
            pc.Add("p_gnsm", gnsm);
            pc.Add("p_cscs", cscs);
            //pc.Add("p_sfgjysy", gjy);
            //pc.Add("p_sfsysy", sy);
            //pc.Add("p_sfsjysy", sjy);
            //pc.Add("p_sfqysy", qy);
            try
            {
                isSuc = pc.DoExecuteNonQuery("pkg_zzjg_manage.proc_add_gnqx");
                errmsg += Convert.ToString(pc.GetValueByKey("p_errmsg"));
            }
            catch (Exception e)
            {
                errmsg += e.Message;
            }
            return isSuc;
        }

        /// <summary>
        /// 删除功能权限
        /// </summary>
        public static bool DeleteGnQx(string dwbm, string gnbm, out string errmsg)
        {
            bool isSuc = false;
            errmsg = string.Empty;
            ParamConvert pc = new ParamConvert();
            pc.Basic(false);
            pc.Add("p_dwbm", dwbm);
            pc.Add("p_gnbm", gnbm);
            try
            {
                isSuc = pc.DoExecuteNonQuery("pkg_zzjg_manage.proc_delete_GnQx");
                errmsg += Convert.ToString(pc.GetValueByKey("p_errmsg"));
            }
            catch (Exception e)
            {
                errmsg += e.Message;
            }
            return isSuc;
        }

        /// <summary>
        /// 移除岗位
        /// </summary>
        public static bool RemoveJob(string dwbm, string bmbm, string jsbm, string gh, out string errmsg)
        {
            bool isSuc = false;
            errmsg = string.Empty;
            ParamConvert pc = new ParamConvert();
            pc.Basic(false);
            pc.Add("p_dwbm", dwbm);
            pc.Add("p_bmbm", bmbm);
            pc.Add("p_jsbm", jsbm);
            pc.Add("p_gh", gh);
            try
            {
                isSuc = pc.DoExecuteNonQuery("pkg_zzjg_manage.proc_remove_job");
                errmsg += Convert.ToString(pc.GetValueByKey("p_errmsg"));
            }
            catch (Exception e)
            {
                errmsg += e.Message;
            }
            return isSuc;
        }

        /// <summary>
        /// 通过工号获取人员信息
        /// </summary>
        public static DataTable GetRyInfoByGh(string dwbm, string gh, out string errmsg)
        {
            errmsg = string.Empty;
            DataTable dt = null;
            ParamConvert pc = new ParamConvert();
            pc.Basic(false);
            pc.Add("p_dwbm", dwbm);
            pc.Add("p_gh", gh);
            try
            {
                dt = pc.DoExecuteDataTable("pkg_zzjg_manage.proc_get_ryinfobygh");
                errmsg += Convert.ToString(pc.GetValueByKey("p_errmsg"));
            }
            catch (Exception e)
            {
                errmsg += e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 得到该单位下的所有单位
        /// </summary>
        public static DataTable GetAllDwByDwbm(string dwbm, out string errmsg)
        {
            errmsg = string.Empty;
            DataTable dt = null;
            ParamConvert pc = new ParamConvert();
            pc.Basic(false);
            pc.Add("p_dwbm", dwbm);
            try
            {
                dt = pc.DoExecuteDataTable("pkg_zzjg_manage.proc_get_alldwbydwbm");
                errmsg += Convert.ToString(pc.GetValueByKey("p_errmsg"));
            }
            catch (Exception e)
            {
                errmsg += e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 删除角色功能权限
        /// </summary>
        public static bool DeleteJsGnQx(string dwbm, string jsbm, string gnbm, out string errmsg)
        {
            errmsg = string.Empty;
            bool isSuc = false;
            ParamConvert pc = new ParamConvert();
            pc.Basic(false);
            pc.Add("p_dwbm", dwbm);
            pc.Add("p_jsbm", jsbm);
            pc.Add("p_gnbm", gnbm);
            try
            {
                isSuc = pc.DoExecuteNonQuery("pkg_zzjg_manage.proc_delete_jsgnqx");
                errmsg += Convert.ToString(pc.GetValueByKey("p_errmsg"));
            }
            catch (Exception e)
            {
                errmsg += e.Message;
            }
            return isSuc;
        }

        public static void UpdatePassWord(string dwbm, string gh, string oldPwd, string newPwd, out string errmsg)
        {
            errmsg = string.Empty;
            ParamConvert pc = new ParamConvert();
            pc.Basic(false);
            pc.Add("p_dwbm", dwbm);
            pc.Add("p_gh", gh);
            pc.Add("p_oldpwd", oldPwd);
            pc.Add("p_newpwd", newPwd);
            try
            {
                pc.DoExecuteNonQuery("pkg_zzjg_manage.proc_update_psword");
                errmsg += Convert.ToString(pc.GetValueByKey("p_errmsg"));
            }
            catch (Exception e)
            {
                errmsg += e.Message;
            }
        }

        #region 人员信息操作相关

        /// <summary>
        /// 获取人员信息
        /// </summary>
        public static DataTable GetRyList(string dwbm, string gh, string strWhere, int pageSize, int pageIndex, out int count, out string err)
        {
            DataTable dt = null;
            count = 0;
            err = string.Empty;
            ParamConvert pc = new ParamConvert();
            pc.Basic(false);
            pc.Add("p_dwbm", dwbm);
            pc.Add("p_gh", gh);
            pc.Add("p_where", strWhere);
            pc.Add("p_pagesize", pageSize);
            pc.Add("p_pageindex", pageIndex);
            pc.Add("p_count", count);
            try
            {
                dt = pc.DoExecuteDataTable("pkg_zzjg_manage.proc_get_ryinfo");
                count = Convert.ToInt32(pc.GetValueByKey("p_count"));
                err = Convert.ToString(pc.GetValueByKey("p_errmsg"));
            }
            catch (Exception e)
            {
                err += e.Message;
            }

            return dt;
        }

        /// <summary>
        /// 添加人员信息
        /// </summary>
        public static bool AddRYInfo(RybmModel ryxx, out string errmsg)
        {
            bool isSuc = false;
            errmsg = string.Empty;
            ParamConvert pc = new ParamConvert();
            pc.Basic(false);
            pc.Add("p_dwbm", ryxx.DWBM);
            pc.Add("p_mc", ryxx.MC);
            pc.Add("p_dlbm", ryxx.DLBM);
            pc.Add("p_gzzh", ryxx.GZZH);
            pc.Add("p_xb", ryxx.XB);
            pc.Add("p_sflsry", ryxx.SFLSRY);
            pc.Add("p_yddhhm", ryxx.YDDHHM);
            pc.Add("p_dzyj", ryxx.DZYJ);
            pc.Add("p_CAID", ryxx.CAID);
            pc.Add("p_sftz", ryxx.SFTZ);
            pc.Add("p_zw", ryxx.ZW);
            try
            {
                isSuc = pc.DoExecuteNonQuery("pkg_zzjg_manage.proc_add_ryinfo");
                errmsg = Convert.ToString(pc.GetValueByKey("p_errmsg"));
            }
            catch (Exception e)
            {
                errmsg += e.Message;
            }
            return isSuc;
        }

        /// <summary>
        /// 修改人员信息
        /// </summary>
        public static bool UpdateRYInfo(RybmModel ryxx, out string errmsg)
        {
            bool isSuc = false;
            errmsg = string.Empty;
            ParamConvert pc = new ParamConvert();
            pc.Basic(false);
            pc.Add("p_dwbm", ryxx.DWBM);
            pc.Add("p_mc", ryxx.MC);
            pc.Add("p_dlbm", ryxx.DLBM);
            pc.Add("p_gzzh", ryxx.GZZH);
            pc.Add("p_xb", ryxx.XB);
            pc.Add("p_sflsry", ryxx.SFLSRY);
            pc.Add("p_yddhhm", ryxx.YDDHHM);
            pc.Add("p_dzyj", ryxx.DZYJ);
            pc.Add("p_CAID", ryxx.CAID);
            pc.Add("p_zw", ryxx.ZW);
            pc.Add("p_gh", ryxx.GH);
            pc.Add("p_sftz", ryxx.SFTZ);
            try
            {
                isSuc = pc.DoExecuteNonQuery("pkg_zzjg_manage.proc_update_ryinfo");
                errmsg = Convert.ToString(pc.GetValueByKey("p_errmsg"));
            }
            catch (Exception e)
            {
                errmsg += e.Message;
            }
            return isSuc;
        }

        /// <summary>
        /// 删除人员信息
        /// </summary>
        public static bool DeleteRYInfo(string dwbm, string ghj, out string errmsg)
        {
            bool isSuc = false;
            errmsg = string.Empty;
            ParamConvert pc = new ParamConvert();
            pc.Basic(false);
            pc.Add("p_dwbm", dwbm);
            pc.Add("p_ghj", ghj);
            try
            {
                isSuc = pc.DoExecuteNonQuery("pkg_zzjg_manage.proc_delete_ryinfo");
                errmsg = Convert.ToString(pc.GetValueByKey("p_errmsg"));
            }
            catch (Exception e)
            {
                errmsg += e.Message;
            }
            return isSuc;
        }

        /// <summary>
        /// 获取角色功能分配信息
        /// </summary>
        public static DataTable GetJsGnfp(JsgnfpModel jsfp, out string err)
        {
            DataTable dt = null;
            err = string.Empty;
            ParamConvert pc = new ParamConvert();
            pc.Basic(false);
            pc.Add("p_dwbm", jsfp.DWBM);
            pc.Add("p_jsbm", jsfp.JSBM);
            pc.Add("p_bmbm", jsfp.BMBM);
            try
            {
                dt = pc.DoExecuteDataTable("pkg_zzjg_manage.proc_get_gninfo");
                dt.Columns.Add("icon");
                foreach (DataRow dr in dt.Rows)
                {
                    if (string.IsNullOrEmpty(dr["FBM"].ToString()))
                        dr["icon"] = "picon";
                    else
                        dr["icon"] = "chicon";
                }
                err = Convert.ToString(pc.GetValueByKey("p_errmsg"));
            }
            catch (Exception e)
            {
                err += e.Message;
            }

            return dt;
        }

        /// <summary>
        /// 获取获取未分配人员信息
        /// </summary>
        public static DataTable GetWfpRyInfo(string dwbm, string gh, string xm, string jsbm, string bmbm, int pagesize, int pageindex, out int count, out string err)
        {
            DataTable dt = null;
            count = 0;
            err = string.Empty;
            ParamConvert pc = new ParamConvert();
            pc.Basic(false);
            pc.Add("p_dwbm", dwbm);
            pc.Add("p_gh", gh);
            pc.Add("p_xm", xm);
            pc.Add("p_jsbm", jsbm);
            pc.Add("p_bmbm", bmbm);
            pc.Add("p_pagesize", pagesize);
            pc.Add("p_pageindex", pageindex);
            pc.Add("p_count", count);
            try
            {
                dt = pc.DoExecuteDataTable("pkg_zzjg_manage.proc_get_wfpryinfo");
                count = Convert.ToInt32(pc.GetValueByKey("p_count"));
                err = Convert.ToString(pc.GetValueByKey("p_errmsg"));
            }
            catch (Exception e)
            {
                err += e.Message;
            }

            return dt;
        }

        /// <summary>
        /// 修改功能参数信息
        /// </summary>
        public static bool UpdateGNCS(JsgnfpModel gnfp, out string errmsg)
        {
            bool isSuc = false;
            errmsg = string.Empty;
            ParamConvert pc = new ParamConvert();
            pc.Basic(false);
            pc.Add("p_dwbm", gnfp.DWBM);
            pc.Add("p_jsbm", gnfp.JSBM);
            pc.Add("p_gnbm", gnfp.GNBM);
            pc.Add("p_bmbm", gnfp.BMBM);
            pc.Add("p_gncs", string.IsNullOrEmpty(gnfp.GNCS) ? (object)DBNull.Value : gnfp.GNCS);
            try
            {
                isSuc = pc.DoExecuteNonQuery("pkg_zzjg_manage.proc_update_gncs");
                errmsg = Convert.ToString(pc.GetValueByKey("p_errmsg"));
            }
            catch (Exception e)
            {
                errmsg += e.Message;
            }
            return isSuc;
        }

        /// <summary>
        /// 添加人员角色分配
        /// </summary>
        public static bool AddRYJSFP(JsgnfpModel jsfp, string ghj, out string errmsg)
        {
            bool isSuc = false;
            errmsg = string.Empty;
            ParamConvert pc = new ParamConvert();
            pc.Basic(false);
            pc.Add("p_dwbm", jsfp.DWBM);
            pc.Add("p_bmbm", jsfp.BMBM);
            pc.Add("p_jsbm", jsfp.JSBM);
            pc.Add("p_ghj", ghj);
            try
            {
                isSuc = pc.DoExecuteNonQuery("pkg_zzjg_manage.proc_add_ryjsfp");
                errmsg = Convert.ToString(pc.GetValueByKey("p_errmsg"));
            }
            catch (Exception e)
            {
                errmsg += e.Message;
            }
            return isSuc;
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        public static bool ResetPwd(string dwbm, string ghj, out string errmsg)
        {
            bool isSuc = false;
            errmsg = string.Empty;
            ParamConvert pc = new ParamConvert();
            pc.Basic(false);
            pc.Add("p_dwbm", dwbm);
            pc.Add("p_ghj", ghj);
            try
            {
                isSuc = pc.DoExecuteNonQuery("pkg_zzjg_manage.proc_update_mmcz");
                errmsg = Convert.ToString(pc.GetValueByKey("p_errmsg"));
            }
            catch (Exception e)
            {
                errmsg += e.Message;
            }
            return isSuc;
        }

        /// <summary>
        /// 获取该用户的角色信息
        /// </summary>
        public static DataTable GetYhJsInfo(JsgnfpModel js, out string err)
        {
            DataTable dt = null;
            err = string.Empty;
            ParamConvert pc = new ParamConvert();
            pc.Basic(false);
            pc.Add("p_dwbm", js.DWBM);
            pc.Add("p_gh", js.GH);
            try
            {
                dt = pc.DoExecuteDataTable("pkg_zzjg_manage.proc_get_ryjs");
                err = Convert.ToString(pc.GetValueByKey("p_errmsg"));
            }
            catch (Exception e)
            {
                err += e.Message;
            }

            return dt;
        }

        public static DataTable GetGxdwList(string dwbm, out string err)
        {
            DataTable dt = null;
            err = string.Empty;
            ParamConvert pc = new ParamConvert();
            pc.Basic(true);
            pc.Add("p_dwbm", dwbm);
            try
            {
                dt = pc.DoExecuteDataTable("pkg_zzjg_manage.proc_get_gxdw");
                err = Convert.ToString(pc.GetValueByKey("p_errmsg"));
            }
            catch (Exception e)
            {
                err += e.Message;
            }
            return dt;
        }

        #endregion
    }

}
