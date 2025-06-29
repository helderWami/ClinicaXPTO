using System;
using System.ComponentModel.DataAnnotations;
using ClinicaXPTO.Models.Enuns;

namespace ClinicaXPTO.DTO
{
    /// <summary>
    /// DTO (DATA TRANSFER OBJECT) - UTENTE
    /// ====================================
    /// Esta classe é usada para transferir dados de utentes entre as camadas da aplicação.
    /// 
    /// DIFERENÇA ENTRE MODEL E DTO:
    /// - MODEL: Representa a entidade no banco de dados (com anotações EF Core)
    /// - DTO: Representa os dados para transferência (sem anotações de banco)
    /// 
    /// VANTAGENS DOS DTOs:
    /// - Separação de responsabilidades
    /// - Controle sobre quais dados são expostos
    /// - Evita exposição de dados sensíveis
    /// - Permite diferentes formatos para diferentes operações
    /// 
    /// PROPRIEDADES CALCULADAS:
    /// - Inclui propriedades calculadas para facilitar o uso no frontend
    /// - Fornece descrições legíveis para enums
    /// </summary>
    public class UtenteDTO
    {
        /// <summary>
        /// ID único do utente
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// ID do utilizador (NULL para utentes anónimos)
        /// </summary>
        public int? UtilizadorId { get; set; }
        
        /// <summary>
        /// Número de utente na clínica
        /// </summary>
        public string NumeroUtente { get; set; } = string.Empty;
        
        /// <summary>
        /// Caminho/URL da fotografia
        /// </summary>
        public string Fotografia { get; set; } = string.Empty;
        
        /// <summary>
        /// Nome completo do utente
        /// </summary>
        public string NomeCompleto { get; set; } = string.Empty;
        
        /// <summary>
        /// Data de nascimento
        /// </summary>
        public DateTime DataNascimento { get; set; }
        
        /// <summary>
        /// Género (Masculino/Feminino)
        /// </summary>
        public Genero Genero { get; set; }
        
        /// <summary>
        /// Número de telemóvel
        /// </summary>
        public string Telemovel { get; set; } = string.Empty;
        
        /// <summary>
        /// Email de contacto
        /// </summary>
        public string EmailContacto { get; set; } = string.Empty;
        
        /// <summary>
        /// Morada completa
        /// </summary>
        public string Morada { get; set; } = string.Empty;
        
        /// <summary>
        /// Data de criação no sistema
        /// </summary>
        public DateTime DataCriacao { get; set; }
        
        /// <summary>
        /// Se o utente está ativo
        /// </summary>
        public bool Ativo { get; set; }

        // ============================================================================
        // PROPRIEDADES CALCULADAS PARA FACILITAR O USO NO FRONTEND
        // ============================================================================
        
        /// <summary>
        /// PROPRIEDADE CALCULADA - Se é utente registado
        /// ==============================================
        /// Retorna true se o utente tem uma conta de utilizador.
        /// Usado para distinguir entre utentes anónimos e registados.
        /// </summary>
        public bool EhRegistado => UtilizadorId.HasValue;
        
        /// <summary>
        /// PROPRIEDADE CALCULADA - Idade do utente
        /// =======================================
        /// Calcula a idade baseada na data de nascimento.
        /// Considera se já fez anos este ano.
        /// </summary>
        public int Idade => DateTime.Today.Year - DataNascimento.Year -
                           (DateTime.Today.DayOfYear < DataNascimento.DayOfYear ? 1 : 0);

        /// <summary>
        /// PROPRIEDADE CALCULADA - Descrição do género
        /// ===========================================
        /// Converte o enum Genero para texto legível.
        /// Usado para exibição no frontend.
        /// </summary>
        public string GeneroDescricao => Genero == Genero.Masculino ? "Masculino" : "Feminino";

        /// <summary>
        /// PROPRIEDADE CALCULADA - Status do utente
        /// ========================================
        /// Converte o boolean Ativo para texto legível.
        /// Usado para exibição no frontend.
        /// </summary>
        public string StatusDescricao => Ativo ? "Ativo" : "Inativo";

        /// <summary>
        /// PROPRIEDADE CALCULADA - Tipo de utente
        /// ======================================
        /// Indica se o utente é "Registado" ou "Anónimo".
        /// Usado para exibição no frontend.
        /// </summary>
        public string TipoUtente => EhRegistado ? "Registado" : "Anónimo";

        /// <summary>
        /// PROPRIEDADE CALCULADA - Se tem fotografia
        /// =========================================
        /// Verifica se o utente tem uma fotografia associada.
        /// Usado para controlar a exibição de imagens no frontend.
        /// </summary>
        public bool TemFotografia => !string.IsNullOrEmpty(Fotografia);
    }
}
