using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

#region 版本控制类
/// <summary>
/// 版本控制类
/// </summary>
public class VersionControl
{
#if ADVANCED_ALONE

#endif
} 
#endregion

/// <summary>
/// 版本名称
/// </summary>
public enum VersionName
{
#if ADVANCED_ALONE
    事项= 0
#else
    案件=0
#endif

}
/// <summary>
/// 部门受案号
/// </summary>
public enum VersionCord
{
#if ADVANCED_ALONE
    唯一编号 = 0
#else
    部门受案号=0
#endif

}
