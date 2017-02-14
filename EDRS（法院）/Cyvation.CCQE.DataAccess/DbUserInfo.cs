using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cyvation.CCQE.DataAccess
{
    /// <summary>
    /// 数据库用户信息
    /// </summary>
    class DbUserInfo
    {
        public string Schema_Name { get; set; }
        public string Password { get; set; }   
        public string OrgCode { get; set; }

        public override string ToString()
        {
            return string.Format("Schema_Name={0};Password={1};OrgCode={2}", Schema_Name, Password, OrgCode);
        }
    }
}
