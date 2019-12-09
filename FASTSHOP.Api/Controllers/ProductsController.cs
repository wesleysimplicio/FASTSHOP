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
    public class ProductsController : Controller
    {
        private readonly IProductBusiness _productBusiness;
        private readonly ILogger _logger;

        public ProductsController(
             IProductBusiness productBusiness,
             ILogger<ProductsController> logger
            )
        {
            this._productBusiness = productBusiness;
            this._logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(this._productBusiness.Get());
            }
            catch (Exception ex)
            {
                string error = "Não foi possível realizar a busca de Productes";
                this._logger.LogError(ex, error);
                return BadRequest(new ErrorItem(1, error));
            }
        }

        [HttpGet("{Code}")]
        public IActionResult GetById(string Code)
        {
            try
            {
                return Ok(this._productBusiness.GetById(Code));
            }
            catch (Exception ex)
            {
                string error = $"Não foi possível realizar a busca do Produto: {Code}";
                this._logger.LogError(ex, error);
                return BadRequest(new ErrorItem(2, error));
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]Product Product)
        {
            try
            {
                var resul = this._productBusiness.Insert(Product);
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
        public IActionResult Put(string Code, [FromBody]Product Product)
        {
            try
            {
                Product.Code = Code;
                var resul = this._productBusiness.Update(Product);
                if (resul)
                {
                    return Ok();
                }
                else
                {
                    _logger.LogDebug("Não foi possível atualizar Produto : " + Code);
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
                _logger.LogDebug("Exclusão do Produto: " + Code);
                var ok = this._productBusiness.Delete(Code);
                if (ok)
                {
                    return new NoContentResult();
                }
                else
                {
                    _logger.LogDebug("Produto não encontrado");
                    return new NotFoundResult();
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Erro ao excluir Produto");
                return new BadRequestResult();
            }
        }

    }
}
