Deployment Overview
Backend Deployment: Deploy the ASP.NET Core API to Azure App Service with a SQL Server database.
Frontend Deployment: Deploy the Next.js application to Vercel (optimal for Next.js) or Azure Static Web Apps.
Environment Configuration: Configure environment variables and ensure both frontend and backend communicate securely.
Step 1: Deploying the Backend (ASP.NET Core API) to Azure : 

1.1 Prerequisites
Azure Account: Sign up at Azure Portal if you don’t already have an account.
Azure CLI: Install the Azure CLI for easier deployment. Azure CLI installation guide.
                              
1.2 Prepare the Backend for Deployment
Connection String: Update the connection string for SQL Server in appsettings.json. This should reference an Azure SQL Database instance.

json

"ConnectionStrings": {
    "DefaultConnection": "Server=<your_sql_server>.database.windows.net;Database=<your_database>;User Id=<your_username>;Password=<your_password>;"
}
Add Azure SQL Database:

Go to Azure Portal > SQL Databases > Create.
Configure the SQL database and server, then use these details in the connection string above.

1.3 Publish the Backend to Azure
Login to Azure:

az login
Create an Azure App Service (Optional if not already created):


az webapp create --resource-group <YourResourceGroup> --plan <YourAppServicePlan> --name <YourBackendAppName> --runtime "DOTNETCORE|6.0"
Publish with Visual Studio:

In Visual Studio, right-click on the project and select Publish.
Choose Azure App Service > Create New.
Select the existing or create a new App Service in your Azure subscription.
Configure the deployment settings and hit Publish.
Or Publish with CLI (if deploying via Azure CLI):

az webapp up --name <YourBackendAppName> --resource-group <YourResourceGroup> --location <YourLocation> --runtime "DOTNETCORE|6.0"
Configure Environment Variables:

In the Azure Portal, go to your App Service > Configuration.
Add environment variables, including your SQL Connection String under Connection Strings.

Step 2: Deploying the Frontend (Next.js) to Vercel or Azure
Option A: Deploying Next.js to Vercel

Vercel is optimized for Next.js and supports features like serverless functions and automatic builds from your Git repository.

Login to Vercel and connect your GitHub, GitLab, or Bitbucket account: Vercel Login.

Import your Project:

Go to New Project > Import Git Repository and select your Next.js project.
Vercel will automatically detect your Next.js configuration.
Configure Environment Variables:

In Vercel, go to Settings > Environment Variables.
Add variables for your API URL (e.g., NEXT_PUBLIC_API_URL) pointing to the backend URL.
Deploy:

Vercel will automatically deploy your project on each push to the main branch.
After deployment, Vercel provides a live URL (e.g., https://yourproject.vercel.app).
Option B: Deploying Next.js to Azure Static Web Apps

If you prefer Azure, Azure Static Web Apps can host your Next.js frontend.

Create an Azure Static Web App:

Go to Azure Portal > Static Web Apps > Create.
Select your subscription and create a new resource group.
Under Deployment Details, connect to your GitHub repository and configure the build settings:
App location: /
Output location: .next
Configure Environment Variables:

Go to Configuration in Azure Static Web Apps, and add the backend URL as an environment variable, e.g., NEXT_PUBLIC_API_URL.
Deploy:

Azure will build and deploy your Next.js app automatically.

Step 3: Configure CORS on the Backend

To allow your frontend to communicate with your API, configure CORS (Cross-Origin Resource Sharing) on your backend.

In your Startup.cs or Program.cs (ASP.NET Core 6+):

public void ConfigureServices(IServiceCollection services)
{
    services.AddCors(options =>
    {
        options.AddPolicy("AllowFrontend",
            builder =>
            {
                builder.WithOrigins("https://your-frontend-url.vercel.app") // or Azure Static Web App URL
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });
    });

    services.AddControllers();
    // Other services...
}

public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.UseCors("AllowFrontend");
    // Other middleware...
}

Step 4: Test and Verify Deployment

Test Frontend-Backend Integration: Go to your frontend URL, register/login, and test API calls to confirm they work as expected.
Debug Errors:

Check the Network tab in browser DevTools to inspect API request/response.
Use Azure App Service Logs and Vercel Logs (or Azure Static Web App logs) to debug issues.

Step 5: Optional - Configure Custom Domain and SSL

Both Azure and Vercel allow you to configure custom domains and automatic SSL for secure connections.

In Vercel or Azure, go to Settings > Domains and add a custom domain.
Enable HTTPS to ensure all traffic is secure.
