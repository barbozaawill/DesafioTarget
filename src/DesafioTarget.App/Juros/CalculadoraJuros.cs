namespace DesafioTarget.App.Juros;

public static class CalculadoraJuros
{
    private const decimal TaxaDiaria = 0.025m; // 2,5% ao dia, apesar de que a regra não define teto
                                               // em atrasos de meses por exemplo os juros superam o valor original e acaba não tendo teto
    public static decimal Calcular(decimal valorJuros, DateOnly dataVencimento, DateOnly dataPagamento)
    {

        int diaAtraso = dataPagamento.DayNumber - dataVencimento.DayNumber;

        if (diaAtraso <= 0)
        { 
            return 0m;
        }

        decimal juros = valorJuros * TaxaDiaria * diaAtraso;

        if (valorJuros < 0)
        {
            throw new ArgumentException("Valor original não pode ser negativo.");
        }

        return Math.Round(juros, 2, MidpointRounding.AwayFromZero);

    }

}
