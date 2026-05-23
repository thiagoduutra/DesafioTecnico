Console.WriteLine("=== DESAFIO TÉCNICO ===\n");

// ============================================================
// QUESTÃO 1
// ============================================================
Console.WriteLine("--- QUESTÃO 1 ---");

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
Console.WriteLine("\n--- QUESTÃO 2 ---");

Questao2("a)", 9, "Números ímpares consecutivos, somando + 2 a cada número ou números primos.");
Questao2("b)", 128, "Potências de 2. É multiplicado por 2 a cada número.");
Questao2("c)", 49, "São quadrados perfeitos N². Próximo: 7² = 49.");
Questao2("d)", 100, "Quadrados perfeitos pares. Próximo: 10² = 100.");
Questao2("e)", 13, "Fibonacci: a cada número é a soma dos dois anteriores. 5 + 8 = 13.");
Questao2("f)", 2,"Verificar Novamente Depois");

// ============================================================
// FUNÇÕES UTILIZADAS:
// ============================================================
static void Imprimir(int valor)
{
    Console.WriteLine($"Valor de SOMA: {valor}");
}
static void Questao2(string pSerie, int pProximo, string pLogica)
{
    Console.WriteLine($"\n  Série:   {pSerie} {pProximo}");
    Console.WriteLine($"  Lógica:  {pLogica}");
}