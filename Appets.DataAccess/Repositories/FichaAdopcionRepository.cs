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
   public class FichaAdopcionRepository
    {
        public IEnumerable<UDP_tbFichaAdopcion_SelectResult> FichaAdopcionList()
        {
            const string query = @"UDP_tbFichaAdopcion_Select";
            var parameters = new DynamicParameters();


            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.Query<UDP_tbFichaAdopcion_SelectResult>(query, parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultado;

            }


        }



        public tbFichaAdopcion Find(int id)
        {
            const string query = @"UDP_tbFichaAdopcion_Find";
            var parameters = new DynamicParameters();
            parameters.Add("@ficha_Id", id, DbType.Int32, ParameterDirection.Input);

            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.QueryFirstOrDefault<tbFichaAdopcion>(query, parameters, commandType: CommandType.StoredProcedure);
                return resultado;

            }


        }

        public string Delete(int id)
        {
            const string query = @"UDP_tbFichaAdopcion_Delete";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id, DbType.Int32, ParameterDirection.Input);
            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.ExecuteScalar<string>(query, parameters, commandType: CommandType.StoredProcedure);
                return resultado;
            }
        }



        public int Insert(int masc_Id, int per_Id, DateTime fecha)
        {
           
         
            int resultado = 0;
            const string sqlQueryEspc = "UDP_tbFichaAdopcion_Insert";

            var parameterEspc = new DynamicParameters();
            parameterEspc.Add("@masc_Id", masc_Id, DbType.Int32, ParameterDirection.Input);
            parameterEspc.Add("@per_Id", per_Id, DbType.Int32, ParameterDirection.Input);
            parameterEspc.Add("@ficha_FechaRegistro", fecha, DbType.Date, ParameterDirection.Input);
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




        public int Update(int ficha_Id, int masc_Id, int per_Id, DateTime fecha)
        {
            int resultado = 0;
            const string sqlQueryEspc = "UDP_tbFichaAdopcion_Update";

            var parameterEspc = new DynamicParameters();
            parameterEspc.Add("@ficha_Id", ficha_Id, DbType.Int32, ParameterDirection.Input);
            parameterEspc.Add("@masc_Id", masc_Id, DbType.Int32, ParameterDirection.Input);
            parameterEspc.Add("@per_Id", per_Id, DbType.Int32, ParameterDirection.Input);
            parameterEspc.Add("@ficha_FechaRegistro", fecha, DbType.Date, ParameterDirection.Input);
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
                    return ficha_Id;

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

