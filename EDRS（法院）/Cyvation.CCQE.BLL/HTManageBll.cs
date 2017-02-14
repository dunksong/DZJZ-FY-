using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Cyvation.CCQE.Model;
using Cyvation.CCQE.Common;

namespace Cyvation.CCQE.BLL
{
    public class HTManageBll
    {
        /// <summary>
        /// 获取考评指标分类
        /// </summary>
        /// <param name="strErr"></param>
        /// <returns></returns>
        public static DataTable GetKpzbfl(out string strErr)
        {
            strErr = string.Empty;
            DataTable dt = new DataTable();
            ParamConvert pc = new ParamConvert();
            pc.Basic(true);
            try
            {
                dt = pc.DoExecuteDataTable("pkg_ht_manage.proc_get_kpzbfl");
                strErr = pc.Errmsg;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
            }
            return dt;
        }

        /// <summary>
        /// 根据分类获取考评指标
        /// </summary>
        /// <param name="flbh"></param>
        /// <param name="strErr"></param>
        /// <returns></returns>
        public static DataTable GetKpzbByFl(string flbh, out string strErr)
        {
            strErr = string.Empty;
            DataTable dt = new DataTable();
            ParamConvert pc = new ParamConvert();
            pc.Basic(true);
            pc.Add("p_flbh", flbh);
            try
            {
                dt = pc.DoExecuteDataTable("pkg_ht_manage.proc_get_kpzbbyfl");
                strErr = pc.Errmsg;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
            }
            return dt;
        }

        /// <summary>
        /// 添加/修改考评指标分类
        /// </summary>
        /// <param name="zbfl"></param>
        /// <param name="strErr"></param>
        public static void AddOrUpdateKpzbfl(KpzbflModel zbfl, out string strErr)
        {
            strErr = string.Empty;
            ParamConvert pc = new ParamConvert();
            pc.Basic(false);
            pc.ConvertParam<KpzbflModel>(zbfl);
            try
            {
                pc.DoExecuteNonQuery("PKG_FLDM_MANAGE.proc_add_kpzbfl");
                zbfl.FLBH = pc.GetValueByKey("p_flbh").TryConvertToString();
                strErr = pc.Errmsg;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
            }
        }

        /// <summary>
        /// 根据分类编号获取指标分类
        /// </summary>
        /// <param name="flbh"></param>
        /// <param name="strErr"></param>
        /// <returns></returns>
        public static KpzbflModel GetZbflByBh(string flbh, out string strErr)
        {
            strErr = string.Empty;
            KpzbflModel kpzbfl = new KpzbflModel();
            ParamConvert pc = new ParamConvert();
            pc.Basic(true);
            pc.Add("p_flbh", flbh);
            try
            {
                kpzbfl = pc.DoExecuteSprocAccessor<KpzbflModel>("pkg_fldm_manage.proc_get_kpzbflbybh").FirstOrDefault();
                strErr = pc.Errmsg;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
            }
            return kpzbfl;
        }

        /// <summary>
        /// 删除指标分类
        /// </summary>
        /// <param name="flbh"></param>
        /// <param name="strErr"></param>
        public static void DelZbflByBh(string flbh, out string strErr)
        {
            strErr = string.Empty;
            ParamConvert pc = new ParamConvert();
            pc.Basic(false);
            pc.Add("p_flbh", flbh);
            try
            {
                pc.DoExecuteNonQuery("pkg_fldm_manage.proc_delete_zbflbybh");
                strErr = pc.Errmsg;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
            }
        }

        /// <summary>
        /// 新增/修改考评指标
        /// </summary>
        /// <param name="kpzb"></param>
        /// <param name="strErr"></param>
        public static void AddOrUpdateKpzb(KpzbModel kpzb, out string strErr)
        {
            strErr = string.Empty;
            ParamConvert pc = new ParamConvert();
            pc.ConvertParam<KpzbModel>(kpzb, false);
            try
            {
                pc.DoExecuteNonQuery("pkg_fldm_manage.proc_add_kpzb");
                kpzb.ZBBH = Convert.ToString(pc.GetValueByKey("p_zbbh"));
                strErr = pc.Errmsg;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
            }
        }

        /// <summary>
        /// 根据考评指标编号获取考评指标
        /// </summary>
        /// <param name="zbbh"></param>
        /// <param name="strErr"></param>
        /// <returns></returns>
        public static KpzbModel GetKpzbByBh(string zbbh, out string strErr)
        {
            strErr = string.Empty;
            KpzbModel kpzb = new KpzbModel();
            ParamConvert pc = new ParamConvert();
            pc.Basic(true);
            pc.Add("p_zbbh", zbbh);
            try
            {
                kpzb = pc.DoExecuteSprocAccessor<KpzbModel>("pkg_fldm_manage.proc_get_zbinfobyzbbh").FirstOrDefault();
                strErr = pc.Errmsg;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
            }
            return kpzb;
        }

        /// <summary>
        /// 根据指标编号删除考评指标
        /// </summary>
        /// <param name="zbbh"></param>
        /// <param name="strErr"></param>
        public static void DelKpzbByBh(string zbbh, out string strErr)
        {
            strErr = string.Empty;
            ParamConvert pc = new ParamConvert();
            pc.Basic(false);
            pc.Add("p_zbbh", zbbh);
            try
            {
                pc.DoExecuteNonQuery("pkg_fldm_manage.proc_delete_zbinfobyzbbh");
                strErr = pc.Errmsg;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
            }
        }

        /// <summary>
        /// 查询类别
        /// </summary>
        public static DataTable GetSslb(out string errmsg)
        {
            DataTable dt = null;
            errmsg = string.Empty;
            ParamConvert pc = new ParamConvert();
            pc.Basic(false);
            try
            {
                dt = pc.DoExecuteDataTable("pkg_fldm_manage.proc_get_sslb");
                errmsg = Convert.ToString(pc.GetValueByKey("p_errmsg"));
            }
            catch (Exception e)
            {
                errmsg += e.Message;
            }

            return dt;
        }

        /// <summary>
        /// 查询一条数据
        /// </summary>
        public static DataTable GetOneSj(string dm, out string errmsg)
        {
            DataTable dt = null;
            errmsg = string.Empty;
            ParamConvert pc = new ParamConvert();
            pc.Basic(false);
            pc.Add("p_dm", dm);
            try
            {
                dt = pc.DoExecuteDataTable("pkg_fldm_manage.proc_get_onesj");
                errmsg = Convert.ToString(pc.GetValueByKey("p_errmsg"));
            }
            catch (Exception e)
            {
                errmsg += e.Message;
            }

            return dt;
        }

        /// <summary>
        /// 根据所属类别代码获取数据字典
        /// </summary>
        public static DataTable GetSjzdBySslbdm(string sslbdm, out string errmsg)
        {
            DataTable dt = null;
            errmsg = string.Empty;
            ParamConvert pc = new ParamConvert();
            pc.Basic(true);
            pc.Add("p_sslbdm", sslbdm);
            try
            {
                dt = pc.DoExecuteDataTable("pkg_fldm_manage.proc_get_sjzdbysslbdm");
                errmsg = Convert.ToString(pc.GetValueByKey("p_errmsg"));
            }
            catch (Exception e)
            {
                errmsg += e.Message;
            }

            return dt;
        }

        /// <summary>
        /// 从数据字典中删除数据
        /// </summary>
        public static void DelSjzd(string dm, out string errmsg)
        {
            errmsg = string.Empty;
            ParamConvert pc = new ParamConvert();
            pc.Basic(false);
            pc.Add("p_dm",dm);
            try
            {
                pc.DoExecuteNonQuery("pkg_fldm_manage.proc_delete_sjzd");
                errmsg = pc.Errmsg;
            }
            catch (Exception ex)
            {
                errmsg = ex.Message;
            }
        }

        /// <summary>
        /// 从数据字典中删除数据
        /// </summary>
        public static void MergeSjzd(SjzdModel item, out string errmsg)
        {
            errmsg = string.Empty;
            ParamConvert paramConvert = new ParamConvert();
            paramConvert.Basic(false);
            paramConvert.Add("p_dm", item.DM); // 编码
            paramConvert.Add("p_mc", item.MC); // 名称
            paramConvert.Add("p_fdm", item.FDM); // 父编码，为空时为根节点。
            paramConvert.Add("p_sslbdm", item.SSLBDM); // 所属类别代码
            paramConvert.Add("p_sslbmc", item.SSLBMC); // 所属类别名称
            paramConvert.Add("p_sm", item.SM); // 说明
            paramConvert.Add("p_xh", item.XH); // 序号
            try
            {
                paramConvert.DoExecuteNonQuery("pkg_fldm_manage.proc_merge_sjzd");
                errmsg = paramConvert.Errmsg;
            }
            catch (Exception ex)
            {
                errmsg = ex.Message;
            }
        }
    }
}
