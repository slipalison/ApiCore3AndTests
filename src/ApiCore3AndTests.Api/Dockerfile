#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ApiCore3AndTests.Api/ApiCore3AndTests.Api.csproj", "ApiCore3AndTests.Api/"]
RUN dotnet restore "ApiCore3AndTests.Api/ApiCore3AndTests.Api.csproj"
COPY . .
WORKDIR "/src/ApiCore3AndTests.Api"
RUN dotnet build "ApiCore3AndTests.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiCore3AndTests.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
HEALTHCHECK --interval=10s --timeout=10s --retries=3 CMD curl --fail http://localhost:80/hc || exit 1
ENTRYPOINT ["dotnet", "ApiCore3AndTests.Api.dll"]