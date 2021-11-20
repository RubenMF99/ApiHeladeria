using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Restaurante.Models;

namespace Restaurante.Controllers
{
    //  api/empleado
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public EmpleadoController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }


        //CONSULTA
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                        select Idempleado,Nombre,Descripcion,IdRestaurante,Imagen
                        from 
                        empleado
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
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
                        delete from empleado 
                        where Idempleado=@EmpleadoIdempleado;
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@EmpleadoIdempleado", id);

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
        public JsonResult Put(Empleado emp)
        {
            string query = @"
                        update empleado set 
                        Nombre =@EmpleadoNombre,
                        Descripcion =@EmpleadoDescripcion,  
                        IdRestaurante =@EmpleadoIdRestaurante,
                        Imagen = @EmpleadoImagen,
                        where Idempleado =@EmpleadoIdIdempleado;
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@EmpleadoId", emp.Idempleado);
                    myCommand.Parameters.AddWithValue("@EmpleadoNombre", emp.nombre);
                    myCommand.Parameters.AddWithValue("@EmpleadoDescripcion", emp.Descripcion);
                    myCommand.Parameters.AddWithValue("@EmpleadoIdRestaurante", emp.IdRestaurante);
                    myCommand.Parameters.AddWithValue("@EmpleadoImagen", emp.Imagen);

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
        public JsonResult Post(Models.Empleado emp)
        {
            string query = @"
                        insert into empleado 
                        (Nombre,Descripcion,IdRestaurante,Imagen) 
                        values
                         (@EmpleadoNombre,@EmpleadoDescripcion,@EmpleadoIdRestaurante,@EmpleadoImagen);
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@EmpleadoNombre", emp.Nombre);
                    myCommand.Parameters.AddWithValue("@EmpleadoDescripcion", emp.Descripcion);
                    myCommand.Parameters.AddWithValue("@EmpleadoIdRestaurante", emp.IdRestaurante);
                    myCommand.Parameters.AddWithValue("@EmpleadoImagen", emp.Imagen);

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