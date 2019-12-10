using FASTSHOP.Api.Domain.Enums;
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
        public IActionResult Get(
                [FromQuery]string name,
                [FromQuery]long? document)
        {
            try
            {
                var client = new Client();
                client.Name = name;
                client.Document = document;
                return Ok(this._clientBusiness.Get(client));
            }
            catch (Exception ex)
            {
                string error = "Não foi possível realizar a busca de clientes";
                this._logger.LogError(ex, error);
                return BadRequest(new ErrorItem(1, error));
            }
        }

        [HttpGet("{document}")]
        public IActionResult GetByDocument(long document)
        {
            try
            {
                return Ok(this._clientBusiness.GetByDocument(document));
            }
            catch (Exception ex)
            {
                string error = $"Não foi possível realizar a busca do cliente: {document}";
                this._logger.LogError(ex, error);
                return BadRequest(new ErrorItem(2, error));
            }
        }

        [HttpGet("{Code}")]
        public IActionResult GetById(string Code)
        {
            try
            {
                return Ok(this._clientBusiness.GetById(Code));
            }
            catch (Exception ex)
            {
                string error = $"Não foi possível realizar a busca do cliente: {Code}";
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
                string error = string.IsNullOrWhiteSpace(ex.Message) ? "Não foi possível inserir" : ex.Message;
                this._logger.LogError(ex, error);
                return BadRequest(new ErrorItem(3, error));
            }
        }

        [HttpPut("{Code}")]
        public IActionResult Put(string Code, [FromBody]Client client)
        {
            try
            {
                client.Code = Code;
                var resul = this._clientBusiness.Update(client);
                if (resul)
                {
                    return Ok();
                }
                else
                {
                    _logger.LogDebug("Não foi possível atualizar cliente : " + Code);
                    return new BadRequestResult();
                }

            }
            catch (Exception ex)
            {
                string error = string.IsNullOrWhiteSpace(ex.Message) ? "Não foi possível atualizar" : ex.Message;
                this._logger.LogError(ex, error);
                return BadRequest(new ErrorItem(4, error));
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
