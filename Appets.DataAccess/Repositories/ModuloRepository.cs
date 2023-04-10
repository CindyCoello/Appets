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
    public class ModuloRepository
    {
        public  IEnumerable<tbModuloPantallas> ListModuloPantallas(int rolid)
        {
            const string query = @"UDP_tbModulos_ListadoRol";
            var parameters = new DynamicParameters();
            parameters.Add("@rol_Id", rolid, DbType.Int32, ParameterDirection.Input);
           
            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
               var resultado = db.Query<tbModuloPantallas>(query, parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultado;
            }
 
        }

        public IEnumerable<UDP_tbModulos_SelectResult> ModuloList()
        {
            const string query = @"UDP_tbModulos_Select";
            var parameters = new DynamicParameters();
            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.Query<UDP_tbModulos_SelectResult>(query, parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultado;
            }
        }

        public tbModulos Find(int id)
        {
            const string query = @"UDP_tbModulos_Find";
            var parameters = new DynamicParameters();
            parameters.Add("@mod_Id", id, DbType.Int32, ParameterDirection.Input);

            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.QueryFirstOrDefault<tbModulos>(query, parameters, commandType: CommandType.StoredProcedure);
                return resultado;
            }
        }

        public int Insert(int comp_Id, string mod_Nombre)
        {
            int resultado = 0;
            const string sqlQueryEspc = "UDP_tbModulos_Insert";

            var parameterEspc = new DynamicParameters();
            parameterEspc.Add("@comp_Id", comp_Id, DbType.Int32, ParameterDirection.Input);
            parameterEspc.Add("@mod_Nombre", mod_Nombre, DbType.String, ParameterDirection.Input);
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


        public int Update(int mod_Id, int comp_Id , string mod_Nombre)
        {
            int resultado = 0;
            const string sqlQueryEspc = "UDP_tbModulos_Update";

            var parameterEspc = new DynamicParameters();
            parameterEspc.Add("@mod_Id", mod_Id, DbType.Int32, ParameterDirection.Input);
            parameterEspc.Add("@comp_Id", comp_Id, DbType.Int32, ParameterDirection.Input);
            parameterEspc.Add("@mod_Nombre", mod_Nombre, DbType.String, ParameterDirection.Input);
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
                    return mod_Id;

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
