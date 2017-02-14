using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 表单类
/// </summary>
public class FromValue
{
    private string name;
    /// <summary>
    /// 表单名称
    /// </summary>
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    private object value;
    /// <summary>
    /// 表单值
    /// </summary>
    public object Value
    {
        get { return this.value; }
        set { this.value = value; }
    }
}
