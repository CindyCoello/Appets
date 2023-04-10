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
   public class OcupacionRepository
    {
        public IEnumerable<tbOcupaciones> OcupacionList()
        {
            const string query = @"UDP_tbOcupaciones_Select";
            var parameters = new DynamicParameters();


            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.Query<tbOcupaciones>(query, parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultado;

            }


        }


        public tbOcupaciones Find(int id)
        {
            const string query = @"UDP_tbOcupaciones_Find";
            var parameters = new DynamicParameters();
            parameters.Add("@ocup_Id", id, DbType.Int32, ParameterDirection.Input);

            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.QueryFirstOrDefault<tbOcupaciones>(query, parameters, commandType: CommandType.StoredProcedure);
                return resultado;

            }


        }

        public int Insert(string ocupacion)
        {
            int resultado = 0;
            const string sqlQueryEspc = "UDP_tbOcupaciones_Insert";

            var parameterEspc = new DynamicParameters();
            parameterEspc.Add("@ocup_Descripcion", ocupacion, DbType.String, ParameterDirection.Input);
            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    resultado = db.ExecuteScalar<int>(sqlQueryEspc, parameterEspc, transaction, commandType: CommandType.StoredProcedure);
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




        public int Update(int ocup_Id, string ocupacion)
        {
            int resultado = 0;
            const string sqlQueryEspc = "UDP_tbOcupaciones_Update";

            var parameterEspc = new DynamicParameters();
            parameterEspc.Add("@ocup_Id", ocup_Id, DbType.Int32, ParameterDirection.Input);
            parameterEspc.Add("@ocup_Descripcion", ocupacion, DbType.String, ParameterDirection.Input);
            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    resultado = db.ExecuteScalar<int>(sqlQueryEspc, parameterEspc, transaction, commandType: CommandType.StoredProcedure);
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
                    return ocup_Id;

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
