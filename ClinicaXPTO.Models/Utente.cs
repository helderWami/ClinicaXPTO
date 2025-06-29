using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ClinicaXPTO.Models.Enuns;
using System.ComponentModel;

namespace ClinicaXPTO.Models
{
    /// <summary>
    /// MODELO DE DADOS - ENTIDADE UTENTE
    /// =================================
    /// Esta classe representa um utente da clínica no banco de dados.
    /// 
    /// CARACTERÍSTICAS PRINCIPAIS:
    /// - Pode ser utente anónimo (sem conta) ou registado (com conta)
    /// - Contém informações pessoais e de contacto
    /// - Relaciona-se com marcações e utilizador (se registado)
    /// 
    /// ANOTAÇÕES:
    /// - [Table]: Define o nome da tabela no banco de dados
    /// - [Key]: Marca a chave primária
    /// - [Required]: Campo obrigatório
    /// - [StringLength]: Limita o tamanho do texto
    /// - [ForeignKey]: Define relacionamento com outra entidade
    /// </summary>
    [Table("Utentes")]
    public class Utente
    {
        /// <summary>
        /// CHAVE PRIMÁRIA - ID único do utente
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// CHAVE ESTRANGEIRA PARA UTILIZADOR
        /// ==================================
        /// - NULL: Utente anónimo (sem conta registada)
        /// - Preenchido: Utente registado (com conta de utilizador)
        /// 
        /// Este campo permite distinguir entre utentes anónimos e registados.
        /// Um utente anónimo pode fazer marcações sem ter uma conta.
        /// </summary>
        public int? UtilizadorId { get; set; }
        
        /// <summary>
        /// NAVIGATION PROPERTY - Relacionamento com a entidade Utilizador
        /// </summary>
        [ForeignKey("UtilizadorId")]
        public Utilizador Utilizador { get; set; }

        /// <summary>
        /// NÚMERO DE UTENTE - Identificador único na clínica
        /// =================================================
        /// Campo obrigatório com máximo de 20 caracteres.
        /// Deve ser único no sistema.
        /// </summary>
        [Required]
        [StringLength(20)]
        public string NumeroUtente { get; set; } = default!;

        /// <summary>
        /// FOTOGRAFIA - Caminho/URL da foto do utente
        /// ==========================================
        /// Campo opcional com máximo de 500 caracteres.
        /// Pode conter caminho local ou URL da imagem.
        /// </summary>
        [StringLength(500)]
        public string Fotografia { get; set; } = default!;

        /// <summary>
        /// NOME COMPLETO - Nome e apelidos do utente
        /// =========================================
        /// Campo obrigatório com máximo de 200 caracteres.
        /// </summary>
        [Required]
        [StringLength(200)]
        public string NomeCompleto { get; set; } = default!;

        /// <summary>
        /// DATA DE NASCIMENTO - Data de nascimento do utente
        /// =================================================
        /// Campo obrigatório usado para calcular idade e validações.
        /// </summary>
        [Required] 
        public DateTime DataNascimento { get; set; }

        /// <summary>
        /// GÉNERO - Masculino ou Feminino
        /// ==============================
        /// Campo obrigatório usando enum Genero.
        /// </summary>
        [Required] 
        public Genero Genero { get; set; }

        /// <summary>
        /// TELEMÓVEL - Número de contacto móvel
        /// ====================================
        /// Campo opcional com validação de formato de telefone.
        /// Máximo de 15 caracteres.
        /// </summary>
        [Phone]
        [StringLength(15)]
        public string Telemovel { get; set; } = default!;

        /// <summary>
        /// EMAIL DE CONTACTO - Email principal do utente
        /// =============================================
        /// Campo obrigatório com validação de formato de email.
        /// Máximo de 100 caracteres.
        /// Deve ser único no sistema.
        /// </summary>
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string EmailContacto { get; set; } = default!;

        /// <summary>
        /// MORADA - Endereço completo do utente
        /// ====================================
        /// Campo opcional com máximo de 500 caracteres.
        /// </summary>
        [StringLength(500)]
        public string Morada { get; set; } = default!;

        /// <summary>
        /// DATA DE CRIAÇÃO - Quando o utente foi criado no sistema
        /// =======================================================
        /// Preenchido automaticamente com a data/hora atual.
        /// </summary>
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
        
        /// <summary>
        /// ESTADO DO UTENTE - Status atual no sistema
        /// ==========================================
        /// Usado para controlar o estado do utente (ativo, inativo, etc.)
        /// </summary>
        public EstadoUtente EstadoUtente { get; set; }
        
        /// <summary>
        /// ATIVO - Se o utente está ativo no sistema
        /// ========================================
        /// Usado para soft delete (não remove fisicamente do banco).
        /// </summary>
        public bool Ativo { get; set; } = false;

        // ============================================================================
        // RELACIONAMENTOS COM OUTRAS ENTIDADES
        // ============================================================================
        
        /// <summary>
        /// PEDIDOS DE MARCAÇÃO - Lista de marcações do utente
        /// ==================================================
        /// Navigation property para acessar todas as marcações deste utente.
        /// Relacionamento 1:N (um utente pode ter várias marcações).
        /// </summary>
        public ICollection<PedidoMarcacao> Pedidos { get; set; } = new List<PedidoMarcacao>();

        // ============================================================================
        // PROPRIEDADES CALCULADAS (NÃO MAPEADAS NO BANCO)
        // ============================================================================
        
        /// <summary>
        /// PROPRIEDADE CALCULADA - Se o utente é registado
        /// ===============================================
        /// Retorna true se o utente tem um UtilizadorId (é registado).
        /// Retorna false se é utente anónimo.
        /// 
        /// [NotMapped]: Não é mapeada no banco de dados.
        /// </summary>
        [NotMapped]
        public bool EhRegistado => UtilizadorId.HasValue;

        /// <summary>
        /// PROPRIEDADE CALCULADA - Idade do utente
        /// =======================================
        /// Calcula a idade baseada na data de nascimento.
        /// Considera se já fez anos este ano.
        /// 
        /// [NotMapped]: Não é mapeada no banco de dados.
        /// </summary>
        [NotMapped]
        public int Idade => DateTime.Today.Year - DataNascimento.Year -
                           (DateTime.Today.DayOfYear < DataNascimento.DayOfYear ? 1 : 0);
    }
}
