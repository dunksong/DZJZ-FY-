/**  版本信息模板在安装目录下，可自行修改。
* XT_DZJZ_JZMBML.cs
*
* 功 能： N/A
* 类 名： XT_DZJZ_JZMBML
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2007/1/2 9:21:20   N/A    初版
*
*/
using System;
namespace EDRS.Model
{
	/// <summary>
	/// 卷宗目录模板明细表
	/// </summary>
	[Serializable]
	public partial class XT_DZJZ_JZMBML
	{
		public XT_DZJZ_JZMBML()
		{}
		#region Model
		private string _mbjzbh;
		private string _mlbh;
		private string _fmlbh;
		private string _mlxsmc;
		private string _mlbm;
		private string _mllx;
		private string _mlxx;
		private decimal? _mlsxh;
		private string _sslbbm;
		private string _sslbmc;
		private string _sfbx="N";
		private string _sfbxqfbxh;
		/// <summary>
		/// 模板卷宗编号（单位编码 + 顺序号）
		/// </summary>
		public string MBJZBH
		{
			set{ _mbjzbh=value;}
			get{return _mbjzbh;}
		}
		/// <summary>
		/// 目录编号（自增）
		/// </summary>
		public string MLBH
		{
			set{ _mlbh=value;}
			get{return _mlbh;}
		}
		/// <summary>
		/// 父目录编号（为 -1 时表示根目录）
		/// </summary>
		public string FMLBH
		{
			set{ _fmlbh=value;}
			get{return _fmlbh;}
		}
		/// <summary>
		/// 目录显示名称
		/// </summary>
		public string MLXSMC
		{
			set{ _mlxsmc=value;}
			get{return _mlxsmc;}
		}
		/// <summary>
		/// 目录别名
		/// </summary>
		public string MLBM
		{
			set{ _mlbm=value;}
			get{return _mlbm;}
		}
		/// <summary>
		/// 目录类型（1：卷；2：目录）
		/// </summary>
		public string MLLX
		{
			set{ _mllx=value;}
			get{return _mllx;}
		}
		/// <summary>
		/// 目录详细信息
		/// </summary>
		public string MLXX
		{
			set{ _mlxx=value;}
			get{return _mlxx;}
		}
		/// <summary>
		/// 目录顺序号（同级目录内的排序）
		/// </summary>
		public decimal? MLSXH
		{
			set{ _mlsxh=value;}
			get{return _mlsxh;}
		}
		/// <summary>
		/// 所属类别编码
		/// </summary>
		public string SSLBBM
		{
			set{ _sslbbm=value;}
			get{return _sslbbm;}
		}
		/// <summary>
		/// 所属类别名称
		/// </summary>
		public string SSLBMC
		{
			set{ _sslbmc=value;}
			get{return _sslbmc;}
		}
		/// <summary>
		/// 是否必须
		/// </summary>
		public string SFBX
		{
			set{ _sfbx=value;}
			get{return _sfbx;}
		}
		/// <summary>
		/// 是否必须在前非必须在后：Y；非必须在前必须在后：N（只对卷有效）
		/// </summary>
		public string SFBXQFBXH
		{
			set{ _sfbxqfbxh=value;}
			get{return _sfbxqfbxh;}
		}
		#endregion Model

	}
}

