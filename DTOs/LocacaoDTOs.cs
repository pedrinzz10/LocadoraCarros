namespace LocadoraCarros.DTOs
{
    /// <summary>
    /// DTO de entrada para o cálculo de locação.
    /// </summary>
    public class LocacaoRequestDTO
    {
        /// <summary>
        /// ID do carro a ser locado.
        /// </summary>
        public int CarroId { get; set; }

        /// <summary>
        /// Data de início da locação.
        /// </summary>
        public DateTime DataInicio { get; set; }

        /// <summary>
        /// Data de fim da locação.
        /// </summary>
        public DateTime DataFim { get; set; }
    }

    /// <summary>
    /// DTO de saída com o relatório completo da locação calculada.
    /// </summary>
    public class LocacaoResponseDTO
    {
        /// <summary>
        /// Modelo do carro locado.
        /// </summary>
        public string Carro { get; set; } = string.Empty;

        /// <summary>
        /// Marca do carro locado.
        /// </summary>
        public string Marca { get; set; } = string.Empty;

        /// <summary>
        /// Data de início da locação.
        /// </summary>
        public DateTime DataInicio { get; set; }

        /// <summary>
        /// Data de fim da locação.
        /// </summary>
        public DateTime DataFim { get; set; }

        /// <summary>
        /// Valor da diária do carro.
        /// </summary>
        public double ValorDiaria { get; set; }

        /// <summary>
        /// Subtotal antes do desconto (dias * valorDiaria).
        /// </summary>
        public double Subtotal { get; set; }

        /// <summary>
        /// Percentual de desconto aplicado (ex: "10%", "5%" ou "0%").
        /// </summary>
        public string Desconto { get; set; } = "0%";

        /// <summary>
        /// Valor final após aplicação do desconto.
        /// </summary>
        public double ValorFinal { get; set; }
    }
}
