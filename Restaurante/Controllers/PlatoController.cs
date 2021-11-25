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
    //  api/plato
    [Route("api/[controller]")]
    [ApiController]
    public class PlatoController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public PlatoController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }


        //CONSULTA
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                        select Idplato,Nombre,Descripcion,Imagen,Precio, IdRestaurante
                        from 
                        plato
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
                        delete from plato
                        where Idplato=@Idplato;
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection mycon = new NpgsqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@Idplato", id);

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
        public JsonResult Put(Plato emp)
        {
            string query = @"
                        update plato set 
                        Nombre =@Nombre,
                        Descripcion =@Descripcion,  
                        Imagen = @Imagen,
                        Precio = @Precio,
                        IdRestaurante =@IdRestaurante
                        where Idplato =@Idplato;
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection mycon = new NpgsqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@Idplato", emp.Idplato);
                    myCommand.Parameters.AddWithValue("@Nombre", emp.Nombre);
                    myCommand.Parameters.AddWithValue("@Descripcion", emp.Descripcion);
                    myCommand.Parameters.AddWithValue("@Precio", emp.Precio);
                    myCommand.Parameters.AddWithValue("@Imagen", emp.Imagen);
                    myCommand.Parameters.AddWithValue("@IdRestaurante", emp.IdRestaurante);

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
        public JsonResult Post(Models.Plato emp)
        {
            string query = @"
                        insert into plato 
                        (Idplato, Nombre,Descripcion,Imagen,Precio,IdRestaurante) 
                        values
                         (@Idplato,@Nombre,@Descripcion,@Imagen,@Precio,@IdRestaurante);
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection mycon = new NpgsqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@Idplato", emp.Idplato);
                    myCommand.Parameters.AddWithValue("@Nombre", emp.Nombre);
                    myCommand.Parameters.AddWithValue("@Descripcion", emp.Descripcion);
                    myCommand.Parameters.AddWithValue("@Precio", emp.Precio);
                    myCommand.Parameters.AddWithValue("@Imagen", emp.Imagen);
                    myCommand.Parameters.AddWithValue("@IdRestaurante", emp.IdRestaurante);

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