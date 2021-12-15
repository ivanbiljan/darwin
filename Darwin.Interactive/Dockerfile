FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Darwin.Interactive/Darwin.Interactive.csproj", "Darwin.Interactive/"]
RUN dotnet restore "Darwin.Interactive/Darwin.Interactive.csproj"
COPY . .
WORKDIR "/src/Darwin.Interactive"
RUN dotnet build "Darwin.Interactive.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Darwin.Interactive.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Darwin.Interactive.dll"]
