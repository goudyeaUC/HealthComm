FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY ["HealthCommBackend.csproj", "./"]
RUN dotnet restore "./HealthCommBackend.csproj"

COPY . .
RUN dotnet publish "HealthCommBackend.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

RUN mkdir -p /app/data

ENV ASPNETCORE_HTTP_PORT=5000

EXPOSE 5000

ENTRYPOINT ["dotnet", "HealthCommBackend.dll"]