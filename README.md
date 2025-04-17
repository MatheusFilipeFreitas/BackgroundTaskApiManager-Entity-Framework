# Background Task Management API

A lightweight and extensible API for managing background tasks, featuring automatic task registration and auto-start configuration. Includes integration with the Dapper library for efficient data access and normalization.

## ✨ Features

- 🔁 **Automatic Task Registration**  
  Tasks are automatically discovered and registered at startup — no manual setup required.

- 🚀 **Auto-Start Configuration**  
  Tasks can be configured to start automatically via configuration or programmatically.

- ⚙️ **Task Lifecycle Management**  
  Easily start, stop, and monitor background tasks through the API.

- 🗃️ **Dapper Integration**  
  Uses the Dapper micro-ORM for high-performance data access and normalization.

## 📦 Tech Stack

- **.NET** (e.g., ASP.NET Core)
- **C#**
- **Dapper**
- **Microsoft.Extensions.Hosting**

## 🛠️ Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download)
- SQL Database (e.g., MSSQL, PostgreSQL)

### Installation

```bash
git clone https://github.com/your-username/background-task-api.git
cd background-task-api
dotnet restore
```

## ⚙️ Configuration

You can configure tasks to start automatically by updating the `appsettings.json` file:

```json
  "StartupTasks": [
    {
      "Name": "Task1",
      "StartTime": "19:00:00",
      "StopTime": "05:00:00"
    }
  ]
```
Add your database updating the `appsettings.json` or `appsettings.json` file:

```json
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=MyDatabase;User Id=myuser;Password=mypassword;TrustServerCertificate=True;"
  },
```

## 📈 API Endpoints

> Example routes for task control (customizable as needed):

- `GET /tasks` – List all registered tasks
- `POST /tasks/{taskName}/start` – Start a specific task
- `POST /tasks/{taskName}/stop` – Stop a specific task

## ✅ Example

Here’s a simple task implementation:

```csharp
public class SampleTask(ILogger<SampleTask> logger) : TaskBase
{
    public override Task ExecuteAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation($"{this.GetType().Name} - {nameof(Task2)}");
        return Task.CompletedTask;
    }
}
```

Add the Manual tasks inside the `/BackgroundTaskHandlerAPI/Models/Tasks/ManualTasks/` folder.
And add the Startup tasks inside the `/BackgroundTaskHandlerAPI/Models/Tasks/StartupTasks/` folder.

## 🤝 Contributing

Contributions, issues, and feature requests are welcome!  
Feel free to fork the repository and submit a pull request.

## 📄 License

This project is licensed under the [MIT License](LICENSE).

---

Made using .NET and Dapper.
