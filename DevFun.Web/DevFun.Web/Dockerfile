FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["DevFun.Web/DevFun.Web.csproj", "DevFun.Web/"]
RUN dotnet restore "DevFun.Web/DevFun.Web.csproj"
COPY . .
WORKDIR "/src/DevFun.Web"
RUN dotnet build "DevFun.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DevFun.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DevFun.Web.dll"]