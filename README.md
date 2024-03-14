 # RequestCounter Assignment

 # About:

The .NET Web Api currently used by our company receives an estimated 60,000-120,000 requests per day. The company wants to grow traffic to over 1 million requests per day in the next few years. To track the growth and usage patterns based on accurate data is important, so the task is collecting data and making it queryable. It is also a requirement to see how the number of requests is changing on a daily aggregated basis.

To do this, the task is implementing a GET endpoint that takes a from date as a parameter, and for the period queried, returns each endpoint in some identifiable way, and the corresponding values for how many times it was called that day. No "Real-Time" data is required; the current day's data need not be returned, it is sufficient to return only backwards from "yesterday". You can add at least two example endpoints that don't need to do anything, just return 200 OK. Don't bother with security issues and the like of course, for the sake of example a public Web Api is perfectly fine.

Our business data is currently stored on SQL Server, our data layer is Entity Framework model, and the application is running on Azure resources (we have no Azure-specific technology dependencies). This is information about the existing application, which does not mean that you need to store the data on SQL Server and use Entity Framework to solve the problem; you can use whatever technology you think is most appropriate for the solution.

# Summarize the solution:

The application increment the counter field by a stored procedure called by declaring Sql Connection and used Serializable Isolation Level.

The solution contains 5 projects:

- RequestCounter.DataAccess.DataContext
Contains the RequestContext datacontext configured to use sql server with a RequestLog model to store Identity, Date, EndPointName and Counter fields. Entity Framework Migration is enabled, the database creation is managed by Update-Database command. It can executed in Package Manager Console and set the DataContext as default project and set the Web Api Project as startup project in the solution.

- RequestCounter.DataAccess.Repository
Represents the unit of work and repository pattern by creating an abstraction layer for the datacontext itself. It contains a RequestLogRepository with the implementation of connecting to the database and call the stored procedure using Serializable Isolation Level.

- RequestConter.BusinessLogic
Represents a layer between the Web Api and the Data Access Layer. The core of the solution is in the data access layer, so the RequestService

- RequestCounter.WebApi
Includes a Web api with a CountRequestAttribute for the purpose of specifying which controller action to count requests. The counting is called by the RequestCountMiddleware. There are two controllers, the RequestCounterController has a GET method to retrieve the request log by a from and to parameter.

- Test.BusinessLogic
Represents a unit test to test the counting using 10000 Tasks. In order to simplify the unit test and focus on counting, it connects to the database by a connection string to ensure the counter is stored as expected.

# Feature missing or to be improved:

- Improving the testing is also a major task.

- Create a data transfer object and map the RequestLog datacontext object

- The exception handling should definitely be improved and centralised.

- Built in logging is also important for logging information, warning or error messages.

- The unit of work can also implement a centralized transaction handling