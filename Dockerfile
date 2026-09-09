FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY . .

RUN dotnet restore "Universe.Api/Universe.Api.csproj"

RUN dotnet build "Universe.Api/Universe.Api.csproj" -c Release -o /app/build --no-restore

RUN dotnet publish "Universe.Api/Universe.Api.csproj" -c Release -o /app/publish --no-restore


FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "Universe.Api.dll"]