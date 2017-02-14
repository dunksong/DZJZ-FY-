using System;
namespace EDRS.Model
{
	/// <summary>
	/// 角色按钮权限
	/// </summary>
	[Serializable]
	public partial class XT_QX_JSANQX
	{
		public XT_QX_JSANQX()
		{}
		#region Model
		private string _qxbm;
		private string _anbh;
		private string _dwbm;
		private string _jsbm;
		private string _bmbm;
		/// <summary>
		/// 权限编号
		/// </summary>
		public string QXBM
		{
			set{ _qxbm=value;}
			get{return _qxbm;}
		}
		/// <summary>
		/// 按钮编号
		/// </summary>
		public string ANBH
		{
			set{ _anbh=value;}
			get{return _anbh;}
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
		/// 
		/// </summary>
		public string JSBM
		{
			set{ _jsbm=value;}
			get{return _jsbm;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BMBM
		{
			set{ _bmbm=value;}
			get{return _bmbm;}
		}
		#endregion Model

	}
}

