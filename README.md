# LocadoraCarros API

API REST para gerenciamento de uma locadora de veículos, desenvolvida com ASP.NET Core 8.0 e Oracle Database.

## Tecnologias

- **.NET 8.0** — Framework principal
- **ASP.NET Core** — Web API
- **Entity Framework Core 8** — ORM
- **Oracle Database** — Banco de dados (oracle.fiap.com.br)
- **Swagger / Swashbuckle** — Documentação interativa da API

## Estrutura do Projeto

```
LocadoraCarros/
├── Controllers/
│   ├── CarrosController.cs       # CRUD de veículos
│   └── LocacoesController.cs     # Cálculo de locações com desconto
├── Models/
│   └── Carro.cs                  # Entidade de veículo
├── Data/
│   └── AppDbContext.cs           # Contexto do EF Core
├── DTOs/
│   └── LocacaoDTOs.cs            # DTOs de entrada e saída para locações
├── ddl/
│   └── create_tables.sql         # Script DDL Oracle + dados de seed
├── Program.cs                    # Configuração e inicialização
├── appsettings.json              # Conexão e configurações
└── LocadoraCarros.csproj         # Dependências do projeto
```

## Banco de Dados

### Tabela `TB_CARROS`

| Coluna        | Tipo             | Descrição               |
|---------------|------------------|-------------------------|
| ID_CARRO      | NUMBER           | Chave primária (auto)   |
| MODELO        | VARCHAR2(100)    | Modelo do veículo       |
| MARCA         | VARCHAR2(100)    | Marca do veículo        |
| ANO           | NUMBER(4)        | Ano de fabricação       |
| VALOR_DIARIA  | NUMBER(10,2)     | Valor da diária (R$)    |

### Dados de Seed

| Modelo  | Marca   | Ano  | Diária    |
|---------|---------|------|-----------|
| Civic   | Honda   | 2022 | R$ 150,00 |
| Corolla | Toyota  | 2023 | R$ 180,00 |
| HB20    | Hyundai | 2021 | R$  90,00 |

Para criar a tabela e inserir os dados, execute o script em `ddl/create_tables.sql`.

## Endpoints

### Carros — `/api/carros`

| Método | Rota               | Descrição              |
|--------|--------------------|------------------------|
| GET    | `/api/carros`      | Lista todos os carros  |
| GET    | `/api/carros/{id}` | Busca carro por ID     |
| POST   | `/api/carros`      | Cadastra novo carro    |
| PUT    | `/api/carros/{id}` | Atualiza carro         |
| DELETE | `/api/carros/{id}` | Remove carro           |

#### Exemplo de corpo (POST / PUT)

```json
{
  "modelo": "Onix",
  "marca": "Chevrolet",
  "ano": 2024,
  "valorDiaria": 120.00
}
```

---

### Locações — `/api/locacoes`

| Método | Rota                      | Descrição                    |
|--------|---------------------------|------------------------------|
| POST   | `/api/locacoes/calcular`  | Calcula o custo da locação   |

#### Request

```json
{
  "carroId": 1,
  "dataInicio": "2025-06-01",
  "dataFim": "2025-06-08"
}
```

#### Response

```json
{
  "carro": "Civic",
  "marca": "Honda",
  "dataInicio": "2025-06-01",
  "dataFim": "2025-06-08",
  "valorDiaria": 150.00,
  "subtotal": 1050.00,
  "desconto": 10,
  "valorFinal": 945.00
}
```

#### Regras de Desconto

| Período       | Desconto |
|---------------|----------|
| 7 dias ou mais | 10%     |
| 3 a 6 dias    | 5%       |
| 1 a 2 dias    | Sem desconto |

## Como Executar

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Acesso ao Oracle Database configurado em `appsettings.json`

### Passos

```bash
# Restaurar dependências
dotnet restore

# Executar a aplicação
dotnet run
```

A API estará disponível em `http://localhost:{porta}`.  
A documentação Swagger será exibida automaticamente na raiz (`/`).

### Configuração da Conexão

Edite `appsettings.json` para ajustar a string de conexão:

```json
"ConnectionStrings": {
  "OracleConnection": "User Id=SEU_USUARIO;Password=SUA_SENHA;Data Source=HOST:1521/ORCL;"
}
```

## Documentação da API

Com a aplicação em execução, acesse `http://localhost:{porta}` para visualizar e testar todos os endpoints via Swagger UI.
