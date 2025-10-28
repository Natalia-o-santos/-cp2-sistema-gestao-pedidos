# Mottu Delivery API - Scripts de Banco de Dados

## Criação do Banco de Dados

```sql
-- Criar banco de dados
CREATE DATABASE MottuDelivery CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

-- Usar o banco
USE MottuDelivery;

-- Criar usuário específico (opcional)
CREATE USER 'mottu_user'@'localhost' IDENTIFIED BY 'mottu_password';
GRANT ALL PRIVILEGES ON MottuDelivery.* TO 'mottu_user'@'localhost';
FLUSH PRIVILEGES;
```

## Estrutura das Tabelas (gerada automaticamente pelo EF Core)

As tabelas serão criadas automaticamente quando a aplicação for executada pela primeira vez, pois utilizamos `context.Database.EnsureCreated()` no Program.cs.

### Tabela: Entregadores
- Id (Guid, PK)
- Nome (VARCHAR(100))
- Cpf (VARCHAR(11), UNIQUE)
- Telefone (VARCHAR(11))
- Email (VARCHAR(100), UNIQUE)
- Status (INT)
- DataCadastro (DATETIME)
- DataUltimaAtualizacao (DATETIME)

### Tabela: Entregas
- Id (Guid, PK)
- Descricao (VARCHAR(500))
- EnderecoOrigem_Logradouro (VARCHAR(200))
- EnderecoOrigem_Numero (VARCHAR(10))
- EnderecoOrigem_Complemento (VARCHAR(100))
- EnderecoOrigem_Bairro (VARCHAR(100))
- EnderecoOrigem_Cidade (VARCHAR(100))
- EnderecoOrigem_Estado (VARCHAR(2))
- EnderecoOrigem_Cep (VARCHAR(8))
- EnderecoDestino_Logradouro (VARCHAR(200))
- EnderecoDestino_Numero (VARCHAR(10))
- EnderecoDestino_Complemento (VARCHAR(100))
- EnderecoDestino_Bairro (VARCHAR(100))
- EnderecoDestino_Cidade (VARCHAR(100))
- EnderecoDestino_Estado (VARCHAR(2))
- EnderecoDestino_Cep (VARCHAR(8))
- Status (INT)
- DataCriacao (DATETIME)
- DataInicio (DATETIME)
- DataConclusao (DATETIME)
- Observacoes (VARCHAR(1000))
- EntregadorId (Guid, FK)

## Dados de Exemplo (Seed Data)

Os dados de exemplo são inseridos automaticamente pelo Entity Framework através do método `SeedData` no `MottuDeliveryDbContext`.

### Entregadores de Exemplo
1. João Silva - CPF: 12345678901 - joao.silva@email.com
2. Maria Santos - CPF: 98765432109 - maria.santos@email.com

### Entregas de Exemplo
1. Entrega de documentos (Concluída) - João Silva

## Comandos Úteis

### Backup do Banco
```bash
mysqldump -u root -p MottuDelivery > mottu_delivery_backup.sql
```

### Restaurar Backup
```bash
mysql -u root -p MottuDelivery < mottu_delivery_backup.sql
```

### Limpar Banco (Desenvolvimento)
```sql
USE MottuDelivery;
SET FOREIGN_KEY_CHECKS = 0;
TRUNCATE TABLE Entregas;
TRUNCATE TABLE Entregadores;
SET FOREIGN_KEY_CHECKS = 1;
```
