# 🚀 Mottu Delivery API - Guia de Instalação e Execução

## 📋 Pré-requisitos

### Software Necessário
- **.NET 8 SDK** - [Download aqui](https://dotnet.microsoft.com/download/dotnet/8.0)
- **MySQL Server** - [Download aqui](https://dev.mysql.com/downloads/mysql/)
- **Visual Studio Code** ou **Visual Studio 2022** - [Download aqui](https://visualstudio.microsoft.com/)

### Verificar Instalações
```bash
# Verificar .NET
dotnet --version

# Verificar MySQL
mysql --version
```

## 🛠️ Instalação Passo a Passo

### 1. Clone o Repositório
```bash
git clone <url-do-repositorio>
cd cp-.NET
```

### 2. Configurar MySQL

#### Instalar MySQL
- **Windows**: Baixe o instalador do MySQL e siga as instruções
- **macOS**: `brew install mysql`
- **Linux**: `sudo apt-get install mysql-server`

#### Criar Banco de Dados
```sql
-- Conectar ao MySQL
mysql -u root -p

-- Criar banco de dados
CREATE DATABASE MottuDelivery CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

-- Criar usuário específico (opcional)
CREATE USER 'mottu_user'@'localhost' IDENTIFIED BY 'mottu_password';
GRANT ALL PRIVILEGES ON MottuDelivery.* TO 'mottu_user'@'localhost';
FLUSH PRIVILEGES;

-- Sair do MySQL
EXIT;
```

### 3. Configurar Connection String

Edite o arquivo `MottuDelivery.API/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=MottuDelivery;User=root;Password=SUA_SENHA;Port=3306;"
  }
}
```

**Substitua `SUA_SENHA` pela senha do seu MySQL.**

### 4. Restaurar Dependências
```bash
cd MottuDelivery.API
dotnet restore
```

### 5. Executar a Aplicação
```bash
dotnet run
```

## 🌐 Acessar a API

### URLs Disponíveis
- **Swagger UI**: `https://localhost:7001` ou `http://localhost:5001`
- **Health Check**: `https://localhost:7001/api/health`
- **API Base**: `https://localhost:7001/api`

### Verificar se Está Funcionando
```bash
# Teste básico
curl -k https://localhost:7001/api/health

# Ou acesse no navegador
# https://localhost:7001
```

## 🧪 Testando a API

### 1. Usando Swagger UI
1. Acesse `https://localhost:7001`
2. Teste os endpoints disponíveis
3. Use os dados de exemplo fornecidos

### 2. Usando Postman
1. Importe a collection do arquivo `Postman-Collection.md`
2. Configure a base URL: `https://localhost:7001`
3. Execute os testes

### 3. Usando cURL
```bash
# Health Check
curl -k -X GET "https://localhost:7001/api/health"

# Listar entregadores
curl -k -X GET "https://localhost:7001/api/entregadores"

# Criar entregador
curl -k -X POST "https://localhost:7001/api/entregadores" \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "João Silva",
    "cpf": "12345678901",
    "telefone": "11999999999",
    "email": "joao.silva@email.com"
  }'
```

## 🔧 Solução de Problemas

### Erro de Conexão com MySQL
```
System.InvalidOperationException: Unable to connect to any of the specified MySQL hosts.
```

**Soluções:**
1. Verificar se o MySQL está rodando: `sudo service mysql start`
2. Verificar a connection string no `appsettings.json`
3. Verificar se o usuário tem permissões no banco

### Erro de Certificado SSL
```
The SSL connection could not be established
```

**Soluções:**
1. Usar `-k` no cURL para ignorar certificados
2. Acessar via HTTP: `http://localhost:5001`
3. Configurar certificados SSL adequadamente

### Erro de Porta em Uso
```
System.IO.IOException: Failed to bind to address https://127.0.0.1:7001
```

**Soluções:**
1. Parar outros serviços na porta 7001
2. Alterar a porta no `launchSettings.json`
3. Usar `dotnet run --urls "https://localhost:7002"`

### Erro de Dependências
```
Could not load file or assembly
```

**Soluções:**
1. Executar `dotnet clean`
2. Executar `dotnet restore`
3. Executar `dotnet build`

## 📊 Dados de Exemplo

A aplicação já vem com dados de exemplo (seed data):

### Entregadores
- **João Silva** - CPF: 12345678901 - joao.silva@email.com
- **Maria Santos** - CPF: 98765432109 - maria.santos@email.com

### Entregas
- **Entrega de documentos** (Concluída) - João Silva

## 🚀 Comandos Úteis

### Desenvolvimento
```bash
# Executar em modo desenvolvimento
dotnet run --environment Development

# Executar com hot reload
dotnet watch run

# Limpar e reconstruir
dotnet clean && dotnet build
```

### Banco de Dados
```bash
# Backup do banco
mysqldump -u root -p MottuDelivery > backup.sql

# Restaurar backup
mysql -u root -p MottuDelivery < backup.sql
```

### Logs
```bash
# Executar com logs detalhados
dotnet run --verbosity detailed
```

## 📝 Próximos Passos

1. **Testar todos os endpoints** usando Swagger UI
2. **Criar entregadores** e entregas de teste
3. **Explorar as funcionalidades** de status e relacionamentos
4. **Verificar a documentação** no Swagger
5. **Analisar o código** para entender a arquitetura

## 🆘 Suporte

Se encontrar problemas:
1. Verifique os logs da aplicação
2. Confirme se todos os pré-requisitos estão instalados
3. Verifique a configuração do MySQL
4. Consulte a documentação do Swagger

---

**Equipe:**
- RM557356 - Alex Ribeiro
- RM559433 - Felipe Damasceno  
- RM560306 - Natalia dos Santos
