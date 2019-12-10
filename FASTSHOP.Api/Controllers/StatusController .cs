using FASTSHOP.Api.Domain.Enums;
using FASTSHOP.Api.Domain.Interfaces;
using FASTSHOP.Api.Domain.Models;
using FASTSHOP.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace FASTSHOP.Api.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    public class StatusController : Controller
    {
        private readonly ILogger _logger;

        public StatusController(
             ILogger<ProductsController> logger
            )
        {
            this._logger = logger;
        }

        [HttpGet]
        public List<string> Get()
        {
            try
            {
                //return Enum.GetValues(typeof(StatusEnum)).Cast<StatusEnum>().ToList();
                return Enum.GetNames(typeof(StatusEnum)).ToList();
            }
            catch (Exception ex)
            {
                string error = "Não foi possível realizar a busca de Productes";
                this._logger.LogError(ex, error);
                return null;
            }
        }

    }
}
