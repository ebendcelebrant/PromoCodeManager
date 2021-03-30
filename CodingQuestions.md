# ASP.NET Developer Questions

## How do you document your code?
Documentation is a one thing the developer community struggle with. While we all agree it is a necessity, developers find it difficult to do and how we end up doing it is usually up to personal preference.
I document my libraries and codes with XML comments and then use Sandcastle to create help files. I ensure that all public fields, properties, and functions are provided with descriptive and appropriate XML documentation comments. For example, I ensure field names are not just stated but described in detail.
While I use inline comments moderately, I practice commenting on code blocks that have unique implementations to help other developers understand them.

## What do you think about unit testing?
I think unit testing is very important for producing quality code products. I have personally seen my code quality improve due to unit testing. It helps us, the developers, find bugs and issues that we have in our code before it goes to production or integration which in turn saves time as there is reduced back and forth between testing and development and money spent on testing since issues are discovered on time.
Also, code design is done better when it is done with unit testing is kept in mind. Easily testable code is better designed and allows for easy refactoring.

## What design patterns have you used in your projects?
I have used quite a few design patterns in my projects, the most common of which is the repository and unit of work pattern. These patterns help create an abstraction between the domain and data access layer of an application. I typical implement the repository pattern by creating a generic repository with the basic CRUD methods.
Another pattern I use is the CQS pattern. This design pattern creates a separation between commands (create, update, delete commands) and queries (read requests). This helps ensure my coding follows SOLID principles especially Single Responsibility principle and makes the code more readable. Yet another pattern I use is the builder pattern. This usually helps when I need to build separate sections of an entity based on criteria or configure it separately.  

## What are the most important performance issues in ASP.Net?
One of the most important performance issues in ASP.Net is excessive logging and unhandled exception handling. While properly handled exceptions can be managed, unhandled exceptions and hidden exceptions are hidden bombs that can affect load times. Also, excessive logging at every level of the application can affect performance.
Other issues include thread locking and synchronization, underlying database issues and code dependencies.
