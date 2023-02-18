#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["UnlockablePostsAPI/UnlockablePostsAPI.csproj", "UnlockablePostsAPI/"]
RUN dotnet restore "UnlockablePostsAPI/UnlockablePostsAPI.csproj"
COPY . .
WORKDIR "/src/UnlockablePostsAPI"
RUN dotnet build "UnlockablePostsAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UnlockablePostsAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UnlockablePostsAPI.dll"]