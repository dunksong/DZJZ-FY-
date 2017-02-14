using System;
namespace EDRS.Model
{
    /// <summary>
    /// 实体类XY_DZJZ_XTPZ。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class XY_DZJZ_XTPZ
    {
        public XY_DZJZ_XTPZ()
        { }
        #region Model
        private string _pzbm;
        private string _systemmark;
        private int _configid;
        private string _configname;
        private string _configvalue;
        /// <summary>
        /// 配置编码
        /// </summary>
        public string PZBM
        {
            set { _pzbm = value; }
            get { return _pzbm; }
        }
        /// <summary>
        /// 所属系统
        /// </summary>
        public string SYSTEMMARK
        {
            set { _systemmark = value; }
            get { return _systemmark; }
        }
        /// <summary>
        /// 配置标示 1.定时清理任务2.ICE消息包大小配置3.访问统一业务ICE地址4.卷宗文件存储路径5.文件存储空间大小分配
        /// </summary>
        public int CONFIGID
        {
            set { _configid = value; }
            get { return _configid; }
        }
        /// <summary>
        /// 配置名称
        /// </summary>
        public string CONFIGNAME
        {
            set { _configname = value; }
            get { return _configname; }
        }
        /// <summary>
        /// 配置值
        /// </summary>
        public string CONFIGVALUE
        {
            set { _configvalue = value; }
            get { return _configvalue; }
        }
        #endregion Model

    }
}

