using System;
namespace EDRS.Model
{
	/// <summary>
	/// 阅卷安排
	/// </summary>
	[Serializable]
	public partial class YX_AG_YJAP
	{
		public YX_AG_YJAP()
		{}
		#region Model
		private string _bmsah;
		private decimal _djbh;
		private decimal _yjbh;
		private DateTime? _yjkssj;
		private DateTime? _yjjzsj;
		private string _jzztxs;
		private string _yjfs;
		private string _fzfs;
		private decimal? _fydysl;
		private string _sfsc="N";
		private DateTime _cjsj= DateTime.Now;
        private DateTime _zhxgsj = DateTime.Now;
		private decimal? _fqdwbm;
		private string _fql;
		private string _cbdw_bm;
		private string _cbdw_mc;
		/// <summary>
		/// 部门受案号
		/// </summary>
		public string BMSAH
		{
			set{ _bmsah=value;}
			get{return _bmsah;}
		}
		/// <summary>
		/// 登记编号
		/// </summary>
		public decimal DJBH
		{
			set{ _djbh=value;}
			get{return _djbh;}
		}
		/// <summary>
		/// 阅卷编号
		/// </summary>
		public decimal YJBH
		{
			set{ _yjbh=value;}
			get{return _yjbh;}
		}
		/// <summary>
		/// 阅卷开始时间
		/// </summary>
		public DateTime? YJKSSJ
		{
			set{ _yjkssj=value;}
			get{return _yjkssj;}
		}
		/// <summary>
		/// 阅卷截止时间
		/// </summary>
		public DateTime? YJJZSJ
		{
			set{ _yjjzsj=value;}
			get{return _yjjzsj;}
		}
		/// <summary>
		/// 卷宗载体形式
		/// </summary>
		public string JZZTXS
		{
			set{ _jzztxs=value;}
			get{return _jzztxs;}
		}
		/// <summary>
		/// 阅卷方式
		/// </summary>
		public string YJFS
		{
			set{ _yjfs=value;}
			get{return _yjfs;}
		}
		/// <summary>
		/// 复制方式
		/// </summary>
		public string FZFS
		{
			set{ _fzfs=value;}
			get{return _fzfs;}
		}
		/// <summary>
		/// 复印或者打印数量
		/// </summary>
		public decimal? FYDYSL
		{
			set{ _fydysl=value;}
			get{return _fydysl;}
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
		/// 承办单位编码
		/// </summary>
		public string CBDW_BM
		{
			set{ _cbdw_bm=value;}
			get{return _cbdw_bm;}
		}
		/// <summary>
		/// 承办单位
		/// </summary>
		public string CBDW_MC
		{
			set{ _cbdw_mc=value;}
			get{return _cbdw_mc;}
		}
		#endregion Model

	}
}

