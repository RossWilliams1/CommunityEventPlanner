**Set Up**

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

**Architecture Considerations**

As I thought about Microservices, I started breaking down the project goals. So, I split my solution into three main parts: 
the user interface, Auth Service, and Community Event Service. I didn't want to mix up community events and user info in one place because the system could expand beyond just events,
and we'd still need to keep track of users without the need of the events. 

One thing I see needing improvement for future growth is the Community Events list. Right now,
it shows all records in one go. But if we scale up to millions of records,
that's going to cause performance problems. To tackle this, I'd suggest adding paging to limit the number of records we pull at once.

**Questions I had and Assumptions**

•	What data we need on events and users? 

  Community Event Planner Database:
  
    Community Event 
	
      Required Name
	  
      Summary
	  
      Required StartDate
	  
      EndDate
  
    User Community event
	
      Required CommunityEventId FK to Community Event Planner
	  
      Required UserId this is the Guid for the identity user model
  
  Auth Database: 
  
    Base Identity Models
	
    Name added to the IdentityUser model

•	Allow user to create events? 

o	 I assume you can do this anonymously and do not store the user guid in the table.
  
•	Allow users to view a list of upcoming events? 

o	 Gets all events in date descending order that have a start date in the future.
  
•	User Registration – there are different ways I could have approached this.

o	Option 1- User can register with filling out a form with email, name ect. This would need to be done for each registration. 
  
o Option 2 - Create a user login and registration that links the user to the event.
  
I opted for Option 2, which entails a straightforward registration process. 
However, I found repeatedly inputting user information for each event to be cumbersome and challenging to track user sign-ups effectively.
Implementing a centralized user record seemed more efficient for both reporting purposes and user experience. Additionally,
as I lacked prior experience setting up user login/registration and authentication systems from scratch, 
I viewed this as an opportunity for learning and growth. Hence, I chose to employ JWT (JSON Web Tokens) due to its streamlined approach,
efficiency, and robust security features. JWT facilitates stateless authentication, reducing reliance on server-side session data storage.

**API Documentation**

**Community Events**

Base URL: https://localhost/api/communityevent/

Get Upcoming Events

•	Description: Retrieves a list of upcoming community events.

•	Method: GET

•	Route: /getupcoming

•	Authentication: Not required

•	Response:

•	200 OK on success, returns a list of upcoming events.

Get Event IDs by User

•	Description: Retrieves event IDs associated with the authenticated user.

•	Method: GET

•	Route: /getidsbyuser

•	Authentication: Required (Bearer Token)

•	Response:

•	200 OK on success, returns a list of event IDs.

•	500 Internal Server Error if the user GUID retrieval fails.

User Registration for Event

•	Description: Registers the authenticated user for a specific event.

•	Method: POST

•	Route: /userregistration

•	Authentication: Required (Bearer Token)

•	Request Body:

•	CommunityEventId: ID of the event the user wants to register for.

•	Response:

•	200 OK on success, returns registration response.

•	500 Internal Server Error if the user GUID retrieval fails.

Create Event

•	Description: Creates a new community event.

•	Method: POST

•	Route: /create

•	Authentication: Not required

•	Request Body:

•	Provide event details in the request body.

•	Response:

•	200 OK on success, event creation successful.


**User Management**
Base URL: https://localhost/api/usermanagement/

Register User

•	Description: Registers a new user.

•	Method: POST

•	Route: /register

•	Authentication: Not required

•	Request Body:

•	Provide user registration details in the request body.

•	Response:

•	200 OK on success, returns registration response.

Login User

•	Description: Authenticates an existing user.

•	Method: POST

•	Route: /login

•	Authentication: Not required

•	Request Body:

•	Provide user login credentials in the request body.

•	Response:

•	200 OK on success, returns login response.

**References**

Build Secured .NET 8 APIs With Custom JWT Authentication & Authorization using Identity Manager! - https://www.youtube.com/watch?v=owk9faapaBs&t=2443s

Using HttpClientFactory in ASP.NET Core Applications - https://code-maze.com/using-httpclientfactory-in-asp-net-core-applications/

AutoMapper - https://docs.automapper.org/en/latest/Getting-started.html

Introduction to Identity on ASP.NET Core - https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-8.0&tabs=visual-studio

JWT - https://jwt.io/introduction



