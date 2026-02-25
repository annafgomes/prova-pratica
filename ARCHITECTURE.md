# ProductCatalog API

## 1. Visão Geral
O ProductCatalog é uma API REST desenvolvida em .NET, estruturada em arquitetura de camadas com base nos princípios da Clean Architecture, com separação  de responsabilidades entre camadas, foco em testabilidade, baixo acoplamento e organização modular.

O projeto contempla:
- CRUD completo de produtos.
- Ativação e desativação de produtos.
- Upload, atualização e remoção de imagem associada ao produto.
- Integração com armazenamento de objetos via MinIO.
- Persistência em banco de dados PostgreSQL.
- Testes automatizados.
- Containerização com Docker.
- Orquestração com Docker Compose.
- Pipeline de Integração Contínua com GitHub Actions.

---

## 2. Arquitetura
A solução foi estruturada seguindo arquitetura em camadas com influências da Clean Architecture.

### 2.1 Estrutura da Solução

ProductCatalog
--
│ 

├── ProductCatalog.API

├── ProductCatalog.Application

├── ProductCatalog.Domain

├── ProductCatalog.Infrastructure

├── ProductCatalog.Tests

└── docker-compose.yml

---

## 3. Camadas da Aplicação

### 3.1 ProductCatalog.Domain
Camada mais interna da aplicação. Não depende de nenhuma outra camada.

**Responsabilidades:**
- Definição das entidades.
- Definição das regras de negócio.
- Definição de contratos por meio de interfaces.

**Principais componentes:**
- Entidade Product.
- Interface IProductRepository.
- Interface IStorageService.

**Regras arquiteturais:**
- Não possui dependência de Infrastructure.
- Não possui dependência de Application.
- Não possui dependência de API.

Esta camada concentra exclusivamente as regras centrais do domínio.

---

### 3.2 ProductCatalog.Application
Camada responsável pelos casos de uso da aplicação. Depende apenas da camada Domain.

**Responsabilidades:**
- Orquestrar regras de negócio.
- Implementar os casos de uso.
- Coordenar chamadas ao repositório e ao serviço de armazenamento.
- Garantir separação entre regras e infraestrutura.

**Principais casos de uso implementados:**
- CreateProductUseCase.
- GetAllProductsUseCase.
- GetProductByIdUseCase.
- UpdateProductUseCase.
- DeleteProductUseCase.
- ActivateProductUseCase.
- DeactivateProductUseCase.
- UpdateProductImageUseCase.
- DeleteProductImageUseCase.

A camada Application depende exclusivamente de abstrações definidas no Domain, respeitando o princípio da inversão de dependência.

---

### 3.3 ProductCatalog.Infrastructure
Camada responsável pelas implementações técnicas e integrações externas. Depende da camada Domain.

**Responsabilidades:**
- Implementação do repositório de produtos.
- Implementação do serviço de armazenamento via MinIO.
- Configuração do DbContext.
- Migrations do Entity Framework Core.
- Configurações de acesso ao banco de dados.

**Principais componentes:**
- ProductRepository.
- ProductDbContext.
- MinioStorageService.
- Migrations versionadas.

A camada Infrastructure implementa as interfaces definidas no Domain, mantendo o domínio desacoplado de detalhes técnicos.

---

### 3.4 ProductCatalog.API
Camada de apresentação da aplicação.

**Responsabilidades:**
- Exposição de endpoints HTTP.
- Configuração da aplicação.
- Registro de dependências.
- Integração entre Application e Infrastructure.

**Principais componentes:**
- ProductsController.
- Program.cs.

A API não contém regras de negócio. Sua função é apenas receber requisições, delegar para os casos de uso e retornar as respostas apropriadas.

---

### 3.5 ProductCatalog.Tests
Projeto dedicado a testes automatizados.

**Responsabilidades:**
- Testar casos de uso.
- Validar regras de negócio.
- Garantir integridade do comportamento esperado.

**Tecnologias utilizadas:**
- xUnit.
- Moq.

**Status atual:**
- 20 testes implementados.
- Execução validada no pipeline de CI.
- Todos os testes aprovados.

---

## 4. Fluxo de Dependência
O fluxo de dependência respeita o seguinte padrão:

API → Application → Domain

Infrastructure → Domain

Tests → Application

**Regras fundamentais:**
- Domain não depende de nenhuma camada.
- Application depende apenas de Domain.
- Infrastructure depende de Domain.
- API compõe as dependências e configura a aplicação.
- Tests validam a camada Application.

---

## 5. Funcionalidades Implementadas
- Cadastro de produto.
- Listagem de produtos.
- Consulta de produto por identificador.
- Atualização de produto.
- Exclusão de produto.
- Ativação de produto.
- Desativação de produto.
- Upload de imagem associada ao produto.
- Atualização de imagem.
- Remoção de imagem.

---

## 6. Persistência
Banco de dados utilizado:
- PostgreSQL.

Tecnologia:
- Entity Framework Core.

Configurações implementadas:
- Migrations versionadas.
- DbContext configurado na camada Infrastructure.
- Connection string configurável por variáveis de ambiente.
- Execução integrada via Docker Compose.

---

## 7. Armazenamento de Arquivos
Serviço utilizado:
- MinIO.

Implementação:
- Interface IStorageService definida no Domain.
- Implementação concreta MinioStorageService na Infrastructure.

Responsabilidades do serviço:
- Upload de imagem.
- Atualização de imagem.
- Remoção de imagem.
- Integração com bucket configurado no ambiente Docker.

---

## 8. Containerização

### 8.1 Dockerfile
- Multi-stage build.
- Etapa de build com SDK do .NET.
- Etapa final com runtime.
- Publicação otimizada da API.

### 8.2 Docker Compose
Serviços orquestrados:
- API.
- PostgreSQL.
- MinIO.

Configurações implementadas:
- Comunicação entre containers via nome do serviço.
- Exposição de portas.
- Variáveis de ambiente configuradas.
- Dependência entre serviços.

---

## 9. Integração Contínua
Pipeline configurado com GitHub Actions.

Etapas executadas:
1. Restore das dependências.
2. Build da solução.
3. Execução dos testes.
4. Build da imagem Docker.

**Critérios de sucesso:**
- Compilação sem erros.
- Execução completa dos testes.
- Build da imagem concluído com sucesso.

**Status atual:**
- Pipeline executando com sucesso.
- Testes automatizados rodando no ambiente Linux do runner.
- Build Docker validado automaticamente.

---

## 10. Princípios Aplicados
- Clean Architecture.
- Separação de responsabilidades.
- Inversão de dependência.
- Baixo acoplamento.
- Alta coesão.
- Testabilidade.
- Organização modular.
- Versionamento estruturado de commits.
- Integração contínua.

---

## 11. Estado Atual do Projeto
- Arquitetura organizada e consistente.
- Regras de negócio isoladas da infraestrutura.
- Persistência desacoplada.
- Armazenamento externo integrado.
- Testes automatizados implementados.
- Pipeline de CI funcional.
- Containerização validada.

