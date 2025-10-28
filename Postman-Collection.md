# Mottu Delivery API - Postman Collection

## Configuração Inicial

1. **Base URL**: `https://localhost:7001` ou `http://localhost:5001`
2. **Content-Type**: `application/json`

## Health Check

### GET /api/health
```json
{
  "status": "Healthy",
  "timestamp": "2025-01-27T10:00:00Z",
  "version": "1.0.0",
  "service": "Mottu Delivery API"
}
```

## Entregadores

### 1. Listar Todos os Entregadores
**GET** `/api/entregadores`

### 2. Obter Entregador por ID
**GET** `/api/entregadores/{id}`

### 3. Criar Entregador
**POST** `/api/entregadores`
```json
{
  "nome": "João Silva",
  "cpf": "12345678901",
  "telefone": "11999999999",
  "email": "joao.silva@email.com"
}
```

### 4. Atualizar Entregador
**PUT** `/api/entregadores/{id}`
```json
{
  "nome": "João Silva Santos",
  "telefone": "11988888888",
  "email": "joao.santos@email.com"
}
```

### 5. Excluir Entregador
**DELETE** `/api/entregadores/{id}`

### 6. Listar por Status
**GET** `/api/entregadores/status/Ativo`

### 7. Listar Disponíveis
**GET** `/api/entregadores/disponiveis`

## Entregas

### 1. Listar Todas as Entregas
**GET** `/api/entregas`

### 2. Obter Entrega por ID
**GET** `/api/entregas/{id}`

### 3. Criar Entrega
**POST** `/api/entregas`
```json
{
  "descricao": "Entrega de documentos importantes",
  "enderecoOrigem": {
    "logradouro": "Rua das Flores",
    "numero": "123",
    "complemento": "Apto 45",
    "bairro": "Centro",
    "cidade": "São Paulo",
    "estado": "SP",
    "cep": "01234567"
  },
  "enderecoDestino": {
    "logradouro": "Av. Paulista",
    "numero": "456",
    "complemento": "",
    "bairro": "Bela Vista",
    "cidade": "São Paulo",
    "estado": "SP",
    "cep": "01310100"
  },
  "entregadorId": "GUID_DO_ENTREGADOR",
  "observacoes": "Entrega urgente - documentos confidenciais"
}
```

### 4. Atualizar Status da Entrega
**PUT** `/api/entregas/{id}/status`
```json
{
  "status": "EmAndamento",
  "observacoes": "Saiu para entrega às 14:30"
}
```

### 5. Atualizar Observações
**PUT** `/api/entregas/{id}/observacoes`
```json
{
  "observacoes": "Cliente não estava no local, reagendado para amanhã"
}
```

### 6. Excluir Entrega
**DELETE** `/api/entregas/{id}`

### 7. Listar por Entregador
**GET** `/api/entregas/entregador/{entregadorId}`

### 8. Listar por Status
**GET** `/api/entregas/status/Pendente`

### 9. Listar por Período
**GET** `/api/entregas/periodo?dataInicio=2025-01-01&dataFim=2025-01-31`

### 10. Listar Pendentes
**GET** `/api/entregas/pendentes`

## Status Possíveis

### Status do Entregador
- `Ativo`
- `Inativo`
- `EmEntrega`

### Status da Entrega
- `Pendente`
- `EmAndamento`
- `Concluida`
- `Cancelada`

## Códigos de Resposta

- **200 OK**: Operação realizada com sucesso
- **201 Created**: Recurso criado com sucesso
- **204 No Content**: Operação realizada sem retorno
- **400 Bad Request**: Dados inválidos ou erro de validação
- **404 Not Found**: Recurso não encontrado
- **500 Internal Server Error**: Erro interno do servidor

## Exemplos de Resposta de Erro

```json
{
  "error": {
    "message": "CPF já cadastrado",
    "timestamp": "2025-01-27T10:00:00Z",
    "path": "/api/entregadores",
    "method": "POST"
  }
}
```

## Workflow de Teste Recomendado

1. **Health Check** - Verificar se a API está funcionando
2. **Criar Entregador** - Cadastrar um entregador
3. **Listar Entregadores** - Verificar se foi criado
4. **Criar Entrega** - Usar o ID do entregador criado
5. **Atualizar Status** - Mudar status para "EmAndamento"
6. **Concluir Entrega** - Mudar status para "Concluida"
7. **Listar Entregas** - Verificar histórico
