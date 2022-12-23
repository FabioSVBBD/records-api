# syntax=docker/dockerfile:1

# Build
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /source

COPY . .
RUN dotnet restore './records/records.csproj' --disable-parallel
RUN dotnet publish './records/records.csproj' -c release -o /app --no-restore

# Serve
FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5000

ENTRYPOINT ["dotnet", "records.dll"]