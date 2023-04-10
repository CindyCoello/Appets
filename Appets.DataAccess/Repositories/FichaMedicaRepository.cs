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
    public class FichaMedicaRepository
    {
        public IEnumerable<UDP_tbFichaMedica_SelectResult> FichaList()
        {
            const string query = @"UDP_tbFichaMedica_Select";
            var parameters = new DynamicParameters();


            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.Query<UDP_tbFichaMedica_SelectResult>(query, parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultado;

            }


        }


        public tbFichaMedica Find(int id)
        {
            const string query = @"UDP_tbFichaMedica_Find";
            var parameters = new DynamicParameters();
            parameters.Add("@medic_Id", id, DbType.Int32, ParameterDirection.Input);

            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.QueryFirstOrDefault<tbFichaMedica>(query, parameters, commandType: CommandType.StoredProcedure);
                return resultado;

            }


        }


        public string Delete(int id)
        {
            const string query = @"UDP_tbFichaMedica_Delete";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id, DbType.Int32, ParameterDirection.Input);
            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                var resultado = db.ExecuteScalar<string>(query, parameters, commandType: CommandType.StoredProcedure);
                return resultado;
            }
        }


        public int Insert(
            int masc_Id, 
            bool medic_Esterilizacion,
            string medic_Personalidad,
            string medic_SaludCuidado, 
            string medic_InformacionAdicional)
        {
            int resultado = 0;
            const string sqlQueryFicha = "UDP_tbFichaMedica_Insert";

            var parameters = new DynamicParameters();
            parameters.Add("@masc_Id", masc_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@medic_Esterilizacion", medic_Esterilizacion, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("@medic_Personalidad", medic_Personalidad, DbType.String, ParameterDirection.Input);
            parameters.Add("@medic_SaludCuidado", medic_SaludCuidado, DbType.String, ParameterDirection.Input);
            parameters.Add("@medic_InformacionAdicional", medic_InformacionAdicional, DbType.String, ParameterDirection.Input);
    

            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    resultado = db.ExecuteScalar<int>(sqlQueryFicha, parameters, transaction, commandType: CommandType.StoredProcedure);
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
            int medic_Id,
            int masc_Id, bool medic_Esterilizacion,
            string medic_Personalidad, 
            string medic_SaludCuidado, 
            string medic_InformacionAdicional)
        {
            int resultado = 0;
            const string sqlQueryFicha = "UDP_tbFichaMedica_Update";

            var parameters = new DynamicParameters();
            parameters.Add("@medic_Id", medic_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@masc_Id", masc_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@medic_Esterilizacion", medic_Esterilizacion, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("@medic_Personalidad", medic_Personalidad, DbType.String, ParameterDirection.Input);
            parameters.Add("@medic_SaludCuidado", medic_SaludCuidado, DbType.String, ParameterDirection.Input);
            parameters.Add("@medic_InformacionAdicional", medic_InformacionAdicional, DbType.String, ParameterDirection.Input);

            using (var db = new SqlConnection(AppetsDbContext.ConnectionString))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    resultado = db.ExecuteScalar<int>(sqlQueryFicha, parameters, transaction, commandType: CommandType.StoredProcedure);
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
                    return medic_Id;

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
