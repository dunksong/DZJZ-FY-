using System;
namespace EDRS.Model
{
	/// <summary>
	/// 阅卷申请表
	/// </summary>
	[Serializable]
	public partial class YX_DZJZ_LSYJSQ
	{
		public YX_DZJZ_LSYJSQ()
		{}
		#region Model
		private string _lszh;
		private string _yjsqdh;
		private DateTime? _sqsj;
		private string _sqsm;
		private string _sfsc="N";
		private string _shrgh;
		private string _shr;
		private string _shsm;
		private DateTime? _shsj;
		private string _yjsqdm;	
		private string _sqdzt="F";
		/// <summary>
		/// 律师证号
		/// </summary>
		public string LSZH
		{
			set{ _lszh=value;}
			get{return _lszh;}
		}
		/// <summary>
		/// 阅卷申请单号
		/// </summary>
		public string YJSQDH
		{
			set{ _yjsqdh=value;}
			get{return _yjsqdh;}
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
		/// 申请说明
		/// </summary>
		public string SQSM
		{
			set{ _sqsm=value;}
			get{return _sqsm;}
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
		/// 审核说明
		/// </summary>
		public string SHSM
		{
			set{ _shsm=value;}
			get{return _shsm;}
		}
		/// <summary>
		/// 审核时间
		/// </summary>
		public DateTime? SHSJ
		{
			set{ _shsj=value;}
			get{return _shsj;}
		}
		/// <summary>
		/// 阅卷申请单名
		/// </summary>
		public string YJSQDM
		{
			set{ _yjsqdm=value;}
			get{return _yjsqdm;}
		}
		
		/// <summary>
		/// F:未提交
        ///T:已提交
        ///Y:通过审核
        ///N:未通过审核
		/// </summary>
		public string SQDZT
		{
			set{ _sqdzt=value;}
			get{return _sqdzt;}
		}
		#endregion Model

	}
}

