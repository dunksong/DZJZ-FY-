using System;
namespace EDRS.Model
{
	/// <summary>
	/// 电子卷宗封面打印模板
	/// </summary>
	[Serializable]
	public partial class YX_DZJZ_FMDYMB
	{
		public YX_DZJZ_FMDYMB()
		{}
		#region Model
		private string _bm;
		private string _bt;
        private string _nr;
		private string _sfmr="N";
		private string _czrgh;
		private string _czr;
		private DateTime? _czsj;
		private string _czip;
		private string _sfsc="N";
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
        /// 内容
        /// </summary>
        public string NR
        {
            set { _nr = value; }
            get { return _nr; }
        }
		/// <summary>
		/// 是否默认
		/// </summary>
		public string SFMR
		{
			set{ _sfmr=value;}
			get{return _sfmr;}
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
		/// 是否删除
		/// </summary>
		public string SFSC
		{
			set{ _sfsc=value;}
			get{return _sfsc;}
		}
		#endregion Model

	}
}

