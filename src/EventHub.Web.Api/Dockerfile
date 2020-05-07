#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/EventHub.Web.Api/EventHub.Web.Api.csproj", "src/EventHub.Web.Api/"]
COPY ["src/EventHub.Service.Commands/EventHub.Service.Commands.csproj", "src/EventHub.Service.Commands/"]
COPY ["src/EventHub.DataAccess.EntityFramework/EventHub.DataAccess.EntityFramework.csproj", "src/EventHub.DataAccess.EntityFramework/"]
COPY ["src/EventHub.Service.Queries/EventHub.Service.Queries.csproj", "src/EventHub.Service.Queries/"]
COPY ["src/EventHub.Infrastructure/EventHub.Infrastructure.csproj", "src/EventHub.Infrastructure/"]
RUN dotnet restore "src/EventHub.Web.Api/EventHub.Web.Api.csproj"
COPY . .
WORKDIR "/src/src/EventHub.Web.Api"
RUN dotnet build "EventHub.Web.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EventHub.Web.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY "src/EventHub.Web.Api/Docker" .
RUN chmod +x ./wait-for-it.sh
ENTRYPOINT ["./wait-for-it.sh", "db:1433", "-t", "120", "--", "dotnet", "EventHub.Web.Api.dll"]