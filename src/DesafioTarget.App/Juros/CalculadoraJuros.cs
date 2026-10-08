namespace DesafioTarget.App.Juros;

public static class CalculadoraJuros
{
    private const decimal TaxaDiaria = 0.025m; // 2,5% ao dia
    public static decimal CalcularMulta(decimal valorJuros, DateOnly dataVencimento, DateOnly dataPagamento)
    {

        int diaAtraso = dataPagamento.DayNumber - dataVencimento.DayNumber;

        if (diaAtraso <= 0)
        { 
            return 0m;
        }

        decimal juros = valorOrigianl * TaxaDiaria * diaAtraso;

        return Math.Round(juros, 2, MidpointRounding.AwayFromZero);

    }
}
