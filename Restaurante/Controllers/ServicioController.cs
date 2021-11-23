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
    //  api/servicio
    [Route("api/[controller]")]
    [ApiController]
    public class ServicioController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public ServicioController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }


        //CONSULTA
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                        select IdServicio,Nombre,Descripcion,Imagen
                        from 
                        servicio
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
                        delete from servicio 
                        where IdServicio=@IdServicio;
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection mycon = new NpgsqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@IdServicio", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }

        //ACTUALIZACIÓN


        [HttpPut]
        public JsonResult Put(Servicio emp)
        {
            string query = @"
                        update empleado set 
                        IdServicio=@IdServicio,
                        Nombre =@EmpleadoNombre,
                        Descripcion =@EmpleadoDescripcion,
                        where Imagen =@Imagen;
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection mycon = new NpgsqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@IdServicio", emp.IdServicio);
                    myCommand.Parameters.AddWithValue("@Nombre", emp.Nombre);
                    myCommand.Parameters.AddWithValue("@Descripcion", emp.Descripcion);
                    myCommand.Parameters.AddWithValue("@Imagen", emp.Imagen);

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
        public JsonResult Post(Models.Servicio emp)
        {
            string query = @"
                        insert into servicio
                        (IdServicio,Nombre,Descripcion,Imagen) 
                        values
                         (@IdServicio,@Nombre,@Descripcion,@Imagen);
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection mycon = new NpgsqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@IdServicio", emp.IdServicio);
                    myCommand.Parameters.AddWithValue("@Nombre", emp.Nombre);
                    myCommand.Parameters.AddWithValue("@Descripcion", emp.Descripcion);
                    myCommand.Parameters.AddWithValue("@Imagen", emp.Imagen);

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