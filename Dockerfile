FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5000

ENV ASPNETCORE_URLS=http://*:5000

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["./CloudCustomers.API/CloudCustomers.API.csproj", "./CloudCustomers.API/"]
RUN dotnet restore "./CloudCustomers.API/CloudCustomers.API.csproj"
COPY . .
WORKDIR "/src/CloudCustomers.API"
RUN dotnet build "./CloudCustomers.API.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "./CloudCustomers.API.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CloudCustomers.API.dll"]


# Build Stage
#FROM mcr.microsoft.com/dotnet/sdk:7.0 As Build

#WORKDIR /source
#EXPOSE 5000

#ENV ASPNETCORE_HTTP_PORT=https://*:5001 ASPNETCORE_URLS=https://*:5000
#COPY . .

#RUN dotnet restore "./CloudCustomers.API/CloudCustomers.API.csproj" --disable-parallel
#RUN dotnet publish "./CloudCustomers.API/CloudCustomers.API.csproj" -c release -o /app --no-restore

# Serve Stage
#FROM mcr.microsoft.com/dotnet/aspnet:7.0



#WORKDIR /app

#COPY --from=Build /app ./
#EXPOSE 5000

#ENTRYPOINT [ "dotnet",  "CloudCustomers.API.dll"]