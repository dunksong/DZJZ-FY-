using System;
namespace EDRS.Model
{
	/// <summary>
	/// 电子卷宗封面打印
	/// </summary>
	[Serializable]
	public partial class YX_DZJZ_FMDY
	{
		public YX_DZJZ_FMDY()
		{}
		#region Model
		private string _bm;
		private string _bt;
		private string _fbt;
		private string _ajmc;
		private string _ajbh;
		private string _fzxyr;
		private DateTime? _lasj;
		private DateTime? _jasj;
		private string _ljdw;
		private string _ljr;
		private string _shr;
		private decimal? _bagj;
		private string _djj;
		private decimal? _gjy;
		private string _czrgh;
		private string _czr;
		private DateTime? _czsj;
		private string _czip;
		private string _czlx;
		/// <summary>
		/// 编码
		/// </summary>
		public string BM
		{
			set{ _bm=value;}
			get{return _bm;}
		}
		/// <summary>
		/// 标题
		/// </summary>
		public string BT
		{
			set{ _bt=value;}
			get{return _bt;}
		}
		/// <summary>
		/// 副标题
		/// </summary>
		public string FBT
		{
			set{ _fbt=value;}
			get{return _fbt;}
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
		/// 案件编号
		/// </summary>
		public string AJBH
		{
			set{ _ajbh=value;}
			get{return _ajbh;}
		}
		/// <summary>
		/// 犯罪嫌疑人
		/// </summary>
		public string FZXYR
		{
			set{ _fzxyr=value;}
			get{return _fzxyr;}
		}
		/// <summary>
		/// 立案时间
		/// </summary>
		public DateTime? LASJ
		{
			set{ _lasj=value;}
			get{return _lasj;}
		}
		/// <summary>
		/// 结案时间
		/// </summary>
		public DateTime? JASJ
		{
			set{ _jasj=value;}
			get{return _jasj;}
		}
		/// <summary>
		/// 立卷单位
		/// </summary>
		public string LJDW
		{
			set{ _ljdw=value;}
			get{return _ljdw;}
		}
		/// <summary>
		/// 立卷人
		/// </summary>
		public string LJR
		{
			set{ _ljr=value;}
			get{return _ljr;}
		}
		/// <summary>
		/// 审核人
		/// </summary>
		public string SHR
		{
			set{ _shr=value;}
			get{return _shr;}
		}
		/// <summary>
		/// 本案共卷
		/// </summary>
		public decimal? BAGJ
		{
			set{ _bagj=value;}
			get{return _bagj;}
		}
		/// <summary>
		/// 第几卷
		/// </summary>
		public string DJJ
		{
			set{ _djj=value;}
			get{return _djj;}
		}
		/// <summary>
		/// 共几页
		/// </summary>
		public decimal? GJY
		{
			set{ _gjy=value;}
			get{return _gjy;}
		}
		/// <summary>
		/// 操作人工号
		/// </summary>
		public string CZRGH
		{
			set{ _czrgh=value;}
			get{return _czrgh;}
		}
		/// <summary>
		/// 操作人
		/// </summary>
		public string CZR
		{
			set{ _czr=value;}
			get{return _czr;}
		}
		/// <summary>
		/// 操作时间
		/// </summary>
		public DateTime? CZSJ
		{
			set{ _czsj=value;}
			get{return _czsj;}
		}
		/// <summary>
		/// 操作IP
		/// </summary>
		public string CZIP
		{
			set{ _czip=value;}
			get{return _czip;}
		}
		/// <summary>
		/// 操作类型
		/// </summary>
		public string CZLX
		{
			set{ _czlx=value;}
			get{return _czlx;}
		}
		#endregion Model

	}
}

