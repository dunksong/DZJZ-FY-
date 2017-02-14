using System;
namespace EDRS.Model
{
	/// <summary>
	/// 卷宗日志记录表
	/// </summary>
	[Serializable]
	public partial class YX_DZJZ_JZRZJL
	{
		public YX_DZJZ_JZRZJL()
		{}
		#region Model
		private decimal _xh;
		private string _dwbm;
		private string _dwmc = "";
		private string _bmbm = "";
		private string _bmmc = "";
		private string _czrgh = "";
		private string _czr;
		private DateTime? _czsj = DateTime.Now;
		private string _czip;
		private string _czlx;
		private string _rznr;
        private string _czajbmsah = "";
		private string _fql = DateTime.Now.Year.ToString();
		/// <summary>
		/// 序号
		/// </summary>
		public decimal XH
		{
			set{ _xh=value;}
			get{return _xh;}
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
		/// 单位名称
		/// </summary>
		public string DWMC
		{
			set{ _dwmc=value;}
			get{return _dwmc;}
		}
		/// <summary>
		/// 部门编码
		/// </summary>
		public string BMBM
		{
			set{ _bmbm=value;}
			get{return _bmbm;}
		}
		/// <summary>
		/// 部门名称
		/// </summary>
		public string BMMC
		{
			set{ _bmmc=value;}
			get{return _bmmc;}
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
		/// 1.  创建卷宗
//2.  删除卷宗
//3.  修改卷宗
//4.  登录系统
//5.  卷宗查询
//6.  用户管理
//7.  律师阅卷日志
//8.             其它
//(律师登录时间，律师登出时间、下载卷宗时间)

		/// </summary>
		public string CZLX
		{
			set{ _czlx=value;}
			get{return _czlx;}
		}
		/// <summary>
		/// 日志内容
		/// </summary>
		public string RZNR
		{
			set{ _rznr=value;}
			get{return _rznr;}
		}
		/// <summary>
		/// 操作案件部门受案号
		/// </summary>
		public string CZAJBMSAH
		{
			set{ _czajbmsah=value;}
			get{return _czajbmsah;}
		}
		/// <summary>
		/// 分区列 创建时间取年份，触发器完成
		/// </summary>
		public string FQL
		{
			set{ _fql=value;}
			get{return _fql;}
		}
		#endregion Model

	}
}

