using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace EDRS.Common
{
    #region 系统配置枚举
    /// <summary>
    /// 系统配置枚举
    /// </summary>
    public enum EnumConfig
    {
        //[Description("date")]
        //[AmbientValue("")]
        //定时清理任务 = 1,
        [Description("number")]
        [AmbientValue("KB")]
        ICE消息包大小 = 2,
        [Description("varchar")]
        [AmbientValue("")]
        访问统一业务ICE地址 = 3,
        //[Description("varchar")]
        //[AmbientValue("")]
        //卷宗文件存储路径 = 4,
        [Description("number")]
        [AmbientValue("T")]
        文件存储空间大小分配 = 5,
        [Description("varchar")]
        [AmbientValue("")]
        卷宗文件上传地址 = 6,
        //[Description("varchar")]
        //[AmbientValue("")]
        //律师资质文件存储路径 = 7,
        [Description("varchar")]
        [AmbientValue("")]
        卷宗文件下载地址 = 8,
        //回调地址 = 9,
        [Description("varchar")]
        [AmbientValue("")]
        路由映射地址 = 9,
        //连接类型（直连或者路由模式） = 10,
        [Description("select")]
        [AmbientValue("")]
        连接类型 = 10,
        //警综平台案件信息接口地址 = 11,
        [Description("varchar")]
        [AmbientValue("")]
        警综平台案件信息接口地址 = 11,
        //警综平台文书信息接口地址 = 12,
        [Description("varchar")]
        [AmbientValue("")]
        警综平台文书信息接口地址 = 12,
        //警综平台上传反馈扫描材料接口 = 13,
        [Description("varchar")]
        [AmbientValue("")]
        警综平台上传反馈扫描材料接口地址 = 13,
        //警综平台上传反馈扫描材料接口 = 14,
        [Description("varchar")]
        [AmbientValue("")]
        警综平台卷宗材料审核状态反馈接口地址 = 14
    } 
    #endregion

    #region 制作状态枚举
    /// <summary>
    /// 制作状态枚举
    /// </summary>
    public enum EnumZzzt
    {
        
//<option value="-2">全部</option>
//<option value="-1">未制作</option>
//<option value="0">制作中</option>
//<option value="1">已上传</option>
//<option value="2">待审核</option>
//<option value="3">审核不通过</option>
//<option value="4">审核通过</option>
//<option value="5">已报送</option>
//<option value="6">报送失败</option>
        [Description("未制作")]
        WZZ=-1,
        [Description("制作中")]
        ZZZ=0,
        [Description("已上传")]
        YSC=1,
        [Description("待审核")]
        DSH=2,
        [Description("审核不通过")]
        BTG=3,
        [Description("审核通过")]        
        TG=4,
        [Description("已报送")]
        YBS = 5,
        [Description("报送失败")]
        BSSB = 6
    } 
    #endregion

}
