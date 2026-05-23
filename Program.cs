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

Console.ReadLine();

static void Imprimir(int valor)
{
    Console.WriteLine($"Valor de SOMA: {valor}");
}

// ============================================================
