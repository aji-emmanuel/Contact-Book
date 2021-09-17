#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src

COPY *.sln .
#COPY ["UserManagement.API/UserManagement.API.csproj", "UserManagement.API/"]
COPY UserManagement.API/*.csproj UserManagement.API/
COPY Core/*.csproj Core/
COPY DataAccess/*.csproj DataAccess/
COPY Model/*.csproj Model/
COPY DTOs/*.csproj DTOs/
COPY HATEOAS/*.csproj HATEOAS/
COPY ImageUploadService/*.csproj ImageUploadService/
RUN dotnet restore UserManagement.API/*.csproj
COPY . .

WORKDIR /src/UserManagement.API
RUN dotnet build 
FROM build AS publish
WORKDIR /src/UserManagement.API
RUN dotnet publish "UserManagement.API.csproj" -c Release -o /app/publish
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=publish src/UserManagement.API/JsonFiles/Roles.json json/
COPY --from=publish src/UserManagement.API/JsonFiles/Users.json json/
#ENTRYPOINT ["dotnet", "UserManagement.API.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet UserManagement.API.dll
    