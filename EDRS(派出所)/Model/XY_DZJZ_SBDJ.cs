using System;
namespace EDRS.Model
{
	/// <summary>
	/// 扫描设备登记表
	/// </summary>
	[Serializable]
	public partial class XY_DZJZ_SBDJ
	{
		public XY_DZJZ_SBDJ()
		{}
		#region Model
		private string _mac;
		private string _devsn;
		private string _devtype;
		private string _devfactory;
		private DateTime? _devusetime;
		private string _ip;
        private string _dwbm;
		/// <summary>
		/// 制作端IP地址
		/// </summary>
		public string MAC
		{
			set{ _mac=value;}
			get{return _mac;}
		}
		/// <summary>
		/// 高速扫描设备唯一号
		/// </summary>
		public string DEVSN
		{
			set{ _devsn=value;}
			get{return _devsn;}
		}
		/// <summary>
		/// 设备型号
		/// </summary>
		public string DEVTYPE
		{
			set{ _devtype=value;}
			get{return _devtype;}
		}
		/// <summary>
		/// 设备厂家
		/// </summary>
		public string DEVFACTORY
		{
			set{ _devfactory=value;}
			get{return _devfactory;}
		}
		/// <summary>
		/// 扫描设备首次使用时间
		/// </summary>
		public DateTime? DEVUSETIME
		{
			set{ _devusetime=value;}
			get{return _devusetime;}
		}
		/// <summary>
		/// 制作端IP地址
		/// </summary>
		public string IP
		{
			set{ _ip=value;}
			get{return _ip;}
		}
        /// <summary>
        /// 单位编码
        /// </summary>
        public string DWBM
        {
            set { _dwbm = value; }
            get { return _dwbm; }
        }
		#endregion Model

	}
}

