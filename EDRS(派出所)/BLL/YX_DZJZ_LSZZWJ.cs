using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDRS.IDAL;
using EDRS.DALFactory;

namespace EDRS.BLL
{
    public partial class YX_DZJZ_LSZZWJ
    {
        private readonly IYX_DZJZ_LSZZWJ dal = DataAccess.CreateYX_DZJZ_LSZZWJ();
        public YX_DZJZ_LSZZWJ(System.Web.HttpRequest _context)
        {
            dal.SetHttpContext(_context);
        }
        public bool AddList(string LSZH, List<string> fileList)
        {
            return dal.AddList(LSZH, fileList);
        }

        public bool DelList(string LSZH, List<string> fileList)
        {
            return dal.DelList(LSZH, fileList);
        }

        public List<string> GetList(string LSZH)
        {
            return dal.GetList(LSZH);
        }
        public bool DelAll(string LSZH)
        {
            return dal.DelAll(LSZH);
        }
    }
}
