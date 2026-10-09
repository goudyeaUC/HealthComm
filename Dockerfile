FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY ["HealthCommBackend/HealthCommBackend.csproj", "HealthCommBackend/"]
RUN dotnet restore "HealthCommBackend/HealthCommBackend.csproj"

COPY . .
RUN dotnet publish "HealthCommBackend.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

RUN mkdir -p /app/data

ENV ASPNETCORE_URLS=http://+:8080

EXPOSE 8080

ENTRYPOINT ["dotnet", "HealthCommBackend.dll"]