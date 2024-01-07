# talent-management
This API is designed to manage talent. Talent is referred to as an employee in the project. It has the following API. <br />
1. [GET] api/employee/get-all -- This API has no pagination. It returns all employees/talent.<br />
2. [GET] api/employee/get-list/1/10 -- This API has pagination implemented. It will return all employees/talent based on page index and size.<br />
3. [GET] api/employee/{id} -- This API returns one specific employee/talent.<br />
4. [POST] api/employee/create -- This API will create new employees/talent.<br />
5. [PUT] api/employee/update -- This API will update the information of employees/talent.<br />
6. [DELETE] api/employee/delete/{id} -- This API will delete any employee/talent. <br />
<br />
<br />
<br />

To run this project, please follow the following steps. 
<br />
<br />
a) Build the project.<br />
b) Update the connection string for the SQL server.<br />
c) In the package manager console run the following commands<br />
      "Add-Migration InitialCreate"<br />
      "update-database"<br />
d) Run the project<br />
<br />
<br />
<br />

As authorization has been applied, for any employee API call, first get the bearer token from Auth/login API using the below credentials. <br />
username/password:<br />
bashir@gmail.com/P@ssword!<br />
tuan@gmail.com/P@ssword!<br />
marc@gmail.com/P@ssword!<br />
