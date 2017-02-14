using System;
namespace EDRS.Model
{
	/// <summary>
	/// 角色编码
	/// </summary>
	[Serializable]
	public partial class XT_QX_JSBM
	{
		public XT_QX_JSBM()
		{}
		#region Model
		private string _jsbm;
		private string _dwbm;
		private string _bmbm;
        private string _bmmc;

        public string BMMC
        {
            get { return _bmmc; }
            set { _bmmc = value; }
        }
		private string _jsmc;
		private decimal _jsxh;
		private string _spjsbm;
		/// <summary>
		/// 角色编码
		/// </summary>
		public string JSBM
		{
			set{ _jsbm=value;}
			get{return _jsbm;}
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
		/// 部门编码
		/// </summary>
		public string BMBM
		{
			set{ _bmbm=value;}
			get{return _bmbm;}
		}
		/// <summary>
		/// 角色名称
		/// </summary>
		public string JSMC
		{
			set{ _jsmc=value;}
			get{return _jsmc;}
		}
		/// <summary>
		/// 角色排序号
		/// </summary>
		public decimal JSXH
		{
			set{ _jsxh=value;}
			get{return _jsxh;}
		}
		/// <summary>
		/// 对应新增的审核角色编码表
		/// </summary>
		public string SPJSBM
		{
			set{ _spjsbm=value;}
			get{return _spjsbm;}
		}
		#endregion Model

	}
}

