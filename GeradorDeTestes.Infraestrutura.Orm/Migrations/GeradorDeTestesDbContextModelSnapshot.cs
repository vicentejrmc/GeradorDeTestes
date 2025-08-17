﻿using GeradorDeTestes.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace GeradorDeTestes.Infraestrutura.Orm.Migrations
{
    [DbContext(typeof(GeradorDeTestesDbContext))]
    partial class GeradorDeTestesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GeradorDeTestes.Dominio.ModuloDisciplina.Disciplina", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Disciplinas");
                });

            modelBuilder.Entity("GeradorDeTestes.Dominio.ModuloMateria.Materia", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("DisciplinaId")
                        .HasColumnType("uuid");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("Serie")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DisciplinaId");

                    b.ToTable("Materias");
                });

            modelBuilder.Entity("GeradorDeTestes.Dominio.ModuloQuestao.Alternativa", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<bool>("Correta")
                        .HasColumnType("boolean");

                    b.Property<char>("Letra")
                        .HasColumnType("character(1)");

                    b.Property<Guid>("QuestaoId")
                        .HasColumnType("uuid");

                    b.Property<string>("Resposta")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.HasKey("Id");

                    b.HasIndex("QuestaoId");

                    b.ToTable("Alternativas");
                });

            modelBuilder.Entity("GeradorDeTestes.Dominio.ModuloQuestao.Questao", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Enunciado")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<Guid>("MateriaId")
                        .HasColumnType("uuid");

                    b.Property<bool>("UtilizadaEmTeste")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("MateriaId");

                    b.ToTable("Questoes");
                });

            modelBuilder.Entity("GeradorDeTestes.Dominio.ModuloTeste.Teste", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DataGeracao")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("DisciplinaId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("MateriaId")
                        .HasColumnType("uuid");

                    b.Property<int>("QteQuestoes")
                        .HasColumnType("integer");

                    b.Property<bool>("Recuperacao")
                        .HasColumnType("boolean");

                    b.Property<int>("Serie")
                        .HasColumnType("integer");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("DisciplinaId");

                    b.HasIndex("MateriaId");

                    b.ToTable("Testes");
                });

            modelBuilder.Entity("QuestaoTeste", b =>
                {
                    b.Property<Guid>("QuestoesId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TestesId")
                        .HasColumnType("uuid");

                    b.HasKey("QuestoesId", "TestesId");

                    b.HasIndex("TestesId");

                    b.ToTable("QuestaoTeste");
                });

            modelBuilder.Entity("GeradorDeTestes.Dominio.ModuloMateria.Materia", b =>
                {
                    b.HasOne("GeradorDeTestes.Dominio.ModuloDisciplina.Disciplina", "Disciplina")
                        .WithMany("Materias")
                        .HasForeignKey("DisciplinaId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Disciplina");
                });

            modelBuilder.Entity("GeradorDeTestes.Dominio.ModuloQuestao.Alternativa", b =>
                {
                    b.HasOne("GeradorDeTestes.Dominio.ModuloQuestao.Questao", "Questao")
                        .WithMany("Alternativas")
                        .HasForeignKey("QuestaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Questao");
                });

            modelBuilder.Entity("GeradorDeTestes.Dominio.ModuloQuestao.Questao", b =>
                {
                    b.HasOne("GeradorDeTestes.Dominio.ModuloMateria.Materia", "Materia")
                        .WithMany("Questoes")
                        .HasForeignKey("MateriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Materia");
                });

            modelBuilder.Entity("GeradorDeTestes.Dominio.ModuloTeste.Teste", b =>
                {
                    b.HasOne("GeradorDeTestes.Dominio.ModuloDisciplina.Disciplina", "Disciplina")
                        .WithMany("Testes")
                        .HasForeignKey("DisciplinaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GeradorDeTestes.Dominio.ModuloMateria.Materia", "Materia")
                        .WithMany("Testes")
                        .HasForeignKey("MateriaId");

                    b.Navigation("Disciplina");

                    b.Navigation("Materia");
                });

            modelBuilder.Entity("QuestaoTeste", b =>
                {
                    b.HasOne("GeradorDeTestes.Dominio.ModuloQuestao.Questao", null)
                        .WithMany()
                        .HasForeignKey("QuestoesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GeradorDeTestes.Dominio.ModuloTeste.Teste", null)
                        .WithMany()
                        .HasForeignKey("TestesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GeradorDeTestes.Dominio.ModuloDisciplina.Disciplina", b =>
                {
                    b.Navigation("Materias");

                    b.Navigation("Testes");
                });

            modelBuilder.Entity("GeradorDeTestes.Dominio.ModuloMateria.Materia", b =>
                {
                    b.Navigation("Questoes");

                    b.Navigation("Testes");
                });

            modelBuilder.Entity("GeradorDeTestes.Dominio.ModuloQuestao.Questao", b =>
                {
                    b.Navigation("Alternativas");
                });
#pragma warning restore 612, 618
        }
    }
}
