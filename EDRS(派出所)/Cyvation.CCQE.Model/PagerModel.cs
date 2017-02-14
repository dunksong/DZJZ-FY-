using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cyvation.CCQE.Model
{
    /// <summary>
    /// 分页信息
    /// </summary>
    public class PagerModel
    {
        /// <summary>
        /// pageIndex
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// pageSize
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// count
        /// </summary>
        public int Count { get; set; }
    }
}
