using System;
namespace EDRS.Model
{
	/// <summary>
	/// 部门编码
	/// </summary>
	[Serializable]
	public partial class XT_ZZJG_BMBM
	{
		public XT_ZZJG_BMBM()
		{}
		#region Model
		private string _bmbm;
		private string _dwbm;
		private string _fbmbm;
		private string _bmmc;
		private string _bmjc;
		private string _bmahjc;
		private string _bmwhjc;
		private string _sflsjg="N";
		private string _sfcbbm;
		private decimal? _bmxh;
		private string _bz;
		private string _sfsc="N";
		private string _bmys;
		/// <summary>
		/// 部门编码
		/// </summary>
		public string BMBM
		{
			set{ _bmbm=value;}
			get{return _bmbm;}
		}
		/// <summary>
		/// 从0001开始，自增长
		/// </summary>
		public string DWBM
		{
			set{ _dwbm=value;}
			get{return _dwbm;}
		}
		/// <summary>
		/// 父编码，为空表示根
		/// </summary>
		public string FBMBM
		{
			set{ _fbmbm=value;}
			get{return _fbmbm;}
		}
		/// <summary>
		/// 部门名称
		/// </summary>
		public string BMMC
		{
			set{ _bmmc=value;}
			get{return _bmmc;}
		}
		/// <summary>
		/// 部门简称
		/// </summary>
		public string BMJC
		{
			set{ _bmjc=value;}
			get{return _bmjc;}
		}
		/// <summary>
		/// 部门案号简称
		/// </summary>
		public string BMAHJC
		{
			set{ _bmahjc=value;}
			get{return _bmahjc;}
		}
		/// <summary>
		/// 部门文号简称
		/// </summary>
		public string BMWHJC
		{
			set{ _bmwhjc=value;}
			get{return _bmwhjc;}
		}
		/// <summary>
		/// Y,N，缺省为N
		/// </summary>
		public string SFLSJG
		{
			set{ _sflsjg=value;}
			get{return _sflsjg;}
		}
		/// <summary>
		/// 是否承办部门0/是否案管部门1
		/// </summary>
		public string SFCBBM
		{
			set{ _sfcbbm=value;}
			get{return _sfcbbm;}
		}
		/// <summary>
		/// 部门序号
		/// </summary>
		public decimal? BMXH
		{
			set{ _bmxh=value;}
			get{return _bmxh;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string BZ
		{
			set{ _bz=value;}
			get{return _bz;}
		}
		/// <summary>
		/// Y,N，所有是否字段内容均统一使用Y\N
		/// </summary>
		public string SFSC
		{
			set{ _sfsc=value;}
			get{return _sfsc;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BMYS
		{
			set{ _bmys=value;}
			get{return _bmys;}
		}
		#endregion Model

	}
}

