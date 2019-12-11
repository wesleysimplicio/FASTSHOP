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
    public class OrdersController : Controller
    {
        private readonly IOrderBusiness _orderBusiness;
        private readonly ILogger _logger;

        public OrdersController(
             IOrderBusiness orderBusiness,
             ILogger<OrdersController> logger
            )
        {
            this._orderBusiness = orderBusiness;
            this._logger = logger;
        }

        [HttpGet]
        public IActionResult Get(
                [FromQuery]string code,
                [FromQuery]string client,
                [FromQuery]string clientId,
                [FromQuery]StatusEnum status,
                [FromQuery]DateTime? createAt)
        {
            try
            {
                var order = new Order();
                order.Code = code;
                order.Client = client;
                order.ClientId = clientId;
                order.Status = status;
                order.CreateAt = createAt;
                return Ok(this._orderBusiness.Get(order));
            }
            catch (Exception ex)
            {
                string error = "Não foi possível realizar a busca de Pedidos";
                this._logger.LogError(ex, error);
                return BadRequest(new ErrorItem(1, error));
            }
        }

        [HttpGet("{Code}")]
        public IActionResult GetById(string Code)
        {
            try
            {
                return Ok(this._orderBusiness.GetById(Code));
            }
            catch (Exception ex)
            {
                string error = $"Não foi possível realizar a busca do Pedido: {Code}";
                this._logger.LogError(ex, error);
                return BadRequest(new ErrorItem(2, error));
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]Order order)
        {
            try
            {
                var resul = this._orderBusiness.Insert(order);
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
        public IActionResult Put(string Code, [FromBody]Order order)
        {
            try
            {
                order.Code = Code;
                var resul = this._orderBusiness.Update(order);
                if (resul)
                {
                    return Ok();
                }
                else
                {
                    _logger.LogDebug("Não foi possível atualizar Pedido : " + Code);
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

        [HttpDelete("{Code}")]
        public IActionResult Delete(string Code)
        {
            try
            {
                _logger.LogDebug("Exclusão do Pedido: " + Code);
                var ok = this._orderBusiness.Delete(Code);
                if (ok)
                {
                    return new NoContentResult();
                }
                else
                {
                    _logger.LogDebug("Pedido não encontrado");
                    return new NotFoundResult();
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Erro ao excluir Pedido");
                return new BadRequestResult();
            }
        }

    }
}
