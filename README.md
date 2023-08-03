# HomeXplorer

<p>ASP.NET Core 6 Web Application Educational Project</p>

<h2>C# Web Development Path at <a href="https://softuni.bg/" target="_blank">SoftUni, Bulgaria</a></h2>

<hr />

Info about my project:
<hr />

HomeXplorer is online platform for renting and managing properties. 
<br/> Anyone can become our agent by registering at the platform and thus we have agents, offering different properties all over the world.
<br /> In this platform any renter could find the home of his dreams.
<br /> Also in this platform any agent could find the best deal for his property.
<br /> So enjoy your time at the platform regardless of whether you are an agent or a renter.

<hr />

<h2>Tech stack:</h2>
<ol>
  <li>C#</li>
  <li>.NET Core 6</li>
  <li>ASP.NET Core 6</li>
  <li>Entity Framework Core</li>
  <li>MS SQL Server</li>
  <li>NUnit and Moq for testing</li>
  <li>Bootstrap 5</li>
  <li>HTML 5</li>
  <li>CSS 3</li>
  <li>JavaScript</li>
  <li>Google reCAPTCHA v3 API</li>
  <li>Cloudinary API</li>
  <li>MailKit SMTP API</li>
</ol>

<hr />
<p>Database Scheme: </p>

<hr />
<h2>App Features:</h2>
When it comes to agents, the web platform allows them to add new properties ; to edit their already added properties ; to delete already added property (it is soft delete for the database) ; to list all their added properties ; to check details of the properties. Each agent has its own customizable profile, where one can see its own profile info and to change its profile picture.
<br/>
<br/>
When it comes to renters, the web platform allows them to check properties which are around them (in their city) ; to check all added properties ; to check details of the properties ; to add a property into their list of favorites properties ; to rent and leave property ; to list all their rented properties ; to list all their favorite properties ; to search properties. Each renter has its own customizable profile, where one can see its own profile info and to change its profile picture. Each renter could add reviews about the web platform (the review must be approved by the administrator).
<br/>
<br/>
When it comes to the platform administrator, one can check platform statistics (total rented properties ; total added properties ; total properties, added to favorites ; total reviews added. The administrator can add new info in the platform (new countries ; cities ; etc.). The administrator could list all registered users based on their roles, distinguishing them as agents and renters.  Also the administrator can approve or delete added review from a renter user. 
<br/>
<br/>
When it comes to non-registered users - they could list and search for properties and to check property details.
<br/>
<br/>
The application has two main layouts - one for the agents ; renters ; non-registered users and one for the administrator.
