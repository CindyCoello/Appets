using Appets.DataAccess.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Appets.DataAccess.Repositories
{
    public class VoluntarioRepository
    {
        public IEnumerable<UDP_tbVoluntarios_SelectResult> VoluntarioList()
        {
            const string query = @"UDP_tbVoluntarios_Select";
            var parameters = new DynamicParameters();
            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.Query<UDP_tbVoluntarios_SelectResult>(query, parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultado;
            }
        }

        public tbVoluntarios Find(int id)
        {
            const string query = @"UDP_tbVoluntarios_Find";
            var parameters = new DynamicParameters();
            parameters.Add("@volun_Id", id, DbType.Int32, ParameterDirection.Input);

            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.QueryFirstOrDefault<tbVoluntarios>(query, parameters, commandType: CommandType.StoredProcedure);
                return resultado;
            }
        }

        public string Delete(int id)
        {
            const string query = @"UDP_tbVoluntarios_Delete";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id, DbType.Int32, ParameterDirection.Input);
            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.ExecuteScalar<string>(query, parameters, commandType: CommandType.StoredProcedure);
                return resultado;
            }
        }

        public int Insert(int per_Id, DateTime volun_FechaIngreso)
        {
            int resultado = 0;
            const string sqlQueryVolun= "UDP_tbVoluntarios_Insert";

            var parameter = new DynamicParameters();
            parameter.Add("@per_Id", per_Id, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@volun_FechaIngreso", volun_FechaIngreso, DbType.Date, ParameterDirection.Input);
           

            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    resultado = db.ExecuteScalar<int>(sqlQueryVolun, parameter, transaction, commandType: CommandType.StoredProcedure);
                    if (resultado > 0)
                    {

                    }
                    else
                    {
                        goto errorTransaction;
                    }
                    transaction.Commit();
                    db.Close();
                    db.Dispose();
                    return 1;

                errorTransaction:
                    transaction.Rollback();
                    db.Close();
                    db.Dispose();
                    return -1;
                }
            }
        }

        public int Update(int volun_Id, int per_Id, DateTime volun_FechaIngreso)
        {
            int resultado = 0;
            const string sqlQueryVolun = "UDP_tbVoluntarios_Update";

            var parameter = new DynamicParameters();
            parameter.Add("@volun_Id", volun_Id, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@per_Id", per_Id, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@volun_FechaIngreso", volun_FechaIngreso, DbType.Date, ParameterDirection.Input);

            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    resultado = db.ExecuteScalar<int>(sqlQueryVolun, parameter, transaction, commandType: CommandType.StoredProcedure);
                    if (resultado == 0)
                    {

                    }
                    else
                    {
                        goto errorTransaction;
                    }
                    transaction.Commit();
                    db.Close();
                    db.Dispose();
                    return volun_Id;

                errorTransaction:
                    transaction.Rollback();
                    db.Close();
                    db.Dispose();
                    return -1;
                }
            }
        }
    }
}
