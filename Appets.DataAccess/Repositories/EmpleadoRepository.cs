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
   public class EmpleadoRepository
    {
        public IEnumerable<UDP_tbEmpleados_SelectResult> EmpleadoList()
        {
            const string query = @"UDP_tbEmpleados_Select";
            var parameters = new DynamicParameters();


            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.Query<UDP_tbEmpleados_SelectResult>(query, parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultado;

            }
        }

        public UDP_tbEmpleados_SelectResult Find(int id)
        {
            const string query = @"UDP_tbEmpleados_Find";
            var parameters = new DynamicParameters();
            parameters.Add("@emp_Id", id, DbType.Int32, ParameterDirection.Input);

            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.QueryFirstOrDefault<UDP_tbEmpleados_SelectResult>(query, parameters, commandType: CommandType.StoredProcedure);
                return resultado;

            }
        }

        public string Delete(int id)
        {
            const string query = @"UDP_tbEmpleados_Delete";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id, DbType.Int32, ParameterDirection.Input);
            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.ExecuteScalar<string>(query, parameters, commandType: CommandType.StoredProcedure);
                return resultado;
            }
        }


        public int Insert(int per_Id, int ocup_Id)
        {
            int resultado = 0;
            const string sqlQueryEspc = "UDP_tbEmpleados_Insert";

            var parameterEspc = new DynamicParameters();
            parameterEspc.Add("@per_Id", per_Id, DbType.Int32, ParameterDirection.Input);
            parameterEspc.Add("@ocup_Id", ocup_Id, DbType.Int32, ParameterDirection.Input);
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


        public int Update(int emp_Id, int per_Id, int ocup_Id)
        {
            int resultado = 0;
            const string sqlQueryEspc = "UDP_tbEmpleados_Update";

            var parameterEspc = new DynamicParameters();
            parameterEspc.Add("@emp_Id", emp_Id, DbType.Int32, ParameterDirection.Input);
            parameterEspc.Add("@per_Id", per_Id, DbType.Int32, ParameterDirection.Input);
            parameterEspc.Add("@ocup_Id", ocup_Id, DbType.Int32, ParameterDirection.Input);
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
                    return emp_Id;

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
