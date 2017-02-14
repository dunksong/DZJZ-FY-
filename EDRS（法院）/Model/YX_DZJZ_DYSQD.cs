using System;
namespace EDRS.Model
{
	/// <summary>
	/// 打印申请单
	/// </summary>
	[Serializable]
	public partial class YX_DZJZ_DYSQD
	{
		public YX_DZJZ_DYSQD()
		{}
		#region Model
		private string _dysqdh;
		private string _sqdm;
		private DateTime? _dysqsj;
		private string _lszh;
		private string _sqbz;
		private string _dyzt="I";
		private string _dyr;
		private string _dyrgh;
		private decimal? _dyzfy;
		private DateTime? _dysj;
		private string _clsm;
		/// <summary>
		/// 打印申请单号
		/// </summary>
		public string DYSQDH
		{
			set{ _dysqdh=value;}
			get{return _dysqdh;}
		}
		/// <summary>
		/// 打印申请单名
		/// </summary>
		public string SQDM
		{
			set{ _sqdm=value;}
			get{return _sqdm;}
		}
		/// <summary>
		/// 打印申请时间
		/// </summary>
		public DateTime? DYSQSJ
		{
			set{ _dysqsj=value;}
			get{return _dysqsj;}
		}
		/// <summary>
		/// 律师序号
		/// </summary>
		public string LSZH
		{
			set{ _lszh=value;}
			get{return _lszh;}
		}
		/// <summary>
		/// 打印申请备注
		/// </summary>
		public string SQBZ
		{
			set{ _sqbz=value;}
			get{return _sqbz;}
		}
		/// <summary>
		/// I:未打印
//Y:已打印
//N:驳回打印
		/// </summary>
		public string DYZT
		{
			set{ _dyzt=value;}
			get{return _dyzt;}
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
		/// 打印总费用
		/// </summary>
		public decimal? DYZFY
		{
			set{ _dyzfy=value;}
			get{return _dyzfy;}
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
		/// 处理说明
		/// </summary>
		public string CLSM
		{
			set{ _clsm=value;}
			get{return _clsm;}
		}
		#endregion Model

	}
}

