using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Domain.DTO
{
    public class TbMensagemDto
    {
        public int IdMensagem  {get; set;}
        public int? IdDiretoria { get; set; }
        public int? IdUnidadeJurisdicionada { get; set; }
        public int? IdSistema { get; set; }
        public int? IdOperador { get; set; }
        public string Assunto { get; set; }
        public string Mensagem { get; set; }
        public string Token { get; set; }
        public int? IdMarcador { get; set; }
        public int? IdMensagemPai { get; set; }
        public DateTime? DataEnvio { get; set; }
        public DateTime? DataArquivamento { get; set; }
        public DateTime? DataInativacao { get; set; }
        public DateTime? DataInclusao { get; set; }
        public DateTime? DataAgendamentoEnvio { get; set; }
        public int IdSessao { get; set; }
        public int? IdSessaoOperacao { get; set; }
        public bool? PodeResponder { get; set; }
        public bool? EnviarWhatsapp { get; set; }
        public bool? EnviarEmail { get; set; }
        public DateTime? Prazo { get; set; }
        public DateTime? DataUltimaMensagemFilha { get; set; }
        public TbMensagemDto MensagemPai { get; set; }
        public List<TbMensagemDto> MensagensFilhas { get; set; }
        public bool IsRascunho { get; set; }
        public bool EnviarRascunho { get; set; }
        public List<IFormFile> File { get; set; }
    }
}