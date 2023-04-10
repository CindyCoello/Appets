using Appets.DataAccess.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Appets.DataAccess.Repositories
{
   public class ModuloPantallaRepository
    {
        public IEnumerable<UDP_tbModuloPantallas_SelectResult> ListModuloPantallas()
        {
            const string query = @"UDP_tbModuloPantallas_Select";
            var parameters = new DynamicParameters();


            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.Query<UDP_tbModuloPantallas_SelectResult>(query, parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultado;

            }


        }


        public IEnumerable<UDP_tbModuloPantallaByRolResult> ListModuloPantallasByRol(int rol_Id)
        {
            const string query = @"UDP_tbModuloPantallaByRol";
            var parameters = new DynamicParameters();
            parameters.Add("@rol_Id", rol_Id, DbType.Int32, ParameterDirection.Input);

            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.Query<UDP_tbModuloPantallaByRolResult>(query, parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultado;

            }


        }

        public tbModuloPantallas Find(int id)
        {
            const string sqlQuery = @"UDP_tbModuloPantallas_Find";
            var parameters = new DynamicParameters();
            parameters.Add("@modpan_Id", id, DbType.Int32, ParameterDirection.Input);
            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {

                var result = db.QueryFirstOrDefault<tbModuloPantallas>(sqlQuery, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public int Insert(int mod_Id, string modpan_Nombre)
            {
                int resultado = 0;
                const string sqlQueryEspc = "UDP_tbModuloPantallas_Insert";
                var parameterEspc = new DynamicParameters();
                parameterEspc.Add("@mod_Id", mod_Id, DbType.Int32, ParameterDirection.Input);
                parameterEspc.Add("@modpan_Nombre", modpan_Nombre, DbType.String, ParameterDirection.Input);
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

            public int Update(int modpan_Id, int mod_Id, string modpan_Nombre)
            {
                int resultado = 0;
                const string sqlQueryEspc = "UDP_tbModuloPantallas_Update";

                var parameterEspc = new DynamicParameters();
                parameterEspc.Add("@modpan_Id", modpan_Id, DbType.Int32, ParameterDirection.Input);
                parameterEspc.Add("@mod_Id", mod_Id, DbType.Int32, ParameterDirection.Input);
                parameterEspc.Add("@modpan_Nombre", modpan_Nombre, DbType.String, ParameterDirection.Input);
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
                        return modpan_Id;

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

