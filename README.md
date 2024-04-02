Set Up: 
Clone branch.
Set CommunityEventPlanner.API as start up project.
Open package Manager Console
set default project to CommunityEventPlanner.Data
run update-database
Set CommunityEventPlanner.Auth as start up project.
Open package Manager Console
set default project to CommunityEventPlanner.Auth
run update-database
Set up multiple start up projects
Set CommunityEventPlanner.Auth action to Start
Set CommunityEventPlanner.Api action to Start
Set CommunityEventPlanner.Ui action to Start
Run application 
In with the UI browser open click the Register link in top right corner
Fill in form and register user
Login as user
Upcoming Events, shows a list of upconing events if logged in you can register to each event with a simple click of register.
Create Event, allows users to create a event.

Architecture Considerations:
As I thought about Microservices, I started breaking down the project goals. So, I split my solution into three main parts: 
the user interface, Auth Service, and Community Event Service. I didn't want to mix up community events and user info in one place because the system could expand beyond just events,
and we'd still need to keep track of users without the need of the events. 

One thing I see needing improvement for future growth is the Community Events list. Right now,
it shows all records in one go. But if we scale up to millions of records,
that's going to cause performance problems. To tackle this, I'd suggest adding paging to limit the number of records we pull at once.

Questions I had and Assumptions: 
•	What data we need on events and users? 
  Community Event Planner Database:
    Community Event 
      Required Name
      Summary
      Required StartDate
      EndDate
  
    User Community event
      Required CommunityEventId – FK to Community Event Planner
      Required UserId – this is the Guid for the identity user model
  
  Auth Database: 
    Base Identity Models
    Name added to the IdentityUser model

•	Allow user to create events? 
  o	 I assume you can do this anonymously and do not store the user guid in the table. 
•	Allow users to view a list of upcoming events? 
  o	Gets all events in date descending order that have a start date in the future.
•	User Registration – there was different way I could have done this. Obviously, this is very vague and would in a normal way would need discussions. 
  o	Option 1- User can register with filling out a form with email, name ect. This would need to be done for each registration. 
  o	Option 2 - Create a user login and registration that links the user to the event.

I opted for Option 2, which entails a straightforward registration process. 
However, I found repeatedly inputting user information for each event to be cumbersome and challenging to track user sign-ups effectively.
Implementing a centralized user record seemed more efficient for both reporting purposes and user experience. Additionally,
as I lacked prior experience setting up user login/registration and authentication systems from scratch, 
I viewed this as an opportunity for learning and growth. Hence, I chose to employ JWT (JSON Web Tokens) due to its streamlined approach,
efficiency, and robust security features. JWT facilitates stateless authentication, reducing reliance on server-side session data storage.

References: 
Build Secured .NET 8 APIs With Custom JWT Authentication & Authorization using Identity Manager! - https://www.youtube.com/watch?v=owk9faapaBs&t=2443s
Using HttpClientFactory in ASP.NET Core Applications - https://code-maze.com/using-httpclientfactory-in-asp-net-core-applications/
AutoMapper - https://docs.automapper.org/en/latest/Getting-started.html
Introduction to Identity on ASP.NET Core - https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-8.0&tabs=visual-studio
JWT - https://jwt.io/introduction



