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
   public class RazasRepository
    {
        public IEnumerable<UDP_tbRazas_SelectResult> RazasList()
        {
            const string query = @"UDP_tbRazas_Select";
            var parameters = new DynamicParameters();
            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.Query<UDP_tbRazas_SelectResult>(query, parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultado;
            }
        }
       
        public IEnumerable<tbRazas> RazasByEspecie(int id)
        {
            const string query = @"UDP_tbRazas_tbRazasByEspecies";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id, DbType.Int32, ParameterDirection.Input);

            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.Query<tbRazas>(query, parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultado;
            }
        }

        public tbRazas Find(int id)
        {
            const string query = @"UDP_tbRazas_Find";
            var parameters = new DynamicParameters();
            parameters.Add("@raza_Id", id, DbType.Int32, ParameterDirection.Input);

            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.QueryFirstOrDefault<tbRazas>(query, parameters, commandType: CommandType.StoredProcedure);
                return resultado;
            }
        }


        public int Insert(string razas, int especie)
        {
            int resultado = 0;
            const string sqlQueryRaza = "UDP_tbRazas_Insert";

            var parameterRaza = new DynamicParameters();
            parameterRaza.Add("@raza_Descripcion", razas, DbType.String, ParameterDirection.Input);
            parameterRaza.Add("@espc_Id", especie, DbType.String, ParameterDirection.Input);
            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    resultado = db.ExecuteScalar<int>(sqlQueryRaza, parameterRaza, transaction, commandType: CommandType.StoredProcedure);
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


        public int Update(int raza_Id, string razas,int especie)
        {
            int resultado = 0;
            const string sqlQueryRaza = "UDP_tbRazas_Update";
            var parameterRaza = new DynamicParameters();
            parameterRaza.Add("@raza_Id", raza_Id, DbType.Int32, ParameterDirection.Input);
            parameterRaza.Add("@raza_Descripcion", razas, DbType.String, ParameterDirection.Input);
            parameterRaza.Add("@espc_Id", especie, DbType.String, ParameterDirection.Input);
            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    resultado = db.ExecuteScalar<int>(sqlQueryRaza, parameterRaza, transaction, commandType: CommandType.StoredProcedure);
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
                    return raza_Id;

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
