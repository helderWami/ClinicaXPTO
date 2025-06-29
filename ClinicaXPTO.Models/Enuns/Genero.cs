using System.ComponentModel;

namespace ClinicaXPTO.Models.Enuns
{
    /// <summary>
    /// ENUMERAÇÃO - GÉNERO
    /// ===================
    /// Esta enumeração define os valores possíveis para o género de uma pessoa.
    /// 
    /// USO NO PROJETO:
    /// - Usado na entidade Utente para definir o género
    /// - Valores são armazenados como inteiros no banco de dados
    /// - Descrições são usadas para exibição no frontend
    /// 
    /// VANTAGENS DOS ENUMS:
    /// - Tipagem forte (evita erros de digitação)
    /// - Valores padronizados em toda a aplicação
    /// - Fácil manutenção e extensão
    /// - Melhor legibilidade do código
    /// 
    /// ANOTAÇÃO [Description]:
    /// - Fornece descrição legível para cada valor
    /// - Usada para exibição em interfaces de utilizador
    /// - Pode ser acessada via reflection
    /// </summary>
    public enum Genero
    {
        /// <summary>
        /// Género masculino
        /// Valor: 0
        /// </summary>
        [Description("Masculino")]
        Masculino = 0,

        /// <summary>
        /// Género feminino
        /// Valor: 1
        /// </summary>
        [Description("Feminino")]
        Feminino = 1
    }
}
