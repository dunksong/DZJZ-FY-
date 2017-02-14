using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cyvation.CCQE.Common;
using Cyvation.CCQE.Model;
using Cyvation.CCQE.BLL;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections;
using LitJson;
using System.Drawing;
using EDRS.Common;
using System.Web.Script.Serialization;

namespace Cyvation.CCQE.Web
{
    /// <summary>
    /// ZZJGHandler 的摘要说明
    /// </summary>
    public class ZZJGHandler : AshxBase
    {

        public class JsonResult
        {
            public bool isTrue { get; set; }
            public string errorMsg { get; set; }

            public string ToJsonString()
            {
                string jsonStr = "";
                jsonStr = "[{\"isTrue\":\"" + isTrue.ToString().ToLower() + "\",\"errorMsg\":\"" + errorMsg + "\"}]";
                return jsonStr;
            }
        }
        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);
            if (UserInfo == null)
            {
                context.Response.Write(ReturnString.JsonToString(Prompt.error, "登录超时，请刷新页面重新登录fn=parent.skip", null));
                return;
            }
            context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            string action = context.Request.Params["action"];
            switch (action)
            {
                case "GetDwBmJsByDwbm":
                    GetDwBmJsByDwbm(context);
                    break;
                case "AddBmInfo":
                    AddBmInfo(context);
                    break;
                case "GetBmInfoByDwbm":
                    GetBmInfoByDwbm(context);
                    break;
                case "AddGnQx":
                    AddGnQx(context);
                    break;
                case "AddJsGnQx":
                    AddJsGnQx(context);
                    break;
                case "AddJsInfo":
                    AddJsInfo(context);
                    break;
                case "AddRyInfo":
                    AddRyInfo(context);
                    break;
                case "UpdateRyInfo":
                    UpdateRyInfo(context);
                    break;
                case "DeleteRyInfo":
                    DeleteRyInfo(context);
                    break;
                case "DeleteGnQx":
                    DeleteGnQx(context);
                    break;
                case "DeleteJsGnQx":
                    DeleteJsGnQx(context);
                    break;
                case "DeleteJsInfo":
                    DeleteJsInfo(context);
                    break;
                case "DwSource":
                    DwSource(context);
                    break;
                case "GetGnflByDwbm":
                    GetGnflByDwbm(context);
                    break;
                case "GetGnmcByflb":
                    GetGnmcByflb(context);
                    break;
                case "GetJSInfoByDwBm":
                    GetJSInfoByDwBm(context);
                    break;
                case "GetJsQxInfo":
                    GetJsQxInfo(context);
                    break;
                case "GetRyInfo":
                    GetRyInfo(context);
                    break;
                case "GetRyinfoByGh":
                    GetRyinfoByGh(context);
                    break;
                case "PassWord":
                    PassWord(context);
                    break;
                case "QueryGnqx":
                    QueryGnqx(context);
                    break;
                case "RemoveJob":
                    RemoveJob(context);
                    break;
                case "GetRyList":
                    GetRyList(context);
                    break;
                case "GetDwInfo":    //获取单位信息
                    GetDwInfo(context);
                    break;
                case "GetBmInfo":    //获取部门信息
                    GetBmInfo(context);
                    break;
                case "GetJsxh":    //获取角色序号
                    GetJsxh(context);
                    break;
                case "GetWfpgnTreeData":    //获取角色上未分配功能
                    GetWfpgnTreeData(context);
                    break;
                case "UpdateJsGnCs":    //修改角色上的功能参数
                    UpdateJsGnCs(context);
                    break;
                case "GetWfpRyInfo":    //获取尚未在该角色上分配的人员
                    GetWfpRyInfo(context);
                    break;
                case "AddRYJSFP":    //为角色绑定人员
                    AddRYJSFP(context);
                    break;
                case "ResetPwd":    //重置密码
                    ResetPwd(context);
                    break;
                case "GetRyJsData":    //获取人员已分配角色
                    GetRyJsData(context);
                    break;
                case "GetGxdwList":    //获取关系单位列表
                    GetGxdwList(context);
                    break;
                case "UpdatePsword":   //修改密码
                    UpdatePsword(context);
                    break;
                case "GetAJLB"://获取案件类型
                    GetAJLB(context);
                    break;
                case "ADDDWQX"://增加单位权限
                    ADDDWQX(context);
                    break;
                case "DELDWQX"://删除单位权限
                    DELDWQX(context);
                    break;
                case "ADDLBQX"://增加案件类别权限
                    ADDLBQX(context);
                    break;
                case "DELLBQX"://删除单位类别权限
                    DELLBQX(context);
                    break;
                case "GetDWQX"://已分配单位权限
                    GetDWQX(context);
                    break;
                case "GetLBQX"://已分配案件分类权限
                    GetLBQX(context);
                    break;
                case "GetAllDwBm"://获取所以未赋予权限的单位
                    GetAllDwBm(context);
                    break;
                case "GetLogList":
                    getAllLogList(context);
                    break;
                case "RoleListBind":
                    RoleListBindIce(context);
                    break;
                case "userRoleList":
                    GetUserRoleList(context);
                    break;
                case "GetGnTree": //获取功能树
                    GetGnTreeList(context);
                    break;
                case "GetAnList"://获取按钮功能集合
                    GetAnList(context);
                    break;
                case "GetAnQxList": //获取按钮权限集合
                    GetAnQxList(context);
                    break;
                case "AddJsAnQx":
                    AddJsAnQx(context);
                    break;
                case "DeleteJsAnQx":
                    DeleteJsAnQx(context);
                    break;
                default:
                    break;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        #region 原有的
        private void GetDwBmJsByDwbm(HttpContext context)
        {
            string dwbm = UserInfo.DWBM;
            string errmsg = string.Empty;
            DataTable dt = ZzjgManage.GetDwBmJsInfoByDwBm(dwbm, out errmsg);
            if (dt != null && string.IsNullOrEmpty(errmsg))
            {
                //Dictionary<int, string> icons = new Dictionary<int, string>();
                //icons.Add(4, "tree_js");
                //icons.Add(5, "tree_bm");
                //icons.Add(7, "tree_dw");

                dt.Columns["BM"].ColumnName = "id";
                dt.Columns["FBM"].ColumnName = "pid";
                dt.Columns["MC"].ColumnName = "text";
                dt.Columns["TP"].ColumnName = "icon";


                string json = EDRS.Common.JsonHelper.JsonString(dt);


                //获取json数据
                //  string json = dt.ToTreeJson("BM", "FBM", "MC", icons);
                context.Response.Write(json);
                OperateLog.AddLog(OperateLog.LogType.角色权限管理Web, "查询单位部门列表数据成功！" + errmsg, UserInfo, UserRole, context.Request);
            }
            else
            {
                /*
                 * 错误日志记录
                 */
                string errInfo = string.Empty;
                errInfo += "当前文件:" + CodeHelper.GetCurSourceFileName() + "\r\n";
                errInfo += "当前行:" + CodeHelper.GetLineNum().ToString() + "\r\n";
                errInfo += "调用函数异常:" + "public static DataTable GetDwBmJsInfoByDwBm(string dwbm, out string err); " + "\r\n";
                errInfo += "传入参数:" + dwbm.ToString() + "\r\n";
                errInfo += errmsg;
                Logger.Error("获取单位部门角色发生错误", errInfo, UserInfo);
                EDRS.Common.LogHelper.LogError(context.Request, "Exception", errmsg, "private void GetDwBmJsByDwbm(HttpContext context)", "Cyvation.CCQE.Web", "");
                OperateLog.AddLog(OperateLog.LogType.角色权限管理Web, "查询单位部门列表数据失败：" + errmsg, UserInfo, UserRole, context.Request);
            }
        }

        private void AddBmInfo(HttpContext context)
        {
            string errmsg = string.Empty;
            string bmmc = context.Request.Params["bmmc"];
            string bmjc = context.Request.Params["bmjc"];
            int bmxh = Convert.ToInt32(context.Request.Params["bmxh"]);
            string bz = context.Request.Params["bz"];
            string fbm = context.Request.Params["fbm"];
            fbm = DataProHelper.ProBmSubOne(fbm);
            string bmbm = context.Request.Params["bmbm"];
            bmbm = DataProHelper.ProBmSubOne(bmbm);

            string dwbm = context.Request.Params["dwbm"];
            if (fbm.Length == 6)
            {
                fbm = "";
            }
            bool isSuc = ZzjgManage.AddBmInfo(dwbm, bmbm, bmmc, bmjc, bmxh, bz, fbm, out errmsg);

            if (isSuc)
            {
                context.Response.Write("操作成功");
                Logger.Info("添加部门成功", UserInfo);
                OperateLog.AddLog(OperateLog.LogType.角色权限管理Web, "添加部门成功！", bmmc, UserInfo, UserRole, context.Request);
            }
            else
            {
                OperateLog.AddLog(OperateLog.LogType.角色权限管理Web, "添加部门失败：" + errmsg, bmmc, UserInfo, UserRole, context.Request);
                context.Response.Write("操作失败" + errmsg);
                /*
                 * 记录日志
                 */
                string errInfo = string.Empty;
                errInfo += "当前文件:" + CodeHelper.GetCurSourceFileName() + "\r\n";
                errInfo += "当前行:" + CodeHelper.GetLineNum().ToString() + "\r\n";
                errInfo += "调用函数异常:" + "public static bool AddBmInfo(string dwbm,string bmbm, string bmmc, string bmjc, int bmxh, string bz, string fbmbm, out string errmsg)" + "\r\n";
                errInfo += "传入参数:" + dwbm.ToString() + ";" + bmmc.ToString() + ";" + bmjc.ToString() + ";"
                            + bmxh.ToString() + ";" + bz.ToString() + ";" + fbm.ToString() + "\r\n";
                errInfo += errmsg;
                Logger.Error("添加部门失败", errInfo, UserInfo);
                EDRS.Common.LogHelper.LogError(context.Request, "Exception", errmsg, "private void AddBmInfo(HttpContext context)", "Cyvation.CCQE.Web", "");
            }

        }

        private void GetBmInfoByDwbm(HttpContext context)
        {
            string dwbm = context.Request.Params["dwbm"];
            string result = string.Empty;
            result = LoadBmData(dwbm);

            context.Response.Write(result);
            context.Response.End();
        }

        string LoadBmData(string dwbm)
        {
            string strJson = string.Empty;
            string err = string.Empty;
            DataTable dt = ZzjgManage.GetBmInfoByDwBm(dwbm, out err);
            if (!string.IsNullOrEmpty(err))
            {
                //异常处理
                /*
                 * 记录日志
                 */
                string errInfo = string.Empty;
                errInfo += "当前文件:" + CodeHelper.GetCurSourceFileName() + "\r\n";
                errInfo += "当前行:" + CodeHelper.GetLineNum().ToString() + "\r\n";
                errInfo += "调用函数异常:" + "public static DataTable GetBmInfoByDwBm(string dwbm,out string err)" + "\r\n";
                errInfo += "传入参数:" + dwbm.ToString() + "\r\n";
                errInfo += err;
                Logger.Error("获取部门信息发生错误", errInfo, UserInfo);
                return strJson;
            }
            dt = ProDataTableBm(dt);
            strJson = ToComboxJson(dt);
            return strJson;
        }

        string ToComboxJson(DataTable dt)
        {
            StringBuilder strJson = new StringBuilder();
            int count = dt.Rows.Count;
            strJson.Append("[");
            for (int i = 0; i < count; i++)
            {
                strJson.Append("{");
                strJson.Append("\"id\"" + ":" + dt.Rows[i]["bmbm"] + ",");
                strJson.Append("\"text\"" + ":" + "\"" + dt.Rows[i]["bmmc"] + "\"" + "}");
                if (i < count - 1)
                    strJson.Append(",");
            }
            strJson.Append("]");
            return strJson.ToString();
        }

        //处理表字段，json数据的的id只能为整数,在table的bmbm前面加1
        DataTable ProDataTableBm(DataTable dt)
        {
            int count = dt.Rows.Count;
            string strTmp = string.Empty;
            for (int i = 0; i < count; i++)
            {
                strTmp = dt.Rows[i]["bmbm"].ToString();
                strTmp = "1" + strTmp;
                dt.Rows[i]["bmbm"] = strTmp;
            }
            return dt;
        }

        private void AddGnQx(HttpContext context)
        {
            string dwbm = UserInfo.DWBM;
            //string dwbm = "440000";
            int isExistedFlb = Convert.ToInt32(context.Request.Params["isExistedFlb"]);
            string gnfl = context.Request.Params["gnfl"];
            //if (isExistedFlb == 1)
            //{
            //    gnfl = DataProHelper.ProBmSubOne(gnfl);
            //}
            gnfl = context.Server.UrlDecode(gnfl);
            string gnbm = context.Request.Params["gnbm"];
            gnbm = DataProHelper.ProBmSubOne(context.Server.UrlDecode(gnbm));
            string gnmc = context.Request.Params["gnmc"];
            gnmc = context.Server.UrlDecode(gnmc);
            string gnct = context.Request.Params["gnct"];
            gnct = context.Server.UrlDecode(gnct);
            string gnxsmc = context.Request.Params["gnxsmc"];
            gnxsmc = context.Server.UrlDecode(gnxsmc);
            int gnxh = 0;
            try
            {
                gnxh = Convert.ToInt32(context.Request.Params["gnxh"]);
            }
            catch (Exception)
            {
            }

            string gnsm = context.Request.Params["gnsm"];
            gnsm = context.Server.UrlDecode(gnsm);
            string cscs = context.Request.Params["cscs"];
            cscs = context.Server.UrlDecode(cscs);

            string gjy = "";// Convert.ToBoolean(context.Request.Params["gjy"].ToString()) ? "Y" : "N";
            string sy = "";// Convert.ToBoolean(context.Request.Params["sy"].ToString()) ? "Y" : "N";
            string sjy = "";// Convert.ToBoolean(context.Request.Params["sjy"].ToString()) ? "Y" : "N";
            string qy = "";// Convert.ToBoolean(context.Request.Params["qy"].ToString()) ? "Y" : "N";


            string errmsg = string.Empty;
            bool isSuc = false;

            isSuc = ZzjgManage.AddGnQx(dwbm, isExistedFlb, gnfl, gnbm, gnmc, gnct, gnxsmc, gnxh, gnsm, cscs, gjy, sy, sjy, qy, out errmsg);
            string oper = string.IsNullOrEmpty(gnbm) ? "新增" : "修改";
            if (isSuc && string.IsNullOrEmpty(errmsg))
            {
                context.Response.Write("操作成功");
                Logger.Info("添加功能权限成功", UserInfo);

                OperateLog.AddLog(OperateLog.LogType.功能模块管理Web, oper + "功能成功！", gnmc, UserInfo, UserRole, context.Request);
            }
            else
            {
                OperateLog.AddLog(OperateLog.LogType.功能模块管理Web, oper + "功能失败：" + errmsg, gnmc, UserInfo, UserRole, context.Request);
                //进行错误处理
                /*
                 * 记录日志
                 */
                string errInfo = string.Empty;
                errInfo += "当前文件:" + CodeHelper.GetCurSourceFileName() + "\r\n";
                errInfo += "当前行:" + CodeHelper.GetLineNum().ToString() + "\r\n";
                errInfo += "调用函数异常:" + "public static bool AddGnQx(string dwbm, int isExistedFlb, string gnfl,string gnbm, string gnmc, string gnct, string gnxsmc,"
                               + " int gnxh, string gnsm,string cscs, out string errmsg);" + "\r\n";
                errInfo += "传入参数:" + dwbm.ToString() + ";" + isExistedFlb.ToString() + ";" + gnfl.ToString() + ";" + gnmc.ToString() +
                            ";" + gnct.ToString() + ";" + gnxsmc.ToString() + ";" + gnxh.ToString() + ";" + gnsm.ToString() + ";" +
                            cscs.ToString() + "\r\n";
                errInfo += errmsg;
                Logger.Error("添加功能权限失败", errInfo, UserInfo);
                EDRS.Common.LogHelper.LogError(context.Request, "Exception", errmsg, "private void AddGnQx(HttpContext context)", "Cyvation.CCQE.Web", "");
                context.Response.Write("操作失败");
            }
        }

        private void AddJsGnQx(HttpContext context)
        {
            string dwbm = context.Request.Params["dwbm"];
            //string dwbm = "440000";
            string bmbm = context.Request.Params["bmbm"];
            bmbm = DataProHelper.ProBmSubOne(bmbm);
            string jsbm = context.Request.Params["jsbm"];
            jsbm = DataProHelper.ProBmSubOne(jsbm);
            string gnbm = context.Request.Params["gnbm"];
            string gnmc = context.Request.Params["gnmc"];
            //gnbm = DataProHelper.ProBmSubOne(gnbm);
            string bz = context.Request.Params["bz"];
            string errmsg = string.Empty;
            bool isSuc = ZzjgManage.AddJsGnQx(dwbm, bmbm, jsbm, gnbm, bz, out errmsg);
            if (string.IsNullOrEmpty(errmsg) && isSuc)
            {
                context.Response.Write("操作成功");
                Logger.Info("添加角色权限成功", UserInfo);
                OperateLog.AddLog(OperateLog.LogType.角色权限管理Web, "添加角色功能权限成功！", gnmc, UserInfo, UserRole, context.Request);
            }
            else
            {
                OperateLog.AddLog(OperateLog.LogType.角色权限管理Web, "添加角色功能权限失败：" + errmsg, gnmc, UserInfo, UserRole, context.Request);
                /*
                 * 记录日志
                 */
                string errInfo = string.Empty;
                errInfo += "当前时间:" + DateTime.Now.ToShortTimeString();
                errInfo += "当前文件:" + CodeHelper.GetCurSourceFileName() + "\r\n";
                errInfo += "当前行:" + CodeHelper.GetLineNum().ToString() + "\r\n";
                errInfo += "调用函数异常:" + "public static bool AddJsGnQx(string dwbm, string bmbm, string jsbm, string gnbm, string bz, out string errmsg);" + "\r\n";
                errInfo += "传入参数:" + dwbm.ToString() + ";" + bmbm.ToString() + ";" + jsbm.ToString() + ";"
                            + gnbm.ToString() + ";" + bz.ToString() + "\r\n";
                errInfo += errmsg;
                Logger.Error("添加角色权限失败", errInfo, UserInfo);
                EDRS.Common.LogHelper.LogError(context.Request, "Exception", errmsg, "private void AddJsGnQx(HttpContext context)", "Cyvation.CCQE.Web", "");
                context.Response.Write("操作失败" + errmsg);
            }
        }

        private void AddJsInfo(HttpContext context)
        {
            string dwbm = context.Request.Params["dwbm"];
            string bmbm = context.Request.Params["bmbm"];
            bmbm = DataProHelper.ProBmSubOne(bmbm);
            string jsmc = context.Request.Params["jsmc"];
            int jsxh = Convert.ToInt32(context.Request.Params["jsxh"]);
            string jsbm = context.Request.Params["jsbm"];
            jsbm = DataProHelper.ProBmSubOne(jsbm);
            string qxzt = context.Request.Params["qxzt"];

            string errmsg = string.Empty;
            string oper = string.IsNullOrEmpty(jsbm) ? "增加" : "修改";
            bool isSuc = ZzjgManage.AddJsInfo(dwbm, bmbm, jsbm, jsmc, jsxh, qxzt, out errmsg);
            if (isSuc && string.IsNullOrEmpty(errmsg))
            {
                context.Response.Write("操作成功");

                OperateLog.AddLog(OperateLog.LogType.角色权限管理Web, oper + "角色信息成功！", jsmc, UserInfo, UserRole, context.Request);
                //Logger.Info("添加角色成功", UserInfo);
            }
            else
            {
                OperateLog.AddLog(OperateLog.LogType.角色权限管理Web, oper + "角色信息失败：" + errmsg, jsmc, UserInfo, UserRole, context.Request);
                context.Response.Write("操作失败" + errmsg);
                //进行错误处理
                /*
                 * 记录日志
                 */
                string errInfo = string.Empty;
                errInfo += "当前文件:" + CodeHelper.GetCurSourceFileName() + "\r\n";
                errInfo += "当前行:" + CodeHelper.GetLineNum().ToString() + "\r\n";
                errInfo += "调用函数异常:" + "ZzjgManage.AddJsInfo(dwbm,bmbm,jsmc,jsxh,out errmsg);" + "\r\n";
                errInfo += "传入参数:" + dwbm.ToString() + ";" + bmbm.ToString() + ";" + jsmc.ToString() +
                        "" + jsxh.ToString() + "\r\n";
                errInfo += errmsg;
                Logger.Error("添加角色失败", errInfo, UserInfo);
                EDRS.Common.LogHelper.LogError(context.Request, "Exception", errmsg, "private void AddJsInfo(HttpContext context)", "Cyvation.CCQE.Web", "");
            }
            context.Response.End();
        }

        private void AddRyInfo(HttpContext context)
        {
            RybmModel ryxx = new RybmModel();
            ryxx.DWBM = UserInfo.DWBM; //context.Request.Params["dwbm"];
            ryxx.MC = context.Request.Params["mc"];
            ryxx.DLBM = context.Request.Params["dlbm"];
            ryxx.GZZH = context.Request.Params["gzzh"];
            ryxx.XB = context.Request.Params["xb"];
            ryxx.SFLSRY = context.Request.Params["lsry"];
            ryxx.SFTZ = context.Request.Params["tz"];
            ryxx.YDDHHM = context.Request.Params["yddhhm"];
            ryxx.DZYJ = context.Request.Params["dzyx"];
            ryxx.CAID = context.Request.Params["CAIDH"];
            ryxx.ZW = context.Request.Params["zw"];
            string errmsg = string.Empty;
            EDRS.BLL.XT_ZZJG_RYBM bll = new EDRS.BLL.XT_ZZJG_RYBM(context.Request);
            if (bll.ExistsDlbm(UserInfo.DWBM, "", ryxx.DLBM))
            {

                OperateLog.AddLog(OperateLog.LogType.人员管理Web, "已存在的登陆别名，更新失败！", ryxx.MC, UserInfo, UserRole, context.Request);
                context.Response.Write("已存在的登陆别名，更新失败！");
                return;
            }
            bool isSuc = ZzjgManage.AddRYInfo(ryxx, out errmsg);
            if (string.IsNullOrEmpty(errmsg) && isSuc)
            {
                OperateLog.AddLog(OperateLog.LogType.人员管理Web, "添加人员成功！", ryxx.MC, UserInfo, UserRole, context.Request);
                context.Response.Write("操作成功");
                //Logger.Info("添加人员成功", UserInfo);
            }
            else
            {
                EDRS.Common.LogHelper.LogError(context.Request, "Exception", errmsg, "private void AddRyInfo(HttpContext context)", "Cyvation.CCQE.Web", "");
                OperateLog.AddLog(OperateLog.LogType.人员管理Web, "添加人员档案失败：" + errmsg, ryxx.MC, UserInfo, UserRole, context.Request);
                context.Response.Write(errmsg);
                /*
                 * 记录日志
                 */
                //string errInfo = string.Empty;
                //errInfo += "当前文件:" + CodeHelper.GetCurSourceFileName() + "\r\n";
                //errInfo += "当前行:" + CodeHelper.GetLineNum().ToString() + "\r\n";
                //errInfo += "调用函数异常:" + "public static bool AddRYInfo(string dwbm, string bmbm, string jsbm, string mc," +
                //             " string dlbm, string gh, string gzzh, string xb, string sflsry, string yddhhm, " +
                //            "string dzyx, string CAID, out string errmsg);" + "\r\n";
                //errInfo += "传入参数:" + dwbm.ToString() + ";" + bmbm.ToString() + ";" + jsbm.ToString() + ";"
                //                + mc.ToString() + ";" + dlbm.ToString() + ";" + gh.ToString() + ";" + lsry.ToString()
                //                    + ";" + yddhhm.ToString() + ";" + dzyx.ToString() + ";" + CAID.ToString() + "\r\n";
                //errInfo += errmsg;
                //Logger.Error("操作失败", errInfo, UserInfo);
            }
        }

        private void UpdateRyInfo(HttpContext context)
        {
            RybmModel ryxx = new RybmModel();
            ryxx.DWBM = UserInfo.DWBM;
            ryxx.MC = context.Request.Params["mc"];
            ryxx.DLBM = context.Request.Params["dlbm"];
            ryxx.GZZH = context.Request.Params["gzzh"];
            ryxx.XB = context.Request.Params["xb"];
            ryxx.SFLSRY = context.Request.Params["lsry"];
            ryxx.SFTZ = context.Request.Params["tz"];
            ryxx.YDDHHM = context.Request.Params["yddhhm"];
            ryxx.DZYJ = context.Request.Params["dzyx"];
            ryxx.CAID = context.Request.Params["CAIDH"];
            ryxx.ZW = context.Request.Params["ZW"];
            ryxx.GH = context.Request.Params["gh"];
            string errmsg = string.Empty;
            EDRS.BLL.XT_ZZJG_RYBM bll = new EDRS.BLL.XT_ZZJG_RYBM(context.Request);
            if (bll.ExistsDlbm(UserInfo.DWBM, ryxx.GH, ryxx.DLBM))
            {
                OperateLog.AddLog(OperateLog.LogType.人员管理Web, "已存在的登陆别名，更新失败！", ryxx.MC, UserInfo, UserRole, context.Request);
                context.Response.Write("已存在的登陆别名，更新失败！");
                return;
            }
            bool isSuc = ZzjgManage.UpdateRYInfo(ryxx, out errmsg);

            if (string.IsNullOrEmpty(errmsg) && isSuc)
            {
                context.Response.Write("操作成功");
                OperateLog.AddLog(OperateLog.LogType.人员管理Web, "修改人员信息成功！", ryxx.MC, UserInfo, UserRole, context.Request);
                //Logger.Info("添加人员成功", UserInfo);
            }
            else
            {
                OperateLog.AddLog(OperateLog.LogType.人员管理Web, "修改人员信息失败：" + errmsg, ryxx.MC, UserInfo, UserRole, context.Request);
                context.Response.Write(errmsg);
                /*
                 * 记录日志
                 */
                //string errInfo = string.Empty;
                //errInfo += "当前文件:" + CodeHelper.GetCurSourceFileName() + "\r\n";
                //errInfo += "当前行:" + CodeHelper.GetLineNum().ToString() + "\r\n";
                //errInfo += "调用函数异常:" + "public static bool AddRYInfo(string dwbm, string bmbm, string jsbm, string mc," +
                //             " string dlbm, string gh, string gzzh, string xb, string sflsry, string yddhhm, " +
                //            "string dzyx, string CAID, out string errmsg);" + "\r\n";
                //errInfo += "传入参数:" + dwbm.ToString() + ";" + bmbm.ToString() + ";" + jsbm.ToString() + ";"
                //                + mc.ToString() + ";" + dlbm.ToString() + ";" + gh.ToString() + ";" + lsry.ToString()
                //                    + ";" + yddhhm.ToString() + ";" + dzyx.ToString() + ";" + CAID.ToString() + "\r\n";
                //errInfo += errmsg;
                //Logger.Error("操作失败", errInfo, UserInfo);
            }
        }

        private void DeleteRyInfo(HttpContext context)
        {
            string dwbm = UserInfo.DWBM;
            string ghj = context.Request.Params["ghj"];
            string errmsg = string.Empty;
            bool isSuc = ZzjgManage.DeleteRYInfo(dwbm, ghj, out errmsg);
            if (string.IsNullOrEmpty(errmsg) && isSuc)
            {
                context.Response.Write("操作成功");
                Logger.Info("删除人员成功", UserInfo);
                OperateLog.AddLog(OperateLog.LogType.部门管理Web, "删除人员成功！", "工号：" + ghj, UserInfo, UserRole, context.Request);
            }
            else
            {
                OperateLog.AddLog(OperateLog.LogType.部门管理Web, "删除人员失败：" + errmsg, "工号：" + ghj, UserInfo, UserRole, context.Request);
                context.Response.Write(errmsg);
                /*
                 * 记录日志
                 */
                string errInfo = string.Empty;
                errInfo += "当前文件:" + CodeHelper.GetCurSourceFileName() + "\r\n";
                errInfo += "当前行:" + CodeHelper.GetLineNum().ToString() + "\r\n";
                errInfo += errmsg;
                Logger.Error("操作失败", errInfo, UserInfo);
            }
        }

        private void DeleteGnQx(HttpContext context)
        {
            string dwbm = UserInfo.DWBM;
            string gnbm = context.Request.Params["gnbm"];
            string gnmc = context.Request.Params["gnmc"];
            gnbm = DataProHelper.ProBmSubOne(gnbm);
            string errmsg = string.Empty;
            bool isSuc = ZzjgManage.DeleteGnQx(dwbm, gnbm, out errmsg);

            if (isSuc && string.IsNullOrEmpty(errmsg))
            {
                context.Response.Write("删除成功");
                Logger.Info("删除功能成功", UserInfo);
                OperateLog.AddLog(OperateLog.LogType.功能模块管理Web, "删除功能成功！", gnmc, UserInfo, UserRole, context.Request);
            }
            else
            {
                //进行错误处理
                /*
                 * 记录日志
                 */
                OperateLog.AddLog(OperateLog.LogType.功能模块管理Web, "删除功能失败！", gnmc, UserInfo, UserRole, context.Request);
                string errInfo = string.Empty;
                errInfo += "当前文件:" + CodeHelper.GetCurSourceFileName() + "\r\n";
                errInfo += "当前行:" + CodeHelper.GetLineNum().ToString() + "\r\n";
                errInfo += "调用函数异常:" + "public static bool DeleteGnQx(string dwbm, string gnbm, out string errmsg);" + "\r\n";
                errInfo += "传入参数:" + dwbm.ToString() + ";" + gnbm.ToString() + "\r\n";
                errInfo += errmsg;
                Logger.Error("删除功能权限失败", errInfo, UserInfo);
                context.Response.Write("删除失败" + errmsg);
            }
        }

        private void DeleteJsGnQx(HttpContext context)
        {
            string dwbm = context.Request.Params["dwbm"];
            string jsbm = context.Request.Params["jsbm"];
            jsbm = DataProHelper.ProBmSubOne(jsbm);
            string gnbm = context.Request.Params["gnbm"];
            string gnmc = context.Request.Params["gnmc"];
            gnbm = DataProHelper.ProBmSubOne(gnbm);

            string errmsg = string.Empty;
            bool isSuc = ZzjgManage.DeleteJsGnQx(dwbm, jsbm, gnbm, out errmsg);

            if (isSuc && string.IsNullOrEmpty(errmsg))
            {
                context.Response.Write("操作成功");
                Logger.Info("删除角色功能权限成功", UserInfo);
                OperateLog.AddLog(OperateLog.LogType.角色权限管理Web, "删除角色功能权限成功！", gnmc, UserInfo, UserRole, context.Request);
            }
            else
            {
                OperateLog.AddLog(OperateLog.LogType.角色权限管理Web, "删除角色功能权限失败：" + errmsg, gnmc, UserInfo, UserRole, context.Request);
                //进行错误处理
                /*
                 * 记录日志
                 */
                string errInfo = string.Empty;
                errInfo += "当前文件:" + CodeHelper.GetCurSourceFileName() + "\r\n";
                errInfo += "当前行:" + CodeHelper.GetLineNum().ToString() + "\r\n";
                errInfo += "调用函数异常:" + "public static bool DeleteJsGnQx(string dwbm, string jsbm, string gnbm, out string errmsg);" + "\r\n";
                errInfo += "传入参数:" + dwbm.ToString() + ";" + jsbm.ToString() + ";" + gnbm.ToString() + "\r\n";
                errInfo += errmsg;
                Logger.Error("删除角色功能权限失败", errInfo, UserInfo);
                context.Response.Write("操作失败" + errmsg);
            }
        }

        private void DeleteJsInfo(HttpContext context)
        {
            string dwbm = context.Request.Params["dwbm"];
            string bmbm = context.Request.Params["bmbm"];
            bmbm = DataProHelper.ProBmSubOne(bmbm);
            string jsbm = context.Request.Params["jsbm"];
            jsbm = DataProHelper.ProBmSubOne(jsbm);
            string errmsg = string.Empty;
            bool isSuc = ZzjgManage.DeleteJsInfo(dwbm, bmbm, jsbm, out errmsg);

            if (isSuc && string.IsNullOrEmpty(errmsg))
            {
                context.Response.Write("操作成功");
                Logger.Info("删除角色成功", UserInfo);
            }
            else
            {
                context.Response.Write("操作失败\r\n" + errmsg);
                /*
                * 记录日志
                */
                string errInfo = string.Empty;
                errInfo += "当前文件:" + CodeHelper.GetCurSourceFileName() + "\r\n";
                errInfo += "当前行:" + CodeHelper.GetLineNum().ToString() + "\r\n";
                errInfo += "调用函数异常:" + "public static bool DeleteJsInfo(string dwbm, string bmbm, string jsbm, out string errmsg);" + "\r\n";
                errInfo += "传入参数:" + dwbm.ToString() + ";" + bmbm.ToString() + ";" + jsbm.ToString() + "\r\n";
                errInfo += errmsg;
                Logger.Error("删除角色失败", errInfo, UserInfo);
            }
        }

        private void DwSource(HttpContext context)
        {
            base.ProcessRequest(context);
            //string dwbm = UserInfo.DWBM;
            string dwbm = ConfigurationManager.AppSettings.Get("DWBM");
            if (UserInfo != null)
            {
                dwbm = UserInfo.DWBM;
            }
            string errmsg = string.Empty;
            try
            {
                DataTable dt = ZzjgManage.GetAllDwByDwbm(dwbm, out errmsg);
                if (dt != null && string.IsNullOrEmpty(errmsg))
                {
                    //找到顶级编码
                    DataRow[] dr = dt.Select("BM" + " = '" + ConfigurationManager.AppSettings.Get("DWBM").ToString() + "'");
                    string topFbm = dr[0]["FBM"].ToString();
                    topFbm = "1" + topFbm;
                    //转化编码字段
                    dt = DataProHelper.ProBMAddOne(dt, "BM");
                    dt = DataProHelper.ProBMAddOne(dt, "FBM");
                    //获取json数据
                    string json = dt.ToTreeJson("BM", "FBM", "MC", topFbm);
                    context.Response.Write(json);
                }
                else
                {
                    /*
                    * 记录日志
                    */
                    string errInfo = string.Empty;
                    errInfo += "当前文件:" + CodeHelper.GetCurSourceFileName() + "\r\n";
                    errInfo += "当前行:" + CodeHelper.GetLineNum().ToString() + "\r\n";
                    errInfo += "调用函数异常:" + "public static DataTable GetAllDwByDwbm(string dwbm, out string errmsg);" + "\r\n";
                    errInfo += "传入参数:" + dwbm.ToString() + "\r\n";
                    errInfo += errmsg;
                    Logger.Error("获取单位失败", errInfo);
                    context.Response.Write(errInfo);
                }
            }
            catch (Exception e)
            {
                context.Response.Write(e.Message);
            }
            finally
            {
                context.Response.End();
            }
        }

        private void GetGnflByDwbm(HttpContext context)
        {
            string dwbm = UserInfo.DWBM;
            string strJson = string.Empty;
            //string dwbm = "440000";
            string errmsg = string.Empty;
            DataTable dt = ZzjgManage.GetGnfl(dwbm, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
            {
                /*
                 * 记录日志
                 */
                string errInfo = string.Empty;
                errInfo += "当前文件:" + CodeHelper.GetCurSourceFileName() + "\r\n";
                errInfo += "当前行:" + CodeHelper.GetLineNum().ToString() + "\r\n";
                errInfo += "调用函数异常:" + "public static DataTable GetGnfl(string dwbm, out string errmsg);" + "\r\n";
                errInfo += "参数信息:" + dwbm.ToString() + "\r\n";
                errInfo += errmsg;
                Logger.Error("获取功能分类失败", errInfo, UserInfo);
            }
            else
            {
                //处理表的编码字段
                // dt = DataProHelper.ProBMAddOne(dt, "BM");
                strJson = dt.ToComboxJson();
            }
            context.Response.Write(strJson);
        }

        private void GetGnmcByflb(HttpContext context)
        {
            string dwbm = UserInfo.DWBM;
            string errmsg = string.Empty;
            //string dwbm = "440000";
            string gnfl = context.Request.Params["gnfl"];
            gnfl = DataProHelper.ProBmSubOne(gnfl);
            DataTable dt = null;
            if (!string.IsNullOrEmpty(gnfl))
            {
                dt = ZzjgManage.GetGnmcByGnfl(dwbm, gnfl, out errmsg);
            }
            if (string.IsNullOrEmpty(errmsg) && dt != null)
            {
                dt = DataProHelper.ProBMAddOne(dt, "bm");
                string strJson = Cyvation.CCQE.Common.JsonHelper.ToComboxJson(dt);
                context.Response.Write(strJson);
            }
            else
            {
                /*
                 * 记录日志
                 */
                string errInfo = string.Empty;
                errInfo += "当前文件:" + CodeHelper.GetCurSourceFileName() + "\r\n";
                errInfo += "当前行:" + CodeHelper.GetLineNum().ToString() + "\r\n";
                errInfo += "调用函数异常:" + "public static DataTable GetGnmcByGnfl(string dwbm, string gnfl, out string errmsg);" + "\r\n";
                errInfo += "传入参数:" + dwbm.ToString() + ";" + gnfl.ToString() + "\r\n";
                errInfo += errmsg;
                Logger.Error("获取功能名称失败", errInfo, UserInfo);
            }
        }

        private void GetJSInfoByDwBm(HttpContext context)
        {
            string dwbm = context.Request.Params["dwbm"];
            string bmbm = context.Request.Params["bmbm"];
            bmbm = DataProHelper.ProBmSubOne(bmbm);
            context.Response.Write(GetJsonData(dwbm, bmbm));

        }

        /// <summary>
        /// 获取Json数据
        /// </summary>
        /// <param name="dwbm"></param>
        /// <param name="bmbm"></param>
        /// <returns></returns>
        string GetJsonData(string dwbm, string bmbm)
        {
            string strJson = string.Empty;
            string errmsg = string.Empty;
            DataTable dt = ZzjgManage.GetJsInfoByDWBM(dwbm, bmbm, out errmsg);
            if (dt != null && string.IsNullOrEmpty(errmsg))
            {
                dt = DataProHelper.ProBMAddOne(dt, "BM");
                strJson = dt.ToComboxJson();
            }
            else
            {
                /*
                 * 记录日志
                 */
                string errInfo = string.Empty;
                errInfo += "当前文件:" + CodeHelper.GetCurSourceFileName() + "\r\n";
                errInfo += "当前行:" + CodeHelper.GetLineNum().ToString() + "\r\n";
                errInfo += "调用函数异常:" + "public static DataTable GetJsInfoByDWBM(string dwbm, string bmbm, out string errmsg)" + "\r\n";
                errInfo += "传入参数:" + dwbm.ToString() + ";" + bmbm.ToString() + "\r\n";
                errInfo += errmsg;
                Logger.Error("获取角色信息发生错误", errInfo, UserInfo);
            }
            return strJson;
        }

        /// <summary>
        /// 处理编码
        /// </summary>
        /// <param name="bm"></param>
        /// <returns></returns>
        string ProBm(string bm)
        {
            if (string.IsNullOrEmpty(bm) && bm.Length == 1)
                return "";
            return bm.Substring(1);
        }

        private void GetJsQxInfo(HttpContext context)
        {
            base.ProcessRequest(context);
            if (UserInfo == null) return;
            string result = string.Empty;
            string dwbm = context.Request.Params["dwbm"];
            string bmbm = context.Request.Params["bmbm"];
            bmbm = DataProHelper.ProBmSubOne(bmbm);
            string jsbm = context.Request.Params["jsbm"];
            jsbm = DataProHelper.ProBmSubOne(jsbm);
            int pageindex = Convert.ToInt32(context.Request.Params["page"]);
            int rows = Convert.ToInt32(context.Request.Params["rows"]);
            result = LoadJsGnData(context, dwbm, bmbm, jsbm, rows, pageindex);

            context.Response.Write(result);
            context.Response.End();
        }

        string LoadJsGnData(HttpContext context, string dwbm, string bmbm, string jsbm, int pageSize, int pageIndex)
        {
            int count = 0;
            string err = string.Empty;
            string strJson = string.Empty;
            DataTable dt = ZzjgManage.GetJsQx(dwbm, bmbm, jsbm, pageSize, pageIndex, out count, out err);

            if (!string.IsNullOrEmpty(err) || dt == null)
            {
                /*
                 * 记录日志
                 */
                string errInfo = string.Empty;
                errInfo += "当前文件:" + CodeHelper.GetCurSourceFileName() + "\r\n";
                errInfo += "当前行:" + CodeHelper.GetLineNum().ToString() + "\r\n";
                errInfo += "调用函数异常:" + "public static DataTable GetJsQx(string dwbm, string bmbm, string jsbm,int pagesize,int pageindex,out int count, out string errmsg)" + "\r\n";
                errInfo += "传入参数:" + dwbm.ToString() + ";" + bmbm.ToString() + ";" + jsbm.ToString() + ";"
                            + pageSize.ToString() + ";" + pageIndex.ToString() + "\r\n";
                errInfo += err;
                Logger.Error("获取角色功能信息发生错误", errInfo, UserInfo);
            }

            string retValue = string.Empty;

            if (!string.IsNullOrEmpty(err))
            {
                OperateLog.AddLog(OperateLog.LogType.角色权限管理Web, "查询角色已分配的功能列表失败：" + err, UserInfo, UserRole, context.Request);
                retValue = err;
            }
            else
            {
                OperateLog.AddLog(OperateLog.LogType.角色权限管理Web, "查询角色已分配的功能列表成功！", UserInfo, UserRole, context.Request);
                List<GndyModel> ghList = new List<GndyModel>();
                retValue = "[";
                foreach (DataRow dr in dt.Rows)
                {
                    GndyModel gn = new GndyModel();
                    foreach (DataColumn c in dt.Columns)
                    {
                        Type type = gn.GetType();
                        foreach (var p in type.GetProperties())
                        {
                            if (p.Name.ToUpper() == c.ColumnName.ToUpper())
                            {
                                p.SetValue(gn, dr[c.ColumnName] == DBNull.Value ? "" : c.DataType == typeof(System.DateTime) ? Convert.ToDateTime(dr[c.ColumnName]).ToString("yyyy年MM月dd日") : Convert.ToString(dr[c.ColumnName]), null);
                                continue;
                            }
                        }
                    }

                    ghList.Add(gn);
                }

                Hashtable hash = new Hashtable();
                //hash.Add("total", count);
                //hash.Add("rows", ghList);
                //2015年7月2日 更改LigerUI数据格式
                hash.Add("Rows", ghList);
                retValue = JsonMapper.ToJson(hash);
                //retValue = "{\"total\":" + count + ",\"rows\":" + dt.ToDatagridJson() + "}";
            }
            return retValue;
        }

        private void GetRyInfo(HttpContext context)
        {
            string result = string.Empty;
            ParamRyFilter param = new ParamRyFilter();
            param.DWBM = context.Request.Params["dwbm"];
            string bmbm = context.Request.Params["bmbm"];
            param.BMBM = DataProHelper.ProBmSubOne(bmbm);
            string jsbm = context.Request.Params["jsbm"];
            param.JSBM = DataProHelper.ProBmSubOne(jsbm);
            param.GH = context.Request.Params["gh"];
            param.GZZH = context.Request.Params["gzzh"];
            param.XM = context.Server.UrlDecode(context.Request.Params["xm"]);
            param.PageIndex = Convert.ToInt32(context.Request.Params["page"]);
            param.PageSize = Convert.ToInt32(context.Request.Params["rows"]);
            result = LoadRyData(context, param);

            context.Response.Write(result);
            context.Response.End();
        }

        /// <summary>
        /// 加载grid数据
        /// </summary>
        /// <param name="context"></param>
        /// <param name="dwbm"></param>
        /// <param name="bmbm"></param>
        /// <param name="xm"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private string LoadRyData(HttpContext context, ParamRyFilter param)
        {
            string err = string.Empty;
            string strJson = string.Empty;
            DataTable dt = ZzjgManage.GetRyInfo(param, out err);

            if (!string.IsNullOrEmpty(err) || dt == null)
            {
                /*
                 * 记录日志
                 */
                string errInfo = string.Empty;
                errInfo += "当前文件:" + CodeHelper.GetCurSourceFileName() + "\r\n";
                errInfo += "当前行:" + CodeHelper.GetLineNum().ToString() + "\r\n";
                errInfo += "调用函数异常:" + "public static DataTable GetRyInfo(string dwbm,string bmbm,string jsbm, string gh,string gzzh, string xm, int pageSize, int pageIndex, out int count, out string err)" + "\r\n";
                errInfo += "传入参数:" + param.DWBM + ";" + param.BMBM + ";" + param.JSBM + ";"
                        + param.GH + ";" + param.GZZH + ";" + param.XM + ";" + param.PageSize
                        + ";" + param.PageIndex + "\r\n";
                errInfo += err;
                Logger.Error("获取人员信息发生错误", errInfo, UserInfo);
            }

            string retValue = string.Empty;



            if (!string.IsNullOrEmpty(err))
            {
                retValue = err;
                OperateLog.AddLog(OperateLog.LogType.角色权限管理Web, "根据部门或单位查询人员列表失败：" + err, UserInfo, UserRole, context.Request);
            }
            else
            {
                OperateLog.AddLog(OperateLog.LogType.角色权限管理Web, "根据部门或单位查询人员列表成功！", UserInfo, UserRole, context.Request);
                List<RyxxModel> ryList = new List<RyxxModel>();
                retValue = "[";
                foreach (DataRow dr in dt.Rows)
                {
                    RyxxModel ry = new RyxxModel();
                    foreach (DataColumn c in dt.Columns)
                    {
                        Type type = ry.GetType();
                        foreach (var p in type.GetProperties())
                        {
                            if (p.Name.ToUpper() == c.ColumnName.ToUpper())
                            {
                                p.SetValue(ry, dr[c.ColumnName] == DBNull.Value ? "" : c.DataType == typeof(System.DateTime) ? Convert.ToDateTime(dr[c.ColumnName]).ToString("yyyy年MM月dd日") : Convert.ToString(dr[c.ColumnName]), null);
                                continue;
                            }
                        }
                    }

                    ryList.Add(ry);
                }

                Hashtable hash = new Hashtable();
                //hash.Add("total", param.Count);
                //hash.Add("rows", ryList);
                //2015年7月2日 修改LigerUI 数据格式
                hash.Add("Rows", ryList);
                retValue = JsonMapper.ToJson(hash);
                //retValue = "{\"total\":" + count + ",\"rows\":" + dt.ToDatagridJson() + "}";
            }
            return retValue;
        }

        string ToGridDataJson(DataTable dt)
        {
            StringBuilder sbJson = new StringBuilder();
            int count = dt.Rows.Count;
            sbJson.Append("{\"total\":" + count + ",");
            sbJson.Append("\"rows\":[");
            for (int i = 0; i < count; i++)
            {
                sbJson.Append("{\"GH\":" + "\"" + dt.Rows[i]["GH"] + "\",");
                sbJson.Append("\"MC\":" + "\"" + dt.Rows[i]["MC"] + "\",");
                sbJson.Append("\"DLBM\":" + "\"" + dt.Rows[i]["DLBM"] + "\",");
                sbJson.Append("\"XB\":" + "\"" + dt.Rows[i]["XB"] + "\",");
                sbJson.Append("\"SFTZ\":" + "\"" + dt.Rows[i]["SFTZ"] + "\",");
                sbJson.Append("\"JSMC\":" + "\"" + dt.Rows[i]["JSMC"] + "\",");
                sbJson.Append("\"GZZH\":" + "\"" + dt.Rows[i]["GZZH"] + "\"");
                sbJson.Append("}");
                if (i < count - 1)
                {
                    sbJson.Append(",");
                }
            }
            sbJson.Append("]}");
            return sbJson.ToString();
        }

        /// <summary>
        /// 处理编码，首部去1
        /// </summary>
        /// <returns></returns>

        private void GetRyinfoByGh(HttpContext context)
        {
            string dwbm = context.Request.Params["dwbm"];
            string gh = context.Request.Params["gh"];
            string errmsg = string.Empty;
            DataTable dt = ZzjgManage.GetRyInfoByGh(dwbm, gh, out errmsg);
            context.Response.Write(dt.ToDatagridJson());
            return;
            if (string.IsNullOrEmpty(errmsg))
            {
                string data = FormatData(dt);
                context.Response.Write(data);
            }
            else
            {
                /*
                 * 记录日志
                 */
                //string errInfo = string.Empty;
                //errInfo += "当前文件:" + CodeHelper.GetCurSourceFileName() + "\r\n";
                //errInfo += "当前行:" + CodeHelper.GetLineNum().ToString() + "\r\n";
                //errInfo += "调用函数异常:" + "public static DataTable GetRyInfoByGh(string dwbm, string gh, out string errmsg);" + "\r\n";
                //errInfo += "传入参数:" + dwbm.ToString() + ";" + gh.ToString() + "\r\n";
                //errInfo += errmsg;
                //Logger.Error("查询人员信息失败", errInfo, UserInfo);
            }
        }

        /// <summary>
        /// 格式化封装数据
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        string FormatData(DataTable dt)
        {
            string data = string.Empty;
            if (dt == null || dt.Rows.Count < 1)
            {
                return null;
            }
            data += "gh:";
            data += dt.Rows[0]["gh"].ToString() + ",";
            data += "mc:";
            data += dt.Rows[0]["mc"].ToString() + ",";
            data += "dlbm:";
            data += dt.Rows[0]["dlbm"].ToString() + ",";
            data += "gzzh:";
            data += dt.Rows[0]["gzzh"].ToString() + ",";
            data += "xb:";
            data += dt.Rows[0]["xb"].ToString() + ",";
            data += "sflsry:";
            data += dt.Rows[0]["sflsry"].ToString() + ",";
            data += "yddhhm:";
            data += dt.Rows[0]["yddhhm"].ToString() + ",";
            data += "dzyj:";
            data += dt.Rows[0]["dzyj"].ToString() + ",";
            data += "caid:";
            data += dt.Rows[0]["caid"].ToString();
            return data;
        }

        private void PassWord(HttpContext context)
        {
            string pass_old = context.Request.Params["pass_old"].Trim();
            string pass_new = context.Request.Params["pass_new"].Trim();
            pass_old = MD5Encrypt.getMd5Hash(pass_old);
            pass_new = MD5Encrypt.getMd5Hash(pass_new);
            string error = string.Empty;
            ZzjgManage.UpdatePassWord(UserInfo.DWBM, UserInfo.GH, pass_old, pass_new, out error);
            string retValue = string.Empty;
            if (!string.IsNullOrEmpty(error))
            {
                retValue = error;
            }
            else
            {
                retValue = "修改成功！";
            }

            context.Response.Write(retValue);
            context.Response.End();
        }

        private void QueryGnqx(HttpContext context)
        {
            string dwbm = UserInfo.DWBM;
            string result = string.Empty;
            string gnbm = context.Request.Params["gnbm"];
            gnbm = DataProHelper.ProBmSubOne(gnbm) ?? "";
            string gnmc = context.Server.UrlDecode(context.Request.Params["gnmc"]);
            string sslb = context.Request.Params["sslb"];
            //sslb = DataProHelper.ProBmSubOne(sslb); 
            int pageIndex = Convert.ToInt32(context.Request.Params["page"]);
            int pageSize = Convert.ToInt32(context.Request.Params["rows"]);
            result = LoadJsGnData(context, dwbm, gnbm, gnmc, sslb, pageSize, pageIndex);
            result = result.Replace("rows", "Rows");
            context.Response.Write(result);
            context.Response.End();
        }

        string LoadJsGnData(HttpContext context, string dwbm, string gnbm, string gnmc, string sslb, int pageSize, int pageIndex)
        {
            int count = 0;
            string err = string.Empty;
            string strJson = string.Empty;
            DataTable dt = ZzjgManage.QueryGnQx(dwbm, gnbm, gnmc, sslb, pageSize, pageIndex, out count, out err);

            if (!string.IsNullOrEmpty(err) || dt == null)
            {
                /*
                 * 记录日志
                 */
                string errInfo = string.Empty;
                errInfo += "当前文件:" + CodeHelper.GetCurSourceFileName() + "\r\n";
                errInfo += "当前行:" + CodeHelper.GetLineNum().ToString() + "\r\n";
                errInfo += "调用函数异常:" + "public static DataTable QueryGnQx(string dwbm,string gnbm, string gnmc, string sslb, int pagesize, int pageindex, out int count,out string errmsg);" + "\r\n";
                errInfo += "传入参数:" + dwbm.ToString() + ";" + gnbm.ToString() + ";" + gnmc.ToString() + ";"
                            + sslb.ToString() + ";" + pageSize.ToString() + ";" + pageIndex.ToString() + "\r\n";
                errInfo += err;
                Logger.Error("获取角色功能信息发生错误", errInfo, UserInfo);
            }

            string retValue = string.Empty;



            if (!string.IsNullOrEmpty(err))
            {
                OperateLog.AddLog(OperateLog.LogType.功能模块管理Web, "查询功能列表失败：" + err, UserInfo, UserRole, context.Request);
                retValue = err;
            }
            else
            {
                OperateLog.AddLog(OperateLog.LogType.功能模块管理Web, "查询功能列表成功！", UserInfo, UserRole, context.Request);
                //List<GndyModel> ghList = new List<GndyModel>();
                //retValue = "[";
                //foreach (DataRow dr in dt.Rows)
                //{
                //    GndyModel gn = new GndyModel();
                //    foreach (DataColumn c in dt.Columns)
                //    {
                //        Type type = gn.GetType();
                //        foreach (var p in type.GetProperties())
                //        {
                //            if (p.Name.ToUpper() == c.ColumnName.ToUpper())
                //            {
                //                p.SetValue(gn, dr[c.ColumnName] == DBNull.Value ? "" : c.DataType == typeof(System.DateTime) ? Convert.ToDateTime(dr[c.ColumnName]).ToString("yyyy年MM月dd日") : Convert.ToString(dr[c.ColumnName]), null);
                //                continue;
                //            }
                //        }
                //    }

                //    ghList.Add(gn);
                //}

                //Hashtable hash = new Hashtable();
                //hash.Add("total", count);
                //hash.Add("rows", dt);
                //retValue = JsonMapper.ToJson(hash);
                DataTable dt2 = dt.Clone();
                dt2.Columns["GNXH"].DataType = typeof(int);
                dt2 = dt.Copy();
                DataView dv = dt2.DefaultView;
                dv.Sort = "GNXH asc";
                dt2 = dv.ToTable();

                retValue = "{\"Total\":" + count + ",\"Rows\":" + EDRS.Common.JsonHelper.JsonString(dt2) + "}";
            }
            return retValue;
        }

        private void RemoveJob(HttpContext context)
        {
            string dwbm = context.Request.Params["dwbm"];
            string gh = context.Request.Params["gh"];
            string bmbm = context.Request.Params["bmbm"];
            string jsbm = context.Request.Params["jsbm"];

            string[] _gh = gh.Split(',');
            string[] _bmbm = bmbm.Split(',');
            string[] _jsbm = jsbm.Split(',');
            string errmsg = string.Empty;
            int errorCount = 0;
            JsonResult returnValue = new JsonResult();
            returnValue.isTrue = true;
            for (int i = 0; i < _gh.Length; i++)
            {
                string __bmbm = DataProHelper.ProBmSubOne(_bmbm[i]);
                string __jsbm = DataProHelper.ProBmSubOne(_jsbm[i]);
                string __gh = _gh[i];


                bool isSuc = ZzjgManage.RemoveJob(dwbm, __bmbm, __jsbm, __gh, out errmsg);
                if (isSuc && string.IsNullOrEmpty(errmsg))
                {
                    OperateLog.AddLog(OperateLog.LogType.人员角色分配Web, "调岗成功！" + errmsg, "角色编码：" + __bmbm + " | 部门：" + __bmbm + " | 工号：" + __gh, UserInfo, UserRole, context.Request);
                    //context.Response.Write("操作成功");
                }
                else
                {
                    returnValue.isTrue = false;
                    errorCount++;
                    /*
                     * 记录日志
                     */
                    string errInfo = string.Empty;
                    errInfo += "当前文件:" + CodeHelper.GetCurSourceFileName() + "\r\n";
                    errInfo += "当前行:" + CodeHelper.GetLineNum().ToString() + "\r\n";
                    errInfo += "调用函数异常:" + "public static bool RemoveJob(string dwbm, string bmbm, string jsbm, string gh,out string errmsg);" + "\r\n";
                    errInfo += "传入参数:" + dwbm.ToString() + ";" + bmbm.ToString() + ";" + jsbm.ToString() + ";"
                            + gh.ToString() + "\r\n";
                    errInfo += errmsg;
                    Logger.Error("调岗失败", errInfo, UserInfo);
                    OperateLog.AddLog(OperateLog.LogType.人员角色分配Web, "调岗失败：" + errmsg, "角色编码：" + __jsbm + " | 部门：" + __bmbm + " | 工号：" + __gh, UserInfo, UserRole, context.Request);
                }
            }
            returnValue.errorMsg = "成功调岗" + (_bmbm.Length - errorCount).ToString() + "人，失败" + errorCount.ToString() + "人！";
            context.Response.Write(returnValue.ToJsonString());
        }

        private void GetRyList(HttpContext context)
        {
            string err = string.Empty;
            int count = 0;
            #region 获取参数
            string dwbm = context.Request.Params["dwbm"].Trim();//UserInfo.DWBM;
            string xm = context.Request.Params["xm"].Trim();
            string gh = context.Request.Params["gh"].Trim();

            int pageindex = Convert.ToInt32(context.Request.Params["page"]);
            int rows = Convert.ToInt32(context.Request.Params["rows"]);
            #endregion

            StringBuilder strWhere = new StringBuilder();
            if (!string.IsNullOrEmpty(dwbm))
                strWhere.AppendFormat(" and dwbm in({0})", dwbm.Replace(";", ","));
            if (!string.IsNullOrEmpty(xm))
                strWhere.AppendFormat(" and mc like '%{0}%'", xm);
            if (!string.IsNullOrEmpty(gh))
                strWhere.AppendFormat(" and GZZH like '%{0}%'", gh);

            DataTable dt = ZzjgManage.GetRyList(this.UserInfo.DWBM, this.UserInfo.GH, strWhere.ToString(), rows, pageindex, out count, out err);

            string retValue = string.Empty;



            if (!string.IsNullOrEmpty(err))
            {
                OperateLog.AddLog(OperateLog.LogType.人员管理Web, "查询人员列表数据失败：" + err, UserInfo, UserRole, context.Request);
                retValue = err;
            }
            else
            {
                OperateLog.AddLog(OperateLog.LogType.人员管理Web, "查询人员列表数据成功！" + err, UserInfo, UserRole, context.Request);
                //"{\"total\":" + count + ",\"rows\":" + dt.ToDatagridJson() + "}";
                retValue = "{\"Rows\":" + dt.ToDatagridJson() + "}";
            }
            context.Response.Write(retValue);
            context.Response.End();
        }

        //获取尚未在该角色上分配的人员
        private void GetWfpRyInfo(HttpContext context)
        {
            string err = string.Empty;
            int count = 0;
            #region 获取参数
            string dwbm = context.Request.Params["dwbm"];
            string xm = context.Server.UrlDecode(context.Request.Params["xm"]);
            string gh = context.Request.Params["gh"].Trim();
            string jsbm = context.Request.Params["jsbm"].Trim();
            jsbm = DataProHelper.ProBmSubOne(jsbm);
            string bmbm = context.Request.Params["bmbm"].Trim();
            bmbm = DataProHelper.ProBmSubOne(bmbm);

            int pageindex = Convert.ToInt32(context.Request.Params["page"]);
            int rows = Convert.ToInt32(context.Request.Params["pagesize"]);
            #endregion

            DataTable dt = ZzjgManage.GetWfpRyInfo(dwbm, gh, xm, jsbm, bmbm, rows, pageindex, out count, out err);

            string retValue = string.Empty;



            if (!string.IsNullOrEmpty(err))
            {
                OperateLog.AddLog(OperateLog.LogType.角色权限管理Web, "查询未分配角色的人员列表失败：" + err, UserInfo, UserRole, context.Request);
                retValue = err;
            }
            else
            {
                OperateLog.AddLog(OperateLog.LogType.角色权限管理Web, "查询未分配角色的人员列表成功！", UserInfo, UserRole, context.Request);
                //retValue = "{\"total\":" + count + ",\"rows\":" + dt.ToDatagridJson() + "}";
                //2015年7月2日 更改LigerUI 数据格式
                retValue = "{\"Rows\":" + dt.ToDatagridJson() + ",\"Total\":" + count + "}";
            }
            context.Response.Write(retValue);
            context.Response.End();
        }

        private void GetDwInfo(HttpContext context)
        {
            string err = string.Empty;
            string dwbm = context.Request.Params["dwbm"];
            DataTable dt = ZzjgManage.GetDwInfo(dwbm, out err);
            if (!string.IsNullOrEmpty(err) || dt == null)
            {
                string errInfo = string.Empty;
                errInfo += "当前时间:" + DateTime.Now.ToShortTimeString();
                errInfo += "当前文件:" + CodeHelper.GetCurSourceFileName() + "\r\n";
                errInfo += "当前行:" + CodeHelper.GetLineNum().ToString() + "\r\n";
                errInfo += "调用函数异常:" + "ZzjgDal.GetDwInfo(dwbm, out err); " + "\r\n";
                errInfo += err;
                Logger.Error(errInfo, "获取单位信息发生错误", UserInfo);
                return;
            }
            string data = string.Empty;
            data = dt.ToDatagridJson();
            //2015年7月2日 修改LigerUI 数据格式
            //data += "dwmc:";
            //data += dt.Rows[0]["DWMC"].ToString() + ",";
            //data += "dwjc:";
            //data += dt.Rows[0]["DWJC"].ToString() + ",";
            //data += "dwjb:";
            //data += dt.Rows[0]["DWJB"].ToString();
            context.Response.Write(data);
        }

        private void GetBmInfo(HttpContext context)
        {
            string err = string.Empty;
            string dwbm = context.Request.Params["dwbm"];
            string bmbm = context.Request.Params["bmbm"];
            string bmmc = context.Request.Params["bmmc"];
            if (!string.IsNullOrEmpty(bmbm))
            {
                bmbm = DataProHelper.ProBmSubOne(bmbm);
                string errmsg = string.Empty;
                DataTable dt = ZzjgManage.GetBmInfo(dwbm, bmbm, out errmsg);
                if (!string.IsNullOrEmpty(errmsg) || dt == null || dt.Rows.Count == 0)
                {
                    /*
                     * 处理错误信息
                     */
                    string errInfo = string.Empty;
                    errInfo += "当前时间:" + DateTime.Now.ToShortTimeString();
                    errInfo += "当前文件:" + CodeHelper.GetCurSourceFileName() + "\r\n";
                    errInfo += "当前行:" + CodeHelper.GetLineNum().ToString() + "\r\n";
                    errInfo += "调用函数异常:" + "ZzjgDal.GetBmxh(dwbm, currentBm,out errmsg);" + "\r\n";
                    errInfo += errmsg;
                    Logger.Error(errInfo, "获取部门信息发生错误", UserInfo);
                }
                else
                {
                    //bmjc_ZzjgSelectBm = dt.Rows[0]["BMJC"].ToString();
                    //bmxh_ZzjgSelectBm = Convert.ToInt32(dt.Rows[0]["BMXH"]);
                    //bz_ZzjgSelectBm = dt.Rows[0]["BZ"].ToString();
                    BmbmModel bmInfo = ModelHandler.FillModel<BmbmModel>(dt.Rows[0]);
                    context.Response.Write(JsonMapper.ToJson(bmInfo));
                }
            }
        }

        private void GetJsxh(HttpContext context)
        {
            string dwbm = context.Request.Params["dwbm"];
            string bmbm = context.Request.Params["bmbm"];
            bmbm = DataProHelper.ProBmSubOne(bmbm);
            string jsbm = context.Request.Params["jsbm"];
            jsbm = DataProHelper.ProBmSubOne(jsbm);
            string errmsg = string.Empty;

            try
            {
                DataTable dt = ZzjgManage.GetJsxh(dwbm, bmbm, jsbm, out errmsg);
                if (dt != null && dt.Rows.Count == 1)
                {
                    context.Response.Write(EDRS.Common.JsonHelper.JsonString(dt));
                    context.Response.End();
                }
            }
            catch (Exception e)
            {
                /*
                 * 记录日志
                 */
                string errInfo = string.Empty;
                errInfo += "当前时间:" + DateTime.Now.ToShortTimeString();
                errInfo += "当前文件:" + CodeHelper.GetCurSourceFileName() + "\r\n";
                errInfo += "当前行:" + CodeHelper.GetLineNum().ToString() + "\r\n";
                errInfo += "调用函数异常:" + "ZzjgDal.GetJsxh(dwbm, bmbm, jsbm,out errmsg);" + "\r\n";
                errInfo += errmsg + e.Message;
                //Logger.Error(errInfo, "添加部门失败", UserInfo);
                LogManagerExtention.GetLogger(this.GetType()).Error(UserInfo.DWBM, UserInfo.GH, "获取角色序号失败", errInfo);
            }
            context.Response.Write(errmsg);
        }

        #region 获取角色未分配功能
        private void GetWfpgnTreeData(HttpContext context)
        {
            string strErr = string.Empty;
            DataTable dt = new DataTable();
            JsgnfpModel jsfp = new JsgnfpModel();
            jsfp.DWBM = context.Request.Params["dwbm"];
            string bmbm = context.Request.Params["bmbm"];
            jsfp.BMBM = DataProHelper.ProBmSubOne(bmbm);
            string jsbm = context.Request.Params["jsbm"];
            jsfp.JSBM = DataProHelper.ProBmSubOne(jsbm);
            dt = ZzjgManage.GetJsGnfp(jsfp, out strErr);
            if (string.IsNullOrEmpty(strErr) && dt != null && dt.Rows.Count > 0)
            {
                OperateLog.AddLog(OperateLog.LogType.角色权限管理Web, "查询角色未分配功能列表成功！", UserInfo, UserRole, context.Request);


                context.Response.Write(dt.ToTreeJsonAll("BM", "FBM", "MC"));
            }
            else if (!string.IsNullOrEmpty(strErr))
            {
                OperateLog.AddLog(OperateLog.LogType.角色权限管理Web, "查询角色未分配功能列表失败：" + strErr, UserInfo, UserRole, context.Request);
                context.Response.Write("获取角色未分配功能数据失败：" + strErr);
            }
            else
            {
                context.Response.Write("未能获取到获取角色未分配功能数据！");
                OperateLog.AddLog(OperateLog.LogType.角色权限管理Web, "查询角色未分配功能列表失败：" + strErr, UserInfo, UserRole, context.Request);
            }
        }
        #endregion

        #region 修改角色上的功能参数
        private void UpdateJsGnCs(HttpContext context)
        {
            string strErr = string.Empty;
            DataTable dt = new DataTable();
            JsgnfpModel jsfp = new JsgnfpModel();
            jsfp.DWBM = context.Request.Params["dwbm"];
            string bmbm = context.Request.Params["bmbm"];
            jsfp.BMBM = DataProHelper.ProBmSubOne(bmbm);
            string jsbm = context.Request.Params["jsbm"];
            jsfp.JSBM = DataProHelper.ProBmSubOne(jsbm);
            jsfp.GNBM = context.Request.Params["gnbm"];
            string gnmc = context.Request.Params["gnmc"];
            jsfp.GNCS = context.Request.Params["gncs"];
            string errmsg = string.Empty;
            bool isSuc = ZzjgManage.UpdateGNCS(jsfp, out errmsg);
            if (isSuc && string.IsNullOrEmpty(errmsg))
            {
                context.Response.Write("操作成功");
                OperateLog.AddLog(OperateLog.LogType.角色权限管理Web, "添加角色功能权限参数成功！", gnmc, UserInfo, UserRole, context.Request);
            }
            else
            {
                OperateLog.AddLog(OperateLog.LogType.角色权限管理Web, "添加角色功能权限参数失败：" + errmsg, gnmc, UserInfo, UserRole, context.Request);
                /*
                 * 记录日志
                 */
                string errInfo = string.Empty;
                errInfo += "当前文件:" + CodeHelper.GetCurSourceFileName() + "\r\n";
                errInfo += "当前行:" + CodeHelper.GetLineNum().ToString() + "\r\n";
                errInfo += "调用函数异常:" + "public static bool UpdateGNCS(model_XT_ZZJG_JSGNFP gnfp,out string errmsg);" + "\r\n";
                errInfo += "传入参数:" + JsonMapper.ToJson(jsfp) + "\r\n";
                errInfo += errmsg;
                Logger.Error("修改角色上的功能参数失败", errInfo, UserInfo);
            }
        }
        #endregion

        #region 为角色绑定人员
        private void AddRYJSFP(HttpContext context)
        {
            string strErr = string.Empty;
            JsgnfpModel jsfp = new JsgnfpModel();
            jsfp.DWBM = context.Request.Params["dwbm"];
            string bmbm = context.Request.Params["bmbm"];
            jsfp.BMBM = DataProHelper.ProBmSubOne(bmbm);
            string jsbm = context.Request.Params["jsbm"];
            jsfp.JSBM = DataProHelper.ProBmSubOne(jsbm);
            string ghj = context.Request.Params["ghj"];
            ghj = ghj.TrimStart(',');
            string errmsg = string.Empty;
            bool isSuc = ZzjgManage.AddRYJSFP(jsfp, ghj, out errmsg);
            if (isSuc && string.IsNullOrEmpty(errmsg))
            {
                context.Response.Write("操作成功");
                OperateLog.AddLog(OperateLog.LogType.角色权限管理Web, "增加角色包含的人员成功！", "角色编码：" + jsfp.JSBM + " | 人员工号：" + ghj, UserInfo, UserRole, context.Request);
            }
            else
            {
                /*
                 * 记录日志
                 */
                OperateLog.AddLog(OperateLog.LogType.角色权限管理Web, "增加角色包含的人员失败：" + errmsg, "角色编码：" + jsfp.JSBM + " | 人员工号：" + ghj, UserInfo, UserRole, context.Request);
                string errInfo = string.Empty;
                errInfo += "当前文件:" + CodeHelper.GetCurSourceFileName() + "\r\n";
                errInfo += "当前行:" + CodeHelper.GetLineNum().ToString() + "\r\n";
                errInfo += "调用函数异常:" + "public static bool AddRYJSFP(model_XT_ZZJG_JSGNFP gnfp,out string errmsg);" + "\r\n";
                errInfo += "传入参数:" + JsonMapper.ToJson(jsfp) + "; ghj:" + ghj + "\r\n";
                errInfo += errmsg;
                EDRS.Common.LogHelper.LogError(context.Request, "Exception", errmsg, "private void AddRYJSFP(HttpContext context)", "Cyvation.CCQE.Web", "");
                Logger.Error("为角色绑定人员失败", errInfo, UserInfo);
            }
        }
        #endregion

        #region 重置密码
        private void ResetPwd(HttpContext context)
        {
            string strErr = string.Empty;
            string dwbm = context.Request.Params["dwbm"];
            string ghj = context.Request.Params["ghj"];
            string errmsg = string.Empty;
            bool isSuc = ZzjgManage.ResetPwd(dwbm, ghj, out errmsg);
            if (isSuc && string.IsNullOrEmpty(errmsg))
            {
                context.Response.Write("操作成功");
                OperateLog.AddLog(OperateLog.LogType.人员管理Web, "重置密码成功！" + errmsg, ghj, UserInfo, UserRole, context.Request);
            }
            else
            {
                /*
                 * 记录日志
                 */
                string errInfo = string.Empty;
                errInfo += "当前文件:" + CodeHelper.GetCurSourceFileName() + "\r\n";
                errInfo += "当前行:" + CodeHelper.GetLineNum().ToString() + "\r\n";
                errInfo += "调用函数异常:" + "public static bool ResetPwd(string dwbm, string ghj,out string errmsg);" + "\r\n";
                errInfo += "传入参数dwbm:" + UserInfo.DWBM + ",ghj:" + ghj + "\r\n";
                errInfo += errmsg;
                Logger.Error("重置密码失败", errInfo, UserInfo);
                OperateLog.AddLog(OperateLog.LogType.人员管理Web, "重置密码失败：" + errmsg, ghj, UserInfo, UserRole, context.Request);
            }
        }
        #endregion

        #region 获取人员已分配角色
        private void GetRyJsData(HttpContext context)
        {
            string strErr = string.Empty;
            DataTable dt = new DataTable();
            JsgnfpModel jsfp = new JsgnfpModel();
            jsfp.DWBM = context.Request.Params["dwbm"];
            jsfp.GH = context.Request.Params["gh"];
            dt = ZzjgManage.GetYhJsInfo(jsfp, out strErr);
            if (string.IsNullOrEmpty(strErr) && dt != null)
            {
                context.Response.Write(dt.ToTreeJson("BM", "FBM", "MC"));
            }
            else if (!string.IsNullOrEmpty(strErr))
            {
                context.Response.Write("获取人员已分配角色失败：" + strErr);
            }
        }
        #endregion

        #region 获取关系单位列表
        private void GetGxdwList(HttpContext context)
        {
            string strErr = string.Empty;
            DataTable dt = new DataTable();
            dt = ZzjgManage.GetGxdwList(UserInfo.DWBM, out strErr);
            if (string.IsNullOrEmpty(strErr) && dt != null)
            {
                context.Response.Write(dt.ToComboxJson());
            }
            else if (!string.IsNullOrEmpty(strErr))
            {
                context.Response.Write("获取关系单位列表失败：" + strErr);
            }
        }
        #endregion

        #region 修改密码
        private void UpdatePsword(HttpContext context)
        {
            string pass_old = context.Request.Params["pass_old"].Trim();
            string pass_new = context.Request.Params["pass_new"].Trim();
            pass_old = MD5Encrypt.getMd5Hash(pass_old);
            pass_new = MD5Encrypt.getMd5Hash(pass_new);
            string error = string.Empty;
            Hashtable hash = new Hashtable();
            ZzjgManage.UpdatePassWord(UserInfo.DWBM, UserInfo.GH, pass_old, pass_new, out error);
            string retValue = string.Empty;
            if (!string.IsNullOrEmpty(error))
            {
                hash.Add("result", "false");
                hash.Add("errmsg", error);
                context.Response.Write(JsonMapper.ToJson(hash));
            }
            else
            {
                hash.Add("result", "true");
                context.Response.Write(JsonMapper.ToJson(hash));
            }
            context.Response.End();
        }
        #endregion
        #endregion

        #region 新增的
        public void GetDWQX(HttpContext context)
        {
            string errmsg = "";
            string jsbm = context.Request.Params["jsbm"];

            string bmbm = context.Request.Params["bmbm"];
            string _dwbm = context.Request.Params["_dwbm"];//角色对应的单位编码
            string dwbm = context.Request.Params["dwbm"];//角色所有的单位的权限
            string key = context.Request.Params["key"];
            _dwbm = _dwbm.Replace("a", "");
            bmbm = bmbm.Replace("a", "");
            jsbm = jsbm.Replace("a", "");
            EDRS.BLL.XT_DM_QX bll = new EDRS.BLL.XT_DM_QX(context.Request);
            DataSet ds = bll.GetDwList(jsbm, _dwbm, bmbm, key);
            if (ds != null && ds.Tables.Count > 0 && string.IsNullOrEmpty(errmsg))
            {
                //获取json数据
                string json = "{\"Rows\":" + ds.Tables[0].ToDatagridJson() + "}";// EDRS.Common.JsonHelper.JsonString(ds.Tables[0]);
                context.Response.Write(json);
                OperateLog.AddLog(OperateLog.LogType.角色权限管理Web, "查询角色已分配的单位权限成功！", UserInfo, UserRole, context.Request);
            }
            else
            {
                /*
                 * 错误日志记录
                 */
                OperateLog.AddLog(OperateLog.LogType.角色权限管理Web, "查询角色已分配的单位权限失败：" + errmsg, UserInfo, UserRole, context.Request);
                string errInfo = string.Empty;
                errInfo += "当前文件:" + CodeHelper.GetCurSourceFileName() + "\r\n";
                errInfo += "当前行:" + CodeHelper.GetLineNum().ToString() + "\r\n";
                errInfo += "调用函数异常:" + "public DataSet GetDWQX(string jsbm); " + "\r\n";
                errInfo += "传入参数:" + jsbm + "\r\n";
                errInfo += errmsg;
                Logger.Error("获取单位部门角色发生错误", errInfo, UserInfo);
            }
        }



        public void GetLBQX(HttpContext context)
        {
            string errmsg = "";
            string jsbm = context.Request.Params["jsbm"];
            string bmbm = context.Request.Params["bmbm"];
            string _dwbm = context.Request.Params["_dwbm"];//角色对应的单位编码
            string dwbm = context.Request.Params["dwbm"];//角色所有的单位的权限
            string key = context.Request.Params["key"];
            _dwbm = _dwbm.Replace("a", "");
            bmbm = bmbm.Replace("a", ""); ;
            jsbm = jsbm.Replace("a", "");
            EDRS.BLL.XT_DM_QX bll = new EDRS.BLL.XT_DM_QX(context.Request);
            DataSet ds = bll.GetLBList(jsbm, _dwbm, bmbm, key);
            if (ds != null && ds.Tables.Count > 0 && string.IsNullOrEmpty(errmsg))
            {
                //获取json数据
                string json = "{\"Rows\":" + ds.Tables[0].ToDatagridJson() + "}"; // EDRS.Common.JsonHelper.JsonString(ds.Tables[0]);
                context.Response.Write(json);
                OperateLog.AddLog(OperateLog.LogType.角色权限管理Web, "查询角色已分配的" + ((VersionName)0).ToString() + "类别成功！", UserInfo, UserRole, context.Request);
            }
            else
            {
                OperateLog.AddLog(OperateLog.LogType.角色权限管理Web, "查询角色已分配的" + ((VersionName)0).ToString() + "类别失败：" + errmsg, UserInfo, UserRole, context.Request);
                /*
                 * 错误日志记录
                 */
                string errInfo = string.Empty;
                errInfo += "当前文件:" + CodeHelper.GetCurSourceFileName() + "\r\n";
                errInfo += "当前行:" + CodeHelper.GetLineNum().ToString() + "\r\n";
                errInfo += "调用函数异常:" + "public DataSet GetLBQX(string jsbm); " + "\r\n";
                errInfo += "传入参数:" + jsbm.ToString() + "\r\n";
                errInfo += errmsg;
                Logger.Error("获取单位部门角色发生错误", errInfo, UserInfo);
            }
        }




        private void ADDDWQX(HttpContext context)
        {
            try
            {
                string jsonText = context.Request.Params["jsonText"];//角色所有的待添加的权限
                jsonText = jsonText.Replace("QXBM", "ID").Replace("QXMC", "Text");

                string _bmbm = context.Request.Params["_bmbm"];//角色对应的部门编码
                string _jsbm = context.Request.Params["_jsbm"];//角色对应的角色编码
                string _dwbm = context.Request.Params["_dwbm"];//角色对应的单位编码
                _dwbm = _dwbm.Replace("a", "");
                _bmbm = _bmbm.Replace("a", ""); ;
                _jsbm = _jsbm.Replace("a", "");
                EDRS.Model.XT_DM_QX model = new EDRS.Model.XT_DM_QX();
                EDRS.BLL.XT_DM_QX bll = new EDRS.BLL.XT_DM_QX(context.Request);
                JavaScriptSerializer jsonH = new JavaScriptSerializer();
                List<EDRS.Model.XT_DM_QX> list = jsonH.Deserialize(jsonText, typeof(List<EDRS.Model.XT_DM_QX>)) as List<EDRS.Model.XT_DM_QX>;
                bool result = bll.AddDW(list, _jsbm, _dwbm, _bmbm);
                JsonResult resultJson = new JsonResult();
                resultJson.isTrue = result;
                resultJson.errorMsg = resultJson.isTrue ? "操作成功！" : "操作失败，请稍后重试！";
                context.Response.Write(resultJson.ToJsonString());

                string tags = "角色编码：" + _jsbm;// +" |  单位编码：";
                //foreach (EDRS.Model.XT_DM_QX qx in list)
                //{
                //    tags += "[" + qx.ID + "," + qx.Text + "]";
                //}
                if (result)
                {
                    OperateLog.AddLog(OperateLog.LogType.角色权限管理Web, "删除角色单位权限成功！", tags, UserInfo, UserRole, context.Request);
                }
                else
                {
                    OperateLog.AddLog(OperateLog.LogType.角色权限管理Web, "删除角色单位权限失败！", tags, UserInfo, UserRole, context.Request);
                }
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(null, "", ex.Message, "ADDDWQX", "ZZJGHandler");
            }
        }





        private void DELDWQX(HttpContext context)
        {
            string jsonText = context.Request.Params["jsonText"];//角色所有的待删除的权限
            jsonText = jsonText.Replace("qxbm", "ID").Replace("qxmc", "Text");

            string _bmbm = context.Request.Params["_bmbm"];//角色对应的部门编码
            string _jsbm = context.Request.Params["_jsbm"];//角色对应的角色编码
            string _dwbm = context.Request.Params["_dwbm"];//角色对应的单位编码
            _dwbm = _dwbm.Replace("a", "");
            _bmbm = _bmbm.Replace("a", ""); ;
            _jsbm = _jsbm.Replace("a", "");

            EDRS.BLL.XT_DM_QX bll = new EDRS.BLL.XT_DM_QX(context.Request);
            JavaScriptSerializer jsonH = new JavaScriptSerializer();
            List<EDRS.Model.XT_DM_QX> list = jsonH.Deserialize(jsonText, typeof(List<EDRS.Model.XT_DM_QX>)) as List<EDRS.Model.XT_DM_QX>;

            bool result = bll.DelDW(list, _jsbm, _dwbm, _bmbm);
            JsonResult resultJson = new JsonResult();
            resultJson.isTrue = result;
            resultJson.errorMsg = resultJson.isTrue ? "操作成功！" : "操作失败，请稍后重试！";
            context.Response.Write(resultJson.ToJsonString());

            string tags = "角色编码：" + _jsbm;
            //foreach (EDRS.Model.XT_DM_QX qx in list)
            //{
            //    tags += "[" + qx.ID + "," + qx.Text + "]";
            //}
            if (result)
            {
                OperateLog.AddLog(OperateLog.LogType.角色权限管理Web, "删除角色单位权限成功！", tags, UserInfo, UserRole, context.Request);
            }
            else
            {
                OperateLog.AddLog(OperateLog.LogType.角色权限管理Web, "删除角色单位权限失败！", tags, UserInfo, UserRole, context.Request);
            }
        }



        private void ADDLBQX(HttpContext context)
        {
            string jsonText = context.Request.Params["jsonText"];//角色所有的待添加的权限
            jsonText = jsonText.Replace("qxbm", "ID").Replace("qxmc", "Text").Replace("ajlbbm", "ID").Replace("ajlbmc", "Text");

            string _bmbm = context.Request.Params["_bmbm"];//角色对应的部门编码
            string _jsbm = context.Request.Params["_jsbm"];//角色对应的角色编码
            string _dwbm = context.Request.Params["_dwbm"];//角色对应的单位编码
            _dwbm = _dwbm.Replace("a", "");
            _bmbm = _bmbm.Replace("a", ""); ;
            _jsbm = _jsbm.Replace("a", "");

            EDRS.BLL.XT_DM_QX bll = new EDRS.BLL.XT_DM_QX(context.Request);
            JavaScriptSerializer jsonH = new JavaScriptSerializer();
            List<EDRS.Model.XT_DM_QX> list = jsonH.Deserialize(jsonText, typeof(List<EDRS.Model.XT_DM_QX>)) as List<EDRS.Model.XT_DM_QX>;

            bool result = bll.AddLB(list, _jsbm, _dwbm, _bmbm);
            JsonResult resultJson = new JsonResult();
            resultJson.isTrue = result;
            resultJson.errorMsg = resultJson.isTrue ? "操作成功！" : "操作失败，请稍后重试！";
            string tags = "角色编码：" + _jsbm;// +" |  类别编码：";
            //foreach (EDRS.Model.XT_DM_QX qx in list)
            //{
            //    tags += "[" + qx.ID + "," + qx.Text + "]";
            //}
            context.Response.Write(resultJson.ToJsonString());
            if (result)
            {
                OperateLog.AddLog(OperateLog.LogType.角色权限管理Web, "添加角色" + ((VersionName)0).ToString() + "类别权限成功！", tags, UserInfo, UserRole, context.Request);
            }
            else
            {

                OperateLog.AddLog(OperateLog.LogType.角色权限管理Web, "添加角色" + ((VersionName)0).ToString() + "类别权限失败！", tags, UserInfo, UserRole, context.Request);
            }
        }



        private void DELLBQX(HttpContext context)
        {
            string jsonText = context.Request.Params["jsonText"];//角色所有的待删除的权限
            jsonText = jsonText.Replace("qxbm", "ID").Replace("qxmc", "Text");

            string _bmbm = context.Request.Params["_bmbm"];//角色对应的部门编码
            string _jsbm = context.Request.Params["_jsbm"];//角色对应的角色编码
            string _dwbm = context.Request.Params["_dwbm"];//角色对应的单位编码
            _dwbm = _dwbm.Replace("a", "");
            _bmbm = _bmbm.Replace("a", ""); ;
            _jsbm = _jsbm.Replace("a", "");

            EDRS.BLL.XT_DM_QX bll = new EDRS.BLL.XT_DM_QX(context.Request);
            JavaScriptSerializer jsonH = new JavaScriptSerializer();
            List<EDRS.Model.XT_DM_QX> list = jsonH.Deserialize(jsonText, typeof(List<EDRS.Model.XT_DM_QX>)) as List<EDRS.Model.XT_DM_QX>;

            bool result = bll.DelLB(list, _jsbm, _dwbm, _bmbm);
            JsonResult resultJson = new JsonResult();
            resultJson.isTrue = result;
            resultJson.errorMsg = resultJson.isTrue ? "操作成功！" : "操作失败，请稍后重试！";
            context.Response.Write(resultJson.ToJsonString());

            string tags = "角色编码：" + _jsbm;// +" |  类别编码：";
            //foreach (EDRS.Model.XT_DM_QX qx in list)
            //{
            //    tags += "[" + qx.ID + "," + qx.Text + "]";
            //}
            if (result)
            {
                OperateLog.AddLog(OperateLog.LogType.角色权限管理Web, "删除角色" + ((VersionName)0).ToString() + "类别权限成功！", tags, UserInfo, UserRole, context.Request);
            }
            else
            {
                OperateLog.AddLog(OperateLog.LogType.角色权限管理Web, "删除角色" + ((VersionName)0).ToString() + "类别权限失败！", tags, UserInfo, UserRole, context.Request);
            }
        }



        private void GetAJLB(HttpContext context)
        {
            string errmsg = "";
            string jsbm = context.Request.Params["jsbm"];
            string bmbm = context.Request.Params["bmbm"];
            string _dwbm = context.Request.Params["_dwbm"];//角色对应的单位编码
            string key = context.Request.Params["key"];
            _dwbm = _dwbm.Replace("a", "");
            bmbm = bmbm.Replace("a", ""); ;
            jsbm = jsbm.Replace("a", "");
            EDRS.BLL.XT_DM_QX bll = new EDRS.BLL.XT_DM_QX(context.Request);
            DataTable dt = bll.GetAJLBList(jsbm, _dwbm, bmbm, key);
            if (dt != null && string.IsNullOrEmpty(errmsg))
            {
                //获取json数据
                string json = "{\"Rows\":" + dt.ToDatagridJson() + "}"; //EDRS.Common.JsonHelper.JsonString(dt);
                context.Response.Write(json);
                OperateLog.AddLog(OperateLog.LogType.角色权限管理Web, "查询角色未分配的" + ((VersionName)0).ToString() + "类别成功！", UserInfo, UserRole, context.Request);
            }
            else
            {
                OperateLog.AddLog(OperateLog.LogType.角色权限管理Web, "查询角色未分配的" + ((VersionName)0).ToString() + "类别失败：" + errmsg, UserInfo, UserRole, context.Request);
                /*
                 * 错误日志记录
                 */
                string errInfo = string.Empty;
                errInfo += "当前文件:" + CodeHelper.GetCurSourceFileName() + "\r\n";
                errInfo += "当前行:" + CodeHelper.GetLineNum().ToString() + "\r\n";
                errInfo += "调用函数异常:" + "public static DataTable GetLBList(out string err); " + "\r\n";
                errInfo += "传入参数:" + "" + "\r\n";
                errInfo += errmsg;
                Logger.Error("获取单位部门角色发生错误", errInfo, UserInfo);
            }
        }



        private void GetAllDwBm(HttpContext context)
        {
            string jsbm = context.Request.Params["jsbm"];
            string bmbm = context.Request.Params["bmbm"];
            string _dwbm = context.Request.Params["_dwbm"];//角色对应的单位编码
            string dwbm = context.Request.Params["dwbm"];//角色所有的单位的权限

            _dwbm = _dwbm.Replace("a", "");
            bmbm = bmbm.Replace("a", "");
            jsbm = jsbm.Replace("a", "");
            //数据查询条件
            string where = string.Empty;
            object[] values = new object[4];
            where += " and SFSC=:SFSC AND DWBM not IN (SELECT distinct QXBM FROM xt_dm_qx WHERE trim(JSBM) = :JSBM AND TRIM(DWBM)=:DWBM AND TRIM(BMBM)=:BMBM)";
            values[0] = "N";
            values[1] = jsbm;
            values[2] = _dwbm;
            values[3] = bmbm;
            //树形循环条件
            bool direction = true;
            bool isOpen = false;
            try
            {
                EDRS.BLL.XT_DM_QX bll = new EDRS.BLL.XT_DM_QX(context.Request);
                DataSet ds = bll.GetTreeList(where, "", values.ToArray());
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    dt.Columns["DWBM"].ColumnName = "ID";
                    dt.Columns["FDWBM"].ColumnName = "PARENTID";
                    dt.Columns["DWMC"].ColumnName = "NAME";

                    context.Response.Write(dt.ToTreeJson("ID", "PARENTID", "NAME"));
                }

            }
            catch (Exception ex)
            {
            }
        }

        private void getAllLogList(HttpContext context)
        {

            string page = context.Request["page"];
            string rows = context.Request["pagesize"];
            int startIndex = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
            int endIndex = Convert.ToInt32(page) * Convert.ToInt32(rows);
            string order_key = context.Request["sortname"];
            string sortorder = context.Request["sortorder"];
            order_key = (order_key == null ? "" : order_key) + " " + (sortorder == null ? "" : sortorder);
            string bmmc = context.Request.Params["bmmc"];
            string dwmc = context.Request.Params["dwmc"];
            string rygh = context.Request.Params["rygh"];
            string czlx = context.Request.Params["czlx"];
            string czsj_start = context.Request.Params["czsj_start"];
            string czsj_end = context.Request.Params["czsj_end"];


            if (string.IsNullOrEmpty(czsj_start) || string.IsNullOrEmpty(czsj_end))
            {

                context.Response.Write(ReturnString.JsonToString(Prompt.error, "请必须选择时间段查询", null));
                return;
            }
            //单位权限
            //List<string> unitRoleList = GetDwBm(context, UserInfo.DWBM, Bmbms, Jsbms);
            //if (unitRoleList.Count == 0)
            //{

            //    context.Response.Write(ReturnString.JsonToString(Prompt.error, "未设置数据权限，请先到角色权限中设置单位权限", null));
            //    return;
            //}

            //数据查询条件
            string where = string.Empty;
            //  List<object> values = new List<object>();
            if (!string.IsNullOrEmpty(dwmc))
            {
                string dwbms = dwmc.Replace(";", ",");
                where += " and  DWBM in (" + StringPlus.ReplaceSingle(dwbms) + ")";
            }
            else
            {
                where += " and DWBM in (select distinct QXBM FROM XT_DM_QX where JSBM IN (" + Jsbms + ") AND QXLX=0 AND trim(DWBM) = '" + UserInfo.DWBM + "' AND BMBM in (" + Bmbms + ") )";
            }
            if (!string.IsNullOrEmpty(bmmc))
            {
                where += " and  BMMC like '%" + bmmc + "%'";
                //values.Add("%" + bmmc + "%");
            }
            if (!string.IsNullOrEmpty(rygh))
            {
                where += " and  TRIM(CZR) like '%" + rygh + "%'";
                //values.Add(rygh);
            }
            if (!string.IsNullOrEmpty(czlx))
            {
                string czlxbh = "";
                switch (czlx)
                {
                    case "1":
                        czlxbh = "4,10";
                        break;
                    case "2":
                        czlxbh = "1,2,3,20";
                        break;
                    case "3":
                        czlxbh = "5,21,22,23,24,25,26,27,28,29";
                        break;
                    case "4":
                        czlxbh = "11,12,13,14,15,16,17,18,19,30,31,32,34";
                        break;
                    case "99":
                        czlxbh = "99";
                        break;
                    default:
                        break;
                }

                where += " and  TRIM(CZLX) in (" + czlxbh + ")";
                //values.Add(czlx);
            }
            if (!string.IsNullOrEmpty(czsj_start))
            {
                where += " and CZSJ >= to_date('" + czsj_start + "','yyyy-MM-dd')";
                //values.Add(Convert.ToDateTime(czsj_start));
            }
            if (!string.IsNullOrEmpty(czsj_end))
            {
                DateTime dt = Convert.ToDateTime(czsj_end).AddDays(1);
                where += " and CZSJ <= to_date('" + dt.ToString("yyyy-MM-dd") + "','yyyy-MM-dd')";
                //values.Add(Convert.ToDateTime(czsj_end));
            }

            //if (unitRoleList.Count > 0)
            //{
            //    if (string.IsNullOrEmpty(dwmc))
            //    {
            //        where += " and DWBM IN (";
            //        for (int i = 0; i < unitRoleList.Count; i++)
            //        {
            //            if (i > 0)
            //            {
            //                where += ",";
            //            }
            //            where += "'" + unitRoleList[i] + "'";
            //        }
            //        where += ")";
            //    }
            //}
            //else
            //{
            //    where += " and (1=2)";
            //}


            //where += " and CZLX in (1,4)";
            try
            {
                int count = 0;
                //GetList
                EDRS.BLL.YX_DZJZ_JZRZJL bll = new EDRS.BLL.YX_DZJZ_JZRZJL(context.Request);
                //DataSet ds = bll.GetListByPage(where, order_key, startIndex, endIndex, values.ToArray());
                DateTime startTime;
                DateTime endTime;
                startTime = Convert.ToDateTime(czsj_start).AddMonths(-EDRS.Common.ConfigHelper.GetConfigInt("LogTime"));
                endTime = Convert.ToDateTime(czsj_end).AddMonths(EDRS.Common.ConfigHelper.GetConfigInt("LogTime")).AddDays(1);
                DataSet ds = bll.GetListByPageProc(startTime, endTime, where, order_key, startIndex, endIndex, ref count);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    //string jsonData = "{\"Rows\":" + dt.ToDatagridJson() + "}";
                    StringBuilder sbJson = new StringBuilder();
                    sbJson.Append("{");
                    sbJson.Append("\"Total\":" + count + ",\"Rows\":[");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            if (j == 0)
                            {
                                sbJson.Append("{");
                            }
                            string name = dt.Columns[j].ColumnName.ToLower();
                            string text = dt.Rows[i][j].ToString();
                            //过滤特殊字符
                            text = text.Replace("\"", "").Replace("[", "【").Replace("]", "】").Replace("\n", "").Replace("\r", "").Replace("\t", "");
                            if (name == "rznr")
                            {
                                //text = "";
                            }
                            sbJson.Append("\"" + name + "\":" + "\"" + text + "\"");
                            if (j == dt.Columns.Count - 1)
                            {
                                sbJson.Append("}");
                            }
                            else
                            {
                                sbJson.Append(",");
                            }
                        }
                        if (i < ds.Tables[0].Rows.Count - 1)
                        {
                            sbJson.Append(",");
                        }
                    }
                    sbJson.Append("]}");
                    string result = sbJson.ToString();
                    context.Response.Write(sbJson.ToString());
                }

            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// 获取单位权限
        /// </summary>
        /// <param name="dwbm"></param>
        /// <param name="bmbm"></param>
        /// <param name="jsbm"></param>
        /// <returns></returns>
        private List<string> GetDwBm(HttpContext context, string dwbm, string bmbm, string jsbm)
        {
            EDRS.BLL.XT_DM_QX bll = new EDRS.BLL.XT_DM_QX(context.Request);
            DataSet ds = bll.GetDwList(jsbm, dwbm, bmbm, "");
            List<string> list = new List<string>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow ro in ds.Tables[0].Rows)
                    list.Add(ro["QXBM"].ToString());
            }
            return list;
        }

        /// <summary>
        /// 根据ICE获取案件基本信息（赋权）
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private void RoleListBindIce(HttpContext context)
        {
            string page = context.Request["page"];
            string rows = context.Request["pagesize"];
            string dwbm = context.Request["dwbm"];
            dwbm = dwbm == "100000" ? "" : dwbm;//高检院不筛除
            string key = context.Request["key"];
            string casename = context.Request["casename"];
            string dutyman = context.Request["dutyman"];
            string relevance = context.Request["relevance"];
            if (relevance == "1")
                relevance = "Y";
            else if (relevance == "2")
                relevance = "N";
            else
                relevance = "";
            DateTime? timebegin = null;
            if (context.Request["timebegin"] != null && !string.IsNullOrEmpty(context.Request["timebegin"]))
                timebegin = Convert.ToDateTime(context.Request["timebegin"]);

            DateTime? timeend = null;
            if (context.Request["timeend"] != null && !string.IsNullOrEmpty(context.Request["timeend"]))
                timeend = Convert.ToDateTime(context.Request["timeend"]);
            string meg;
            int count;
            EDRS.Common.IceServicePrx iceprx = new IceServicePrx();

            DataTable dt = iceprx.AJJBXXJson("", null, null, dwbm, casename, timebegin, timeend, dutyman, relevance, int.Parse(rows), int.Parse(page), out count, out meg);
            string resultJson = "{\"Total\":" + count + ",\"Rows\":" + (dt == null ? "[]" : dt.ToDatagridJson()) + "}";
            context.Response.Write(resultJson);
        }
        private void GetUserRoleList(HttpContext context)
        {
            string gh = context.Request["GH"];
            string dwbm = context.Request["dwbm"];
            EDRS.BLL.XT_QX_RYJZQXFP bll = new EDRS.BLL.XT_QX_RYJZQXFP(context.Request);
            DataTable dt = bll.GetRyJzQxFpTable(dwbm, gh);
            string resultJson = dt.ToDatagridJson();
            context.Response.Write(resultJson);
        }

        
        /// <summary>
        /// 获取功能树
        /// </summary>
        /// <returns></returns>
        private void GetGnTreeList(HttpContext context)
        {
            string where = "";

            EDRS.BLL.XT_ZZJG_DWBM dmbll = new EDRS.BLL.XT_ZZJG_DWBM(context.Request);

            DataSet ds2 = dmbll.GetTreeList("", " DWBM=" + UserInfo.DWBM, false, null);
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 1)
                where += " and sfdjy='N' ";

            EDRS.BLL.XT_QX_GNDY bll = new EDRS.BLL.XT_QX_GNDY(context.Request);
            DataSet ds = bll.GetListByType(UserInfo.DWBM, where);

            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.ContentType = "application/json";
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                dt.Columns["FLBM"].ColumnName = "ID";
                dt.Columns["FFLBM"].ColumnName = "PARENTID";
                dt.Columns["FLMC"].ColumnName = "NAME";
                dt.Columns.Add("icon");
                foreach (DataRow dr in dt.Rows)
                {
                    if (string.IsNullOrEmpty(dr["PARENTID"].ToString()))
                        dr["icon"] = "picon";
                    else
                        dr["icon"] = "chicon";
                }
                string data = new TreeJson(dt, "ID", "NAME", "PARENTID", "", "", "", true, true).ResultJson.ToString();
                context.Response.Write(data);
                context.Response.End();
            }
            context.Response.Write(ReturnString.JsonToString(Prompt.error, "未找到功能", null));
            context.Response.End();

        }

        /// <summary>
        /// 获取按钮功能集合
        /// </summary>
        private void GetAnList(HttpContext context)
        {
            string dwbm = context.Request.Params["dwbm"];
            string bmbm = context.Request.Params["bmbm"];
            bmbm = DataProHelper.ProBmSubOne(bmbm);
            string jsbm = context.Request.Params["jsbm"];
            jsbm = DataProHelper.ProBmSubOne(jsbm);

            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.ContentType = "application/json";
            EDRS.BLL.XT_QX_ANDY bll = new EDRS.BLL.XT_QX_ANDY(context.Request);
            // a.anbm not in(select anbh from XT_QX_JSANQX where DWBM='440300000000' and BMBM='0000' and JSBM='000' ) 
            DataSet ds = bll.GetListByPage(" and anbm not in(select anbh from XT_QX_JSANQX where DWBM=:DWBM and BMBM=:BMBM and JSBM=:JSBM ) ", "GNXH", 1, 1000, new object[] { dwbm, bmbm, jsbm });
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                context.Response.Write(EDRS.Common.JsonHelper.JsonString(ds.Tables[0]));
                context.Response.End();
            }
            context.Response.Write(ReturnString.JsonToString(Prompt.error, "未找到按钮功能", null));
            context.Response.End();
        }

        /// <summary>
        /// 获取按钮功能权限集合
        /// </summary>
        /// <param name="context"></param>
        private void GetAnQxList(HttpContext context)
        {
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.ContentType = "application/json";
            string dwbm = context.Request.QueryString["dwbm"];
            string bmbm = context.Request.QueryString["bmbm"];
            bmbm = DataProHelper.ProBmSubOne(bmbm);
            string jsbm = context.Request.QueryString["jsbm"];
            jsbm = DataProHelper.ProBmSubOne(jsbm);
            

            if (string.IsNullOrEmpty(dwbm) || string.IsNullOrEmpty(bmbm) || string.IsNullOrEmpty(jsbm))
            {
                context.Response.Write(ReturnString.JsonToString(Prompt.error, "参数单位、部门或者角色编码不存在", null));
                context.Response.End();
            }

            EDRS.BLL.XT_QX_JSANQX bll = new EDRS.BLL.XT_QX_JSANQX(context.Request);
            DataSet ds = bll.GetListByPage(" and DWBM=:DWBM and BMBM=:BMBM and JSBM=:JSBM", "", 1, 1000, new object[] { dwbm, bmbm, jsbm });
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                context.Response.Write("{\"Rows\":" + EDRS.Common.JsonHelper.JsonString(ds.Tables[0]) + "}");
                context.Response.End();
            }
            context.Response.Write( ReturnString.JsonToString(Prompt.error, "未分配权限", null));
            context.Response.End();

        }
        /// <summary>
        /// 设置角色按钮权限
        /// </summary>
        /// <param name="context"></param>
        private void AddJsAnQx(HttpContext context)
        {
            string dwbm = context.Request.Params["dwbm"];
            //string dwbm = "440000";
            string bmbm = context.Request.Params["bmbm"];
            bmbm = DataProHelper.ProBmSubOne(bmbm);
            string jsbm = context.Request.Params["jsbm"];
            jsbm = DataProHelper.ProBmSubOne(jsbm);
            string gnbm = context.Request.Params["gnbm"];
            string gnmc = context.Request.Params["gnmc"];
            //gnbm = DataProHelper.ProBmSubOne(gnbm);
            string bz = context.Request.Params["bz"];
            string errmsg = string.Empty;
            EDRS.Model.XT_QX_JSANQX model = new EDRS.Model.XT_QX_JSANQX();
            model.ANBH = gnbm;
            model.BMBM = bmbm;
            model.DWBM = dwbm;
            model.JSBM = jsbm;
            EDRS.BLL.XT_QX_JSANQX bll = new EDRS.BLL.XT_QX_JSANQX(context.Request);
            if( bll.Add(model))
            //bool isSuc = ZzjgManage.AddJsGnQx(dwbm, bmbm, jsbm, gnbm, bz, out errmsg);
            //if (string.IsNullOrEmpty(errmsg) && isSuc)
            {
                context.Response.Write(true);
              
                OperateLog.AddLog(OperateLog.LogType.角色权限管理Web, "添加角色按钮权限成功！", gnmc, UserInfo, UserRole, context.Request);
            }
            else
            {
                context.Response.Write(false);
                OperateLog.AddLog(OperateLog.LogType.角色权限管理Web, "添加角色按钮权限失败：" + errmsg, gnmc, UserInfo, UserRole, context.Request);
            }
        }

        /// <summary>
        /// 按钮权限删除
        /// </summary>
        /// <param name="context"></param>
        private void DeleteJsAnQx(HttpContext context)
        {
            string qxbm = context.Request.Params["qxbm"];
        
            string errmsg = string.Empty;
          
            EDRS.BLL.XT_QX_JSANQX bll = new EDRS.BLL.XT_QX_JSANQX(context.Request);
            if(bll.Delete(qxbm))
            {
                context.Response.Write("操作成功");
                OperateLog.AddLog(OperateLog.LogType.角色权限管理Web, "删除角色按钮权限成功！","", UserInfo, UserRole, context.Request);
            }
            else
            {
                OperateLog.AddLog(OperateLog.LogType.角色权限管理Web, "删除角色按钮权限失败：" + errmsg, "", UserInfo, UserRole, context.Request);
              
            }
        }

        #endregion
    }
}