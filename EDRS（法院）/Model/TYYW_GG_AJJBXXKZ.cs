using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDRS.Model
{
    /// <summary>
    /// 案件基本信息表
    /// </summary>
    [Serializable]
    public partial class TYYW_GG_AJJBXXKZ
    {
        public TYYW_GG_AJJBXXKZ()
		{}
        #region Model
        private string _ajkzxh;
        private string _ahmc;
        private string _ay;
        private string _sajg;
        private string _yg;
        private string _bg;
        private string _yyr;
        private string _sqzxr;
        private string _bzxr;

        private DateTime? _sarq;
        private DateTime? _jarq;
        private string _cjjg;
        private string _zxbd;
        private string _sljg;
        private string _zxjg;
        private string _jafs;
        private string _gldaxlh;
        private string _hytcy;

        private string _cbr;
        private string _sjy;
        private decimal _zcs = 0;
        private decimal _djc = 0;
        private decimal _ys = 0;
        private DateTime? _gdrq;
        private string _ywt;
        private string _bgqx;

        private string _ajlbbm;
        private decimal _h = 0;
        private string _nf;
        private string _ywlbbm;
        private string _ywlbmc;

        private string _dwbm;
        private string _dwmc;
        private string _dwjc;
        private string _ywbm;
        private string _ywmc;

        private string _ajlbmc;
        private string _qah;
        private string _jh;
        private string _mlh;

        private string _czrgh;        
        private string _czrmc;        
        private DateTime _czsj = DateTime.Now;

        /// <summary>
        /// 操作人名称
        /// </summary>
        public string CZRMC
        {
            get { return _czrmc; }
            set { _czrmc = value; }
        }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime CZSJ
        {
            get { return _czsj; }
            set { _czsj = value; }
        }
        /// <summary>
        /// 操作人工号
        /// </summary>
        public string CZRGH
        {
            get { return _czrgh; }
            set { _czrgh = value; }
        }
        /// <summary>
        /// 案件扩展序号
        /// </summary>
        public string AJKZXH
        {
            set { _ajkzxh = value; }
            get { return _ajkzxh; }
        }

        /// <summary>
        /// 案号名称
        /// </summary>
        public string AHMC
        {
            set { _ahmc = value; }
            get { return _ahmc; }
        }
        /// <summary>
        /// 案由
        /// </summary>
        public string AY
        {
            set { _ay = value; }
            get { return _ay; }
        }
        /// <summary>
        /// 送案机关
        /// </summary>
        public string SAJG
        {
            set { _sajg = value; }
            get { return _sajg; }
        }
        /// <summary>
        /// 原告
        /// </summary>
        public string YG
        {
            set { _yg = value; }
            get { return _yg; }
        }

        /// <summary>
        /// 被告
        /// </summary>
        public string BG
        {
            set { _bg = value; }
            get { return _bg; }
        }
        /// <summary>
        /// 异议人
        /// </summary>
        public string YYR
        {
            set { _yyr = value; }
            get { return _yyr; }
        }
        /// <summary>
        /// 申请执行人
        /// </summary>
        public string SQZXR
        {
            set { _sqzxr = value; }
            get { return _sqzxr; }
        }
        /// <summary>
        /// 被执行人
        /// </summary>
        public string BZXR
        {
            set { _bzxr = value; }
            get { return _bzxr; }
        }

        /// <summary>
        /// 收案日期
        /// </summary>
        public DateTime? SARQ
        {
            set { _sarq = value; }
            get { return _sarq; }
        }
        /// <summary>
        /// 结案日期
        /// </summary>
        public DateTime? JARQ
        {
            set { _jarq = value; }
            get { return _jarq; }
        }
        /// <summary>
        /// 裁决机关
        /// </summary>
        public string CJJG
        {
            set { _cjjg = value; }
            get { return _cjjg; }
        }
        /// <summary>
        /// 执行标的
        /// </summary>
        public string ZXBD
        {
            set { _zxbd = value; }
            get { return _zxbd; }
        }
        /// <summary>
        /// 审理结果
        /// </summary>
        public string SLJG
        {
            set { _sljg = value; }
            get { return _sljg; }
        }

        /// <summary>
        /// 执行结果
        /// </summary>
        public string ZXJG
        {
            set { _zxjg = value; }
            get { return _zxjg; }
        }
        /// <summary>
        /// 结案方式
        /// </summary>
        public string JAFS
        {
            set { _jafs = value; }
            get { return _jafs; }
        }
        /// <summary>
        /// 关联档案系列号
        /// </summary>
        public string GLDAXLH
        {
            set { _gldaxlh = value; }
            get { return _gldaxlh; }
        }

        /// <summary>
        /// 合议庭成员
        /// </summary>
        public string HYTCY
        {
            set { _hytcy = value; }
            get { return _hytcy; }
        }
        /// <summary>
        /// 承办人
        /// </summary>
        public string CBR
        {
            set { _cbr = value; }
            get { return _cbr; }
        }
        /// <summary>
        /// 书记员
        /// </summary>
        public string SJY
        {
            set { _sjy = value; }
            get { return _sjy; }
        }

        /// <summary>
        /// 册数
        /// </summary>
        public decimal ZCS
        {
            set { _zcs = value; }
            get { return _zcs; }
        }
        /// <summary>
        /// 第几页
        /// </summary>
        public decimal DJC
        {
            set { _djc = value; }
            get { return _djc; }
        }

        /// <summary>
        /// 页数
        /// </summary>
        public decimal YS
        {
            set { _ys = value; }
            get { return _ys; }
        }


        /// <summary>
        /// 归档日期
        /// </summary>
        public DateTime? GDRQ
        {
            set { _gdrq = value; }
            get { return _gdrq; }
        }


        /// <summary>
        /// 业务庭
        /// </summary>
        public string YWT
        {
            set { _ywt = value; }
            get { return _ywt; }
        }

        /// <summary>
        /// 保管期限
        /// </summary>
        public string BGQX
        {
            set { _bgqx = value; }
            get { return _bgqx; }
        }
        /// <summary>
        /// 案件类别编码(字)
        /// </summary>
        public string AJLBBM
        {
            set { _ajlbbm = value; }
            get { return _ajlbbm; }
        }
     
        /// <summary>
        /// 号
        /// </summary>
        public decimal H
        {
            set { _h = value; }
            get { return _h; }
        }
        /// <summary>
        /// 年份
        /// </summary>
        public string NF
        {
            set { _nf = value; }
            get { return _nf; }
        }
        /// <summary>
        /// 业务类别编码
        /// </summary>
        public string YWLBBM
        {
            set { _ywlbbm = value; }
            get { return _ywlbbm; }
        }
        /// <summary>
        /// 业务类别名称
        /// </summary>
        public string YWLBMC
        {
            set { _ywlbmc = value; }
            get { return _ywlbmc; }
        }
        /// <summary>
        /// 单位编码
        /// </summary>
        public string DWBM
        {
            set { _dwbm = value; }
            get { return _dwbm; }
        }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string DWMC
        {
            set { _dwmc = value; }
            get { return _dwmc; }
        }
        /// <summary>
        /// 单位简称
        /// </summary>
        public string DWJC
        {
            set { _dwjc = value; }
            get { return _dwjc; }
        }

       
        /// <summary>
        /// 目录号
        /// </summary>
        public string MLH
        {
            get { return _mlh; }
            set { _mlh = value; }
        }
        /// <summary>
        /// 卷号
        /// </summary>
        public string JH
        {
            get { return _jh; }
            set { _jh = value; }
        }
        /// <summary>
        /// 全案号
        /// </summary>
        public string QAH
        {
            get { return _qah; }
            set { _qah = value; }
        }
        /// <summary>
        /// 案件类别名称
        /// </summary>
        public string AJLBMC
        {
            get { return _ajlbmc; }
            set { _ajlbmc = value; }
        }

        #endregion Model

    }
}
