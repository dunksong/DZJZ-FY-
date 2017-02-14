using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

public class OperateLog
{
    #region 添加日志方法

    public static void AddLog(LogType typeId, string logMessge, EDRS.Model.XT_ZZJG_RYBM UserInfo, List<EDRS.Model.XT_QX_JSBM> UserRole, HttpRequest req)
    {
        AddLog(typeId, logMessge, "", UserInfo, UserRole, req);
    }
    /// <summary>
    /// 添加日志方法
    /// </summary>
    /// <param name="typeId">日志类型编号</param>
    /// <param name="logMessge">日志内容</param>
    /// <param name="billTag">单据标示</param>
    public static void AddLog(LogType typeId, string logMessge, string billTag, EDRS.Model.XT_ZZJG_RYBM UserInfo, List<EDRS.Model.XT_QX_JSBM> UserRole, HttpRequest req)
    {
        try
        {
            EDRS.Model.YX_DZJZ_JZRZJL entity = new EDRS.Model.YX_DZJZ_JZRZJL();
            EDRS.BLL.YX_DZJZ_JZRZJL rzBll = new EDRS.BLL.YX_DZJZ_JZRZJL(req);
            entity.CZLX = ((int)typeId).ToString();
            if (UserInfo != null)
            {
                entity.DWBM = UserInfo.DWBM;
                entity.DWMC = UserInfo.DWMC;
                entity.CZRGH = UserInfo.GH;
                entity.CZR = UserInfo.MC;
            }
            if (UserRole != null && UserRole.Count > 0)
            {
                entity.BMBM = UserRole[0].BMBM;
                entity.BMMC = UserRole[0].BMMC;
            }            
            entity.RZNR = logMessge;
            entity.CZAJBMSAH = billTag == null ? "" : billTag;
            rzBll.Add(entity);
        }
        catch (Exception)
        {
        }
    }

    /// <summary>
    /// 添加日志方法
    /// </summary>
    /// <param name="typeId">日志类型编号</param>
    /// <param name="logMessge">日志内容</param>
    /// <param name="billTag">单据标示</param>
    public static void AddLogList(LogType typeId, string logMessge, string[] billTag, EDRS.Model.XT_ZZJG_RYBM UserInfo, List<EDRS.Model.XT_QX_JSBM> UserRole, HttpRequest req)
    {
        try
        {
            List<EDRS.Model.YX_DZJZ_JZRZJL> entityList = new List<EDRS.Model.YX_DZJZ_JZRZJL>();
            EDRS.BLL.YX_DZJZ_JZRZJL rzBll = new EDRS.BLL.YX_DZJZ_JZRZJL(req);

            for (int i = 0; i < billTag.Length; i++)
            {
                EDRS.Model.YX_DZJZ_JZRZJL entity = new EDRS.Model.YX_DZJZ_JZRZJL();
                entity.CZSJ = DateTime.Now;
                //代理IP
                entity.CZIP = req.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(entity.CZIP))
                {
                    //真实IP
                    entity.CZIP = req.ServerVariables["REMOTE_ADDR"];
                }
                entity.CZLX = ((int)typeId).ToString();
                if (UserInfo != null)
                {
                    entity.DWBM = UserInfo.DWBM;
                    entity.DWMC = UserInfo.DWMC;
                    entity.CZRGH = UserInfo.GH;
                    entity.CZR = UserInfo.MC;
                }
                if (UserRole != null && UserRole.Count > 0)
                {
                    entity.BMBM = UserRole[0].BMBM;
                    entity.BMMC = UserRole[0].BMMC;
                }
                entity.RZNR = logMessge;
                entity.CZAJBMSAH = billTag[i];
                entityList.Add(entity);
            }

            rzBll.AddByModelList(entityList);
        }
        catch (Exception)
        {
        }
    }

    #endregion

    /// <summary>
    /// 错误日志类型枚举
    /// </summary>
    public enum LogType
    {
        登录系统 = 4,
        登录系统Web = 10,
        单位管理Web = 11,
        部门管理Web = 12,
        人员角色分配Web = 13,
        功能权限管理Web = 14,
        单位权限管理Web = 15,
        案件类别权限管理Web = 16,
        功能模块管理Web = 17,
        参数配置Web = 18,
        卷宗模板配置Web = 19,
        案件卷宗制作Web = 20,
        卷宗统计报表Web = 21,
        单位案件查询Web = 22,
        单位卷宗制作情况查询Web = 23,
        卷宗业务情况Web = 24,
        单位卷宗统计Web = 25,
        卷宗制作工作量统计Web = 26,
        卷宗数量统计Web = 27,
        卷宗月统计图Web = 28,
        卷宗业务情况统计Web = 29,
        人员管理Web = 30,
        角色权限管理Web = 31,
        功能分类管理Web=32,
        阅卷Web=33,
        设备登记Web = 34,
        卷宗OCR及打包状态 = 99,
    }
}
