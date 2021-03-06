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
    //  api/reservas
    [Route("api/[controller]")]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public ReservasController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }


        //CONSULTA
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                        select idreserva, nombre,telefono,email,numper,nombreser,fecha,hora,indicaciones,imagen from cliente 
                        INNER JOIN reserva  ON cliente.cedula = reserva.cedula  
                        INNER JOIN servicio ON reserva.idservicio = servicio.idservicio
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
                        delete from reserva
                        where Idreserva=@Idreserva;    
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection mycon = new NpgsqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@Idreserva", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }

        //ACTUALIZACI?N


        [HttpPut("{id}")]
        public JsonResult Put(Reservas emp)
        {
            string query = @"
                        update reserva set 
                        Idservicio =@Idservicio, 
                        Cedula =@Cedula
                        Email = @Email,
                        Numper = @Numper,
                        Fecha =@Fecha,
                        Hora = @Hora,
                        Indicaciones = @Indicaciones
                        where Idreserva =@Idreserva;
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection mycon = new NpgsqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@Idreserva", emp.Idreserva);
                    myCommand.Parameters.AddWithValue("@Idservicio", emp.Idservicio);
                    myCommand.Parameters.AddWithValue("@Cedula", emp.Cedula);
                    myCommand.Parameters.AddWithValue("@Email", emp.Email);
                    myCommand.Parameters.AddWithValue("@Numper", emp.Numper);
                    myCommand.Parameters.AddWithValue("@Fecha", emp.Fecha);
                    myCommand.Parameters.AddWithValue("@Hora", emp.Hora);
                    myCommand.Parameters.AddWithValue("@Indicaciones", emp.Indicaciones);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }
        //CREACI?N

        [HttpPost]
        public JsonResult Post(Models.Reservas emp)
        {
            string query = @"
                        insert into reserva
                        values
                         (@Idreserva,@Idservicio,@Cedula,@Email,@Numper,@Fecha,@Hora,@Indicaciones);
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection mycon = new NpgsqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@Idreserva", emp.Idreserva);
                    myCommand.Parameters.AddWithValue("@Idservicio", emp.Idservicio);
                    myCommand.Parameters.AddWithValue("@Cedula", emp.Cedula);
                    myCommand.Parameters.AddWithValue("@Email", emp.Email);
                    myCommand.Parameters.AddWithValue("@Numper", emp.Numper);
                    myCommand.Parameters.AddWithValue("@Fecha", emp.Fecha);
                    myCommand.Parameters.AddWithValue("@Hora", emp.Hora);
                    myCommand.Parameters.AddWithValue("@Indicaciones", emp.Indicaciones);


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