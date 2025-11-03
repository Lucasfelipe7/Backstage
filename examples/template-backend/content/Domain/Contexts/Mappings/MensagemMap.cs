using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Mappings
{
    public class MensagemMap : IEntityTypeConfiguration<TbMensagem>
    {
        public void Configure(EntityTypeBuilder<TbMensagem> builder)
        {
            builder.ToTable("TbMensagem");
            builder.HasKey(m => m.IdMensagem);
            builder.Property(m => m.IdUnidadeJurisdicionada);
            builder.Property(m => m.IdDiretoria);
            builder.Property(m => m.IdSistema);
            builder.Property(m => m.IdOperador);
            builder.Property(m => m.IdMarcador);
            builder.Property(m => m.Assunto).IsRequired().HasMaxLength(128);
            builder.Property(m => m.Mensagem).IsRequired().HasMaxLength(8000);
            builder.Property(m => m.Token).IsRequired().HasMaxLength(256);
            builder.Property(m => m.IdMensagemPai);
            builder.Property(m => m.DataEnvio).HasColumnType("smalldatetime");
            builder.Property(m => m.DataArquivamento).HasColumnType("smalldatetime");
            builder.Property(m => m.DataInativacao).HasColumnType("smalldatetime");
            builder.Property(m => m.DataInclusao).IsRequired().HasColumnType("smalldatetime");
            builder.Property(m => m.IdSessao).IsRequired();
            builder.Property(m => m.IdSessaoOperacao);
            builder.Property(m => m.PodeResponder).HasColumnType("bit");
            builder.Property(m => m.EnviarWhatsapp).HasColumnType("bit");
            builder.Property(m => m.EnviarEmail).HasColumnType("bit");
            builder.Property(m => m.DataAgendamentoEnvio).HasColumnType("smalldatetime");
            builder.Property(m => m.Prazo).HasColumnType("smalldatetime");
            builder.Property(m => m.DataUltimaMensagemFilha).HasColumnType("smalldatetime");

            builder.HasOne(m => m.MensagemPai)
                    .WithMany(m => m.MensagensFilhas)
                    .HasForeignKey(m => m.IdMensagemPai);
        }
    }
}