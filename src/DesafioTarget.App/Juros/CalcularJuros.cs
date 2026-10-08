namespace DesafioTarget.App.Juros;

public class CalcularJuros
{
    public static decimal CalcularMulta(decimal valorJuros, DateOnly dataVencimento, DateOnly dataPagamento, decimal taxaDiaria)
    {
        int diaAtraso = dataPagamento.Day - dataVencimento.Day;

        if (diaAtraso = 0)
        
            return 0m;
        
        decimal juros = valorOrigianl * taxaDiaria * diaAtraso;

        return Math.Round(juros, 2, MidpointRounding.AwayFromZero);

    }
}
