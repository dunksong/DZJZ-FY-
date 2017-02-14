/**  版本信息模板在安装目录下，可自行修改。
* YX_DZJZ_JZML.cs
*
* 功 能： N/A
* 类 名： YX_DZJZ_JZML
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
	/// 卷宗目录
	/// </summary>
	[Serializable]
	public partial class YX_DZJZ_JZML
	{
		public YX_DZJZ_JZML()
		{}
		#region Model
		private string _jzbh;
		private string _mlbh;
		private string _sfsc;
		private DateTime _cjsj;
		private DateTime _zhxgsj;
		private decimal? _fqdwbm;
		private string _fql;
		private string _dwbm;
		private string _bmsah;
		private string _fmlbh;
		private string _mlxsmc;
		private string _mlxx;
		private decimal? _mlsxh;
		private string _mlbm;
		private string _mllx;
		private string _sslbbm;
		private string _sslbmc;
		private string _sfsm;
		/// <summary>
		/// 卷宗编号
		/// </summary>
		public string JZBH
		{
			set{ _jzbh=value;}
			get{return _jzbh;}
		}
		/// <summary>
		/// 目录编号
		/// </summary>
		public string MLBH
		{
			set{ _mlbh=value;}
			get{return _mlbh;}
		}
		/// <summary>
		/// 是否删除
		/// </summary>
		public string SFSC
		{
			set{ _sfsc=value;}
			get{return _sfsc;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CJSJ
		{
			set{ _cjsj=value;}
			get{return _cjsj;}
		}
		/// <summary>
		/// 最后修改时间
		/// </summary>
		public DateTime ZHXGSJ
		{
			set{ _zhxgsj=value;}
			get{return _zhxgsj;}
		}
		/// <summary>
		/// 分区单位编码
		/// </summary>
		public decimal? FQDWBM
		{
			set{ _fqdwbm=value;}
			get{return _fqdwbm;}
		}
		/// <summary>
		/// 分区列
		/// </summary>
		public string FQL
		{
			set{ _fql=value;}
			get{return _fql;}
		}
		/// <summary>
		/// 单位编码
		/// </summary>
		public string DWBM
		{
			set{ _dwbm=value;}
			get{return _dwbm;}
		}
		/// <summary>
		/// 部门受案号
		/// </summary>
		public string BMSAH
		{
			set{ _bmsah=value;}
			get{return _bmsah;}
		}
		/// <summary>
		/// 父目录编号
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
		/// 目录详细信息
		/// </summary>
		public string MLXX
		{
			set{ _mlxx=value;}
			get{return _mlxx;}
		}
		/// <summary>
		/// 目录顺序号
		/// </summary>
		public decimal? MLSXH
		{
			set{ _mlsxh=value;}
			get{return _mlsxh;}
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
		/// 目录类型
		/// </summary>
		public string MLLX
		{
			set{ _mllx=value;}
			get{return _mllx;}
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
		/// 是否涉密
		/// </summary>
		public string SFSM
		{
			set{ _sfsm=value;}
			get{return _sfsm;}
		}
		#endregion Model

	}
}

