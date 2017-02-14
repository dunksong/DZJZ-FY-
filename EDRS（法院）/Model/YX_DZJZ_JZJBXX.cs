
using System;
namespace EDRS.Model
{
	/// <summary>
	/// 卷宗基本信息表
	/// </summary>
	[Serializable]
	public partial class YX_DZJZ_JZJBXX
	{
		public YX_DZJZ_JZJBXX()
		{}
		#region Model
		private string _jzbh;
		private string _sfsc="N";
		private DateTime _cjsj= DateTime.Now;
        private DateTime _zhxgsj = DateTime.Now;
		private decimal? _fqdwbm;
		private string _fql;
		private string _dwbm;
		private string _bmsah;
		private string _tysah;
		private string _jzmc;
		private string _jzlj;
		private DateTime? _jzscsj;
		private string _jzscrgh;
		private string _jzscrxm;
		private string _jzms;
		private string _jzxgh;
		private string _sfkygx="N";
		private string _gxywbmjh;
		private string _mrsfgkpz="N";
        private string _accomplices;
        private string _ajmb_bm;
        private string _ajmb_mc;
        private string _idnumber;
        private string _isrecord;
        private string _suspectname;
        private string _wsbh;
        private string _ajbh;
        private string _zzzt;
        private string _lazzzt;
        private string _jzpz;
        private string _jzshrbh;
        private string _jzshr;
        private DateTime? _jzshsj;
        private string _wsmc;
        private string _smajla;
        private string _smajcd;
        

		/// <summary>
		/// 卷宗编号(6位单位 + 8位顺序号)
		/// </summary>
		public string JZBH
		{
			set{ _jzbh=value;}
			get{return _jzbh;}
		}
		/// <summary>
		/// 是否删除
		/// </summary>
		public string SFSC
		{
			set{ _sfsc=value;}
			get{return _sfsc;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CJSJ
		{
			set{ _cjsj=value;}
			get{return _cjsj;}
		}
		/// <summary>
		/// 最后修改时间
		/// </summary>
		public DateTime ZHXGSJ
		{
			set{ _zhxgsj=value;}
			get{return _zhxgsj;}
		}
		/// <summary>
		/// 分区单位编码
		/// </summary>
		public decimal? FQDWBM
		{
			set{ _fqdwbm=value;}
			get{return _fqdwbm;}
		}
		/// <summary>
		/// 分区列
		/// </summary>
		public string FQL
		{
			set{ _fql=value;}
			get{return _fql;}
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
		/// 部门受案号
		/// </summary>
		public string BMSAH
		{
			set{ _bmsah=value;}
			get{return _bmsah;}
		}
		/// <summary>
		/// 统一受案号
		/// </summary>
		public string TYSAH
		{
			set{ _tysah=value;}
			get{return _tysah;}
		}
		/// <summary>
		/// 卷宗名称
		/// </summary>
		public string JZMC
		{
			set{ _jzmc=value;}
			get{return _jzmc;}
		}
		/// <summary>
		/// 卷宗存储路径
		/// </summary>
		public string JZLJ
		{
			set{ _jzlj=value;}
			get{return _jzlj;}
		}
		/// <summary>
		/// 卷宗上传时间
		/// </summary>
		public DateTime? JZSCSJ
		{
			set{ _jzscsj=value;}
			get{return _jzscsj;}
		}
		/// <summary>
		/// 卷宗上传人工号
		/// </summary>
		public string JZSCRGH
		{
			set{ _jzscrgh=value;}
			get{return _jzscrgh;}
		}
		/// <summary>
		/// 卷宗上传人姓名
		/// </summary>
		public string JZSCRXM
		{
			set{ _jzscrxm=value;}
			get{return _jzscrxm;}
		}
		/// <summary>
		/// 卷宗描述
		/// </summary>
		public string JZMS
		{
			set{ _jzms=value;}
			get{return _jzms;}
		}
		/// <summary>
		/// 卷宗修改号（电子卷宗控制到一个卷宗一个案件不能多台电脑同时操作）
		/// </summary>
		public string JZXGH
		{
			set{ _jzxgh=value;}
			get{return _jzxgh;}
		}
		/// <summary>
		/// 是否跨院共享
		/// </summary>
		public string SFKYGX
		{
			set{ _sfkygx=value;}
			get{return _sfkygx;}
		}
		/// <summary>
		/// 共享业务编码集合（以逗号分隔）
		/// </summary>
		public string GXYWBMJH
		{
			set{ _gxywbmjh=value;}
			get{return _gxywbmjh;}
		}
		/// <summary>
		/// 默认是否公开批注
		/// </summary>
		public string MRSFGKPZ
		{
			set{ _mrsfgkpz=value;}
			get{return _mrsfgkpz;}
		}

        /// <summary>
        /// 同伙
        /// </summary>
        public string Accomplices
        {
            get { return _accomplices; }
            set { _accomplices = value; }
        }
        /// <summary>
        /// 模板编码
        /// </summary>
        public string Ajmb_bm
        {
            get { return _ajmb_bm; }
            set { _ajmb_bm = value; }
        }
        /// <summary>
        /// 模板名称
        /// </summary>
        public string Ajmb_mc
        {
            get { return _ajmb_mc; }
            set { _ajmb_mc = value; }
        }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string Idnumber
        {
            get { return _idnumber; }
            set { _idnumber = value; }
        }
        /// <summary>
        /// 是否刻录
        /// </summary>
        public string Isrecord
        {
            get { return _isrecord; }
            set { _isrecord = value; }
        }

        /// <summary>
        /// 嫌疑人姓名
        /// </summary>
        public string Suspectname
        {
            get { return _suspectname; }
            set { _suspectname = value; }
        }

        /// <summary>
        /// 文书编号
        /// </summary>
        public string WSBH
        {
            get { return _wsbh; }
            set { _wsbh = value; }
        }
        /// <summary>
        /// 警综平台案件编号
        /// </summary>
        public string AJBH
        {
            get { return _ajbh; }
            set { _ajbh = value; }
        }
        /// <summary>
        /// 制作状态，0：未制作，1：制作中，2：已上传，3：审核不通过，4：审核通过，5：已报送
        /// </summary>
        public string ZZZT
        {
            get { return _zzzt; }
            set { _zzzt = value; }
        }
        /// <summary>
        /// 制作状态，0：未制作，1：制作中，2：已上传，3：审核不通过，4：审核通过，5：已报送
        /// </summary>
        public string LAZZZT
        {
            get { return _lazzzt; }
            set { _lazzzt = value; }
        }
        /// <summary>
        /// 卷宗批注
        /// </summary>
        public string JZPZ
        {
            get { return _jzpz; }
            set { _jzpz = value; }
        }
        /// <summary>
        /// 卷宗审核人编号
        /// </summary>
        public string JZSHRBH
        {
            get { return _jzshrbh; }
            set { _jzshrbh = value; }
        }
        /// <summary>
        /// 卷宗审核人
        /// </summary>
        public string JZSHR
        {
            get { return _jzshr; }
            set { _jzshr = value; }
        }
        /// <summary>
        /// 卷宗审核人
        /// </summary>
        public DateTime? JZSHSJ
        {
            get { return _jzshsj; }
            set { _jzshsj = value; }
        }

        /// <summary>
        /// 文书名称
        /// </summary>
        public string WSMC
        {
            get { return _wsmc; }
            set { _wsmc = value; }
        }
        /// <summary>
        /// 立案状态
        /// </summary>
        public string SMAJLA
        {
            get { return _smajla; }
            set { _smajla = value; }
        }
        /// <summary>
        /// 存档状态
        /// </summary>
        public string SMAJCD
        {
            get { return _smajcd; }
            set { _smajcd = value; }
        }
		#endregion Model

	}
}

