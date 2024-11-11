using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace BeyondTalents.Repositories
{
    public abstract class RepositoryBase
    {
        private string _connectionString;
        public RepositoryBase()
        {
            _connectionString = @"Data Source=GUSTAVS-PC\MSSQL2022DEV;Initial Catalog=BeyondTalentsDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
        }

        protected SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
