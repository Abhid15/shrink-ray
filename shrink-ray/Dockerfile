FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["shrink-ray/shrink-ray/shrink-ray.csproj", "shrink-ray"]
RUN dotnet restore "shrink-ray/shrink-ray.csproj"
COPY . .
WORKDIR "/src/shrink-ray"
RUN dotnet build "shrink-ray.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "shrink-ray.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "shrink-ray.dll"]
