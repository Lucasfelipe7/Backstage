using AutoMapper;
using Domain.DTO;
using Domain.DTO.Mensagem;
using Domain.Entities;

namespace Application.AutoMapper
{
    /// <summary>
    /// Configuração do AutoMapper para a classe TbMensagem
    /// </summary>
    public class TbMensagemProfile
        : Profile
    {
        
        public TbMensagemProfile()
        {
            CreateMap<TbMensagemDto, TbMensagem>().ReverseMap();
            CreateMap<MensagemGetAllFilter, MensagemGetAllFilterDto>().ReverseMap();
            CreateMap<TbMensagem, TbMensagemDto>().ForMember(x => x.DataInclusao, opt => opt.Ignore());
        }
    }
}
