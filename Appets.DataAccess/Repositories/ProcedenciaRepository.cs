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
    public class ProcedenciaRepository
    {
        public IEnumerable<tbProcedencia> ProcedenciaList()
        {
            const string query = @"UDP_tbProcedencia_Select";
            var parameters = new DynamicParameters();


            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.Query<tbProcedencia>(query, parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultado;
            }
        }


        public tbProcedencia Find(int id)
        {
            const string query = @"UDP_tbProcedencia_Find";
            var parameters = new DynamicParameters();
            parameters.Add("@proc_Id", id, DbType.Int32, ParameterDirection.Input);

            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.QueryFirstOrDefault<tbProcedencia>(query, parameters, commandType: CommandType.StoredProcedure);
                return resultado;
            }
        }

        public int Insert(string procedencia)
        {
            int resultado = 0;
            const string sqlQueryProc = "UDP_tbProcedencia_Insert";

            var parameterProc = new DynamicParameters();
            parameterProc.Add("@proc_Descripcion", procedencia, DbType.String, ParameterDirection.Input);
            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    resultado = db.ExecuteScalar<int>(sqlQueryProc, parameterProc, transaction, commandType: CommandType.StoredProcedure);
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


        public int Update(int proc_Id, string proc_Descripcion)
        {
            int resultado = 0;
            const string sqlQueryProc = "UDP_tbProcedencia_Update";

            var parameterProc = new DynamicParameters();
            parameterProc.Add("@proc_Id", proc_Id, DbType.Int32, ParameterDirection.Input);
            parameterProc.Add("@proc_Descripcion", proc_Descripcion, DbType.String, ParameterDirection.Input);
            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    resultado = db.ExecuteScalar<int>(sqlQueryProc, parameterProc, transaction, commandType: CommandType.StoredProcedure);
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
                    return proc_Id;

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
