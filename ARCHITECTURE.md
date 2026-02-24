# Arquitetura do Projeto – Product Catalog API

## 1. Visão Geral

Este projeto segue uma arquitetura em camadas inspirada nos princípios de Clean Architecture.

O objetivo é manter:

- Baixo acoplamento
- Alta coesão
- Separação de responsabilidades
- Independência da regra de negócio

A camada de domínio (Domain) não depende de nenhuma outra camada.

---

## 2. Estrutura de Pastas

ProductCatalog.API  
ProductCatalog.Application  
ProductCatalog.Domain  
ProductCatalog.Infrastructure  
ProductCatalog.Tests  

- API → Camada de apresentação (Controllers, Swagger, configuração da aplicação)
- Application → Casos de uso e contratos (interfaces)
- Domain → Entidades e regras de negócio
- Infrastructure → Implementações de persistência e integrações externas
- Tests → Testes unitários

---

## 3. Diagrama de Dependências

API → Application → Domain  
Infrastructure → Domain  
Tests → Domain / Application  

A camada Domain é o núcleo da aplicação e não possui dependências externas.

---

## 4. Princípios Utilizados

- SOLID
- Clean Code
- Arquitetura em Camadas
- Inversão de Dependência
- Encapsulamento de regras de negócio

---

## 5. Banco de Dados

Será utilizado PostgreSQL como banco relacional.

A persistência será implementada na camada Infrastructure utilizando Entity Framework Core.

---

## 6. Containerização

O projeto será executado via Docker com:

- API
- PostgreSQL
- Serviço para simulação de armazenamento de imagens (MinIO ou similar)

Será utilizado docker-compose para orquestração.

---

## 7. CI/CD

Será implementado pipeline no GitHub Actions contendo:

- Restore
- Build
- Test
- Fail em caso de erro

---

## 8. Modelo de Domínio (Em Evolução)

A entidade principal é Product, que conterá:

- Id
- Nome
- Categoria
- Preço
- Status (Ativo/Inativo)
- Caminho da imagem

Regras de negócio:

- Nome é obrigatório
- Preço deve ser maior que zero
- Categoria é obrigatória
- Produto pode ser ativado ou inativado