using System;
namespace EDRS.Model
{
	/// <summary>
	/// 案件类别编码表
	/// </summary>
	[Serializable]
	public partial class XT_DM_AJLBBM
	{
        public XT_DM_AJLBBM()
		{}
		#region Model
        private string _ywbm;
        private string _ajlbbm;
        private string _ajlbmc;
        private string _ajsljc;
        private string _sfsc;
        private int _xh;
		/// <summary>
        /// 业务编码
		/// </summary>
        public string YWBM
		{
            set { _ywbm = value; }
            get { return _ywbm; }
		}
		/// <summary>
        /// 案件类别编码
		/// </summary>
        public string AJLBBM
		{
            set { _ajlbbm = value; }
            get { return _ajlbbm; }
		}
		/// <summary>
        /// 案件类别名称
		/// </summary>
        public string AJLBMC
		{
            set { _ajlbmc = value; }
            get { return _ajlbmc; }
		}
		/// <summary>
        /// 案件受理简称
		/// </summary>
        public string AJSLJC
		{
            set { _ajsljc = value; }
            get { return _ajsljc; }
		}
		/// <summary>
        /// 是否删除
		/// </summary>
        public string SFSC
		{
            set { _sfsc = value; }
            get { return _sfsc; }
		}
		/// <summary>
        /// 序号
		/// </summary>
        public int XH
		{
            set { _xh = value; }
            get { return _xh; }
		}
		#endregion Model

	}
}

