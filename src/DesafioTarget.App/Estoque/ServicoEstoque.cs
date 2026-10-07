namespace DesafioTarget.App.Estoque;

public class ServicoEstoque
{
    private readonly List<Produto> _produtos;
    private readonly List<Movimentacao> _movimentacoes = new();
    private int _ultimoId = 0;

    public ServicoEstoque(List<Produto> produtos)
    {
        _produtos = produtos;
    }

    public Movimentacao Movimentar(int codigoProduto, TipoMovimentacao tipo, int quantidade, string descricao)
    {
        var produto = _produtos.FirstOrDefault(p => p.CodigoProduto == codigoProduto);
        if (produto == null)
        {
            throw new ArgumentException($"Produto com código {codigoProduto} não encontrado.");
        }
        if (quantidade <= 0)
        {
            throw new ArgumentException("A quantidade deve ser maior que zero.");
        }
        if (string.IsNullOrWhiteSpace(descricao))
        {
            throw new ArgumentException("A descrição da movimentação não pode ser vazia.");
        }
        if (tipo == TipoMovimentacao.Saida && produto.Estoque < quantidade)
        {
            throw new InvalidOperationException($"Estoque insuficiente para o produto {produto.DescricaoProduto}. Estoque atual: {produto.Estoque}, quantidade solicitada: {quantidade}.");
        }
        // atualiza o estoque do produto
        produto.Estoque += tipo == TipoMovimentacao.Entrada ? quantidade : -quantidade;

        _ultimoId++;
        //precisei colocar fora do movimentacao pro id ser incrementado corretamente quando for printado no terminal
        //se tirar vai ver que começa como 0 e não 1

        // cria movimentação
        var movimentacao = new Movimentacao
        {
            Id = _ultimoId,
            CodigoProduto = codigoProduto,
            Tipo = tipo,
            Quantidade = quantidade,
            Descricao = descricao,
            DataHora = DateTime.Now,
            EstoqueFinal = produto.Estoque
        };
        _movimentacoes.Add(movimentacao);
        return movimentacao;
    }
}
