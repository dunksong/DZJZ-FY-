using System;
namespace EDRS.Model
{
	/// <summary>
	/// 卷宗案件信息表
	/// </summary>
	[Serializable]
	public partial class YX_DZJZ_JZAJXX
	{
		public YX_DZJZ_JZAJXX()
		{}
		#region Model
		private string _bmsah;
		private string _tysah;
		private string _ajmc;
		private string _ajlb_bm;
		private string _ajlb_mc;
		private string _cbdwbm;
		private string _cbdwmc;
		private string _cbbmbm;
		private string _cbbmmc;
		private string _cbrgh;
		private string _cbr;
		private DateTime? _zzsj;
		private decimal? _jzsl;
		private decimal? _jzwjs;
		private decimal? _jzys;
		private decimal? _jzszkj;
		private string _fql;
		private DateTime? _zjycxgsj;
		private string _zjycxgrgh;
		private string _zjycxgrxm;
		private string _sfsc="N";
        

       
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
		/// 案件名称
		/// </summary>
		public string AJMC
		{
			set{ _ajmc=value;}
			get{return _ajmc;}
		}
		/// <summary>
		/// 案件类别编码
		/// </summary>
		public string AJLB_BM
		{
			set{ _ajlb_bm=value;}
			get{return _ajlb_bm;}
		}
		/// <summary>
		/// 案件类别名称
		/// </summary>
		public string AJLB_MC
		{
			set{ _ajlb_mc=value;}
			get{return _ajlb_mc;}
		}
		/// <summary>
		/// 承办单位编码
		/// </summary>
		public string CBDWBM
		{
			set{ _cbdwbm=value;}
			get{return _cbdwbm;}
		}
		/// <summary>
		/// 承办单位名称
		/// </summary>
		public string CBDWMC
		{
			set{ _cbdwmc=value;}
			get{return _cbdwmc;}
		}
		/// <summary>
		/// 承办部门编码
		/// </summary>
		public string CBBMBM
		{
			set{ _cbbmbm=value;}
			get{return _cbbmbm;}
		}
		/// <summary>
		/// 承办部门名称
		/// </summary>
		public string CBBMMC
		{
			set{ _cbbmmc=value;}
			get{return _cbbmmc;}
		}
		/// <summary>
		/// 承办人工号
		/// </summary>
		public string CBRGH
		{
			set{ _cbrgh=value;}
			get{return _cbrgh;}
		}
		/// <summary>
		/// 承办人
		/// </summary>
		public string CBR
		{
			set{ _cbr=value;}
			get{return _cbr;}
		}
		/// <summary>
		/// 制作时间
		/// </summary>
		public DateTime? ZZSJ
		{
			set{ _zzsj=value;}
			get{return _zzsj;}
		}
		/// <summary>
		/// 本案件有多少个卷宗
		/// </summary>
		public decimal? JZSL
		{
			set{ _jzsl=value;}
			get{return _jzsl;}
		}
		/// <summary>
		/// 本案件有多少卷宗文件
		/// </summary>
		public decimal? JZWJS
		{
			set{ _jzwjs=value;}
			get{return _jzwjs;}
		}
		/// <summary>
		/// 本案件有多少卷宗页
		/// </summary>
		public decimal? JZYS
		{
			set{ _jzys=value;}
			get{return _jzys;}
		}
		/// <summary>
		/// 本案件卷宗所站用空间(kb)
		/// </summary>
		public decimal? JZSZKJ
		{
			set{ _jzszkj=value;}
			get{return _jzszkj;}
		}
		/// <summary>
		/// 分区列 创建时间取年份，触发器完成
		/// </summary>
		public string FQL
		{
			set{ _fql=value;}
			get{return _fql;}
		}
		/// <summary>
		/// 最近一次修改时间
		/// </summary>
		public DateTime? ZJYCXGSJ
		{
			set{ _zjycxgsj=value;}
			get{return _zjycxgsj;}
		}
		/// <summary>
		/// 最近一次修改人工号
		/// </summary>
		public string ZJYCXGRGH
		{
			set{ _zjycxgrgh=value;}
			get{return _zjycxgrgh;}
		}
		/// <summary>
		/// 最近一次修改人姓名
		/// </summary>
		public string ZJYCXGRXM
		{
			set{ _zjycxgrxm=value;}
			get{return _zjycxgrxm;}
		}
		/// <summary>
		/// 是否删除(Y/N)
		/// </summary>
		public string SFSC
		{
			set{ _sfsc=value;}
			get{return _sfsc;}
		}
        

		#endregion Model

	}
}

