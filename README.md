# STD.PL .NET 8 Project - Minimal API with Docker, Refit, and Hacker News API Integration

This project was developed using **.NET 8** to meet the requirements of a technical test. It features a **Minimal API** architecture, supports **Docker** (Linux-based), and leverages **Refit** for seamless integration with external APIs. The application interacts with the **Hacker News API** to retrieve and serve data.

## Features

- **Minimal API:** Lightweight framework for building RESTful APIs.
- **Hacker News API Integration:** Fetches data from the [Hacker News API](https://github.com/HackerNews/API).
- **Refit for API Calls:** Simplifies HTTP client handling and integration using interface-based design.
- **Docker Support:** Ensures portability and deployment efficiency using Linux-based containers.

## Potential Improvements

The project is functional but could be enhanced with the following features:

1. **OAuth Authentication:** Add OAuth to improve security and control over API access.
2. **Caching Mechanism:** Evaluate using caching (e.g., Redis or in-memory cache) to optimize performance for frequently accessed data.
3. **API Versioning:** Implement API versioning to manage changes and maintain backward compatibility.
4. **Telemetry:** Integrate OpenTelemetry to monitor, trace, and log API performance and usage.
5. **Cancellation Token Support:** Add and propagate `CancellationToken` to improve API responsiveness and resource management.

## Technologies Used

- **.NET 8:** Core framework for building the API.
- **Minimal API:** Simplified and performant API architecture.
- **Docker:** Used for containerization and deployment.
- **Refit:** Facilitates type-safe HTTP calls.
- **Hacker News API:** External data source for retrieving news, stories, and comments.

## Prerequisites

Ensure the following are installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/)

## Running the Project

### Run Locally

1. Clone the repository:
   ```bash
   git clone [<REPOSITORY_URL>](https://github.com/hkirschke/STD.PL.git)
   cd <PROJECT_DIRECTORY>
