using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yy.IceBase.CommunicationBaseLib;
using System.Data;
using System.ComponentModel;
using Yy.SliceTransferInterface;
using Yy.IceBase.ClientCore;

namespace EDRS.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class IceServicePrx
    {
        private string downPrx;
        /// <summary>
        /// 路径
        /// </summary>
        public string DownPrx
        {
            get { return downPrx; }
            set { downPrx = value; }
        }

        private decimal sizeMax = 10240000;
        /// <summary>
        /// 消息内容大小
        /// </summary>
        public decimal SizeMax
        {
            get { return sizeMax; }
            set { sizeMax = value; }
        }

        #region 获取案件基本信息
        private Lazy<ZC57s.CaseInfoServ.DigitalDossier.ICInterface.DigitalDossierServicePrx> _digitalDossierServicePrx = null;

        private ZC57s.CaseInfoServ.DigitalDossier.ICInterface.DigitalDossierServicePrx GetDossierService()
        {
            var prx = (ZC57s.CaseInfoServ.DigitalDossier.ICInterface.DigitalDossierServicePrx)null;


            //   var downPrx = ConfigHelper.GetConfigString("UploadPrx");

            if (!string.IsNullOrEmpty(downPrx))
            {
                _digitalDossierServicePrx = new Lazy<ZC57s.CaseInfoServ.DigitalDossier.ICInterface.DigitalDossierServicePrx>(() =>
                {
                    Ice.Properties ps = Ice.Util.createProperties();

                    ps.setProperty("Ice.MessageSizeMax", sizeMax.ToString());
                    Ice.InitializationData ida = new Ice.InitializationData { properties = ps };
                    var com = Ice.Util.initialize(ida);

                    var objprx = com.stringToProxy(downPrx);
                    return ZC57s.CaseInfoServ.DigitalDossier.ICInterface.DigitalDossierServicePrxHelper.checkedCast(objprx);
                });
            }
            if (_digitalDossierServicePrx != null && _digitalDossierServicePrx.Value != null)
            {
                prx = _digitalDossierServicePrx.Value;
            }
            return prx;
        }
        #region 获取所属类别基本信息
        public List<ZC57s.CaseInfoServ.DigitalDossier.ICInterface.ClsSSLB> GetSSLBInfos(out string strError)
        {
            strError = "";
            var sslbList = new List<ZC57s.CaseInfoServ.DigitalDossier.ICInterface.ClsSSLB>();
            try
            {
                ZC57s.CaseInfoServ.DigitalDossier.ICInterface.DigitalDossierServicePrx prx = GetDossierService();
                sslbList = prx.GetSSLBInfos(out strError);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    strError = ex.InnerException.Message.ToString();
                else
                    strError = ex.Message.ToString();
                LogHelper.LogError(null, "Ice", strError, "GetAjjbxx", "IceServicePrx");
            }
            return sslbList;
        }
        #endregion
        #region 查询案件基本信息
        /// <summary>
        /// 查询案件基本信息
        /// </summary>
        /// <param name="bmsah">部门受案号</param>
        /// <param name="ajlbbm">角色案件类别权限</param>
        /// <param name="caseRoleList">人员案件权限</param>
        /// <param name="ajmc">案件名称</param>
        /// <param name="slrq_s">受理日期开始</param>
        /// <param name="slrq_e">受理日期结束</param>
        /// <param name="cbrxm">承办人姓名</param>
        /// <param name="ajglzt">案件关联状态</param>
        /// <param name="pageSize">每页数</param>
        /// <param name="currentPage">当前第几页</param>
        /// <returns></returns>
        public List<ZC57s.CaseInfoServ.DigitalDossier.ICInterface.ClsAJJBXX> GetAjjbxx(string bmsah, List<string> ajlbbm, List<string> caseRoleList, List<string> dwbm, string ajmc, DateTime? slrq_s, DateTime? slrq_e, string cbrxm, string ajglzt, bool PowerEnable, int pageSize, int currentPage, out int count, out string msg)
        {
            var ajjbxxList = new List<ZC57s.CaseInfoServ.DigitalDossier.ICInterface.ClsAJJBXX>();
            try
            {
                var ajsp = new ZC57s.CaseInfoServ.DigitalDossier.ICInterface.AjjbxxSearchParam();
                var spp = new ZC57s.CaseInfoServ.DigitalDossier.ICInterface.SearchPageParam();
                ajsp.BMSAH = bmsah;
                if (ajlbbm != null)
                {
                    ajsp.LstPowerAJLBBM = new List<string>();
                    ajsp.LstPowerAJLBBM.AddRange(ajlbbm);
                }
                if (dwbm != null)
                {
                    ajsp.LstPowerDWBM = new List<string>();
                    ajsp.LstPowerDWBM.AddRange(dwbm);
                }

                ajsp.AJMC = ajmc;
                ajsp.SLRQ_Start = slrq_s;
                ajsp.SLRQ_End = slrq_e;
                ajsp.CBRXM = cbrxm;
                ajsp.PageSize = pageSize;
                ajsp.CurrentPage = currentPage;
                ajsp.IsNeedCount = true;
                ajsp.PowerEnable = PowerEnable;
                ajsp.SFZZ = ajglzt;
                ZC57s.CaseInfoServ.DigitalDossier.ICInterface.DigitalDossierServicePrx prx = GetDossierService();
                ajjbxxList = prx.GetAjjbxx(ajsp, out spp, out msg).ToList();

                if (spp != null)
                    count = spp.Count;
                else
                    count = 0;
            }
            catch (Exception ex)
            {
                count = 0;
                if (ex.InnerException != null)
                    msg = ex.InnerException.Message.ToString();
                else
                    msg = ex.Message.ToString();
                LogHelper.LogError(null, "Ice", msg, "GetAjjbxx", "IceServicePrx");
            }

            return ajjbxxList;
        }
        #endregion


        #region 查询案件基本信息数量
        /// <summary>
        /// 查询案件基本信息数量
        /// </summary>
        /// <param name="ajlbbm">角色案件类别权限</param>
        /// <param name="dwbm">单位编码集合</param>
        /// <param name="slrq_s">受理日期开始</param>
        /// <param name="slrq_e">受理日期结束</param>
        /// <param name="ajglzt">案件关联状态</param>
        /// <param name="pageSize">每页数</param>
        /// <param name="currentPage">当前第几页</param>
        /// <returns></returns>
        public int GetAjjbxxCount(List<string> ajlbbm, string dwbm, DateTime? slrq_s, DateTime? slrq_e, bool PowerEnable, out string msg)
        {
            var ajjbxxList = new List<ZC57s.CaseInfoServ.DigitalDossier.ICInterface.ClsAJJBXX>();
            try
            {
                var ajsp = new ZC57s.CaseInfoServ.DigitalDossier.ICInterface.AjjbxxSearchParam();
                var spp = new ZC57s.CaseInfoServ.DigitalDossier.ICInterface.SearchPageParam();
                ajsp.BMSAH = "";
                //if (ajlbbm != null)
                //{
                //    ajsp.LstPowerAJLBBM = new List<string>();
                //    ajsp.LstPowerAJLBBM.AddRange(ajlbbm);
                //}
                //if (dwbm != null)
                //{
                //    ajsp.LstPowerDWBM = new List<string>();
                //    ajsp.LstPowerDWBM.Add(dwbm);
                //}
                ajsp.DWBM = dwbm;
                ajsp.AJMC = "";
                ajsp.SLRQ_Start = slrq_s;
                ajsp.SLRQ_End = slrq_e;
                ajsp.CBRXM = "";
                ajsp.PageSize = 0;
                ajsp.CurrentPage = 0;
                ajsp.IsNeedCount = true;
                ajsp.PowerEnable = false;
                ajsp.SFZZ = "";
                ZC57s.CaseInfoServ.DigitalDossier.ICInterface.DigitalDossierServicePrx prx = GetDossierService();
                prx.GetAjjbxx(ajsp, out spp, out msg);

                if (spp != null)
                    return spp.Count;
                //count = spp.Count;
                //else
                //    count = 0;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    msg = ex.InnerException.Message.ToString();
                else
                    msg = ex.Message.ToString();
                LogHelper.LogError(null, "Ice", msg, "GetAjjbxx", "IceServicePrx");
            }

            return 0;
        }
        #endregion

        #region 查询案件基本信息 部门集合
        /// <summary>
        /// 查询案件基本信息 部门集合
        /// </summary>
        /// <param name="bmsah">部门受案号</param>
        /// <param name="ajlbbm">角色案件类别权限</param>
        /// <param name="caseRoleList">人员案件权限</param>
        /// <param name="ajmc">案件名称</param>
        /// <param name="slrq_s">受理日期开始</param>
        /// <param name="slrq_e">受理日期结束</param>
        /// <param name="cbrxm">承办人姓名</param>
        /// <param name="ajglzt">案件关联状态</param>
        /// <param name="pageSize">每页数</param>
        /// <param name="currentPage">当前第几页</param>
        /// <returns></returns>
        public List<ZC57s.CaseInfoServ.DigitalDossier.ICInterface.ClsAJJBXX> GetAjjbxx(string bmsah, List<string> ajlbbm, List<string> caseRoleList, List<string> dwbm, string ajmc, DateTime? slrq_s, DateTime? slrq_e, string cbrxm, string ajglzt, int pageSize, int currentPage, out int count, out string msg)
        {
            return GetAjjbxx(bmsah, ajlbbm, caseRoleList, dwbm, ajmc, slrq_s, slrq_e, cbrxm, ajglzt, true, pageSize, currentPage, out count, out msg);
        }
        #endregion

        #region 查询案件基本信息 部门非集合
        /// <summary>
        /// 查询案件基本信息 部门非集合
        /// </summary>
        /// <param name="bmsah"></param>
        /// <param name="ajlbbm"></param>
        /// <param name="caseRoleList"></param>
        /// <param name="dwbm"></param>
        /// <param name="ajmc"></param>
        /// <param name="slrq_s"></param>
        /// <param name="slrq_e"></param>
        /// <param name="cbrxm"></param>
        /// <param name="ajglzt"></param>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <param name="count"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public List<ZC57s.CaseInfoServ.DigitalDossier.ICInterface.ClsAJJBXX> GetAjjbxx(string bmsah, List<string> ajlbbm, List<string> caseRoleList, string dwbm, string ajmc, DateTime? slrq_s, DateTime? slrq_e, string cbrxm, string ajglzt, int pageSize, int currentPage, out int count, out string msg)
        {
            List<string> listDwbm = null;
            if (!string.IsNullOrEmpty(dwbm))
                listDwbm = new List<string> { dwbm };
            return GetAjjbxx(bmsah, ajlbbm, caseRoleList, listDwbm, ajmc, slrq_s, slrq_e, cbrxm, ajglzt, true, pageSize, currentPage, out count, out msg);
        }
        #endregion

        #region 转换为table数据集
        /// <summary>
        /// 转换为table数据集
        /// </summary>
        /// <param name="bmsah"></param>
        /// <param name="ajlbbm"></param>
        /// <param name="caseRoleList"></param>
        /// <param name="dwbm"></param>
        /// <param name="ajmc"></param>
        /// <param name="slrq_s"></param>
        /// <param name="slrq_e"></param>
        /// <param name="cbrxm"></param>
        /// <param name="ajglzt"></param>
        /// <param name="PowerEnable"></param>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <param name="count"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public DataTable AJJBXXJson(string bmsah, List<string> ajlbbm, List<string> caseRoleList, List<string> dwbm, string ajmc, DateTime? slrq_s, DateTime? slrq_e, string cbrxm, string ajglzt, bool PowerEnable, int pageSize, int currentPage, out int count, out string msg)
        {
            List<ZC57s.CaseInfoServ.DigitalDossier.ICInterface.ClsAJJBXX> ajjbxxList = GetAjjbxx(bmsah, ajlbbm, caseRoleList, dwbm, ajmc, slrq_s, slrq_e, cbrxm, ajglzt, PowerEnable, pageSize, currentPage, out count, out msg);

            if (ajjbxxList != null && ajjbxxList.Count > 0)
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(ZC57s.CaseInfoServ.DigitalDossier.ICInterface.ClsAJJBXX));
                DataTable dt = new DataTable();
                for (int i = 0; i < properties.Count; i++)
                {
                    PropertyDescriptor property = properties[i];
                    dt.Columns.Add(property.Name);
                }
                object[] values = new object[properties.Count];
                foreach (ZC57s.CaseInfoServ.DigitalDossier.ICInterface.ClsAJJBXX t in ajjbxxList)
                {
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = properties[i].GetValue(t);
                    }
                    dt.Rows.Add(values);
                }
                return dt;
            }
            return null;
        }
        #endregion

        #region 查询案件基本信息 部门集合
        /// <summary>
        /// 查询案件基本信息 部门集合
        /// </summary>
        /// <param name="bmsah">部门受案号</param>
        /// <param name="ajlbbm">角色案件类别权限</param>
        /// <param name="caseRoleList">人员案件权限列表</param>
        /// <param name="ajmc">案件名称</param>
        /// <param name="slrq_s">受理日期开始</param>
        /// <param name="slrq_e">受理日期结束</param>
        /// <param name="cbrxm">承办人姓名</param>
        /// <param name="ajglzt">案件关联状态</param>
        /// <param name="pageSize">每页数</param>
        /// <param name="currentPage">当前第几页</param>
        /// <returns></returns>
        public DataTable AJJBXXJson(string bmsah, List<string> ajlbbm, List<string> caseRoleList, List<string> dwbm, string ajmc, DateTime? slrq_s, DateTime? slrq_e, string cbrxm, string ajglzt, int pageSize, int currentPage, out int count, out string msg)
        {
            return AJJBXXJson(bmsah, ajlbbm, caseRoleList, dwbm, ajmc, slrq_s, slrq_e, cbrxm, ajglzt, true, pageSize, currentPage, out count, out msg);
        }
        #endregion

        #region 查询案件基本信息 部门非集合
        /// <summary>
        /// 查询案件基本信息 部门非集合
        /// </summary>
        /// <param name="bmsah"></param>
        /// <param name="ajlbbm"></param>
        /// <param name="caseRoleList"></param>
        /// <param name="dwbm"></param>
        /// <param name="ajmc"></param>
        /// <param name="slrq_s"></param>
        /// <param name="slrq_e"></param>
        /// <param name="cbrxm"></param>
        /// <param name="ajglzt"></param>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <param name="count"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public DataTable AJJBXXJson(string bmsah, List<string> ajlbbm, List<string> caseRoleList, string dwbm, string ajmc, DateTime? slrq_s, DateTime? slrq_e, string cbrxm, string ajglzt, int pageSize, int currentPage, out int count, out string msg)
        {

            return AJJBXXJson(bmsah, ajlbbm, caseRoleList, new List<string>() { dwbm }, ajmc, slrq_s, slrq_e, cbrxm, ajglzt, true, pageSize, currentPage, out count, out msg);
        }
        #endregion
        #endregion


        #region 卷宗文件下载

        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="iceUrl"></param>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <param name="md5"></param>
        /// <returns></returns>
        public bool DownFile(string iceUrl, string filePath, string fileName, string md5, ref byte[] bytes, ref string msg)
        {
            try
            {

                TransferPrx ServerProxy = TransferPrxHelper.uncheckedCast(ClientCommunicationService.Instance.Communicator.stringToProxy(iceUrl));
               
                Yy.DownElectronicFile.TranInterface.YyDownFileInfo info = new Yy.DownElectronicFile.TranInterface.YyDownFileInfo();
                info.AJLB = "";
                info.WJLJ = filePath;//该数据来源于数据库(WJLJ)
                info.WJMC = fileName;//该数据来源于数据库(WJMC)
                info.MD5 = md5;//这是正确的MD5
                info.WJHZ = ".pdf";
                //对比文件MD5值
                bool result = ServerProxy.GetCheckFileMD5(info);
                //if (result)
                //{
                //byte[] bytes = new byte[] { };
                //获取文件，返回获取状态，out 文件流
                //result = ServerProxy.GetElectroniceFile(info, out bytes);
                string showType = ConfigHelper.GetConfigString("FileShowType");
                if (showType == "1")
                    result = ServerProxy.GetElectroniceFile(info, out bytes);
                else
                    result = ServerProxy.GetElectronicePdfFile(info, out bytes);
                return result;
                //}
                //else
                //    msg = "文件唯一码验证不正确";
                //连接测试
                //result = ServerProxy.Test();

            }
            catch (Ice.UnknownException ex)
            {
                msg = ex.unknown;
            }
            return false;
        }

        #endregion
    }
}
