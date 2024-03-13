# Learnify

Welcome to the Learnify repository! Follow the steps below to set up and run the project on your local machine.

## Prerequisites

Before you begin, ensure that you have the following installed on your machine:

- [.NET SDK](https://dotnet.microsoft.com/download)
- [Visual Studio Code](https://code.visualstudio.com/) (or any preferred code editor)
- [Git](https://git-scm.com/)

## Getting Started

1. Clone the repository to your local machine:

    ```bash
    git clone https://github.com/AntsOrg/learnify.git
    ``` 
    or if you are using SSH :
    ```bash
    git clone git@github.com:AntsOrg/learnify.git
    ```

2. Navigate to the project directory:

    ```bash
    cd learnify 
    ```

## Restore Dependencies

3. Restore project dependencies using the following command:

    ```bash
    dotnet restore
    ```

## Database Setup (if applicable)

4. This project involves database - PostgreSQL, setup database and then run: 

    ```bash
    dotnet ef migrations add InitialMigration
    dotnet ef database update
    ```

## Run the Application

5. Start the application with the following command:

    ```bash
    dotnet run
    ```

6. Open your web browser and navigate to [http://localhost:5000](http://localhost:5000)  or any port you specified to view the application.

## Additional Notes

- If you encounter any issues, check the `appsettings.json` file for configuration settings that may need adjustment based on your local environment.

- Make sure to ignore sensitive information such as database connection strings in the version control system.

- If you are using Visual Studio Code, ensure that you have the C# extension installed.

- Remember to exclude unnecessary files from version control (e.g., `/obj`, `/bin`, `/Migrations`, `.vscode`) by including them in the `.gitignore` file.

__Happy coding!__
