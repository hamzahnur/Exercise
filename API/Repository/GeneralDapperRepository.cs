using API.Repository.Interface;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;


namespace API.Repository
{
    public class GeneralDapperRepository<Entity> : IDapperRepository<Entity> where Entity : class
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _connection;

        public GeneralDapperRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new SqlConnection(_configuration.GetConnectionString("APIContext"));
        }

        public Entity Query(string query, DynamicParameters parameters)
        {
            var result = _connection.Query<Entity>(query, parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
            return result;
        }

    }
}
