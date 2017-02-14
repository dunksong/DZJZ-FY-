using System;
namespace EDRS.Model
{
	/// <summary>
	/// 卷宗文件存储表（单页存储表）
	/// </summary>
	[Serializable]
	public partial class YX_DZJZ_JZWJCC
	{
		public YX_DZJZ_JZWJCC()
		{}
		#region Model
		private string _bmsah;
		private string _tysah;
		private string _jzwjbh;
		private string _jzwjmc;
		private string _jzwjxsmc;
		private string _jzwjcflj;
		private DateTime? _jzwjscsj;
		private string _jzwjmd5;
		private decimal? _jzwjdx;
		private string _fql;
		private string _sfsc="N";
		private DateTime? _cjsj;
		private decimal? _xzcs;
		private string _wjhz;
		private decimal? _wjsxh;
		private string _wjbzxx;
		private string _jzwjxxmc;
		private string _jzmlbh;
		private string _jbh;
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
		/// 卷宗文件编号
		/// </summary>
		public string JZWJBH
		{
			set{ _jzwjbh=value;}
			get{return _jzwjbh;}
		}
		/// <summary>
		/// 卷宗文件名称
		/// </summary>
		public string JZWJMC
		{
			set{ _jzwjmc=value;}
			get{return _jzwjmc;}
		}
		/// <summary>
		/// 卷宗文件显示名称
		/// </summary>
		public string JZWJXSMC
		{
			set{ _jzwjxsmc=value;}
			get{return _jzwjxsmc;}
		}
		/// <summary>
		/// 卷宗文件存放的相对路径
		/// </summary>
		public string JZWJCFLJ
		{
			set{ _jzwjcflj=value;}
			get{return _jzwjcflj;}
		}
		/// <summary>
		/// 卷宗文件上传时间
		/// </summary>
		public DateTime? JZWJSCSJ
		{
			set{ _jzwjscsj=value;}
			get{return _jzwjscsj;}
		}
		/// <summary>
		/// 卷宗文件MD5值
		/// </summary>
		public string JZWJMD5
		{
			set{ _jzwjmd5=value;}
			get{return _jzwjmd5;}
		}
		/// <summary>
		/// 卷宗文件大小(KB)
		/// </summary>
		public decimal? JZWJDX
		{
			set{ _jzwjdx=value;}
			get{return _jzwjdx;}
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
		/// (Y/N)
		/// </summary>
		public string SFSC
		{
			set{ _sfsc=value;}
			get{return _sfsc;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime? CJSJ
		{
			set{ _cjsj=value;}
			get{return _cjsj;}
		}
		/// <summary>
		/// 下载次数
		/// </summary>
		public decimal? XZCS
		{
			set{ _xzcs=value;}
			get{return _xzcs;}
		}
		/// <summary>
		/// 文件后缀
		/// </summary>
		public string WJHZ
		{
			set{ _wjhz=value;}
			get{return _wjhz;}
		}
		/// <summary>
		/// 文件顺序号
		/// </summary>
		public decimal? WJSXH
		{
			set{ _wjsxh=value;}
			get{return _wjsxh;}
		}
		/// <summary>
		/// 文件备注信息
		/// </summary>
		public string WJBZXX
		{
			set{ _wjbzxx=value;}
			get{return _wjbzxx;}
		}
		/// <summary>
		/// 卷宗文件名称(带目录)
		/// </summary>
		public string JZWJXXMC
		{
			set{ _jzwjxxmc=value;}
			get{return _jzwjxxmc;}
		}
		/// <summary>
		/// 卷宗目录编号
		/// </summary>
		public string JZMLBH
		{
			set{ _jzmlbh=value;}
			get{return _jzmlbh;}
		}
		/// <summary>
		/// 所属卷编号
		/// </summary>
		public string JBH
		{
			set{ _jbh=value;}
			get{return _jbh;}
		}
		#endregion Model

	}
}

