using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.DTO;
using Domain.DTO.Mensagem;
using Domain.Entities;
using Exceptions;
using Serilog;
using TCE.Base.Services;
using TCE.Base.UnitOfWork;

namespace Services
{
    public class MensagemService : BaseService<TbMensagem>
    {
        private readonly IUnitOfWork _uow;

        private readonly IMapper _mapper;

        public MensagemService(IUnitOfWork uow, 
            IMapper mapper) : base(uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<List<TbMensagem>> GetAllAsync(MensagemGetAllFilter filter)
        {
            List<TbMensagem> mensagens = GetAll(
                predicate: m => m.DataInativacao == null
                    && (filter.IdMensagem == 0 || filter.IdMensagem == m.IdMensagem)
                    && (filter.IdUnidadeJurisdicionada == 0 || filter.IdUnidadeJurisdicionada == m.IdUnidadeJurisdicionada)
                    && (filter.IdDiretoria == 0 || filter.IdDiretoria == m.IdDiretoria)
                    && (filter.IdSistema == 0 || filter.IdSistema == m.IdSistema)
                    && (filter.IdOperador == 0 || filter.IdOperador == m.IdOperador)
                    && (filter.IdMensagemPai == 0 || filter.IdMensagemPai == m.IdMensagemPai)
                    && (filter.PendentesEnvio || m.DataEnvio == null)
                    && (filter.ExibirAgendados || m.DataAgendamentoEnvio == null)
                    && (filter.ExibirPendentesdeEnvioWhatsapp || m.EnviarWhatsapp == true)
                    && (filter.ExibirPendentesEnvioEmail || m.EnviarEmail == true)
            ).ToList();

            return mensagens;
        }

        public async Task<TbMensagem> Create(TbMensagemDto mensagem)
        {
            try
            {
                _uow.ForceBeginTransaction();

                mensagem.DataInclusao = DateTime.Now;

                mensagem.Token = this.StringSha256Hash(DateTime.Now.Millisecond.ToString());

                mensagem.EnviarEmail ??= false;

                mensagem.EnviarWhatsapp ??= false;

                if (mensagem.IsRascunho || mensagem.DataAgendamentoEnvio != null)
                {
                    mensagem.DataEnvio = null;
                }
                else
                {
                    mensagem.DataEnvio = DateTime.Now;
                }

                TbMensagem mensagemCriada = this.Add<MensagemValidator>(_mapper.Map<TbMensagem>(mensagem));

                if (!mensagem.IsRascunho && mensagem.DataAgendamentoEnvio == null)
                {
                    if (mensagemCriada.IdMensagemPai == null)
                    {
                        mensagemCriada.DataUltimaMensagemFilha = mensagem.DataEnvio;    
                    }
                    else
                    {
                        var msgPai = GetById(msg => msg.IdMensagem == mensagemCriada.IdMensagemPai);
                        msgPai.DataUltimaMensagemFilha = mensagem.DataEnvio;
                        Update<MensagemValidator>(msgPai);
                    }
                }

                _uow.CommitTransaction();
                return mensagemCriada;

            }
            catch (Exception e)
            {
                _uow.RollbackTransaction();
                var msg = "Erro no cadastro de Mensagem";
                Log.Error($"Erro na atualização de rascunho:\n{e.Message}\n\n{e.StackTrace}");
                throw new ValidationException(msg, e);
            }

        }

        public string StringSha256Hash(string text) =>
                    string.IsNullOrEmpty(text) ? string.Empty : BitConverter.ToString(new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(text))).Replace("-", string.Empty);


        [SuppressMessage("SonarLint", "csharpsquid:S1144", Justification = "Remover se não for utilizar.")]
        private void UpdateMessageProperties(TbMensagemDto mensagemDto, TbMensagem mensagem)
        {
            mensagem.Token = "Teste";
            mensagem.EnviarEmail = mensagemDto.EnviarEmail ?? false;
            mensagem.EnviarWhatsapp = mensagemDto.EnviarWhatsapp ?? false;
            mensagem.Mensagem = mensagemDto.Mensagem;
            mensagem.Assunto = mensagemDto.Assunto;
            mensagem.IdMarcador = mensagemDto.IdMarcador;
            mensagem.PodeResponder = mensagemDto.PodeResponder;

            _uow.GetRepository<TbMensagem>().Update(mensagem);

            if (mensagemDto.EnviarRascunho)
                mensagem.DataEnvio = DateTime.Now;
        }

        public async Task<TbMensagem> GetByIdAsync(int id)
        {
            TbMensagem mensagem = GetById(x => x.IdMensagem == id);
            return mensagem;
        }

        public TbMensagem Inativar(int idMensagem)
        {
            TbMensagem mensagemBanco = this.GetById(
                predicate: m => m.IdMensagem == idMensagem
            );

            if (mensagemBanco == null)
                throw new NaoEncontradoException("A mensagem especificada não foi encontrada.");
            if (mensagemBanco.DataInativacao != null)
                throw new JaInativadoException("A mensagem especificada já foi inativada.");

            mensagemBanco.DataInativacao = DateTime.Now;
            return this.Update<MensagemValidator>(mensagemBanco);
        }

        public TbMensagem Reativar(int idMensagem)
        {
            TbMensagem mensagemBanco = this.GetById(
                predicate: m => m.IdMensagem == idMensagem
            );

            if (mensagemBanco == null)
                throw new NaoEncontradoException("A mensagem especificada não foi encontrada.");
            if (mensagemBanco.DataInativacao == null)
                throw new NaoInativadoException("A mensagem especificada não foi inativada.");

            mensagemBanco.DataInativacao = null;
            return this.Update<MensagemValidator>(mensagemBanco);
        }
    }
}