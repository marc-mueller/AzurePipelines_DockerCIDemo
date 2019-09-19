FROM microsoft/dotnet:2.2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2.104-sdk AS build
WORKDIR /src
COPY ["DevFun.Api/DevFun.Api.csproj", "DevFun.Api/"]
COPY ["DevFun.Logic/DevFun.Logic.csproj", "DevFun.Logic/"]
COPY ["DevFun.Common/DevFun.Common.csproj", "DevFun.Common/"]
COPY ["DevFun.Storage/DevFun.Storage.csproj", "DevFun.Storage/"]
COPY ["DevFun.Logic.Unit.Tests/DevFun.Logic.Unit.Tests.csproj", "DevFun.Logic.Unit.Tests/"]
RUN dotnet restore "DevFun.Api/DevFun.Api.csproj"
RUN dotnet restore "DevFun.Logic.Unit.Tests/DevFun.Logic.Unit.Tests.csproj"
COPY . .
WORKDIR "/src/DevFun.Api"
RUN dotnet build "DevFun.Api.csproj" -c Release -o /app

FROM build AS test
# use the label to identity this layer later
LABEL test=true
# install the report generator tool
RUN dotnet tool install dotnet-reportgenerator-globaltool --version 4.0.6 --tool-path /tools --configfile ../nuget.official-only.config
# run the test and collect code coverage (requires coverlet.msbuild to be added to test project)
# for exclude, use %2c for ,
RUN dotnet test --results-directory /testresults --logger "trx;LogFileName=test_results.xml" /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput=/testresults/coverage/ ../DevFun.Logic.Unit.Tests/DevFun.Logic.Unit.Tests.csproj
# generate html reports using report generator tool
RUN /tools/reportgenerator "-reports:/testresults/coverage/coverage.cobertura.xml" "-targetdir:/testresults/coverage/reports" "-reporttypes:HTMLInline;HTMLChart"

FROM build AS publish
RUN dotnet publish "DevFun.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DevFun.Api.dll"]