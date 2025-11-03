using AutoMapper;
using System;

namespace Application.AutoMapper
{
    /// <summary>
    /// Classe geral de mapeamento
    /// </summary>
    public static class ConfigureMap
    {
        /// <summary>
        /// Configurar mapeamentos
        /// </summary>
        public static IMapper Configure(IServiceProvider services)
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TbMensagemProfile>();
            });

            IMapper mapper = mapperConfig.CreateMapper();
            return mapper;
        }
    }
}
