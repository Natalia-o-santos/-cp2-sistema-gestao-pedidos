# CP2 - Sistema de Gestão de Pedidos - Evolução do CP1

## 📋 Documento Complementar do Projeto

### 🎯 Contexto do Desafio

Este projeto é uma **evolução natural do CP1**, onde desenvolvemos um sistema básico de gestão de pedidos com as entidades Cliente, Funcionário e Pedido. O CP2 aplica os princípios de **Clean Architecture** e **Domain-Driven Design (DDD)** para transformar o sistema simples em uma API RESTful robusta e escalável.

### 🚀 Evolução do CP1 para CP2

**CP1 - Sistema Básico:**
- Entidades simples com propriedades básicas
- Relacionamentos diretos sem validações
- Estrutura monolítica sem separação de responsabilidades

**CP2 - Sistema Evoluído:**
- Entidades ricas com comportamento e validações
- Clean Architecture com 4 camadas bem definidas
- API RESTful com DTOs e mapeamento
- Banco de dados com migrations e relacionamentos complexos
- Documentação completa com Swagger

### 💡 Problema Identificado

**Desafio Principal:** O sistema do CP1, embora funcional, não seguia boas práticas de desenvolvimento, resultando em:
- Código acoplado e difícil de manter
- Falta de validações de negócio
- Ausência de separação de responsabilidades
- Dificuldade para evoluir e escalar
- Falta de documentação e testes

### 🏗️ Solução Implementada

**Sistema de Gestão de Pedidos Evoluído** - Uma API RESTful que evolui o CP1 aplicando:

1. **Gestão de Clientes**
   - Cadastro e atualização com validações
   - Controle de status (Ativo/Inativo/Bloqueado)
   - Histórico de pedidos

2. **Gestão de Funcionários**
   - Cadastro com cargo e salário
   - Controle de disponibilidade
   - Relacionamento many-to-many com pedidos

3. **Gestão de Pedidos**
   - Criação e acompanhamento de pedidos
   - Atualização de status em tempo real
   - Atribuição de funcionários
   - Controle de valor total

4. **Relacionamentos Inteligentes**
   - Cliente → Pedido (1:N)
   - Funcionário ↔ Pedido (N:N)
   - Sistema de status para controle de fluxo

### 🏗️ Arquitetura da Solução

**Clean Architecture + Domain-Driven Design (DDD)**

- **Domain Layer:** Entidades ricas com regras de negócio
- **Application Layer:** Casos de uso e serviços
- **Infrastructure Layer:** Persistência e integrações externas
- **Presentation Layer:** Controllers e DTOs

### 🎯 Benefícios Esperados

1. **Para a Mottu:**
   - Maior eficiência operacional
   - Redução de custos operacionais
   - Melhor controle de qualidade

2. **Para Entregadores:**
   - Interface clara para gerenciar entregas
   - Histórico de performance
   - Comunicação direta com clientes

3. **Para Clientes:**
   - Rastreamento em tempo real
   - Comunicação transparente
   - Entregas mais pontuais

### 🔧 Tecnologias Utilizadas

- **.NET 8** - Framework principal
- **Entity Framework Core** - ORM para persistência
- **MySQL** - Banco de dados relacional
- **Swagger/OpenAPI** - Documentação da API
- **Clean Architecture** - Padrão arquitetural
- **Domain-Driven Design** - Metodologia de design

### 📊 Entidades do Domínio

1. **Entregador**
   - ID, Nome, CPF, Telefone, Email
   - Status (Ativo/Inativo), Data de Cadastro
   - Relacionamento: 1:N com Entregas

2. **Entrega**
   - ID, Descrição, Endereço de Origem/Destino
   - Status (Pendente/EmAndamento/Concluida/Cancelada)
   - Data de Criação, Data de Conclusão
   - Relacionamento: N:1 com Entregador

### 🎯 Objetivos de Aprendizado

Este projeto demonstra:
- Aplicação prática de Clean Architecture
- Implementação de DDD em .NET
- Criação de APIs RESTful robustas
- Boas práticas de desenvolvimento
- Integração com banco de dados relacional
- Documentação técnica completa

---

**Equipe:**
- RM557356 - Alex Ribeiro
- RM559433 - Felipe Damasceno  
- RM560306 - Natalia dos Santos

**Disciplina:** Advanced Business Development with .NET - 2025
