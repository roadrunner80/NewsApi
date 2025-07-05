# NewsApi

## Overview
NewsApi is a simple .NET Web API project that provides endpoints for managing news articles. It includes a controller for encapsulate HTTP requests to the [GNewsApi](https://gnews.io/)

## Project Structure
```
NewsApi
├── App
│   ├── Controllers
│   │   └── NewsController.cs
│   ├── Mappers
│   │   └── NewsMapper.cs
│   ├── Clients
│   │   └── GNewsClient.cs
│   ├── Models
│   │   └── GNews.cs
│   │   └── GNewsItem.cs
│   │   └── NewsItem.cs
│   ├── Services
│   │   └── NewsService.cs
│   ├── Program.cs
│   ├── Startup.cs
│   └── NewsApi.csproj
├── Test
│   ├── MapperTest.cs (Unit Test for NewsMapper)
│   ├── NewsControllerTest.cs (Integration test using wire mock for GNewsApi)
│   └── Test.csproj
└── NewsApi.sln
```

## Getting Started

### Prerequisites
- .NET SDK (version 8.0 or later)
- A code editor (e.g., Visual Studio Code)

### Installation
1. Clone the repository:
   ```
   git clone https://github.com/roadrunner80/NewsApi
   ```
2. Navigate to the project directory:
   ```
   cd App/NewsApi
   ```

### Running the API
1. Restore the dependencies:
   ```
   dotnet restore
   ```
2. Run the application:
   ```
   dotnet run
   ```
3. The API will be available at `http://localhost:5000`.

### Endpoints

#### Response Format for a News Item
| Property | Description |
| -------- | ------- |
| title	| The main title of the article. |
| description | The small paragraph under the title.	|
| content | All the content of the article. The content is truncated if the expand parameter is not set to content. Full content is only available if you have a paid subscription activated on your account. url	The URL of the article. image	The main image of the article.	|
| publishedAt| The date of publication of the article. The date is always in the UTC time zone. |
| source.name | The name of the source. |
| source.url | The homepage of the source. |

#### Endpoints


| Endpoint | Description | Response Body | Parameters |
| -------- | ------- | ------- | -------- |
| `GET /news` | Retrieves a list news. | collection of News Items | `numberOfArticles` default 5 |
| `GET search/{keyword}` | search for news by keyword. | collection of News Items | `keyword` only one word, just characters, `numberOfArticles` default 5  |
| `GET /news/search/at/{date}` | search for news at a date. | a News Item | `date` format YYYY-MM-DD |

## ToDo
  - Containerize the project using Docker so that it can be started with a single CLI command
  - Unit Test GNewsClient
  - Add Test Case for Mapper test that test null values
  - Add test case to validate 404 if search at returns no news article
  - Improve base URL Mock for GNewsClient (ugly hack with enviroment variables)