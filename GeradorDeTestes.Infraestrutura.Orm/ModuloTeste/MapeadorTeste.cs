using GeradorDeTestes.Dominio.ModuloTeste;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorDeTestes.Infraestrutura.Orm.ModuloTeste;
public class MapeadorTeste : IEntityTypeConfiguration<Teste>
{
    public void Configure(EntityTypeBuilder<Teste> builder)
    {
        builder.Property(t => t.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(t => t.Titulo)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(t => t.DataGeracao)
            .IsRequired();

        builder.Property(t => t.Recuperacao)
            .IsRequired();

        builder.HasOne(t => t.Disciplina)
            .WithMany(d => d.Testes)
            .IsRequired();

        builder.HasOne(t => t.Materia)
            .WithMany(m => m.Testes)
            .IsRequired(false);

        builder.Property(t => t.Serie)
            .IsRequired();

        builder.HasMany(t => t.Questoes)
            .WithMany(t => t.Testes);
    }
}
