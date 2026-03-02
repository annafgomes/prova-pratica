## Como Executar o Projeto

Este projeto pode ser executado utilizando Docker 

---

###  Executar com Docker 

Esta é a forma mais simples e garante que todos os serviços necessários serão iniciados automaticamente.

#### Requisitos

- Docker Desktop instalado e em execução

#### Passos

1. Clone o repositório:

```bash
git clone https://github.com/annafgomes/prova-pratica.git
cd ProductCatalog
```

2. Execute o comando:

```bash
docker compose up --build
```

3. Após a inicialização dos containers, acesse a documentação da API em:

```
http://localhost:5000/swagger
```
ou
```
http://localhost:8080/swagger
```
4. Acesse o MinIO em:
```
http://localhost:9001
```
user: admin

senha: admin123

Serviços iniciados automaticamente:

- API
- PostgreSQL
- MinIO

---

