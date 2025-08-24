Pretty standard to start api inside ParcelDelivery.Api folder use
"dotnet run"

go to swagger to use/ test api
"http://localhost:5225/swagger"

To run tests inside ParcelDelivery.Testing folder use
"dotnet test"

May need to do a
"dotnet build"
to get tests to show on testing panel in ide

You could use IDE if you are on Visual Studio rather than commands.

Summary:
Controller based Asp.NET web api project using C#. Was pretty pressed for time but tried to incorporate everything I would with a new project including folder structure, models and services (or in this case a service). Not complete satisfied with it but given the time constrait it works well enough. Some testing added in a xunit testing project just to show I can do it.

Shortcuts/ Tradeoffs:
Couple hardcoded bits like Delivery Services and Statuses
Minimal models, services, controller and utility files (kept all to one file if possible)
Not enough testing for my liking

Improvements
I can list like a hundred but to list a few for a real mvp...
-More time
-More endpoints for adding new statuses for example
-Everything will be in tables
-Entity Framework and automapper so no manual DTO conversion
-Separate things out into more files ie multiple services
-Better error handling but creating my own exception/ adding in middleware, also removes the gross try catch on all the endpoints
-Logging in controller for internal logging

Ok I think i will commit this now pretty sure I am overrunning :)

Tools used:
VS Code
Alot of internet searches
dotnet scaffolding for initial app and test app creation
did use some AI to transform object to model for some dto files to save me typing it out
