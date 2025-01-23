# search-hits
A simple API and Web client to send searches for the supplied input to the configured search engines, scrape their search hit count and add the numbers up.
The API is using .NET 9 and is self hosted (Kestrel). Requires .NET 9 runtime.
The Web client is a React app hosted using Vite. Requires Node.js (decently recent version).

Supplied bat files to quickly build and run in a Windows environment.
For other environments (or to run manually step by step):
- API:
  - Build by heading into the **Api** directory and type **dotnet build**
  - Run by heading into the **Api\bin\Debug\net9.0** directory and type **dotnet api.dll**
- Client:
  - Install NPM packages by heading into the **client** direcory and type **npm install**
  - Run by heading into the **client** directory and type **npm run dev**

The API runs on port 5000 and can be tested using the search string as its path, e.g. **http://localhost:5000/giraffe** or **http://localhost:5000/abundant%20excess**.
The client uses port 80 by default, but will use another port if it's already in use (see the output window when starting), so it can be accessed using **http://localhost** or **http://localhost:80** (or alternate port).
