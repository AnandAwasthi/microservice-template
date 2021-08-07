#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["__NAME__/__NAME__.csproj", "__NAME__/"]
RUN dotnet restore "__NAME__/__NAME__.csproj"
COPY . .
WORKDIR "/src/__NAME__"
RUN dotnet build "__NAME__.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "__NAME__.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "__NAME__.dll"]