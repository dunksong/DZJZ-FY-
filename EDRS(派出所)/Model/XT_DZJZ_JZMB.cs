/**  版本信息模板在安装目录下，可自行修改。
* XT_DZJZ_JZMB.cs
*
* 功 能： N/A
* 类 名： XT_DZJZ_JZMB
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2007/1/2 9:21:20   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*/
using System;
namespace EDRS.Model
{
	/// <summary>
	/// 卷宗目录模板主表
	/// </summary>
	[Serializable]
	public partial class XT_DZJZ_JZMB
	{
		public XT_DZJZ_JZMB()
		{}
		#region Model
		private string _mbjzbh;
		private string _ajlb;
		private string _sych="1";
		/// <summary>
		/// 模板卷宗编号
		/// </summary>
		public string MBJZBH
		{
			set{ _mbjzbh=value;}
			get{return _mbjzbh;}
		}
		/// <summary>
		/// 案件类别编码
		/// </summary>
		public string AJLB
		{
			set{ _ajlb=value;}
			get{return _ajlb;}
		}
		/// <summary>
		/// 适用场合（1.电子卷宗；2.证据分析）
		/// </summary>
		public string SYCH
		{
			set{ _sych=value;}
			get{return _sych;}
		}
		#endregion Model

	}
}

