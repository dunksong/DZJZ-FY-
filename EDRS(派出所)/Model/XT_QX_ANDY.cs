using System;
namespace EDRS.Model
{
	/// <summary>
	/// 按钮管理
	/// </summary>
	[Serializable]
	public partial class XT_QX_ANDY
	{
		public XT_QX_ANDY()
		{}
		#region Model
		private string _anbm;
		private string _anbh;
        private string _ymmc;
        private string _anmc;
		/// <summary>
		/// 按钮编码
		/// </summary>
		public string ANBM
		{
			set{ _anbm=value;}
			get{return _anbm;}
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
		/// 页面名称
		/// </summary>
		public string YMMC
		{
			set{ _ymmc=value;}
			get{return _ymmc;}
		}
        /// <summary>
        /// 按钮名称
        /// </summary>
        public string ANMC
        {
            set { _anmc = value; }
            get { return _anmc; }
        }
		#endregion Model

	}
}

