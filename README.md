# Desafio Target

Aplicação de console em C# / .NET 9 com os três exercícios do desafio acessados por um menu.

## Como executar

Requisito: [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)

```bash
dotnet run --project src/DesafioTarget.App
```

## Regras

**1. Comissão:** a regra é aplicada a cada venda (menos de R$ 100 → 0%, de R$ 100 a R$ 499,99 → 1%, a partir de R$ 500 → 5%). Cada comissão é arredondada para centavos antes de somar por vendedor.

**2. Estoque:** os produtos vêm do `estoque.json` e as movimentações são digitadas pelo usuário. Cada uma recebe um ID sequencial. A saída não pode ser maior que o estoque disponível. Os dados ficam em memória enquanto o programa roda.

**3. Juros:** juros simples de 2,5% por dia de atraso sobre o valor original. Se a conta ainda não venceu, os juros são zero.
