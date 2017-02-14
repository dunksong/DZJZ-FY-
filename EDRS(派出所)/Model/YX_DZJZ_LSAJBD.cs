using System;
namespace EDRS.Model
{
	/// <summary>
	/// 律师阅卷绑定表(一个案件一个律师可以多次阅卷)
	/// </summary>
	[Serializable]
	public partial class YX_DZJZ_LSAJBD
	{
		public YX_DZJZ_LSAJBD()
		{}
		#region Model
		private string _gh;
        private string _dwbm;
        private string _mc;
		private string _bmsah;
		private string _yjxh;		
		private string _ajmc;
		private string _ajlbbm;
		private string _ajlbmc;
		private DateTime? _yjkssj;
		private DateTime? _yjjssj;
		private string _yjzh;
		private string _yjmm;
		private DateTime? _jdsj;
		private string _jdr;
		private string _jdrgh;
		private string _jdbmbm;
		private string _jdbmmc;
		private string _jddwbm;
		private string _jddwmc;
		private string _sfsc="N";
		private string _yjsqdh;
        private string _jzztxs;
        
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
            set { _dwbm = value; }
            get { return _dwbm; }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string MC
        {
            set { _mc = value; }
            get { return _mc; }
        }
		/// <summary>
		/// 阅卷案件部门受案号
		/// </summary>
		public string BMSAH
		{
			set{ _bmsah=value;}
			get{return _bmsah;}
		}
		/// <summary>
		/// 阅卷序号
		/// </summary>
		public string YJXH
		{
			set{ _yjxh=value;}
			get{return _yjxh;}
		}		
		/// <summary>
		/// 阅卷案件名称
		/// </summary>
		public string AJMC
		{
			set{ _ajmc=value;}
			get{return _ajmc;}
		}
		/// <summary>
		/// 阅卷案件类别编码
		/// </summary>
		public string AJLBBM
		{
			set{ _ajlbbm=value;}
			get{return _ajlbbm;}
		}
		/// <summary>
		/// 阅卷案件类别名称
		/// </summary>
		public string AJLBMC
		{
			set{ _ajlbmc=value;}
			get{return _ajlbmc;}
		}		
		/// <summary>
		/// 律师第一次对此案件进行阅卷时间
		/// </summary>
		public DateTime? YJKSSJ
		{
			set{ _yjkssj=value;}
			get{return _yjkssj;}
		}
		/// <summary>
		/// 当达到安排结束时间时，需提示律师“阅卷时间已到，如需继续阅卷请重新申请”。并自动退出系统，记录阅卷结束时间。律师主动退出的记录阅卷结束时间。在安排时间内可以反复登录阅卷，开始时间只记录第一次，而阅卷结束时间每次都记录。
		/// </summary>
		public DateTime? YJJSSJ
		{
			set{ _yjjssj=value;}
			get{return _yjjssj;}
		}
		/// <summary>
		/// 阅卷账号为，律师证号，密码为随机8位密码
		/// </summary>
		public string YJZH
		{
			set{ _yjzh=value;}
			get{return _yjzh;}
		}
		/// <summary>
		/// 阅卷账号为，律师证号，密码为随机8位密码
		/// </summary>
		public string YJMM
		{
			set{ _yjmm=value;}
			get{return _yjmm;}
		}
		/// <summary>
		/// 接待时间
		/// </summary>
		public DateTime? JDSJ
		{
			set{ _jdsj=value;}
			get{return _jdsj;}
		}
		/// <summary>
		/// 接待人
		/// </summary>
		public string JDR
		{
			set{ _jdr=value;}
			get{return _jdr;}
		}
		/// <summary>
		/// 接待人工号
		/// </summary>
		public string JDRGH
		{
			set{ _jdrgh=value;}
			get{return _jdrgh;}
		}
		/// <summary>
		/// 接待部门编码
		/// </summary>
		public string JDBMBM
		{
			set{ _jdbmbm=value;}
			get{return _jdbmbm;}
		}
		/// <summary>
		/// 接待部门名称
		/// </summary>
		public string JDBMMC
		{
			set{ _jdbmmc=value;}
			get{return _jdbmmc;}
		}
		/// <summary>
		/// 接待单位编码
		/// </summary>
		public string JDDWBM
		{
			set{ _jddwbm=value;}
			get{return _jddwbm;}
		}
		/// <summary>
		/// 接待单位名称
		/// </summary>
		public string JDDWMC
		{
			set{ _jddwmc=value;}
			get{return _jddwmc;}
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
		/// 阅卷申请表单号
		/// </summary>
		public string YJSQDH
		{
			set{ _yjsqdh=value;}
			get{return _yjsqdh;}
		}
        /// <summary>
        /// 卷宗载体形式
        /// </summary>
        public string JZZTXS
        {
            get { return _jzztxs; }
            set { _jzztxs = value; }
        }
		#endregion Model

	}
}

