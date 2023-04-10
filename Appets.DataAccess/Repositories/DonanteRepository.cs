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
    public class DonanteRepository
    {
        public IEnumerable<UDP_tbDonantes_SelectResult> DonanteList()
        {
            const string query = @"UDP_tbDonantes_Select";
            var parameters = new DynamicParameters();


            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.Query<UDP_tbDonantes_SelectResult>(query, parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultado;

            }

        }

        public tbDonantes Find(int id)
        {
            const string query = @"UDP_tbDonantes_Find";
            var parameters = new DynamicParameters();
            parameters.Add("@don_Id", id, DbType.Int32, ParameterDirection.Input);

            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.QueryFirstOrDefault<tbDonantes>(query, parameters, commandType: CommandType.StoredProcedure);
                return resultado;

            }


        }

        public int Insert(int per_Id, DateTime don_Fecha, string don_Descripcion)
        {
            int resultado = 0;
            const string sqlQueryDonante = "UDP_tbDonantes_Insert";

            var parameterDonante = new DynamicParameters();
            parameterDonante.Add("@per_Id", per_Id, DbType.Int32, ParameterDirection.Input);
            parameterDonante.Add("@don_Fecha", don_Fecha, DbType.Date, ParameterDirection.Input);
            parameterDonante.Add("@don_Descripcion", don_Descripcion, DbType.String, ParameterDirection.Input);

            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    resultado = db.ExecuteScalar<int>(sqlQueryDonante, parameterDonante, transaction, commandType: CommandType.StoredProcedure);
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


        public int Update(int don_Id, int per_Id, System.DateTime don_Fecha, string don_Descripcion)
        {
            int resultado = 0;
            const string sqlQueryDonante = "UDP_tbDonantes_Update";

            var parameterDonante = new DynamicParameters();
            parameterDonante.Add("@don_Id", per_Id, DbType.Int32, ParameterDirection.Input);
            parameterDonante.Add("@per_Id", per_Id, DbType.Int32, ParameterDirection.Input);
            parameterDonante.Add("@don_Fecha", don_Fecha, DbType.Date, ParameterDirection.Input);
            parameterDonante.Add("@don_Descripcion", don_Descripcion, DbType.String, ParameterDirection.Input);

            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    resultado = db.ExecuteScalar<int>(sqlQueryDonante, parameterDonante, transaction, commandType: CommandType.StoredProcedure);
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
                    return don_Id;

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
