using Dapper;
using Microsoft.Data.Sqlite;

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
Questao2("f)", 2,"Não soube responder!");

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
// QUESTÃO 4
// ============================================================
Console.WriteLine("\n--- QUESTÃO 4 ---");

Questao4();

// ============================================================
// QUESTÃO 5
// ============================================================
Console.WriteLine("\n--- QUESTÃO 5 ---");
Questao5();

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
static void Questao4()
{
    using var connection = new SqliteConnection("Data Source=desafio.db");
    connection.Open();

    // -------------------------------------------------------
    // Criação das tabelas
    // -------------------------------------------------------
    connection.Execute(@"
        CREATE TABLE IF NOT EXISTS Estado (
            Codigo TEXT NOT NULL PRIMARY KEY,
            Nome   TEXT NOT NULL
        )
    ");

    connection.Execute(@"
        CREATE TABLE IF NOT EXISTS TiposTelefone (
            Id        INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
            Descricao TEXT    NOT NULL
        )
    ");

    connection.Execute(@"
        CREATE TABLE IF NOT EXISTS Cliente (
            Id           INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
            RazaoSocial  TEXT    NOT NULL,
            CNPJ         TEXT    NOT NULL UNIQUE,
            EstadoCodigo TEXT    NOT NULL,
            FOREIGN KEY (EstadoCodigo) REFERENCES Estado(Codigo)
        )
    ");

    connection.Execute(@"
        CREATE TABLE IF NOT EXISTS Telefone (
            Id             INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
            ClienteId      INTEGER NOT NULL,
            TipoTelefoneId INTEGER NOT NULL,
            Numero         TEXT    NOT NULL,
            FOREIGN KEY (ClienteId)      REFERENCES Cliente(Id),
            FOREIGN KEY (TipoTelefoneId) REFERENCES TiposTelefone(Id)
        )
    ");

    // -------------------------------------------------------
    // Insere dados apenas se o banco estiver vazio
    // -------------------------------------------------------
    var total = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Estado");
    if (total == 0)
    {
        connection.Execute(@"
            INSERT INTO Estado VALUES
            ('SP', 'São Paulo'),
            ('RJ', 'Rio de Janeiro'),
            ('MG', 'Minas Gerais')
        ");

        connection.Execute(@"
            INSERT INTO TiposTelefone (Descricao) VALUES
            ('Comercial'),
            ('Residencial'),
            ('Celular')
        ");

        connection.Execute(@"
            INSERT INTO Cliente (RazaoSocial, CNPJ, EstadoCodigo) VALUES
            ('SRConstrucoes', '11111111000101', 'SP'),
            ('Carrossel S/A', '22222222000102', 'SP'),
            ('Gama Filho ME', '33333333000103', 'RJ'),
            ('Macuco Ltda',   '44444444000104', 'SP')
        ");

        connection.Execute(@"
            INSERT INTO Telefone (ClienteId, TipoTelefoneId, Numero) VALUES
            (1, 1, '(11) 3001-1000'),
            (1, 3, '(11) 99001-0001'),
            (2, 1, '(11) 3002-2000'),
            (3, 2, '(21) 3003-3000'),
            (4, 1, '(11) 3004-4000'),
            (4, 3, '(11) 99004-0004')
        ");
    }

    var resultado = connection.Query(@"
        SELECT C.Id AS CodigoCliente, C.RazaoSocial, T.Numero AS Telefone, TT.Descricao AS TipoTelefone
        FROM Cliente C
        INNER JOIN Estado E  ON E.Codigo = C.EstadoCodigo
        LEFT  JOIN Telefone T  ON T.ClienteId = C.Id
        LEFT  JOIN TiposTelefone TT ON TT.Id = T.TipoTelefoneId
        WHERE E.Codigo = 'SP'
        ORDER BY C.RazaoSocial
    ");

    Console.WriteLine("\n  Clientes do estado de SP:\n");
    foreach (var item in resultado)
    {
        Console.WriteLine($"  [{item.CodigoCliente}] {item.RazaoSocial}");
        Console.WriteLine($"      Telefone: {item.Telefone} ({item.TipoTelefone})");
    }
}
static void Questao5()
{
    const double distanciaTotal = 125.0; 
    const double velocidadeCarro = 90.0;  
    const double velocidadeCaminhao = 80.0;  
    const int numeroPedagios = 3;
    const double atrasoMinPorPedagio = 5.0;

    double atrasoHoras = (numeroPedagios * atrasoMinPorPedagio) / 60.0;

    double t = (distanciaTotal + velocidadeCarro * atrasoHoras) / (velocidadeCarro + velocidadeCaminhao);

    double distanciaCarro = velocidadeCarro * (t - atrasoHoras);
    double distanciaCaminhao = velocidadeCaminhao * t;

    Console.WriteLine($"  Tempo até o encontro: {t * 60:F1} minutos");
    Console.WriteLine($"  Carro percorreu: {distanciaCarro:F2} km de Ribeirão Preto");
    Console.WriteLine($"  Caminhão percorreu: {distanciaCaminhao:F2} km de Barretos");
    Console.WriteLine($"  Ponto de encontro: {distanciaCarro:F2} km de Ribeirão Preto");
    Console.WriteLine();
    Console.WriteLine("  RESPOSTA:");
    Console.WriteLine("  Quando dois veículos se cruzam estão no mesmo ponto.");
    Console.WriteLine($"  Ambos estão a {distanciaCarro:F2} km de Ribeirão Preto.");
}