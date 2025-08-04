using GeradorDeTestes.Dominio.ModuloDisciplina;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorDeTestes.Infraestrutura.Orm.ModuloDisciplina;
public class MapeadorDisciplina : IEntityTypeConfiguration<Disciplina>
{
    public void Configure(EntityTypeBuilder<Disciplina> builder)
    {
        builder.Property(d => d.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(d => d.Nome)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasMany(d => d.Materias)
            .WithOne(m => m.Disciplina)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
