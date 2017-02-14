
using System;
namespace EDRS.Model
{
	/// <summary>
	/// 案件基本信息表
	/// </summary>
	[Serializable]
	public partial class TYYW_GG_AJJBXX
	{
		public TYYW_GG_AJJBXX()
		{}
		#region Model
		private string _bmsah;
		private string _tysah;
		private string _sfsc="N";
		private string _sfygxtsj="N";
		private string _cbdw_bm;
		private string _cbdw_mc;
		private decimal _fqdwbm;
		private string _fql;
		private DateTime _cjsj= DateTime.Now;
        private DateTime _zhxgsj = DateTime.Now;
        private DateTime _slrq = DateTime.Now;
		private string _ajmc;
		private string _ajlb_bm;
		private string _ajlb_mc;
		private string _zcjg_dwdm;
		private string _zcjg_dwmc;
		private string _ysdw_dwdm;
		private string _ysdw_dwmc;
		private string _yswswh;
		private string _ysay_aydm;
		private string _ysay_aymc;
		private string _ysqtay_aydms;
		private string _ysqtay_aymcs;
		private string _cbrgh;
		private string _cbr;
		private string _cbbm_bm;
		private string _cbbm_mc;
		private string _ajzt="0";
		private string _sfswaj="N";
		private string _sfdbaj="N";
		private string _zxhd_mc;
		private DateTime? _wcrq;
		private DateTime? _gdrq;
		private string _gdrgh;
		private string _gdr;
		private string _aqzy;
		private string _dqjd;
		private DateTime? _blksrq;
		private decimal? _blts;
		private DateTime? _dqrq;
		private DateTime? _bjrq;
		private string _yjzt="0";
		private string _jyyjzt="0";
		private decimal? _jyyjhcqxyrgs=null;
		private string _lcslbh;
		private string _fxdj;
		private string _sfgzaj="N";
		private string _fz;
		private string _ysyj_dm;
		private string _ysyj_mc;
		private string _sfjbaj="N";
		private string _zxhd_dm;
		private string _dqyjjd;
		private string _yascssjd_dm;
		private string _yascssjd_mc;
		private string _ysrjdh;
        private string _xyr;
        private string _sfzh;
        private string _taryxx;
        private string _wsbh;
        private string _wsmc;
        private string _ajbh;
        private string _zzzt;
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
		/// 是否删除
		/// </summary>
		public string SFSC
		{
			set{ _sfsc=value;}
			get{return _sfsc;}
		}
		/// <summary>
		/// 是否异构系统数据
		/// </summary>
		public string SFYGXTSJ
		{
			set{ _sfygxtsj=value;}
			get{return _sfygxtsj;}
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
		/// <summary>
		/// 分区单位编码
		/// </summary>
		public decimal FQDWBM
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
		/// 受理日期
		/// </summary>
		public DateTime SLRQ
		{
			set{ _slrq=value;}
			get{return _slrq;}
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
		/// 案件类别
		/// </summary>
		public string AJLB_MC
		{
			set{ _ajlb_mc=value;}
			get{return _ajlb_mc;}
		}
		/// <summary>
		/// 侦查机关代码
		/// </summary>
		public string ZCJG_DWDM
		{
			set{ _zcjg_dwdm=value;}
			get{return _zcjg_dwdm;}
		}
		/// <summary>
		/// 侦查机关
		/// </summary>
		public string ZCJG_DWMC
		{
			set{ _zcjg_dwmc=value;}
			get{return _zcjg_dwmc;}
		}
		/// <summary>
		/// 移送单位代码
		/// </summary>
		public string YSDW_DWDM
		{
			set{ _ysdw_dwdm=value;}
			get{return _ysdw_dwdm;}
		}
		/// <summary>
		/// 移送单位
		/// </summary>
		public string YSDW_DWMC
		{
			set{ _ysdw_dwmc=value;}
			get{return _ysdw_dwmc;}
		}
		/// <summary>
		/// 移送文书文号
		/// </summary>
		public string YSWSWH
		{
			set{ _yswswh=value;}
			get{return _yswswh;}
		}
		/// <summary>
		/// 移送案由代码
		/// </summary>
		public string YSAY_AYDM
		{
			set{ _ysay_aydm=value;}
			get{return _ysay_aydm;}
		}
		/// <summary>
		/// 移送案由
		/// </summary>
		public string YSAY_AYMC
		{
			set{ _ysay_aymc=value;}
			get{return _ysay_aymc;}
		}
		/// <summary>
		/// 移送其他案由代码
		/// </summary>
		public string YSQTAY_AYDMS
		{
			set{ _ysqtay_aydms=value;}
			get{return _ysqtay_aydms;}
		}
		/// <summary>
		/// 移送其他案由
		/// </summary>
		public string YSQTAY_AYMCS
		{
			set{ _ysqtay_aymcs=value;}
			get{return _ysqtay_aymcs;}
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
		/// 承办部门编码
		/// </summary>
		public string CBBM_BM
		{
			set{ _cbbm_bm=value;}
			get{return _cbbm_bm;}
		}
		/// <summary>
		/// 承办部门
		/// </summary>
		public string CBBM_MC
		{
			set{ _cbbm_mc=value;}
			get{return _cbbm_mc;}
		}
		/// <summary>
		/// 案件状态
		/// </summary>
		public string AJZT
		{
			set{ _ajzt=value;}
			get{return _ajzt;}
		}
		/// <summary>
		/// 是否涉外案件
		/// </summary>
		public string SFSWAJ
		{
			set{ _sfswaj=value;}
			get{return _sfswaj;}
		}
		/// <summary>
		/// 是否督办案件
		/// </summary>
		public string SFDBAJ
		{
			set{ _sfdbaj=value;}
			get{return _sfdbaj;}
		}
		/// <summary>
		/// 专项活动
		/// </summary>
		public string ZXHD_MC
		{
			set{ _zxhd_mc=value;}
			get{return _zxhd_mc;}
		}
		/// <summary>
		/// 完成日期
		/// </summary>
		public DateTime? WCRQ
		{
			set{ _wcrq=value;}
			get{return _wcrq;}
		}
		/// <summary>
		/// 归档日期
		/// </summary>
		public DateTime? GDRQ
		{
			set{ _gdrq=value;}
			get{return _gdrq;}
		}
		/// <summary>
		/// 归档人工号
		/// </summary>
		public string GDRGH
		{
			set{ _gdrgh=value;}
			get{return _gdrgh;}
		}
		/// <summary>
		/// 归档人
		/// </summary>
		public string GDR
		{
			set{ _gdr=value;}
			get{return _gdr;}
		}
		/// <summary>
		/// 案情摘要
		/// </summary>
		public string AQZY
		{
			set{ _aqzy=value;}
			get{return _aqzy;}
		}
		/// <summary>
		/// 当前阶段
		/// </summary>
		public string DQJD
		{
			set{ _dqjd=value;}
			get{return _dqjd;}
		}
		/// <summary>
		/// 办理开始日期
		/// </summary>
		public DateTime? BLKSRQ
		{
			set{ _blksrq=value;}
			get{return _blksrq;}
		}
		/// <summary>
		/// 办理天数
		/// </summary>
		public decimal? BLTS
		{
			set{ _blts=value;}
			get{return _blts;}
		}
		/// <summary>
		/// 到期日期
		/// </summary>
		public DateTime? DQRQ
		{
			set{ _dqrq=value;}
			get{return _dqrq;}
		}
		/// <summary>
		/// 办结日期
		/// </summary>
		public DateTime? BJRQ
		{
			set{ _bjrq=value;}
			get{return _bjrq;}
		}
		/// <summary>
		/// 预警状态
		/// </summary>
		public string YJZT
		{
			set{ _yjzt=value;}
			get{return _yjzt;}
		}
		/// <summary>
		/// 羁押预警状态
		/// </summary>
		public string JYYJZT
		{
			set{ _jyyjzt=value;}
			get{return _jyyjzt;}
		}
		/// <summary>
		/// 羁押预警或超期的嫌疑人个数
		/// </summary>
		public decimal? JYYJHCQXYRGS
		{
			set{ _jyyjhcqxyrgs=value;}
			get{return _jyyjhcqxyrgs;}
		}
		/// <summary>
		/// 流程实例编号
		/// </summary>
		public string LCSLBH
		{
			set{ _lcslbh=value;}
			get{return _lcslbh;}
		}
		/// <summary>
		/// 风险等级
		/// </summary>
		public string FXDJ
		{
			set{ _fxdj=value;}
			get{return _fxdj;}
		}
		/// <summary>
		/// 是否关注案件
		/// </summary>
		public string SFGZAJ
		{
			set{ _sfgzaj=value;}
			get{return _sfgzaj;}
		}
		/// <summary>
		/// 附注
		/// </summary>
		public string FZ
		{
			set{ _fz=value;}
			get{return _fz;}
		}
		/// <summary>
		/// 移送意见代码
		/// </summary>
		public string YSYJ_DM
		{
			set{ _ysyj_dm=value;}
			get{return _ysyj_dm;}
		}
		/// <summary>
		/// 移送意见
		/// </summary>
		public string YSYJ_MC
		{
			set{ _ysyj_mc=value;}
			get{return _ysyj_mc;}
		}
		/// <summary>
		/// 是否交办案件
		/// </summary>
		public string SFJBAJ
		{
			set{ _sfjbaj=value;}
			get{return _sfjbaj;}
		}
		/// <summary>
		/// 专项活动代码
		/// </summary>
		public string ZXHD_DM
		{
			set{ _zxhd_dm=value;}
			get{return _zxhd_dm;}
		}
		/// <summary>
		/// 当前预警阶段
		/// </summary>
		public string DQYJJD
		{
			set{ _dqyjjd=value;}
			get{return _dqyjjd;}
		}
		/// <summary>
		/// 原案所处诉讼阶段代码
		/// </summary>
		public string YASCSSJD_DM
		{
			set{ _yascssjd_dm=value;}
			get{return _yascssjd_dm;}
		}
		/// <summary>
		/// 原案所处诉讼阶段
		/// </summary>
		public string YASCSSJD_MC
		{
			set{ _yascssjd_mc=value;}
			get{return _yascssjd_mc;}
		}
		/// <summary>
		/// 移送人及电话
		/// </summary>
		public string YSRJDH
		{
			set{ _ysrjdh=value;}
			get{return _ysrjdh;}
		}

        /// <summary>
        /// 嫌疑人
        /// </summary>
        public string XYR
        {
            set { _xyr = value; }
            get { return _xyr; }
        }
        /// <summary>
        /// 嫌疑人身份证号
        /// </summary>
        public string SFZH
        {
            set { _sfzh = value; }
            get { return _sfzh; }
        }
        /// <summary>
        /// 同案人员信息
        /// </summary>
        public string TARYXX
        {
            set { _taryxx = value; }
            get { return  _taryxx;}
        }

        /// <summary>
        /// 文书编号
        /// </summary>
        public string WSBH
        {
            get { return _wsbh; }
            set { _wsbh = value; }
        }
        /// <summary>
        /// 文书名称
        /// </summary>
        public string WSMC
        {
            get { return _wsmc; }
            set { _wsmc = value; }
        }
        /// <summary>
        /// 警综平台案件编号
        /// </summary>
        public string AJBH
        {
            get { return _ajbh; }
            set { _ajbh = value; }
        }
        /// <summary>
        /// 制作状态，0：未制作，1：制作中，2：已上传，3：审核不通过，4：审核通过，5：已报送
        /// </summary>
        public string ZZZT
        {
            get { return _zzzt; }
            set { _zzzt = value; }
        }
		#endregion Model

	}
}

