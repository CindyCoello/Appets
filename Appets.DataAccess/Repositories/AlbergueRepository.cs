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
    public class AlbergueRepository
    {
        public IEnumerable<tbAlbergue> AlbergueList()
        {
            const string query = @"UDP_tbAlbergue_Select";
            var parameters = new DynamicParameters();


            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.Query<tbAlbergue>(query, parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultado;

            }


        }


        public tbAlbergue Find(int id)
        {
            const string query = @"UDP_tbAlbergue_Find";
            var parameters = new DynamicParameters();
            parameters.Add("@alberg_Id", id, DbType.Int32, ParameterDirection.Input);

            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.QueryFirstOrDefault<tbAlbergue>(query, parameters, commandType: CommandType.StoredProcedure);
                return resultado;

            }


        }


        //public tbAlbergue Delete(int id)
        //{
        //    const string query = @"UDP_tbAlbergue_Delete";
        //    var parameters = new DynamicParameters();
        //    parameters.Add("@alberg_Id", id, DbType.Int32, ParameterDirection.Input);

        //    using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
        //    {
        //        var resultado = db.QueryFirstOrDefault<tbAlbergue>(query, parameters, commandType: CommandType.StoredProcedure);
        //        return resultado;

        //    }


        //}

        public int Insert(
            string rtn,
            string nombre,
            string ubicacion,
            string telefono,
            string correo,
            string mision,
            string inform)
        {
            int resultado = 0;
            const string sqlQueryAlberg = "UDP_tbAlbergue_Insert";

            var parameterAlberg = new DynamicParameters();
            parameterAlberg.Add("@alberg_RTN", rtn, DbType.String, ParameterDirection.Input);
            parameterAlberg.Add("@alberg_Nombre", nombre, DbType.String, ParameterDirection.Input);
            parameterAlberg.Add("@alberg_Ubicacion", ubicacion, DbType.String, ParameterDirection.Input);
            parameterAlberg.Add("@alberg_Telefono", telefono, DbType.String, ParameterDirection.Input);
            parameterAlberg.Add("@alberg_Correo", correo, DbType.String, ParameterDirection.Input);
            parameterAlberg.Add("@alberg_Mision", mision, DbType.String, ParameterDirection.Input);
            parameterAlberg.Add("@alberg_InformacionAdicion", inform, DbType.String, ParameterDirection.Input);

            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    resultado = db.ExecuteScalar<int>(sqlQueryAlberg, parameterAlberg, transaction, commandType: CommandType.StoredProcedure);
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




        public int Update(
            int Alberg_Id,
            string rtn,
            string nombre,
            string ubicacion,
            string telefono, 
            string correo,
            string mision,
            string inform)
        {
            int resultado = 0;
            const string sqlQueryAlberg = "UDP_tbAlbergue_Update";

            var parameterAlberg = new DynamicParameters();
            parameterAlberg.Add("@alberg_Id", Alberg_Id, DbType.Int32, ParameterDirection.Input);
            parameterAlberg.Add("@alberg_RTN", rtn, DbType.String, ParameterDirection.Input);
            parameterAlberg.Add("@alberg_Nombre", nombre, DbType.String, ParameterDirection.Input);
            parameterAlberg.Add("@alberg_Ubicacion", ubicacion, DbType.String, ParameterDirection.Input);
            parameterAlberg.Add("@alberg_Telefono", telefono, DbType.String, ParameterDirection.Input);
            parameterAlberg.Add("@alberg_Correo", correo, DbType.String, ParameterDirection.Input);
            parameterAlberg.Add("@alberg_Mision", mision, DbType.String, ParameterDirection.Input);
            parameterAlberg.Add("@alberg_InformacionAdicion", inform, DbType.String, ParameterDirection.Input);
            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    resultado = db.ExecuteScalar<int>(sqlQueryAlberg, parameterAlberg, transaction, commandType: CommandType.StoredProcedure);
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
                    return Alberg_Id;

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
