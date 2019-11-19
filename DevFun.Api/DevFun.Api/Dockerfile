FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src

ARG PAT
RUN wget -qO- https://raw.githubusercontent.com/Microsoft/artifacts-credprovider/master/helpers/installcredprovider.sh | bash
ENV NUGET_CREDENTIALPROVIDER_SESSIONTOKENCACHE_ENABLED true
ENV VSS_NUGET_EXTERNAL_FEED_ENDPOINTS "{\"endpointCredentials\": [{\"endpoint\":\"https://4tecture.pkgs.visualstudio.com/_packaging/Products/nuget/v3/index.json\", \"password\":\"${PAT}\"}]} "

COPY ["DevFun.Api/DevFun.Api.csproj", "DevFun.Api/"]
COPY ["DevFun.Logic/DevFun.Logic.csproj", "DevFun.Logic/"]
COPY ["DevFun.Common/DevFun.Common.csproj", "DevFun.Common/"]
COPY ["DevFun.Storage/DevFun.Storage.csproj", "DevFun.Storage/"]
COPY ["DevFun.Logic.Unit.Tests/DevFun.Logic.Unit.Tests.csproj", "DevFun.Logic.Unit.Tests/"]
RUN dotnet restore -s "https://4tecture.pkgs.visualstudio.com/_packaging/Products/nuget/v3/index.json" -s "https://api.nuget.org/v3/index.json" "DevFun.Api/DevFun.Api.csproj"
RUN dotnet restore -s "https://4tecture.pkgs.visualstudio.com/_packaging/Products/nuget/v3/index.json" -s "https://api.nuget.org/v3/index.json" "DevFun.Logic.Unit.Tests/DevFun.Logic.Unit.Tests.csproj"
COPY . .
WORKDIR "/src/DevFun.Api"
RUN dotnet build "DevFun.Api.csproj" -c Release -o /app/build

FROM build AS test
# use the label to identity this layer later
LABEL test=true
# install the report generator tool
#RUN dotnet tool install dotnet-reportgenerator-globaltool --version 4.0.6 --tool-path /tools --configfile ../nuget.official-only.config
# run the test and collect code coverage (requires coverlet.msbuild to be added to test project)
# for exclude, use %2c for ,
RUN dotnet test --results-directory /testresults --logger "trx;LogFileName=test_results.trx" /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput=/testresults/coverage/ ../DevFun.Logic.Unit.Tests/DevFun.Logic.Unit.Tests.csproj
# generate html reports using report generator tool
#RUN /tools/reportgenerator "-reports:/testresults/coverage/coverage.cobertura.xml" "-targetdir:/testresults/coverage/reports" "-reporttypes:HTMLInline;HTMLChart"

FROM build AS publish
RUN dotnet publish "DevFun.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DevFun.Api.dll"]