using System;
namespace EDRS.Model
{
	/// <summary>
	/// 人员编码
	/// </summary>
	[Serializable]
	public partial class XT_ZZJG_RYBM
	{
		public XT_ZZJG_RYBM()
		{}
		#region Model
		private string _gh;
		private string _dwbm;
        private string _dwmc;
		private string _mc;
		private string _dlbm;
		private string _kl;
		private string _yddhhm;
		private string _dzyj;
		private string _gzzh;
		private string _ydwbm;
		private string _ydwmc;
		private string _sflsry="N";
		private string _sftz="N";
        private byte[] _zp;
		private string _sfsc="N";
		private string _xb="1";
		private string _caid;
        private string _zw;
		/// <summary>
		/// 工号
		/// </summary>
		public string GH
		{
			set{ _gh=value;}
			get{return _gh;}
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
        /// 单位编码
        /// </summary>
        public string DWMC
        {
            set { _dwmc = value; }
            get { return _dwmc; }
        }
		/// <summary>
		/// 名称
		/// </summary>
		public string MC
		{
			set{ _mc=value;}
			get{return _mc;}
		}
		/// <summary>
		/// RTX用户名
		/// </summary>
		public string DLBM
		{
			set{ _dlbm=value;}
			get{return _dlbm;}
		}
		/// <summary>
		/// 口令
		/// </summary>
		public string KL
		{
			set{ _kl=value;}
			get{return _kl;}
		}
		/// <summary>
		/// 承办人联系电话
		/// </summary>
		public string YDDHHM
		{
			set{ _yddhhm=value;}
			get{return _yddhhm;}
		}
		/// <summary>
		/// 电子邮件
		/// </summary>
		public string DZYJ
		{
			set{ _dzyj=value;}
			get{return _dzyj;}
		}
		/// <summary>
		/// 工作证号
		/// </summary>
		public string GZZH
		{
			set{ _gzzh=value;}
			get{return _gzzh;}
		}
		/// <summary>
		/// 原单位编码
		/// </summary>
		public string YDWBM
		{
			set{ _ydwbm=value;}
			get{return _ydwbm;}
		}
		/// <summary>
		/// 原单位名称
		/// </summary>
		public string YDWMC
		{
			set{ _ydwmc=value;}
			get{return _ydwmc;}
		}
		/// <summary>
		/// Y/N
		/// </summary>
		public string SFLSRY
		{
			set{ _sflsry=value;}
			get{return _sflsry;}
		}
		/// <summary>
		/// 是否停职
		/// </summary>
		public string SFTZ
		{
			set{ _sftz=value;}
			get{return _sftz;}
		}
		/// <summary>
		/// 照片
		/// </summary>
        public byte[] ZP
		{
			set{ _zp=value;}
			get{return _zp;}
		}
		/// <summary>
		/// 是否删除Y/N
		/// </summary>
		public string SFSC
		{
			set{ _sfsc=value;}
			get{return _sfsc;}
		}
		/// <summary>
		/// 0:女 1:男
		/// </summary>
		public string XB
		{
			set{ _xb=value;}
			get{return _xb;}
		}
		/// <summary>
		/// CA证号
		/// </summary>
		public string CAID
		{
			set{ _caid=value;}
			get{return _caid;}
		}

        /// <summary>
        /// 职务
        /// </summary>
        public string ZW
        {
            set { _zw = value; }
            get { return _zw; }
        }
		#endregion Model

	}
}

