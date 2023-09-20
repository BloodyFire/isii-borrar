REM - In the following the commands to create a solution along with the projects of AppForMovies are presented.

REM - Replace AppForMovies with the name of the product you are going to develop

REM - First, CLONE YOUR REPO, preferably in c:/repos
REM - Second, copy this file to the folder where your repo has been cloned and then run this command
SET PROJECT_NAME=OneHope
if PROJECT_NAME=="" SET PROJECT_NAME=AppForMovies

REM - Create your solution
dotnet new sln --name "%PROJECT_NAME%"

REM - Create the folder for the source code and create the projects
md src
cd src

REM ----------------------------SRC------------------------------------------------

REM -------- create a library to share classes definition between both projects --------
dotnet new  classlib -f net6.0 -n "%PROJECT_NAME%".Shared

REM -----------------------  add the package for annotations to validate the properties in runtime
cd "%PROJECT_NAME%".Shared
dotnet add package Microsoft.AspNetCore.Components.DataAnnotations.Validation --version 3.2.0-rc1.20223.4
cd ..

REM -------- create the API project without authorization using framework 6.0 --------
dotnet new  webapi -au none -f net6.0 -n "%PROJECT_NAME%".API

REM -----------------------  add referebce to the AppForMovies.Shared project
cd "%PROJECT_NAME%".API
dotnet add reference ../"%PROJECT_NAME%".Shared/"%PROJECT_NAME%".Shared.csproj
cd ..

REM -----------------------  add packages to the project
cd "%PROJECT_NAME%".API
dotnet add package Microsoft.EntityFrameworkCore.Design --version 6.0.19
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 6.0.19
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 6.0.19
REM dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design --version 6.0.19
cd ..

REM -------- create the Web Project with authorization using framework 6.0 ------------
dotnet new  blazorserver -au Individual -f net6.0 -n "%PROJECT_NAME%".Web

REM -----------------------  add referebce to the AppForMovies.Shared project
cd "%PROJECT_NAME%".Web
dotnet add reference ../"%PROJECT_NAME%".Shared/"%PROJECT_NAME%".Shared.csproj
cd ..

REM ----------------------- add packages to the project 
cd "%PROJECT_NAME%".Web
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 6.0.19
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design --version 6.0.14
cd ..

REM - add the created projects to the solution
cd..
dotnet sln "%PROJECT_NAME%".sln add src/"%PROJECT_NAME%".API
dotnet sln "%PROJECT_NAME%".sln add src/"%PROJECT_NAME%".Web
dotnet sln "%PROJECT_NAME%".sln add src/"%PROJECT_NAME%".Shared

REM -------------------------------TEST-----------------------------------------------------------------------
REM - Create the folder for the testing code and create the projects
md test
cd test

REM -------- create the project for the unit test
dotnet new xunit -f net6.0 -n "%PROJECT_NAME%".UT

REM ----------------------- add the reference to the project to be tested
cd "%PROJECT_NAME%".UT
dotnet add reference ../../src/"%PROJECT_NAME%".API/"%PROJECT_NAME%".API.csproj
dotnet add reference ../../src/"%PROJECT_NAME%".Shared/"%PROJECT_NAME%".Shared.csproj

REM -----------------------  add packages
dotnet add package Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore --version 6.0.19
dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 6.0.19
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 6.0.19
dotnet add package xunit --version 2.4.2
dotnet add package coverlet.collector --version 6.0.0
cd ..

REM -------- create the project for the functional test
dotnet new xunit -f net6.0 -n "%PROJECT_NAME%".UIT

EM -----------------------  add packages
cd "%PROJECT_NAME%".UIT
dotnet add package Selenium.Support --version 4.10.0
dotnet add package Selenium.WebDriver --version 4.10.0
dotnet add package Selenium.WebDriver.ChromeDriver --version 114.0.5735.9000
dotnet add package xunit --version 2.5.0
dotnet add package xunit.runner.visualstudio --version 2.5.0
dotnet add package coverlet.collector --version 6.0.0
cd..

REM - add the created projects to the solution
cd..
dotnet sln "%PROJECT_NAME%".sln add test/"%PROJECT_NAME%".UT
dotnet sln "%PROJECT_NAME%".sln add test/"%PROJECT_NAME%".UIT



REM -------------------------------DESIGN-----------------------------------
REM - create the design project
md design
cd design
dotnet new classlib -f net6.0 -n "%PROJECT_NAME%".Design
cd ..

REM - add the project to the solution
dotnet sln "%PROJECT_NAME%".sln add design/"%PROJECT_NAME%".Design
