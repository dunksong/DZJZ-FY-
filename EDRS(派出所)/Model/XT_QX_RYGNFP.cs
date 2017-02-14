using System;
namespace EDRS.Model
{
	/// <summary>
	/// XT_QX_RYGNFP:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class XT_QX_RYGNFP
	{
		public XT_QX_RYGNFP()
		{}
		#region Model
		private string _dwbm;
		private string _gh;
		private string _gnbm;
		private string _bmbm;
		private string _gncs;
		private string _bz;
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
		public string GH
		{
			set{ _gh=value;}
			get{return _gh;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string GNBM
		{
			set{ _gnbm=value;}
			get{return _gnbm;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BMBM
		{
			set{ _bmbm=value;}
			get{return _bmbm;}
		}
		/// <summary>
		/// ????????(????)
		/// </summary>
		public string GNCS
		{
			set{ _gncs=value;}
			get{return _gncs;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BZ
		{
			set{ _bz=value;}
			get{return _bz;}
		}
		#endregion Model

	}
}

