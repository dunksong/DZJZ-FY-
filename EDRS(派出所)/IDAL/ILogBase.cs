using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDRS.IDAL
{
    public interface ILogBase
    {
        void SetHttpContext(System.Web.HttpRequest _context);
    }
}
