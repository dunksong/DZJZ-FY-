using System;
namespace EDRS.Model
{
	/// <summary>
	/// 所属类别
	/// </summary>
	[Serializable]
	public partial class XT_DZJZ_SSLB
	{
		public XT_DZJZ_SSLB()
		{}
		#region Model
		private string _sslbbm;
		private string _fsslbbm;
		private string _sslblx;
		private string _sslbmc;
		private decimal _sslbsx=1;
		private string _sslbsm;
		/// <summary>
		/// 所属类别编码
		/// </summary>
		public string SSLBBM
		{
			set{ _sslbbm=value;}
			get{return _sslbbm;}
		}
		/// <summary>
		/// 父所属类别编码
		/// </summary>
		public string FSSLBBM
		{
			set{ _fsslbbm=value;}
			get{return _fsslbbm;}
		}
		/// <summary>
		/// 所属类别类型（1、卷；2、目录；3、文件）
		/// </summary>
		public string SSLBLX
		{
			set{ _sslblx=value;}
			get{return _sslblx;}
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
		/// 所属类别顺序
		/// </summary>
		public decimal SSLBSX
		{
			set{ _sslbsx=value;}
			get{return _sslbsx;}
		}
		/// <summary>
		/// 所属类别说明
		/// </summary>
		public string SSLBSM
		{
			set{ _sslbsm=value;}
			get{return _sslbsm;}
		}
		#endregion Model

	}
}

