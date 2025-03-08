# 📘 Guia de Instalação do Projeto

Este documento fornece instruções para instalar e executar o projeto utilizando **Docker Compose** (indicado) e **modo manual**.

## 📌 Pré-requisitos
Certifique-se de ter os seguintes softwares instalados:
- [Docker](https://www.docker.com/get-started) e Docker Compose
- [.NET SDK 8+](https://dotnet.microsoft.com/download)
- [Node.js 18+](https://nodejs.org/)
- SQLite3 (opcional, caso queira acessar o banco manualmente)
- A aplicação utiliza o SQLite para facilitar a execução do programa e por ser uma implementação simples ser mais leve e facil de gerenciar.
---

## 🚀 Executando com Docker Compose
1. **Navegue até a pasta raiz do projeto:**
   ```sh
   cd payment-control
   ```
2. **Construa e inicie os contêineres:**
   ```sh
   docker-compose up --build -d
   ```
3. **Acesse os serviços:**
   - **Backend:** `http://localhost:5198`
   - **Frontend:** `http://localhost:3000`

4. **Para parar os contêineres:**
   ```sh
   docker-compose down
   ```

---

## 🛠 Executando Manualmente (Sem Docker)

### 1️⃣ Iniciando o Backend
1. **Navegue até a pasta do backend:**
   ```sh
   cd backend/payment-control-api
   ```
2. **Restaurar pacotes e compilar:**
   ```sh
   dotnet restore
   dotnet build
   ```
3. **Aplicar migrações e rodar:**
   ```sh
   cd payment-control-infrastructure
   dotnet ef database update
   cd ..
   $env:ASPNETCORE_URLS="http://localhost:5198"; 
   dotnet run
   ```
4. O backend estará rodando em `http://localhost:5198`

### 2️⃣ Iniciando o Frontend
1. **Navegue até a pasta do frontend:**
   ```sh
   cd frontend
   ```
2. **Instalar dependências:**
   ```sh
   npm install
   ```
3. **Rodar o servidor de desenvolvimento:**
   ```sh
   npm start
   ```
4. O frontend estará rodando em `http://localhost:3000`

5. Para navegar entre as telas utilize o icone do Menu
---



## ✅ Testando a Aplicação
Após iniciar os serviços, você pode testar acessando:
- **API Backend:** `http://localhost:5198/swagger` (se Swagger estiver habilitado)
- **Interface Web:** `http://localhost:3000`
🚀

