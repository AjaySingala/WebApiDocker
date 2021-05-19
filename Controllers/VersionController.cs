using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

// REF: https://code.visualstudio.com/docs/containers/quickstart-aspnet-core
// docker-compose up --build

namespace WebApiDocker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VersionController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IConfiguration _configuration;

        public VersionController(ILogger<WeatherForecastController> logger,
            IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<string> GetVersion()
        {
            var connectionString = _configuration.GetConnectionString("mssql");
            var connection = new SqlConnection(connectionString);
            connection.Open();

            var result = await connection.QueryFirstAsync<string>("SELECT @@VERSION");

            return result;
        }

        [HttpGet("Test/{name}")]
        public string Test(string name)
        {
            return $"{name}: Hello there! This is a test message";
        }
    }
}
