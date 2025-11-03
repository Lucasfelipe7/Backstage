namespace Domain.DTO.Mensagem;

public record MensagemGetAllFilter(
    int IdMensagem = 0, 
    int IdUnidadeJurisdicionada = 0, 
    int IdDiretoria = 0,
    int IdSistema = 0, 
    int IdMensagemPai = 0, 
    bool ExibirDestinatarios = false, 
    bool PendentesEnvio = false,
    int IdOperador = 0, 
    bool ExibirAgendados = false, 
    bool ExibirPendentesdeEnvioWhatsapp = false,
    bool ExibirPendentesEnvioEmail = false
);