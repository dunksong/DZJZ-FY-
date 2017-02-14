using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cyvation.CCQE.DataAccess.Mapping
{
     class DefaultResultSetMapper<TResult> : IResultSetMapper<TResult>
    {
        readonly IRowMapper<TResult> rowMapper;

        public DefaultResultSetMapper(IRowMapper<TResult> rowMapper)
        {
            this.rowMapper = rowMapper;
        }

        public IEnumerable<TResult> MapSet(IDataReader reader)
        {
            using (reader)
            {
                while (reader.Read())
                {
                    yield return rowMapper.MapRow(reader);
                }
            }
        }
    }
}
