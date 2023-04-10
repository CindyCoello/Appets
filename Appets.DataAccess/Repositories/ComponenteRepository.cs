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
   public class ComponenteRepository
    {

        public IEnumerable<tbComponentes> ComponentesList()
        {
            const string query = @"UDP_tbComponentes_Select";
            var parameters = new DynamicParameters();
          

            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.Query<tbComponentes>(query, parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultado;

            }


        }



        public tbComponentes Find(int id)
        {
            const string query = @"UDP_tbComponente_Find";
            var parameters = new DynamicParameters();
            parameters.Add("@comp_Id", id, DbType.Int32, ParameterDirection.Input);

            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.QueryFirstOrDefault<tbComponentes>(query, parameters, commandType: CommandType.StoredProcedure);
                return resultado;

            }


        }



        public int Delete(int comp_Id)
        {
            int resultado = 0;
            const string sqlQuery = "UDP_tbComponente_Delete";

            var parameter = new DynamicParameters();
            parameter.Add("@comp_Id", comp_Id, DbType.Int32, ParameterDirection.Input);
            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {

                    try
                    {
                        resultado = db.ExecuteScalar<int>(sqlQuery, parameter, transaction, commandType: CommandType.StoredProcedure);
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
                        return comp_Id;

                    errorTransaction:
                        transaction.Rollback();
                        db.Close();
                        db.Dispose();
                        return -1;
                    }
                    catch (Exception)
                    {

                        return -1;
                    }

                }
            }
        }

        public int Insert(string componente)
        {
            int resultado = 0;
            const string sqlQueryEspc = "UDP_tbComponente_Insert";

            var parameterEspc = new DynamicParameters();
            parameterEspc.Add("@comp_Nombre", componente, DbType.String, ParameterDirection.Input);
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




        public int Update(int idComp, string componente)
        {
            int resultado = 0;
            const string sqlQueryEspc = "UDP_tbComponentes_Update";

            var parameterEspc = new DynamicParameters();
            parameterEspc.Add("@comp_Id", idComp, DbType.Int32, ParameterDirection.Input);
            parameterEspc.Add("@comp_Nombre", componente, DbType.String, ParameterDirection.Input);
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
                    return idComp;

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
