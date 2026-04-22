FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8090
EXPOSE 8091

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["ShoppingProducts.sln", "."]
COPY ["ShoppingProducts.API/ShoppingProducts.csproj", "ShoppingProducts.API/"]
COPY ["ShoppingProducts.Domain/", "ShoppingProducts.Domain/"]
COPY ["ShoppingProducts.Services/", "ShoppingProducts.Services/"]
COPY ["ShoppingProducts.Tests/*.csproj", "ShoppingProducts.Tests/"]

RUN dotnet restore

COPY . . 

WORKDIR "/src/ShoppingProducts.API"
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT [ "dotnet", "ShoppingProducts.dll" ]

# docker run -e ASPNETCORE_ENVIRONMENT=Development \
        #    -e ASPNETCORE_URLS=http://+:8090 \
        #    -p 8090:8090 \
        #    product:latest