# Sistema de Gestão de Pedidos - CP2 - 2tdspr

## Descrição do Projeto

Sistema de gestão de pedidos desenvolvido como **evolução do CP1**, aplicando os princípios de **Clean Architecture** e **Domain-Driven Design (DDD)**. Este projeto transforma o sistema básico do CP1 em uma API RESTful robusta e escalável.

### 🎯 Evolução do CP1 para CP2 coforme pedido em aula

**CP1 - Sistema Básico:**
```csharp
// Entidades simples do CP1
public class cliente
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public List<pedido> Pedidos { get; set; } = new();
}
```

**CP2 - Sistema Evoluído:**
```csharp
// Evolução das entidades do CP1 com Clean Architecture
public class Cliente
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public DateTime DataCadastro { get; private set; }
    public StatusCliente Status { get; private set; }
    public ICollection<Pedido> Pedidos { get; private set; } = new List<Pedido>();
    
    // + Validações, comportamentos e regras de negócio
}
```

### Problema Resolvido

O CP2 resolve os seguintes desafios identificados no CP1:
- Código acoplado e difícil de manter
- Falta de validações de negócio
- Ausência de separação de responsabilidades
- Dificuldade para evoluir e escalar
- Falta de documentação e testes

### Solução Implementada

- **Gestão completa de Clientes**: Cadastro, atualização e controle de status
- **Gestão de Funcionários**: Cadastro com cargo e salário, controle de disponibilidade
- **Gestão de Pedidos**: Criação, acompanhamento e atribuição de funcionários
- **Relacionamentos inteligentes**: Sistema de relacionamentos complexos entre entidades
- **API RESTful robusta**: Endpoints bem documentados com respostas HTTP apropriadas

## Arquitetura

### Clean Architecture + DDD

```
MottuDelivery.API (Presentation Layer)
├── Controllers
├── Middleware
└── Program.cs

MottuDelivery.Application (Application Layer)
├── DTOs
├── Services
├── Mappings
└── Validators

MottuDelivery.Domain (Domain Layer)
├── Entities
├── Enums
├── ValueObjects
└── Interfaces

MottuDelivery.Infrastructure (Infrastructure Layer)
├── Data
└── Repositories
```

### Princípios Aplicados

- **Separação de Responsabilidades**: Cada camada tem sua responsabilidade específica
- **Inversão de Dependência**: Interfaces definidas no Domain, implementadas na Infrastructure
- **Entidades Ricas**: Comportamento e validação de negócio nas entidades
- **DTOs e Mapping**: Separação entre modelos de domínio e modelos de apresentação
- **Validação**: FluentValidation para validação de entrada

## 🛠️ Tecnologias Utilizadas

- **.NET 8** - Framework principal
- **Entity Framework Core** - ORM para persistência
- **MySQL** - Banco de dados relacional
- **AutoMapper** - Mapeamento entre objetos
- **FluentValidation** - Validação de dados
- **Swagger/OpenAPI** - Documentação da API
- **Clean Architecture** - Padrão arquitetural
- **Domain-Driven Design** - Metodologia de design

##  Entidades do Domínio

### 👤 Cliente
- **Propriedades**: ID, Nome, Email, Status, Datas
- **Comportamentos**: Ativar/Inativar, Atualizar dados, Verificar se pode fazer pedidos
- **Validações**: Email único, Nome obrigatório, Formato de email válido

### 👨‍💼 Funcionário
- **Propriedades**: ID, Nome, Cargo, Salário, Data de Contratação, Status, Datas
- **Comportamentos**: Ativar/Inativar, Atualizar dados, Verificar disponibilidade
- **Validações**: Salário maior que zero, Cargo obrigatório, Nome válido

### 📦 Pedido
- **Propriedades**: ID, Descrição, Status, Datas, Observações, Valor Total
- **Comportamentos**: Iniciar, Concluir, Cancelar, Atribuir funcionários
- **Validações**: Valor maior que zero, Descrição obrigatória, Status apropriado

### 🔗 Relacionamentos
- **Cliente → Pedido**: 1:N (Um cliente pode ter vários pedidos)
- **Funcionário ↔ Pedido**: N:N (Muitos funcionários podem trabalhar em muitos pedidos)

## Rotas Disponíveis

### Clientes (`/api/clientes`)

| Método | Endpoint | Descrição | Status Codes |
|--------|----------|-----------|--------------|
| GET | `/api/clientes` | Lista todos os clientes | 200 |
| GET | `/api/clientes/{id}` | Obtém cliente por ID | 200, 404 |
| POST | `/api/clientes` | Cria novo cliente | 201, 400 |
| PUT | `/api/clientes/{id}` | Atualiza cliente | 200, 400, 404 |
| DELETE | `/api/clientes/{id}` | Exclui cliente | 204, 400, 404 |
| GET | `/api/clientes/status/{status}` | Lista por status | 200, 400 |
| GET | `/api/clientes/ativos` | Lista ativos | 200 |

### Funcionários (`/api/funcionarios`)

| Método | Endpoint | Descrição | Status Codes |
|--------|----------|-----------|--------------|
| GET | `/api/funcionarios` | Lista todos os funcionários | 200 |
| GET | `/api/funcionarios/{id}` | Obtém funcionário por ID | 200, 404 |
| POST | `/api/funcionarios` | Cria novo funcionário | 201, 400 |
| PUT | `/api/funcionarios/{id}` | Atualiza funcionário | 200, 400, 404 |
| DELETE | `/api/funcionarios/{id}` | Exclui funcionário | 204, 400, 404 |
| GET | `/api/funcionarios/status/{status}` | Lista por status | 200, 400 |
| GET | `/api/funcionarios/cargo/{cargo}` | Lista por cargo | 200 |
| GET | `/api/funcionarios/disponiveis` | Lista disponíveis | 200 |

### Pedidos (`/api/pedidos`)

| Método | Endpoint | Descrição | Status Codes |
|--------|----------|-----------|--------------|
| GET | `/api/pedidos` | Lista todos os pedidos | 200 |
| GET | `/api/pedidos/{id}` | Obtém pedido por ID | 200, 404 |
| POST | `/api/pedidos` | Cria novo pedido | 201, 400 |
| PUT | `/api/pedidos/{id}/status` | Atualiza status | 200, 400, 404 |
| PUT | `/api/pedidos/{id}/funcionarios` | Atribui funcionários | 200, 400, 404 |
| DELETE | `/api/pedidos/{id}` | Exclui pedido | 204, 400, 404 |
| GET | `/api/pedidos/cliente/{clienteId}` | Lista por cliente | 200 |
| GET | `/api/pedidos/funcionario/{funcionarioId}` | Lista por funcionário | 200 |
| GET | `/api/pedidos/status/{status}` | Lista por status | 200, 400 |
| GET | `/api/pedidos/periodo` | Lista por período | 200 |
| GET | `/api/pedidos/pendentes` | Lista pendentes | 200 |

### Health Check (`/api/health`)
| Método | Endpoint | Descrição | Status Codes |
|--------|----------|-----------|--------------|
| GET | `/api/health` | Verifica saúde da API | 200 |

## 📝 Exemplos de Uso

### Criar Cliente
```json
POST /api/clientes
{
  "nome": "João Silva",
  "email": "joao.silva@email.com"
}
```

### Criar Funcionário
```json
POST /api/funcionarios
{
  "nome": "Carlos Oliveira",
  "cargo": "Desenvolvedor",
  "salario": 5000.00
}
```

### Criar Pedido
```json
POST /api/pedidos
{
  "descricao": "Desenvolvimento de sistema web",
  "clienteId": "guid-do-cliente",
  "valorTotal": 15000.00,
  "observacoes": "Projeto urgente"
}
```

### Atualizar Status do Pedido
```json
PUT /api/pedidos/{id}/status
{
  "status": "EmAndamento",
  "observacoes": "Iniciado processamento"
}
```

### Atribuir Funcionários ao Pedido
```json
PUT /api/pedidos/{id}/funcionarios
{
  "funcionarioIds": ["guid-funcionario-1", "guid-funcionario-2"]
}
```

## 🚀 Instruções de Execução

### Pré-requisitos
- .NET 8 SDK
- MySQL Server
- Visual Studio Code ou Visual Studio

### 1. Clone o Repositório
```bash
git clone <url-do-repositorio>
cd MottuDelivery
```

### 2. Configure o Banco de Dados
```bash
# Instale o MySQL e crie o banco
mysql -u root -p
CREATE DATABASE MottuDelivery;
```

### 3. Configure a Connection String
Edite o arquivo `MottuDelivery.API/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=MottuDelivery;User=root;Password=SUA_SENHA;Port=3306;"
  }
}
```

### 4. Execute a Aplicação
```bash
cd MottuDelivery.API
dotnet restore
dotnet run
```

### 5. Acesse a Documentação
- **Swagger UI**: `https://localhost:7001` ou `http://localhost:5001`
- **Health Check**: `https://localhost:7001/api/health`

## 🧪 Testando a API

### Usando Swagger UI
1. Acesse `https://localhost:7001`
2. Use os endpoints disponíveis na interface
3. Teste as operações CRUD

### Usando cURL
```bash
# Health Check
curl -X GET "https://localhost:7001/api/health"

# Listar entregadores
curl -X GET "https://localhost:7001/api/entregadores"

# Criar entregador
curl -X POST "https://localhost:7001/api/entregadores" \
  -H "Content-Type: application/json" \
  -d '{"nome":"João Silva","cpf":"12345678901","telefone":"11999999999","email":"joao@email.com"}'
```

## 📊 Status Codes Utilizados

- **200 OK**: Operação realizada com sucesso
- **201 Created**: Recurso criado com sucesso
- **204 No Content**: Operação realizada sem retorno
- **400 Bad Request**: Dados inválidos ou erro de validação
- **404 Not Found**: Recurso não encontrado
- **500 Internal Server Error**: Erro interno do servidor

## 🔒 Segurança

- **Validação de entrada**: Todos os dados são validados antes do processamento
- **Tratamento de exceções**: Middleware global para tratamento de erros
- **CORS configurado**: Para desenvolvimento, permite todas as origens
- **Dados sensíveis**: Senhas e dados sensíveis não são expostos

## 📈 Funcionalidades Implementadas

### ✅ CRUD Completo
- **Entregadores**: Create, Read, Update, Delete
- **Entregas**: Create, Read, Update, Delete

### ✅ Relacionamentos
- Entregador ↔ Entrega (1:N)
- Navegação entre entidades

### ✅ Respostas HTTP Apropriadas
- Status codes corretos para cada operação
- Mensagens de erro descritivas

### ✅ Boas Práticas
- Clean Architecture implementada
- DDD aplicado com entidades ricas
- DTOs e MappingConfig
- Validações com FluentValidation
- Tratamento de exceções global

## 👥 Equipe

- **RM559999** - Bruno Mateus Tizer das Chagas
- **RM560306** - Natalia dos Santos


