using System;
namespace EDRS.Model
{
	/// <summary>
    /// 卷宗模板配置
	/// </summary>
    [Serializable]
    public partial class XY_DZJZ_MBPZB
    {
        public XY_DZJZ_MBPZB()
        { }
        #region Model
        private string _dossiertypevaluemember;
        private string _caseinfotypeid;
        private string _caseinfotypename;
        private string _unitid;
        private string _dossiertypedisplaymember;
        private string _dossierparentmember;
        private string _dossierevidencevaluemember;
        private int _sortindex;
        private string _category;
        private string _sslbbm;
        private string _sslbmc;
        private string _fSslbBM;
        private string _auto;

        public string FSslbBM
        {
            get { return _fSslbBM; }
            set { _fSslbBM = value; }
        }
        
        /// <summary>
        /// 所属类别编码
        /// </summary>
        public string SSLBBM
        {
            get { return _sslbbm; }
            set { _sslbbm = value; }
        }
        /// <summary>
        /// 所属类别名称
        /// </summary>
        public string SSLBMC
        {
            get { return _sslbmc; }
            set { _sslbmc = value; }
        }
        /// <summary>
        /// 配置编码
        /// </summary>
        public string DossierTypeValueMember
        {
            set { _dossiertypevaluemember = value; }
            get { return _dossiertypevaluemember; }
        }
        /// <summary>
        /// 案件类别
        /// </summary>
        public string CaseInfoTypeID
        {
            set { _caseinfotypeid = value; }
            get { return _caseinfotypeid; }
        }
        /// <summary>
        /// 案件类别名称
        /// </summary>
        public string CaseInfoTypeName
        {
            set { _caseinfotypename = value; }
            get { return _caseinfotypename; }
        }
        /// <summary>
        /// 单位编号
        /// </summary>
        public string UnitID
        {
            set { _unitid = value; }
            get { return _unitid; }
        }
        /// <summary>
        /// 模板名称
        /// </summary>
        public string DossierTypeDisplayMember
        {
            set { _dossiertypedisplaymember = value; }
            get { return _dossiertypedisplaymember; }
        }
        /// <summary>
        /// 父级编码
        /// </summary>
        public string DossierParentMember
        {
            set { _dossierparentmember = value; }
            get { return _dossierparentmember; }
        }
        /// <summary>
        /// 卷宗证明对象
        /// </summary>
        public string DossierEvidenceValueMember
        {
            set { _dossierevidencevaluemember = value; }
            get { return _dossierevidencevaluemember; }
        }
        /// <summary>
        /// 排序
        /// </summary>
        public int SortIndex
        {
            set { _sortindex = value; }
            get { return _sortindex; }
        }
        /// <summary>
        /// 模板类别 J卷,M目录,W文件
        /// </summary>
        public string Category
        {
            set { _category = value; }
            get { return _category; }
        }
        /// <summary>
        /// 是否自动生成
        /// </summary>
        public string Auto
        {
            set { _auto = value; }
            get { return _auto; }
        }
        #endregion Model

    }
}

