namespace DesafioTarget.App.Estoque;

public class Movimentacao
{
    public int Id { get; set; }
    public int CodigoProduto { get; set; }
    public TipoMovimentacao Tipo { get; set; }
    public int Quantidade { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public DateTime DataHora { get; set; }
    public int EstoqueFinal { get; set; }
}
