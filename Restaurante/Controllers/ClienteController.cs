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
    //  api/cliente
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public ClienteController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }


        //CONSULTA
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                        select Cedula,Nombre,Descripcion,Telefono,Usuario, Password,Imagen
                        from 
                        cliente
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
                        delete from cliente 
                        where Cedula=@Cedula;
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection mycon = new NpgsqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@Cedula", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }

        //ACTUALIZACI�N


        [HttpPut("{id}")]
        public JsonResult Put(Cliente emp)
        {
            string query = @"
                        update cliente set
                        Nombre = @Nombre,
                        Descripcion = @Descripcion,  
                        Telefono = @Telefono,
                        Usuario =  @Usuario,
                        Password = @Password
                        Imagen = @Imagen
                        where Cedula = @Cedula;

            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection mycon = new NpgsqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@Cedula", emp.Cedula);
                    myCommand.Parameters.AddWithValue("@Nombre", emp.Nombre);
                    myCommand.Parameters.AddWithValue("@Descripcion", emp.Descripcion);
                    myCommand.Parameters.AddWithValue("@Telefono", emp.Telefono);
                    myCommand.Parameters.AddWithValue("@Usuario", emp.Usuario);
                    myCommand.Parameters.AddWithValue("@Password", emp.Password);
                    myCommand.Parameters.AddWithValue("@Imagen", emp.Imagen);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }
        //CREACI�N

        [HttpPost]
        public JsonResult Post(Models.Cliente emp)
        {
            string query = @"
                        insert into cliente 
                        (Cedula,Nombre,Descripcion,Telefono, Usuario, Password,Imagen) 
                        values
                         (@Cedula,@Nombre,@Descripcion,@Telefono,@Usuario,@Password,@Imagen);
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection mycon = new NpgsqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, mycon))
                {

                    myCommand.Parameters.AddWithValue("@Cedula", emp.Cedula);
                    myCommand.Parameters.AddWithValue("@Nombre", emp.Nombre);
                    myCommand.Parameters.AddWithValue("@Descripcion", emp.Descripcion);
                    myCommand.Parameters.AddWithValue("@Telefono", emp.Telefono);
                    myCommand.Parameters.AddWithValue("@Usuario", emp.Usuario);
                    myCommand.Parameters.AddWithValue("@Password", emp.Password);
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