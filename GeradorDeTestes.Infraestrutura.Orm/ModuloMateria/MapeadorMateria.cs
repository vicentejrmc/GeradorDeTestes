using GeradorDeTestes.Dominio.ModuloMateria;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorDeTestes.Infraestrutura.Orm.ModuloMateria;
public class MapeadorMateria : IEntityTypeConfiguration<Materia>
{
    public void Configure(EntityTypeBuilder<Materia> builder)
    {
         builder.Property(m => m.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(m => m.Nome)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(m => m.Serie)
            .IsRequired()
            .HasConversion<int>();

        builder.HasOne(m => m.Disciplina)
            .WithMany(d => d.Materias)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();
    }
}
