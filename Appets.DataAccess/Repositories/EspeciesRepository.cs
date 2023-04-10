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
   public class EspeciesRepository
    {
        public IEnumerable<tbEspecies> EspeciesList()
        {
            const string query = @"UDP_tbEspecies_Select";
            var parameters = new DynamicParameters();


            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.Query<tbEspecies>(query, parameters, commandType: CommandType.StoredProcedure)
                    .ToList();
                return resultado;

            }


        }


        public tbEspecies Find(int id)
        {
            const string query = @"UDP_tbEspecies_Find";
            var parameters = new DynamicParameters();
            parameters.Add("@espc_Id", id, DbType.Int32, ParameterDirection.Input);
            
            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.QueryFirstOrDefault<tbEspecies>(query, parameters, commandType: CommandType.StoredProcedure);
                return resultado;

            }


        }

        public string Delete(int id)
        {
            const string query = @"UDP_tbEspecies_Delete";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id, DbType.Int32, ParameterDirection.Input);
            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.ExecuteScalar<string>(query, parameters, commandType: CommandType.StoredProcedure);
                return resultado;
            }
        }

        public int Insert(string especies)
        {
            int resultado = 0;
            const string sqlQueryEspc = "UDP_tbEspecies_Insert";
            
            var parameterEspc = new DynamicParameters();
            parameterEspc.Add("@espc_Descripcion", especies, DbType.String, ParameterDirection.Input);
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




        public int Update(int espc_Id ,string especies)
        {
            int resultado = 0;
            const string sqlQueryEspc = "UDP_tbEspecies_Update";

            var parameterEspc = new DynamicParameters();
            parameterEspc.Add("@espc_Id", espc_Id, DbType.Int32, ParameterDirection.Input);
            parameterEspc.Add("@espc_Descripcion", especies, DbType.String, ParameterDirection.Input);
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
                    return espc_Id;

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
