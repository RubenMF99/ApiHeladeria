using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Restaurante.Models;

namespace Restaurante.Controllers
{
    //  api/sugerencia
    [Route("api/[controller]")]
    [ApiController]
    public class SugerenciaController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public SugerenciaController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }


        //CONSULTA
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                        select Idsugerencia,Asunto,Nombre,Cedula,Email, Servicio, Sugerencia
                        from 
                        sugerencias INNER JOIN cliente USING(Cedula)
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection mycon = new NpgsqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult(table);
        }
        //ELIMINACION
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                        delete from sugerencias
                        where Idsugerencia=@Idsugerencia;
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection mycon = new NpgsqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@Idsugerencia", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }

        //ACTUALIZACIÓN


        [HttpPut("{id}")]
        public JsonResult Put(Sugerencias emp)
        {
            string query = @"
                        update sugerencias set
                        Asunto = @Asunto,
                        Cedula = @Cedula,  
                        Email = @Email,
                        Servicio=@Servicio,
                        Sugerencia=@Sugerencia
                        where Idsugerencia = @Idsugerencia;

            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection mycon = new NpgsqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@Idsugerencia", emp.Idsugerencia);
                    myCommand.Parameters.AddWithValue("@Asunto", emp.Asunto);
                    myCommand.Parameters.AddWithValue("@Cedula", emp.Cedula);
                    myCommand.Parameters.AddWithValue("@Email", emp.Email);
                    myCommand.Parameters.AddWithValue("@Servicio", emp.Servicio);
                    myCommand.Parameters.AddWithValue("@Sugerencia", emp.Sugerencia);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }
        //CREACIÓN

        [HttpPost]
        public JsonResult Post(Models.Sugerencias emp)
        {
            string query = @"
                        insert into sugerencias
                        (Idsugerencia,Asunto,Cedula,Email,Servicio,Sugerencia) 
                        values
                         (@Idsugerencia,@Asunto,@Cedula,@Email,@Servicio,@Sugerencia);
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection mycon = new NpgsqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, mycon))
                {

                    myCommand.Parameters.AddWithValue("@Idsugerencia", emp.Idsugerencia);
                    myCommand.Parameters.AddWithValue("@Asunto", emp.Asunto);
                    myCommand.Parameters.AddWithValue("@Cedula", emp.Cedula);
                    myCommand.Parameters.AddWithValue("@Email", emp.Email);
                    myCommand.Parameters.AddWithValue("@Servicio", emp.Servicio);
                    myCommand.Parameters.AddWithValue("@Sugerencia", emp.Sugerencia);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }
    }
}