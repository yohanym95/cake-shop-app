FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
# COPY ["shopapp/shopapp.csproj", "shopapp/"]
# RUN dotnet restore "shopapp/shopapp.csproj"
COPY *.csproj ./
RUN dotnet restore
COPY . .
# WORKDIR "/src/shopapp"
RUN dotnet build  -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "shopapp.dll"]
