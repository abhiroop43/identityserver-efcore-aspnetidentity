# IdentityServer4 with ASP.NET Identity

1. Point appsettings.json connection string to existing database
2. Create IdentityServer migrations: dotnet ef migrations add IntialCreateIdentity --context ConfigurationDbContext
3. Add migrations to database: dotnet ef database update --context ConfigurationDbContext
4. Create ASP.NET Identity migrations: dotnet ef migrations add IdentityServerNewColumns --context ApplicationDbContext
5. Add migrations to database: dotnet ef database update --context ApplicationDbContext
6. Navigate to https://localhost:5001/.well-known/openid-configuration to check if response is generated
7. Register a user in the database with your existing .NET Core App.
8. Now, you can create Bearer Token for this user with a POST request to the URL: https://localhost:5001/connect/token

   Request Body:
   client_id=ro.client&client_secret=secret&grant_type=password&username=<your_username>&password=<your_password>&scope=maqta.ae

   Request Header:
   "Content-Type":"application/x-www-form-urlencoded"

   You should get a response like below:

   {
    "access_token": "eyJhbGciOiJSUzI1NiIsImtpZCI6IjU4Y2RlN2JjNTc1Y2IyNTRiZDA1NjEzYTcwZjljNmY3IiwidHlwIjoiSldUIn0.eyJuYmYiOjE1NDM5MTc5NTEsImV4cCI6MTU0MzkyMTU1MSwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NTAwMSIsImF1ZCI6WyJodHRwczovL2xvY2FsaG9zdDo1MDAxL3Jlc291cmNlcyIsIm1hcXRhLmFlIl0sImNsaWVudF9pZCI6InJvLmNsaWVudCIsInN1YiI6IjNhMzhjMzAwLTkwNzEtNDA3ZC1hYTk3LTA4OWM2NjZkNmFiYiIsImF1dGhfdGltZSI6MTU0MzkxNzk1MSwiaWRwIjoibG9jYWwiLCJzY29wZSI6WyJtYXF0YS5hZSJdLCJhbXIiOlsicHdkIl19.SH8Lfenb3C6FJu51bZkYKPzX0d6RIo2ouhuLewcMijZWgPcGsvnGBbVnBsOnyDEFRcf_DXOy9YHhwkm5ryY4UmI20l0KWp0FNZtlCMV4icNzYdCCjaQzoKSsmpgjH0-hhjAwd8ZwW2nCss6Gk8kID7O7E8TqVDXvHo36ljUPcSm_VaG_OIVDhWUBjS9KXRj5etg8OBZO3kWnNFdPA1UeOtYMZTQYC2R6YufABvGA2aZmqea7O5LurS07dMrshCFpvVNXfBefWtzIsZqqH7EoRFj4TWkIcxlZsP6Qo_4G3z45oZ-z9snND6YEDoXzYBIPJhKKI_W9N7VThxzOs9IGyw",
    "expires_in": 3600,
    "token_type": "Bearer"
}
