using System;
namespace EDRS.Model
{
	/// <summary>
	/// 人员角色分配
	/// </summary>
	[Serializable]
	public partial class XT_QX_RYJSFP
	{
		public XT_QX_RYJSFP()
		{}
		#region Model
		private string _dwbm;
		private string _bmbm;
		private string _jsbm;
		private string _gh;
		private string _zjldgh;
		/// <summary>
		/// 单位编码
		/// </summary>
		public string DWBM
		{
			set{ _dwbm=value;}
			get{return _dwbm;}
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
		/// 角色编码
		/// </summary>
		public string JSBM
		{
			set{ _jsbm=value;}
			get{return _jsbm;}
		}
		/// <summary>
		/// 工号
		/// </summary>
		public string GH
		{
			set{ _gh=value;}
			get{return _gh;}
		}
		/// <summary>
		/// 直接领导工号
		/// </summary>
		public string ZJLDGH
		{
			set{ _zjldgh=value;}
			get{return _zjldgh;}
		}
		#endregion Model

	}
}

