using System;
namespace EDRS.Model
{
	/// <summary>
	/// 实体类YX_DZJZ_LSYJXZSQ 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class YX_DZJZ_LSYJXZSQ
	{
		public YX_DZJZ_LSYJXZSQ()
		{}
		#region Model
		private string _sqbm;
		private string _lszh;
		private string _lsxm;
		private string _ssdw;
		private string _ajmc;
		private string _xyrmc;
		private string _sqrmc;
		private string _sqdw;
		private string _xzdz;
		private string _sqdzt;
		private DateTime? _sqsj;
		private string _bz;
		private string _sfsc;
		private string _shrgh;
		private string _shr;
		private DateTime? _shsj;
		/// <summary>
		/// 下载申请编码
		/// </summary>
		public string SQBM
		{
			set{ _sqbm=value;}
			get{return _sqbm;}
		}
		/// <summary>
		/// 律师证号
		/// </summary>
		public string LSZH
		{
			set{ _lszh=value;}
			get{return _lszh;}
		}
		/// <summary>
		/// 律师姓名
		/// </summary>
		public string LSXM
		{
			set{ _lsxm=value;}
			get{return _lsxm;}
		}
		/// <summary>
		/// 所属单位
		/// </summary>
		public string SSDW
		{
			set{ _ssdw=value;}
			get{return _ssdw;}
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
		/// 嫌疑人
		/// </summary>
		public string XYRMC
		{
			set{ _xyrmc=value;}
			get{return _xyrmc;}
		}
		/// <summary>
		/// 申请人
		/// </summary>
		public string SQRMC
		{
			set{ _sqrmc=value;}
			get{return _sqrmc;}
		}
		/// <summary>
		/// 申请单位
		/// </summary>
		public string SQDW
		{
			set{ _sqdw=value;}
			get{return _sqdw;}
		}
		/// <summary>
		/// 下载地址
		/// </summary>
		public string XZDZ
		{
			set{ _xzdz=value;}
			get{return _xzdz;}
		}
		/// <summary>
		/// D:待审核N:已驳回Y:已审核
		/// </summary>
		public string SQDZT
		{
			set{ _sqdzt=value;}
			get{return _sqdzt;}
		}
		/// <summary>
		/// 申请时间
		/// </summary>
		public DateTime? SQSJ
		{
			set{ _sqsj=value;}
			get{return _sqsj;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string BZ
		{
			set{ _bz=value;}
			get{return _bz;}
		}
		/// <summary>
		/// 是否删除Y N
		/// </summary>
		public string SFSC
		{
			set{ _sfsc=value;}
			get{return _sfsc;}
		}
		/// <summary>
		/// 审核人工号
		/// </summary>
		public string SHRGH
		{
			set{ _shrgh=value;}
			get{return _shrgh;}
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
		/// 审核时间
		/// </summary>
		public DateTime? SHSJ
		{
			set{ _shsj=value;}
			get{return _shsj;}
		}
		#endregion Model

	}
}

