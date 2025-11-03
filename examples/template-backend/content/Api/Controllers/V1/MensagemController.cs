using Asp.Versioning;
using AutoMapper;
using Domain.DTO;
using Domain.DTO.Mensagem;
using Domain.Entities;
using Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using System;
using System.Threading.Tasks;
using TceCore.ACL;

namespace Application.Controllers.V1
{

    /// <summary>
    /// Controlador para gerenciar rotas relacionadas à mensagem.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class MensagemController : Controller
    {
        /// <sumary>
        ///     Retorna todas as mensagens que casam com os filtros passados nos parâmetros.
        /// </sumary>
        /// <param name="mensagemService">
        ///     Objeto de serviço para mensagem obtido por injeção de dependências.
        /// </param>
        /// <param name="filter">
        ///     Agrupa as seguintes propriedades para filtro
        /// </param>
        /// <param name="Mapper"></param>
        /// <param name="IdMensagem">
        ///     Filtro que seleciona as mensagens pelo seu identificador. Quando esse filtro é igual a 0, ele é desconsiderado na busca.
        /// </param>
        /// <param name="IdUnidadeJurisdicionada">
        ///     Filtro que seleciona as mensagens pelo identificador do operador que a enviou. Quando esse filtro é igual a 0, ele é desconsiderado na busca.
        /// </param>
        /// <param name="IdDiretoria">
        ///     Filtro que seleciona as mensagens pelo identificador da diretoria que a enviou. Quando esse filtro é igual a 0, ele é desconsiderado na busca.
        /// </param>
        /// <param name="IdSistema">
        ///     Filtro que seleciona as mensagens pelo identificador do sistema do qual ela foi enviada. Quando esse filtro é igual a 0, ele é desconsiderado na busca.
        /// </param>
        /// <param name="IdMensagemPai">
        ///     Filtro que seleciona as mensagens filhas de uma mensagem que possua identificador igual a idMensagemPai. Quando esse filtro é igual a 0, ele é desconsiderado na busca.
        /// </param>
        /// <param name="Rascunho">
        ///     Filtro que seleciona os destinatários pela DataEnvio. Quando ele é igual a 0, ele é desconsiderado na busca.
        /// </param>
        /// <param name="Arquivado">
        ///     Filtro que seleciona os destinatários pela DataArquivamento. Quando ele é igual a 0, ele é desconsiderado na busca.
        /// </param>
        /// <param name="Enviado">
        ///     Filtro que seleciona os destinatários pela DataEnvio. Quando ele é igual a 0, ele é desconsiderado na busca.
        /// </param>
        /// <param name="ExibirDestinatarios">
        ///     Habilita a exibição de Destinatários associados.
        /// </param>
        /// <param name="IdOperador">
        ///     IdOperador de quem enviou a mensagem
        /// </param>
        /// <param name="ExibirAgendados">
        ///     permite a exibição de mensagens agendadas
        /// </param>
        /// <response code="200">Lista com todos os destinatários selecionados.</response>
        /// <response code="400">incorrect request</response>
        /// <response code="401">not authorized</response>
        /// <response code="404">resource not found</response>
        /// <response code="500">internal error server</response>
        [HttpGet]
        [AuthorizeTCE(2)]
        public async Task<IActionResult> GetAll(
            [FromServices] MensagemService mensagemService,
            [FromQuery] MensagemGetAllFilterDto filter,
            [FromServices] IMapper Mapper)
        {
            var filterGetAll = Mapper.Map<MensagemGetAllFilter>(filter);
            
            var retorno = await mensagemService.GetAllAsync(filterGetAll);

            if (filter.Rascunho)
                retorno = retorno.FindAll(x => x.DataEnvio == null);

            if (filter.Enviado)
                retorno = retorno.FindAll(x => x.DataEnvio != null);

            if (filter.Arquivado)
                retorno = retorno.FindAll(x => x.DataArquivamento != null);

            return Ok(retorno);
        }

        /// <sumary>
        ///     Retorna a mensagem pelo idMensagem
        /// </sumary>
        /// <param name="mensagemService">
        ///     Objeto de serviço para mensagem obtido por injeção de dependências.
        /// </param>
        /// <param name="id">
        ///     Filtro que seleciona as mensagens pelo seu identificador. Quando esse filtro é igual a 0, ele é desconsiderado na busca.
        /// </param>
        /// <response code="200">Objeto com mensagem.</response>
        /// <response code="400">incorrect request</response>
        /// <response code="401">not authorized</response>
        /// <response code="404">resource not found</response>
        /// <response code="500">internal error server</response>
        /// //rota para editar rascunho
        [HttpGet("{id}")]
        [AuthorizeTCE]
        public async Task<IActionResult> GetById(
            [FromServices] MensagemService mensagemService,
            int id)
        {
            var retorno = await mensagemService.GetByIdAsync(id);
            return Ok(retorno);
        }

        /// <sumary>
        ///     Insere uma nova mensagem.
        /// </sumary>
        /// <param name="mensagemService">
        ///     Objeto de serviço para mensagem obtido por injeção de dependências.
        /// </param>
        /// <param name="mensagemDto">
        ///     Objeto da mensagem que será inserido.
        /// </param>
        /// <response code="200">O objeto da mensagem que foi inserido.</response>
        /// <response code="400">incorrect request</response>
        /// <response code="401">not authorized</response>
        /// <response code="404">resource not found</response>
        /// <response code="500">internal error server</response>
        [HttpPost]
        [AuthorizeTCE]
        public async Task<IActionResult> Add(
            [FromServices] MensagemService mensagemService,
            [FromForm] TbMensagemDto mensagemDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TbMensagem? mensagemPai = null;
            if (mensagemDto.IdMensagemPai != null)
            {
                mensagemPai = await mensagemService.GetByIdAsync((int)mensagemDto.IdMensagemPai);
            }

            if (mensagemDto.IdMensagemPai == null
                || (mensagemDto.IdMensagemPai != null
                    && mensagemPai != null
                    && mensagemPai.PodeResponder != null
                    && (bool)mensagemPai.PodeResponder))
            {
                return Ok(await mensagemService.Create(mensagemDto));
            }
            else
            {
                return BadRequest("A mensagem pai não pode ser respondida.");
            }
        }

        [HttpPut]
        [AuthorizeTCE]
        public IActionResult Update(
            [FromServices] MensagemService mensagemService,
            [FromBody] TbMensagem mensagem)
        {

            return Ok(mensagemService.Update<MensagemValidator>(mensagem));

        }

        /// <sumary>
        ///     Inativa a mensagem especificada.
        /// </sumary>
        /// <param name="mensagemService">
        ///     Objeto de serviço para mensagem obtido por injeção de dependências.
        /// </param>
        /// <param name="id">
        ///     O identificador da mensagem que será inativada.
        /// </param>
        /// <response code="200">success</response>
        /// <response code="400">incorrect request</response>
        /// <response code="401">not authorized</response>
        /// <response code="404">resource not found</response>
        /// <response code="500">internal error server</response>
        [HttpPut("Inativar/{id}")]
        [AuthorizeTCE]
        public IActionResult Inativar(
            [FromServices] MensagemService mensagemService,
            int id)
        {
            try
            {
                var result = mensagemService.Inativar(id);
                return Ok(result);
            }
            catch (JaInativadoException e)
            {
                return BadRequest(e.Message);
            }
            catch (NaoEncontradoException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <sumary>
        ///     Reativa a mensagem especificada.
        /// </sumary>
        /// <param name="mensagemService">
        ///     Objeto de serviço para mensagem obtido por injeção de dependências.
        /// </param>
        /// <param name="id">
        ///     O identificador da mensagem que será reativada.
        /// </param>
        /// <response code="200">success</response>
        /// <response code="400">incorrect request</response>
        /// <response code="401">not authorized</response>
        /// <response code="404">resource not found</response>
        /// <response code="500">internal error server</response>
        [HttpPut("Reativar/{id}")]
        [AuthorizeTCE]
        public IActionResult Reativar(
            [FromServices] MensagemService mensagemService,
            int id)
        {
            try
            {
                var result = mensagemService.Reativar(id);
                return Ok(result);
            }
            catch (NaoInativadoException e)
            {
                return BadRequest(e.Message);
            }
            catch (NaoEncontradoException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}