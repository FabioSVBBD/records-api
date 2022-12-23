# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:6.0 as build-env
WORKDIR /records

COPY records/*.csproj .
RUN dotnet restore
COPY records .
RUN dotnet publish -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime
WORKDIR /records
COPY --from=build-env /publish .
EXPOSE 80 443
ENTRYPOINT ["dotnet", "records.dll"]