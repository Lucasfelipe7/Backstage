namespace Domain.DTO.Mensagem;

public record MensagemGetAllFilterDto(
    int IdMensagem = 0,
    int IdUnidadeJurisdicionada = 0,
    int IdDiretoria = 0,
    int IdSistema = 0,
    int IdMensagemPai = 0,
    int IdOperador = 0,
    bool Rascunho = false,
    bool Arquivado = false,
    bool Enviado = false,
    bool ExibirDestinatarios = false,
    bool ExibirAgendados = false
);