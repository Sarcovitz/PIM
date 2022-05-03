#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PIMApi/PIMApi.csproj", "PIMApi/"]
RUN dotnet restore "PIMApi/PIMApi.csproj"
COPY . .
WORKDIR "/src/PIMApi"
RUN dotnet build "PIMApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PIMApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PIMApi.dll"]