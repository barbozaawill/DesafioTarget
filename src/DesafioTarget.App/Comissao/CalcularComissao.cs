namespace DesafioTarget.App.Comissao;

public static class CalculadoraComissao
{
    public static decimal Calcular(decimal valorVenda)
    {
        decimal percentual;

        if (valorVenda < 100)
        {
            percentual = 0;
        }
        else if (valorVenda < 500)
        {
            percentual = 0.01m;
        }
        else
        {
            percentual = 0.05m;
        }

        return Math.Round(valorVenda * percentual, 2, MidpointRounding.AwayFromZero);
    }
}
