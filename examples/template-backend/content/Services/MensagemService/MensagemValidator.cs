using System;
using Domain.Entities;
using FluentValidation;

namespace Services {
    public class MensagemValidator : AbstractValidator<TbMensagem> {
        public MensagemValidator() {
            RuleFor(c => c)
                .NotNull()
                .WithMessage("Não foi possível iniciar o objeto.");
            
            RuleFor(c => c.Assunto)
                .NotEmpty()
                .WithMessage("Necessário informar o Assunto da TbMensagem.")
                .Length(1, 128)
                .WithMessage("O Assunto da TbMensagem precisa ter entre 1 e 128 caracteres.");
            
            RuleFor(c => c.Mensagem)
                .NotEmpty()
                .WithMessage("Necessário informar a Mensagem da TbMensagem.")
                .Length(1, 8000)
                .WithMessage("A Mensagem da TbMensagem precisa ter entre 1 e 8000 caracteres.");
        }
    }
}