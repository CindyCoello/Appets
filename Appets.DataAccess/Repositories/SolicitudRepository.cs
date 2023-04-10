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
   public class SolicitudRepository
    {
        public IEnumerable<UDP_tbSolicitud_SelectResult> SolicitudList()
        {
            const string query = @"UDP_tbSolicitud_Select";
            var parameters = new DynamicParameters();


            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.Query<UDP_tbSolicitud_SelectResult>(query, parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultado;

            }


        }


        public tbSolicitud Find(int id)
        {
            const string query = @"UDP_tbSolicitud_Find";
            var parameters = new DynamicParameters();
            parameters.Add("@solic_Id", id, DbType.Int32, ParameterDirection.Input);

            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.QueryFirstOrDefault<tbSolicitud>(query, parameters, commandType: CommandType.StoredProcedure);
                return resultado;

            }


        }

        public int Insert(string solic_Correo, string solic_NombreCompleto, DateTime solic_Fecha,int masc_Id)
        {

            //System.DateTime fecha = System.DateTime.Parse(solic_Fecha);
            int resultado = 0;
            const string sqlQueryEspc = "UDP_tbSolicitud_Insert";

            var parameterEspc = new DynamicParameters();
            parameterEspc.Add("@solic_Correo", solic_Correo, DbType.String, ParameterDirection.Input);
            parameterEspc.Add("@solic_NombreCompleto", solic_NombreCompleto, DbType.String, ParameterDirection.Input);
            parameterEspc.Add("@solic_Fecha", solic_Fecha, DbType.DateTime, ParameterDirection.Input);
            parameterEspc.Add("@masc_Id", masc_Id, DbType.Int32, ParameterDirection.Input);
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




        public int Update(int solic_Id, string solic_Correo, string solic_NombreCompleto, DateTime solic_Fecha, int masc_Id)
        {
            int resultado = 0;
            const string sqlQueryEspc = "UDP_tbSolicitud_Update";

            var parameterEspc = new DynamicParameters();
            parameterEspc.Add("@solic_Id", solic_Id, DbType.Int32, ParameterDirection.Input);
            parameterEspc.Add("@solic_Correo", solic_Correo, DbType.String, ParameterDirection.Input);
            parameterEspc.Add("@solic_NombreCompleto", solic_NombreCompleto, DbType.String, ParameterDirection.Input);
            parameterEspc.Add("@solic_Fecha", solic_Fecha, DbType.DateTime, ParameterDirection.Input);
            parameterEspc.Add("@masc_Id", masc_Id, DbType.Int32, ParameterDirection.Input);
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
                    return solic_Id;

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
