using System;
namespace EDRS.Model
{
	/// <summary>
    /// 业务编码表
	/// </summary>
	[Serializable]
	public partial class XT_DM_YWBM
	{
        public XT_DM_YWBM()
		{}
		#region Model
        private string _ywbm;
        private string _ywmc;
        private string _ywjc;
        private string _bz;
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
        /// 业务名称
		/// </summary>
        public string YWMC
		{
            set { _ywmc = value; }
            get { return _ywmc; }
		}
		/// <summary>
        /// 业务简称
		/// </summary>
        public string YWJC
		{
            set { _ywjc = value; }
            get { return _ywjc; }
		}
		/// <summary>
        /// 备注
		/// </summary>
        public string BZ
		{
            set { _bz = value; }
            get { return _bz; }
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

