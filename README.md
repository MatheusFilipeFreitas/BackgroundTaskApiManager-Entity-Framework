# Background Task Management API

Uma API leve e extensível para gerenciamento de tarefas em segundo plano, com registro automático de tarefas e configuração de auto-start. Inclui integração com a biblioteca Dapper para acesso eficiente a dados e normalização.

## ✨ Funcionalidades

- 🔁 **Registro Automático de Tarefas**  
  Tarefas são descobertas e registradas automaticamente na inicialização — sem necessidade de configuração manual.

- 🚀 **Configuração de Auto-Start**  
  Tarefas podem ser configuradas para iniciar automaticamente via configuração ou código.

- ⚙️ **Gerenciamento de Ciclo de Vida das Tarefas**  
  Inicie, pare e monitore tarefas em segundo plano facilmente via API.

- 🗃️ **Integração com Dapper**  
  Uso do micro-ORM Dapper para acesso a dados de alta performance e normalização.

## 📦 Stack Tecnológico

- **.NET** (ex: ASP.NET Core)
- **C#**
- **Dapper**
- **Microsoft.Extensions.Hosting**

## 🛠️ Primeiros Passos

### Pré-requisitos

- [.NET SDK](https://dotnet.microsoft.com/download)
- Banco de dados SQL (ex: MSSQL, PostgreSQL)

### Instalação

```bash
git clone https://github.com/seu-usuario/background-task-api.git
cd background-task-api
dotnet restore
```

## ⚙️ Configuração
Atualize o appsettings.json com suas preferências:

```bash
{
  "TaskSettings": {
    "AutoStartEnabled": true
  },
  "ConnectionStrings": {
    "DefaultConnection": "sua-connection-string-aqui"
  }
}
```

## ▶️ Executando a Aplicação
Execute o projeto com o comando:

```bash
dotnet run
```
A API será iniciada e as tarefas com auto-start habilitado serão executadas automaticamente.

## 🧩 Uso
### Registrando uma Tarefa
Para registrar uma nova tarefa, basta implementar a interface IBackgroundTask:

```bash
public class SampleTask : IBackgroundTask
{
    public string Name => "SampleTask";

    public Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Sua lógica de tarefa em segundo plano
        return Task.CompletedTask;
    }
}
```

Essa tarefa será registrada automaticamente se estiver incluída no projeto e seguir a convenção definida.

## 💾 Exemplo com Dapper
Utilize o Dapper para acessar e normalizar dados:

```bash
using (var connection = new SqlConnection(_connectionString))
{
    var data = await connection.QueryAsync<MyModel>("SELECT * FROM MyTable");
}
```
Certifique-se de ter sua string de conexão configurada corretamente no appsettings.json.

## ✅ TODO
 - Adicionar dashboard ou UI de monitoramento
 - Implementar página de documentação

## 📄 Licença
Este projeto está licenciado sob a Licença MIT.  
Consulte o arquivo [LICENSE](https://github.com/MatheusFilipeFreitas/BackgroundTaskApiManager-Entity-Framework/blob/main/LICENSE) para mais detalhes.

