using System;
namespace EDRS.Model
{
	/// <summary>
	/// 卷宗文件申请打印表(一个案件一个律师当前阅卷，可以申请打印多页文件。)
	/// </summary>
	[Serializable]
	public partial class YX_DZJZ_WJSQDY
	{
		public YX_DZJZ_WJSQDY()
		{}
		#region Model
		private string _lszh;
		private string _bmsah;
		private string _yjxh;
		private string _jzwjbh;
		private DateTime? _sqsj;
		private decimal? _sqfs;
		private DateTime? _dysj;
		private decimal? _dyfs;
		private decimal? _dyfy;
		private string _dyr;
		private string _dyrgh;
		private string _dybmbm;
		private string _dybmmc;
		private string _dydwbm;
		private string _dydwmc;
		private string _sfsc="N";
		private string _dysqdh;
		private string _xh;
		/// <summary>
		/// 律师证号
		/// </summary>
		public string LSZH
		{
			set{ _lszh=value;}
			get{return _lszh;}
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
		/// 卷宗文件编号
		/// </summary>
		public string JZWJBH
		{
			set{ _jzwjbh=value;}
			get{return _jzwjbh;}
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
		/// 申请分数
		/// </summary>
		public decimal? SQFS
		{
			set{ _sqfs=value;}
			get{return _sqfs;}
		}
		/// <summary>
		/// 打印时间
		/// </summary>
		public DateTime? DYSJ
		{
			set{ _dysj=value;}
			get{return _dysj;}
		}
		/// <summary>
		/// 打印分数
		/// </summary>
		public decimal? DYFS
		{
			set{ _dyfs=value;}
			get{return _dyfs;}
		}
		/// <summary>
		/// 打印费用需要有一个单价设置
		/// </summary>
		public decimal? DYFY
		{
			set{ _dyfy=value;}
			get{return _dyfy;}
		}
		/// <summary>
		/// 打印人
		/// </summary>
		public string DYR
		{
			set{ _dyr=value;}
			get{return _dyr;}
		}
		/// <summary>
		/// 打印人工号
		/// </summary>
		public string DYRGH
		{
			set{ _dyrgh=value;}
			get{return _dyrgh;}
		}
		/// <summary>
		/// 打印部门编码
		/// </summary>
		public string DYBMBM
		{
			set{ _dybmbm=value;}
			get{return _dybmbm;}
		}
		/// <summary>
		/// 打印部门名称
		/// </summary>
		public string DYBMMC
		{
			set{ _dybmmc=value;}
			get{return _dybmmc;}
		}
		/// <summary>
		/// 打印单位编码
		/// </summary>
		public string DYDWBM
		{
			set{ _dydwbm=value;}
			get{return _dydwbm;}
		}
		/// <summary>
		/// 打印单位名称
		/// </summary>
		public string DYDWMC
		{
			set{ _dydwmc=value;}
			get{return _dydwmc;}
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
		/// 打印申请单号
		/// </summary>
		public string DYSQDH
		{
			set{ _dysqdh=value;}
			get{return _dysqdh;}
		}
		/// <summary>
		/// 序号
		/// </summary>
		public string XH
		{
			set{ _xh=value;}
			get{return _xh;}
		}
		#endregion Model

	}
}

