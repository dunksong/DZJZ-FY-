using System;
namespace EDRS.Model
{
	/// <summary>
	/// 单位编码
	/// </summary>
	[Serializable]
	public partial class XT_ZZJG_DWBM
	{
		public XT_ZZJG_DWBM()
		{}
		#region Model
		private string _dwbm;
		private string _dwmc;
		private string _dwjc;
		private string _dwjb;
		private string _fdwbm;
		private string _sfsc="N";
		/// <summary>
		/// 全国统一6位编码
		/// </summary>
		public string DWBM
		{
			set{ _dwbm=value;}
			get{return _dwbm;}
		}
		/// <summary>
		/// 单位名称
		/// </summary>
		public string DWMC
		{
			set{ _dwmc=value;}
			get{return _dwmc;}
		}
		/// <summary>
		/// 单位简称
		/// </summary>
		public string DWJC
		{
			set{ _dwjc=value;}
			get{return _dwjc;}
		}
		/// <summary>
		/// 1，2，3，4，5
		/// </summary>
		public string DWJB
		{
			set{ _dwjb=value;}
			get{return _dwjb;}
		}
		/// <summary>
		/// 父单位，为空表示根
		/// </summary>
		public string FDWBM
		{
			set{ _fdwbm=value;}
			get{return _fdwbm;}
		}
		/// <summary>
		/// Y,N，所有是否字段内容均统一使用Y\N。
		/// </summary>
		public string SFSC
		{
			set{ _sfsc=value;}
			get{return _sfsc;}
		}
		#endregion Model

	}
}

