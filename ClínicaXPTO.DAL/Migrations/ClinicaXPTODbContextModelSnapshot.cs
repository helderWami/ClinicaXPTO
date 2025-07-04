﻿// <auto-generated />
using System;
using ClinicaXPTO.DAL.AppDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ClinicaXPTO.DAL.Migrations
{
    [DbContext(typeof(ClinicaXPTODbContext))]
    partial class ClinicaXPTODbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ClinicaXPTO.Models.ItemPedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<TimeSpan?>("HorarioSolicitado")
                        .HasColumnType("time");

                    b.Property<string>("Observacoes")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("PedidoMarcacaoId")
                        .HasColumnType("int");

                    b.Property<int?>("ProfissionalId")
                        .HasColumnType("int");

                    b.Property<int>("SubsistemaSaudeId")
                        .HasColumnType("int");

                    b.Property<int>("TipoActoClinicoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PedidoMarcacaoId");

                    b.HasIndex("ProfissionalId");

                    b.HasIndex("SubsistemaSaudeId");

                    b.HasIndex("TipoActoClinicoId");

                    b.ToTable("ItemPedidos");
                });

            modelBuilder.Entity("ClinicaXPTO.Models.PedidoMarcacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AgendadoPorId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DataAgendamento")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataHoraAgendada")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataPedido")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataRealizacao")
                        .HasColumnType("datetime2");

                    b.Property<int>("Estado")
                        .HasColumnType("int");

                    b.Property<DateTime?>("FimIntervalo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("InicioIntervalo")
                        .HasColumnType("datetime2");

                    b.Property<string>("Observacoes")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int?>("RealizadoPorId")
                        .HasColumnType("int");

                    b.Property<int>("UtenteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AgendadoPorId");

                    b.HasIndex("RealizadoPorId");

                    b.HasIndex("UtenteId");

                    b.ToTable("PedidoMarcacoes");
                });

            modelBuilder.Entity("ClinicaXPTO.Models.Profissional", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Especialidade")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("NomeCompleto")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("NumeroOrdem")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Profissionais");
                });

            modelBuilder.Entity("ClinicaXPTO.Models.SubsistemaSaude", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("SubsistemasSaude");
                });

            modelBuilder.Entity("ClinicaXPTO.Models.TipoActoClinico", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<TimeSpan?>("DuracaoPadrao")
                        .HasColumnType("time");

                    b.Property<string>("Observacoes")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<decimal?>("Preco")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("Id");

                    b.ToTable("TipoActoClinicos");
                });

            modelBuilder.Entity("ClinicaXPTO.Models.Utente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailContacto")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("EstadoUtente")
                        .HasColumnType("int");

                    b.Property<string>("Fotografia")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("Genero")
                        .HasColumnType("int");

                    b.Property<string>("Morada")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("NomeCompleto")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("NumeroUtente")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Telemovel")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<int?>("UtilizadorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UtilizadorId");

                    b.ToTable("Utentes");
                });

            modelBuilder.Entity("ClinicaXPTO.Models.Utilizador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Perfil")
                        .HasColumnType("int");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Utilizadores");
                });

            modelBuilder.Entity("ClinicaXPTO.Models.ItemPedido", b =>
                {
                    b.HasOne("ClinicaXPTO.Models.PedidoMarcacao", "PedidoMarcacao")
                        .WithMany("Itens")
                        .HasForeignKey("PedidoMarcacaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClinicaXPTO.Models.Profissional", "Profissional")
                        .WithMany("ItensPedido")
                        .HasForeignKey("ProfissionalId");

                    b.HasOne("ClinicaXPTO.Models.SubsistemaSaude", "SubsistemaSaude")
                        .WithMany("ItensPedido")
                        .HasForeignKey("SubsistemaSaudeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClinicaXPTO.Models.TipoActoClinico", "TipoActoClinico")
                        .WithMany("ItensPedido")
                        .HasForeignKey("TipoActoClinicoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PedidoMarcacao");

                    b.Navigation("Profissional");

                    b.Navigation("SubsistemaSaude");

                    b.Navigation("TipoActoClinico");
                });

            modelBuilder.Entity("ClinicaXPTO.Models.PedidoMarcacao", b =>
                {
                    b.HasOne("ClinicaXPTO.Models.Utilizador", "AgendadoPor")
                        .WithMany()
                        .HasForeignKey("AgendadoPorId");

                    b.HasOne("ClinicaXPTO.Models.Utilizador", "RealizadoPor")
                        .WithMany()
                        .HasForeignKey("RealizadoPorId");

                    b.HasOne("ClinicaXPTO.Models.Utente", "Utente")
                        .WithMany("Pedidos")
                        .HasForeignKey("UtenteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AgendadoPor");

                    b.Navigation("RealizadoPor");

                    b.Navigation("Utente");
                });

            modelBuilder.Entity("ClinicaXPTO.Models.Utente", b =>
                {
                    b.HasOne("ClinicaXPTO.Models.Utilizador", "Utilizador")
                        .WithMany()
                        .HasForeignKey("UtilizadorId");

                    b.Navigation("Utilizador");
                });

            modelBuilder.Entity("ClinicaXPTO.Models.PedidoMarcacao", b =>
                {
                    b.Navigation("Itens");
                });

            modelBuilder.Entity("ClinicaXPTO.Models.Profissional", b =>
                {
                    b.Navigation("ItensPedido");
                });

            modelBuilder.Entity("ClinicaXPTO.Models.SubsistemaSaude", b =>
                {
                    b.Navigation("ItensPedido");
                });

            modelBuilder.Entity("ClinicaXPTO.Models.TipoActoClinico", b =>
                {
                    b.Navigation("ItensPedido");
                });

            modelBuilder.Entity("ClinicaXPTO.Models.Utente", b =>
                {
                    b.Navigation("Pedidos");
                });
#pragma warning restore 612, 618
        }
    }
}
