using System;
namespace EDRS.Model
{
	/// <summary>
	/// 角色功能授权表
	/// </summary>
	[Serializable]
	public partial class XT_QX_JSGNFP
	{
		public XT_QX_JSGNFP()
		{}
		#region Model
		private string _dwbm;
		private string _jsbm;
		private string _gnbm;
		private string _gncs;
		private string _bmbm;
		private string _bz;
		/// <summary>
		/// 单位编码
		/// </summary>
		public string DWBM
		{
			set{ _dwbm=value;}
			get{return _dwbm;}
		}
		/// <summary>
		/// 角色编码
		/// </summary>
		public string JSBM
		{
			set{ _jsbm=value;}
			get{return _jsbm;}
		}
		/// <summary>
		/// 功能编码
		/// </summary>
		public string GNBM
		{
			set{ _gnbm=value;}
			get{return _gnbm;}
		}
		/// <summary>
		/// 功能参数
		/// </summary>
		public string GNCS
		{
			set{ _gncs=value;}
			get{return _gncs;}
		}
		/// <summary>
		/// 部门编码
		/// </summary>
		public string BMBM
		{
			set{ _bmbm=value;}
			get{return _bmbm;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string BZ
		{
			set{ _bz=value;}
			get{return _bz;}
		}
		#endregion Model

	}
}

