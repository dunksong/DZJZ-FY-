using System;
namespace EDRS.Model
{
	/// <summary>
	/// XT_QX_GNFL:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class XT_QX_GNFL
	{
		public XT_QX_GNFL()
		{}
		#region Model
		private string _flbm;
		private string _flmc;
		private string _fflbm;
		private decimal? _flxh;
		private string _sfsc;
		private string _dwbm;
		private string _urldz;
		/// <summary>
		/// ??6????????????
		/// </summary>
		public string FLBM
		{
			set{ _flbm=value;}
			get{return _flbm;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FLMC
		{
			set{ _flmc=value;}
			get{return _flmc;}
		}
		/// <summary>
		/// ??????????????????
		/// </summary>
		public string FFLBM
		{
			set{ _fflbm=value;}
			get{return _fflbm;}
		}
		/// <summary>
		/// ??????
		/// </summary>
		public decimal? FLXH
		{
			set{ _flxh=value;}
			get{return _flxh;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SFSC
		{
			set{ _sfsc=value;}
			get{return _sfsc;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DWBM
		{
			set{ _dwbm=value;}
			get{return _dwbm;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string URLDZ
		{
			set{ _urldz=value;}
			get{return _urldz;}
		}
		#endregion Model

	}
}

