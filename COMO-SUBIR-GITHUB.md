# 🚀 Como Subir o Projeto CP2 no GitHub

## 📋 Passo a Passo Completo

### 1. **Preparar o Repositório Local**

```bash
# Navegar para a pasta do projeto
cd /Users/nataliadeoliveirasantos/Documents/cp-.NET

# Inicializar o Git (se ainda não foi feito)
git init

# Adicionar todos os arquivos
git add .

# Fazer o primeiro commit
git commit -m "feat: CP2 - Sistema de Gestão de Pedidos com Clean Architecture

- Implementação de Clean Architecture e DDD
- Entidades: Cliente, Funcionário, Pedido
- API RESTful completa com CRUD
- Banco MySQL com EF Core
- Documentação Swagger/OpenAPI
- Validações com FluentValidation
- Mapeamento com AutoMapper

Equipe: RM557356, RM559433, RM560306"
```

### 2. **Criar Repositório no GitHub**

1. Acesse [github.com](https://github.com)
2. Clique em **"New repository"** (botão verde)
3. Preencha os dados:
   - **Repository name**: `cp2-sistema-gestao-pedidos`
   - **Description**: `Sistema de Gestão de Pedidos - CP2 | Clean Architecture + DDD | .NET 8`
   - **Visibility**: Public (para facilitar avaliação)
   - **NÃO marque** "Add a README file" (já temos um)
   - **NÃO marque** "Add .gitignore" (já temos um)
4. Clique em **"Create repository"**

### 3. **Conectar Repositório Local ao GitHub**

```bash
# Adicionar o repositório remoto (substitua SEU_USUARIO pelo seu username do GitHub)
git remote add origin https://github.com/SEU_USUARIO/cp2-sistema-gestao-pedidos.git

# Verificar se foi adicionado corretamente
git remote -v

# Fazer push para o GitHub
git push -u origin main
```

### 4. **Se Der Erro de Branch**

```bash
# Renomear branch para main (se necessário)
git branch -M main

# Fazer push novamente
git push -u origin main
```

## 🔧 **Comandos Alternativos (se necessário)**

### Se o GitHub pedir autenticação:
```bash
# Usar token de acesso pessoal
git remote set-url origin https://SEU_TOKEN@github.com/SEU_USUARIO/cp2-sistema-gestao-pedidos.git
git push -u origin main
```

### Se quiser usar SSH:
```bash
# Configurar SSH (primeira vez)
ssh-keygen -t ed25519 -C "seu-email@exemplo.com"

# Adicionar chave SSH ao GitHub
cat ~/.ssh/id_ed25519.pub
# Copie a saída e adicione em: GitHub > Settings > SSH and GPG keys

# Usar SSH para conectar
git remote set-url origin git@github.com:SEU_USUARIO/cp2-sistema-gestao-pedidos.git
git push -u origin main
```

## 📝 **Estrutura Final no GitHub**

Seu repositório ficará assim:
```
cp2-sistema-gestao-pedidos/
├── README.md                    # Documentação principal
├── CP2-Projeto-Documento.md     # Documento complementar
├── INSTALACAO.md                # Guia de instalação
├── Postman-Collection.md         # Coleção Postman
├── Database-Scripts.md          # Scripts do banco
├── ENTREGA-FINAL.md             # Resumo da entrega
├── .gitignore                   # Arquivos ignorados
├── MottuDelivery.sln            # Solução principal
├── MottuDelivery.API/           # Camada de Apresentação
├── MottuDelivery.Application/   # Camada de Aplicação
├── MottuDelivery.Domain/        # Camada de Domínio
├── MottuDelivery.Infrastructure/ # Camada de Infraestrutura
└── cp-1/                        # Projeto CP1 original
```

## ✅ **Verificação Final**

Após subir, verifique se:
- [ ] Todos os arquivos estão no GitHub
- [ ] README.md está sendo exibido corretamente
- [ ] Link do repositório está funcionando
- [ ] Documentação está completa

## 🎯 **Link para Entrega**

Seu repositório estará disponível em:
`https://github.com/SEU_USUARIO/cp2-sistema-gestao-pedidos`

## 🚨 **Dicas Importantes**

1. **Nunca suba senhas**: O `.gitignore` já protege arquivos sensíveis
2. **Commits descritivos**: Use mensagens claras sobre o que foi feito
3. **Documentação completa**: O README.md é a primeira impressão
4. **Estrutura organizada**: Mantenha a organização das pastas

## 📞 **Se Der Problema**

Se encontrar algum erro:
1. Verifique se o Git está instalado: `git --version`
2. Verifique se está na pasta correta: `pwd`
3. Verifique se o repositório foi criado no GitHub
4. Verifique se o username está correto

---

**Equipe:**
- RM557356 - Alex Ribeiro
- RM559433 - Felipe Damasceno  
- RM560306 - Natalia dos Santos
