using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocadoraCarros.Models
{
    /// <summary>
    /// Entidade que representa um carro disponível para locação.
    /// </summary>
    [Table("TB_CARROS")]
    public class Carro
    {
        /// <summary>
        /// Identificador único do carro.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID_CARRO")]
        public int Id { get; set; }

        /// <summary>
        /// Modelo do carro (ex: "Civic").
        /// </summary>
        [Required(ErrorMessage = "O modelo é obrigatório.")]
        [MaxLength(100)]
        [Column("MODELO")]
        public string Modelo { get; set; } = string.Empty;

        /// <summary>
        /// Marca do carro (ex: "Honda").
        /// </summary>
        [Required(ErrorMessage = "A marca é obrigatória.")]
        [MaxLength(100)]
        [Column("MARCA")]
        public string Marca { get; set; } = string.Empty;

        /// <summary>
        /// Ano de fabricação do carro (ex: 2020).
        /// </summary>
        [Required(ErrorMessage = "O ano é obrigatório.")]
        [Column("ANO")]
        public int Ano { get; set; }

        /// <summary>
        /// Valor da diária de locação (ex: 150.00).
        /// </summary>
        [Required(ErrorMessage = "O valor da diária é obrigatório.")]
        [Column("VALOR_DIARIA")]
        public double ValorDiaria { get; set; }
    }
}
