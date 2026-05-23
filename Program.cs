Console.WriteLine("=== DESAFIO TÉCNICO ===\n");

// ============================================================
// QUESTÃO 1
// ============================================================

Console.WriteLine("--- QUESTÃO 1 ---\n");

int INDICE = 12, SOMA = 0, K = 1;

while (K < INDICE)
{
    K = K + 1;
    SOMA = SOMA + K;
}

Imprimir(SOMA);

// ============================================================
// QUESTÃO 2
// ============================================================

Console.WriteLine("--- QUESTÃO 2 ---\n");

Questao2("a)", 9, "Números ímpares consecutivos, somando + 2 a cada número ou números primos.");
Questao2("b)", 128, "Potências de 2. É multiplicado por 2 a cada número.");
Questao2("c)", 49, "São quadrados perfeitos N². Próximo: 7² = 49.");
Questao2("d)", 100, "Quadrados perfeitos pares. Próximo: 10² = 100.");
Questao2("e)", 13, "Fibonacci: a cada número é a soma dos dois anteriores. 5 + 8 = 13.");
Questao2("f)", 2,"Verificar Novamente Depois");

// ============================================================
// QUESTÃO 3
// ============================================================

Console.WriteLine("--- QUESTÃO 3 ---\n");

double[] faturamento = new double[]
{
    0, 0, 1500.50, 2300.75, 4100.00, 3200.10, 0,   
    0, 0, 2800.23, 1900.25, 5000.00, 4300.50, 0,       
    0, 0, 3100.41, 2750.80, 1800.00, 6200.90, 0,       
    0, 0, 4500.21, 3900.60, 2100.40, 5800.20, 0,       
    0, 0, 1200.00, 4800.75, 3600.30, 2900.00, 0,      
};

Questao3(faturamento);

// ============================================================
// FUNÇÕES UTILIZADAS:
// ============================================================
static void Imprimir(int valor)
{
    Console.WriteLine($"Valor da soma: {valor}");
}
static void Questao2(string pSerie, int pProximo, string pLogica)
{
    Console.WriteLine($"\n  Série:   {pSerie} {pProximo}");
    Console.WriteLine($"  Lógica:  {pLogica}");
}
static void Questao3(double[] faturamento)
{
    double menor = double.MaxValue;
    double maior = double.MinValue;
    double soma = 0;
    int dias = 0;

    foreach (var valor in faturamento)
    {
        if (valor <= 0) continue;

        if (valor < menor) menor = valor;
        if (valor > maior) maior = valor;

        soma += valor;
        dias++;
    }

    double media = soma / dias;

    int diasAcimaMedia = faturamento.Count(v => v > media);

    Console.WriteLine($"  Menor faturamento: R$ {menor:F2}");
    Console.WriteLine($"  Maior faturamento: R$ {maior:F2}");
    Console.WriteLine($"  Média anual:       R$ {media:F2}");
    Console.WriteLine($"  Dias acima da média: {diasAcimaMedia}");
}