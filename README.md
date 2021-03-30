# PromoCodeManager
An ASP.Net Core API for managing promo codes.

### Coding task:
Develop an ASP.Net Core API for managing promo codes.
Please use this layout as a reference
https://www.figma.com/file/6J7oriX3K4zPLF2lrhfIvJ/front-end-test-prototype
API should support the following functionality:
<ul>
<li> Ability to search services by name </li>
<li> Ability to Activate bonus for a Service for the current user</li>
<li> Infinite scroll for the Services list</li>
<li> An API user should be authorized.</li>
<li> Store data in any relational database, use EF Core to access data.</li>
<li> Use tests in your project.</li>
<li> The project should include a README.md with instructions on how to run it locally.</li></ul>

## How To Run
#### Requirements
Visual Studio was used in this project and the solution file is in VS format. <br />
#### Steps
<ol>
<li>Clone the repository and go to the root folder <i>PromoCodeManager-master</i>.</li>
<li>Double Click on the solution file called ALX_CodingAssignment.sln</li>
<li>The VS project should be open on your computer.</li>
<li>The Test project is named ALX_CodingAssignment_Test while the API is in ALX_CodingAssignment</li>
<li>The API has been documented and UI enabled with Swashbuckle, so you can run and test each endpoint by clicking the "play" button on VS<br />
  <b>Note:<b> Due to the authentication enabled, Postman or any other API testing tool maybe preferable as you will need to retrieve the user token when you log in
    and use it as the bearer token in your authorization header to access the authorized endpoints</li>
<li>Test and Enjoy!!</li>
</ol>
