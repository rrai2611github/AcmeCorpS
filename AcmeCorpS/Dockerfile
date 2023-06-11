# Dockerfile

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj .
RUN dotnet restore

# Copy everything else and build the API
COPY . .
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Set environment variables if needed
# ENV ASPNETCORE_ENVIRONMENT=Production

# Expose the API port
EXPOSE 5000

# Start the API
ENTRYPOINT ["dotnet", "AcmeCorp.dll"]
