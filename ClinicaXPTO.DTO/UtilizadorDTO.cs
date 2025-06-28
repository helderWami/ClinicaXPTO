using System;
using System.ComponentModel.DataAnnotations;
using ClinicaXPTO.Models.Enuns;

namespace ClinicaXPTO.DTO
{
    public class UtilizadorDTO
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public Perfil Perfil { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }

        public string PerfilDescricao => Perfil switch
        {
            Perfil.UtenteRegistado => "Utente Registado",
            Perfil.Administrativo => "Administrativo",
            Perfil.Administrador => "Administrador da Aplicação",
            _ => "Desconhecido"
        };

        public string StatusDescricao => Ativo ? "Ativo" : "Inativo";

        public bool EhAdministrador => Perfil == Perfil.Administrador;

        public bool EhAdministrativo => Perfil == Perfil.Administrativo;

        public bool EhUtente => Perfil == Perfil.UtenteRegistado;

        public string TempoCriacao
        {
            get
            {
                var diferenca = DateTime.UtcNow - DataCriacao;
                if (diferenca.TotalDays >= 1)
                    return $"há {diferenca.Days} dias";
                if (diferenca.TotalHours >= 1)
                    return $"há {(int)diferenca.TotalHours} horas";
                return $"há {(int)diferenca.TotalMinutes} minutos";
            }
        }
    }
}
