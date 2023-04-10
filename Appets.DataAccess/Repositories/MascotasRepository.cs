using Appets.DataAccess.Entities;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Appets.DataAccess.Repositories
{
   public class MascotasRepository
    {
        public IEnumerable<UDP_tbMascotas_SelectResult> MascotasList()
        {
            const string query = @"UDP_tbMascotas_Select";
            var parameters = new DynamicParameters();


            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.Query<UDP_tbMascotas_SelectResult>(query, parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultado;
            }
        }


        public tbMascotas Find(int id)
        {
            const string query = @"UDP_tbMascotas_Find";
            var parameters = new DynamicParameters();
            parameters.Add("@masc_Id", id, DbType.Int32, ParameterDirection.Input);

            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.QueryFirstOrDefault<tbMascotas>(query, parameters, commandType: CommandType.StoredProcedure);
                return resultado;
            }
        }


        public tbMascotas Reservado(int masc_Id, Boolean masc_EsReservado)
        {
            const string query = @"UDP_tbMascotas_Reservado";
            var parameters = new DynamicParameters();
            parameters.Add("@masc_Id", masc_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@masc_EsReservado", masc_EsReservado, DbType.Boolean, ParameterDirection.Input);

            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.QueryFirstOrDefault<tbMascotas>(query, parameters, commandType: CommandType.StoredProcedure);
                return resultado;
            }
        }


        public tbMascotas Adoptado(int masc_Id, Boolean masc_EsAdoptado)
        {
            const string query = @"UDP_tbMascotas_Adoptado";
            var parameters = new DynamicParameters();
            parameters.Add("@masc_Id", masc_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@masc_EsAdoptado", masc_EsAdoptado, DbType.Boolean, ParameterDirection.Input);

            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.QueryFirstOrDefault<tbMascotas>(query, parameters, commandType: CommandType.StoredProcedure);
                return resultado;
            }
        }

        public string Delete(int id)
        {
            const string query = @"UDP_Mascotas_Delete";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id, DbType.Int32, ParameterDirection.Input);
            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.ExecuteScalar<string>(query, parameters, commandType: CommandType.StoredProcedure);
                return resultado;
            }
        }


        public int Insert(
            string masc_Imagen,
            string masc_Nombre, 
            int espc_Id, 
            int raza_Id,
            int masc_Edad,
            string masc_Sexo,
            string masc_Peso,
            string masc_Talla,
            string masc_Color,
            int emp_Id,
            //bool masc_EsAdoptado,
            //bool masc_EsReservado,
            string masc_HistorialDescripcion,
            int proc_Id)
        {
            int resultado = 0;
            const string sqlQueryEspc = "UDP_tbMascotas_Insert";

            var parameterEspc = new DynamicParameters();
            parameterEspc.Add("@masc_Imagen", masc_Imagen, DbType.String, ParameterDirection.Input);
            parameterEspc.Add("@masc_Nombre", masc_Nombre, DbType.String, ParameterDirection.Input);
            parameterEspc.Add("@espc_Id", espc_Id, DbType.Int32, ParameterDirection.Input);
            parameterEspc.Add("@raza_Id", raza_Id, DbType.Int32, ParameterDirection.Input);
            parameterEspc.Add("@masc_Edad", masc_Edad, DbType.Int32, ParameterDirection.Input);
            parameterEspc.Add("@masc_Sexo", masc_Sexo, DbType.String, ParameterDirection.Input);
            parameterEspc.Add("@masc_Peso", masc_Peso, DbType.String, ParameterDirection.Input);
            parameterEspc.Add("@masc_Talla", masc_Talla, DbType.String, ParameterDirection.Input);
            parameterEspc.Add("@masc_Color", masc_Color, DbType.String, ParameterDirection.Input);
            parameterEspc.Add("@emp_Id", emp_Id, DbType.Int32, ParameterDirection.Input);
            //parameterEspc.Add("@masc_EsAdoptado", masc_EsAdoptado, DbType.Boolean, ParameterDirection.Input);
            //parameterEspc.Add("@masc_EsReservado", masc_EsReservado, DbType.Boolean, ParameterDirection.Input);
            parameterEspc.Add("@masc_HistorialDescripcion", masc_HistorialDescripcion, DbType.String, ParameterDirection.Input);
            parameterEspc.Add("@proc_Id", proc_Id, DbType.Int32, ParameterDirection.Input);


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

        public int update(
            int masc_Id,
            string masc_Nombre,
            int espc_Id,
            int raza_Id,
            int masc_Edad,
            string masc_Sexo,
            string masc_Peso,
            string masc_Talla,
            string masc_Color,
            int emp_Id,
            //bool masc_EsAdoptado,
            //bool masc_EsReservado,
            string masc_HistorialDescripcion,
            int proc_Id)
          {
            int resultado = 0;
            const string sqlQueryEspc = "UDP_tbMascotas_Update";

            var parameterEspc = new DynamicParameters();
            parameterEspc.Add("@masc_Id", masc_Id, DbType.Int32, ParameterDirection.Input);
            //parameterEspc.Add("@masc_Imagen", masc_Imagen, DbType.String, ParameterDirection.Input);
            parameterEspc.Add("@masc_Nombre", masc_Nombre, DbType.String, ParameterDirection.Input);
            parameterEspc.Add("@espc_Id", espc_Id, DbType.Int32, ParameterDirection.Input);
            parameterEspc.Add("@raza_Id", raza_Id, DbType.Int32, ParameterDirection.Input);
            parameterEspc.Add("@masc_Edad", masc_Edad, DbType.Int32, ParameterDirection.Input);
            parameterEspc.Add("@masc_Sexo", masc_Sexo, DbType.String, ParameterDirection.Input);
            parameterEspc.Add("@masc_Peso", masc_Peso, DbType.String, ParameterDirection.Input);
            parameterEspc.Add("@masc_Talla", masc_Talla, DbType.String, ParameterDirection.Input);
            parameterEspc.Add("@masc_Color", masc_Color, DbType.String, ParameterDirection.Input);
            parameterEspc.Add("@emp_Id", emp_Id, DbType.Int32, ParameterDirection.Input);
            //parameterEspc.Add("@masc_EsAdoptado", masc_EsAdoptado, DbType.Boolean, ParameterDirection.Input);
            //parameterEspc.Add("@masc_EsReservado", masc_EsReservado, DbType.Boolean, ParameterDirection.Input);
            parameterEspc.Add("@masc_HistorialDescripcion", masc_HistorialDescripcion, DbType.String, ParameterDirection.Input);
            parameterEspc.Add("@proc_Id", proc_Id, DbType.Int32, ParameterDirection.Input);

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
                    return masc_Id;

                errorTransaction:
                    transaction.Rollback();
                    db.Close();
                    db.Dispose();
                    return -1;
                }
            }
        }


        public tbMascotas updateMascotasImagen1(string tbMascotas, int masc_Id)
        {
            var resultado = new tbMascotas();
            const string sqlQuery = @"UDP_tbMascotas_UpdateImagen";

            var parameters = new DynamicParameters();
            parameters.Add("@masc_Imagen", tbMascotas, DbType.String, ParameterDirection.Input);
            parameters.Add("@masc_Id", masc_Id, DbType.Int32, ParameterDirection.Input);

            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    resultado = db.ExecuteScalar<tbMascotas>(sqlQuery, parameters, transaction, commandType: CommandType.StoredProcedure);
                    if (resultado == null)
                    {

                    }
                    else
                    {
                        goto errorTransaction;
                    }
                    transaction.Commit();
                    db.Close();
                    db.Dispose();
                    return resultado;

                errorTransaction:
                    transaction.Rollback();
                    db.Close();
                    db.Dispose();
                    return resultado;
                }
            }
        }

    }
}
