# 📘 LibraryService 

API REST desenvolvIda em **.NET 10** com foco em um CRUD básico, autenticação via **JWT Bearer**, uso de **Entity Framework Core**, documentação com **Swagger**, e execução tanto local quanto via **Docker Compose**.

---

# 🚀 Tecnologias utilizadas

* .NET 10 (ASP.NET Core)
* Entity Framework Core
* SQL Server
* JWT (Bearer Token)
* Swagger (OpenAPI)
* Docker & Docker Compose
* Middleware customizado para logs

---

# 📂 Estrutura do projeto

```
BookCatalogAPI
│
├── Controllers
├── Models
├── Dto
├── Services
│   ├── Autor
│   ├── Livro
│   └── Auth
├── Data
├── Migrations
├── Swagger
├── Log (Middleware)
├── Program.cs
├── appsettings.json
├── docker-compose.yml
└── Dockerfile
```

---

# 🔐 Autenticação (JWT Bearer)

A API utiliza autenticação baseada em **JWT (Bearer Token)**.

## 📌 Fluxo

1. Usuário realiza login
2. API valIda credenciais
3. Gera um token JWT
4. Cliente envia o token no header:

```
Authorization: Bearer {seu_token}
```

## 📌 Configuração

```json
"Jwt": {
  "Key": "sua-chave-secreta",
  "Issuer": "BookCatalogAPI",
  "Audience": "BookCatalogAPI"
}
```

---

# 🗄️ Entity Framework Core

Utilizado como ORM para manipulação do banco de dados.

## 📌 FuncionalIdades

* Mapeamento via Models
* Migrations automáticas
* Relacionamentos (Autor ↔ Livro)

## 📌 Comandos úteis

```bash
dotnet ef migrations add InitialCreate
dotnet ef Database update
```

---

# 📚 Swagger (Documentação)

A API possui documentação interativa via Swagger.

## 📌 Acesso

```
http://localhost:5146/swagger
```

---

# 📦 CRUD implementado

## 🔹 Autor

* Criar author
* Listar authors
* Buscar por ID
* Editar
* Excluir

## 🔹 Livro

* Criar book
* Listar books
* Buscar por ID
* Buscar por author

---

# 🧠 Middleware de Logs

Middleware customizado para interceptar requisições e registrar logs.

## 📌 ResponsabilIdades

* Capturar requisições
* Registrar informações de execução
* Auxiliar debug

---

# 🧪 Executando o projeto localmente

## 🔹 Pré-requisitos

* .NET 10 SDK
* SQL Server

## 🔹 Passos

```bash
dotnet restore
dotnet run
```

---

# 🐳 Executando com Docker

## 🔹 Build + Run

```bash
docker-compose up --build
```

---

## 🔹 Serviços

### API

* Porta: `5146`
* URL: `http://localhost:5146`

### Banco SQL Server

* Porta: `1440`
* Server: `localhost,1440`

---

## 🔹 Variáveis de ambiente

Utilize `.env`:

```
SA_PASSWORD=SuaSenhaForte123!
JWT_KEY=SuaChaveJWT
```

---

# 🔗 Conexão com banco no Docker

```
Server=db;Database=db01_api_dotnet;UserApp=sa;Password=${SA_PASSWORD}
```

---

# ⚙️ Configuração importante (Docker)

A API está configurada para escutar corretamente dentro do container:

```
ASPNETCORE_URLS=http://0.0.0.0:5146
```

---

# 📌 Observações

* Uso de **Dependency Injection** em toda aplicação
* Separação por camadas (Controller → Service → Data)
* DTOs para entrada de dados
* Segurança básica com JWT
* Pronto para evolução (microservices / cloud)

---

# 🎯 Objetivo do projeto

Projeto voltado para:

* Prática de backend com .NET
* Entendimento de arquitetura em camadas
* Integração com banco de dados
* Introdução a autenticação segura
* Uso de containers (Docker)

---

# 🚀 Próximos passos (evolução)

* Adicionar Roles e authorização avançada
* Implementar refresh token
* Health checks
* Deploy em cloud (AWS)
* Kubernetes

---

# 👨‍💻 Autor
Gabriel Fortes
