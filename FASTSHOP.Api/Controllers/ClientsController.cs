using FASTSHOP.Api.Domain.Interfaces;
using FASTSHOP.Api.Domain.Models;
using FASTSHOP.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;

namespace FASTSHOP.Api.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    public class ClientsController : Controller
    {
        private readonly IClientBusiness _clientBusiness;
        private readonly ILogger _logger;

        public ClientsController(
             IClientBusiness clientBusiness,
             ILogger<ClientsController> logger
            )
        {
            this._clientBusiness = clientBusiness;
            this._logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(this._clientBusiness.Get());
            }
            catch (Exception ex)
            {
                string error = "Não foi possível realizar a busca de clientes";
                this._logger.LogError(ex, error);
                return BadRequest(new ErrorItem(1, error));
            }
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(string Id)
        {
            try
            {
                return Ok(this._clientBusiness.GetById(Id));
            }
            catch (Exception ex)
            {
                string error = $"Não foi possível realizar a busca do cliente: {Id}";
                this._logger.LogError(ex, error);
                return BadRequest(new ErrorItem(2, error));
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]Client client)
        {
            try
            {
                var resul = this._clientBusiness.Insert(client);
                if (resul)
                {
                    return Ok();
                }
                else
                {
                    _logger.LogDebug("Não foi possível inserir");
                    return new BadRequestResult();
                }

            }
            catch (Exception ex)
            {
                string error = $"Não foi possível inserir";
                this._logger.LogError(ex, error);
                return BadRequest(new ErrorItem(2, error));
            }
        }

        [HttpPut("{Id}")]
        public IActionResult Put(string Id, [FromBody]Client client)
        {
            try
            {
                client.Id = Id;
                var resul = this._clientBusiness.Update(client);
                if (resul)
                {
                    return Ok();
                }
                else
                {
                    _logger.LogDebug("Não foi possível atualizar cliente : " + Id);
                    return new BadRequestResult();
                }

            }
            catch (Exception ex)
            {
                string error = $"Não foi possível inserir";
                this._logger.LogError(ex, error);
                return BadRequest(new ErrorItem(2, error));
            }
        }

        [HttpDelete("{Document}")]
        public IActionResult Delete(long? Document)
        {
            try
            {
                _logger.LogDebug("Exclusão do cliente: " + Document);
                var ok = this._clientBusiness.Delete(Document);
                if (ok)
                {
                    return new NoContentResult();
                }
                else
                {
                    _logger.LogDebug("Cliente não encontrado");
                    return new NotFoundResult();
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Erro ao excluir cliente");
                return new BadRequestResult();
            }
        }

    }
}
