#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["ExpenseService/ExpenseService.csproj", "ExpenseService/"]
RUN dotnet restore "ExpenseService/ExpenseService.csproj"
COPY . .
WORKDIR "/src/ExpenseService"
RUN dotnet build "ExpenseService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ExpenseService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExpenseService.dll"]
