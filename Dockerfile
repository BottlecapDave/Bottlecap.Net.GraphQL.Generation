FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /app

# Install coverlet
ENV PATH="$PATH:/root/.dotnet/tools"
RUN dotnet tool install --global coverlet.console

# Copy csproj and restore as distinct layers
COPY src/Bottlecap.Net.GraphQL.Generation/*.csproj ./Bottlecap.Net.GraphQL.Generation/
COPY src/Bottlecap.Net.GraphQL.Generation.Attributes/*.csproj ./Bottlecap.Net.GraphQL.Generation.Attributes/
COPY src/Bottlecap.Net.GraphQL.Generation.Cli/*.csproj ./Bottlecap.Net.GraphQL.Generation.Cli/

COPY src/Tests/UnitTests.Bottlecap.Net.GraphQL.Generation/*.csproj ./Tests/UnitTests.Bottlecap.Net.GraphQL.Generation/
COPY src/Tests/IntegrationTests.Bottlecap.Net.GraphQL.Generation/*.csproj ./Tests/IntegrationTests.Bottlecap.Net.GraphQL.Generation/
COPY src/Tests/IntegrationTests.Bottlecap.Net.GraphQL.Generation.Support/*.csproj ./Tests/IntegrationTests.Bottlecap.Net.GraphQL.Generation.Support/

COPY src/Examples/GraphQLExample/*.csproj ./Examples/GraphQLExample/
COPY src/Examples/GraphQLExample.Data/*.csproj ./Examples/GraphQLExample.Data/

COPY src/*.sln ./
RUN dotnet restore

COPY src/Bottlecap.Net.GraphQL.Generation/ ./Bottlecap.Net.GraphQL.Generation/
COPY src/Bottlecap.Net.GraphQL.Generation.Attributes/ ./Bottlecap.Net.GraphQL.Generation.Attributes/
COPY src/Bottlecap.Net.GraphQL.Generation.Cli/ ./Bottlecap.Net.GraphQL.Generation.Cli/

COPY src/Tests/UnitTests.Bottlecap.Net.GraphQL.Generation/ ./Tests/UnitTests.Bottlecap.Net.GraphQL.Generation/
COPY src/Tests/IntegrationTests.Bottlecap.Net.GraphQL.Generation/ ./Tests/IntegrationTests.Bottlecap.Net.GraphQL.Generation/
COPY src/Tests/IntegrationTests.Bottlecap.Net.GraphQL.Generation.Support/ ./Tests/IntegrationTests.Bottlecap.Net.GraphQL.Generation.Support/

COPY src/Examples/GraphQLExample/ ./Examples/GraphQLExample/
COPY src/Examples/GraphQLExample.Data/ ./Examples/GraphQLExample.Data/

# Execute unit tests
RUN dotnet test ./Tests/UnitTests.Bottlecap.Net.GraphQL.Generation/UnitTests.Bottlecap.Net.GraphQL.Generation.csproj /p:CollectCoverage=true /p:CoverletOutput="../result/codecoverage/coverage.json"
RUN dotnet test ./Tests/IntegrationTests.Bottlecap.Net.GraphQL.Generation/IntegrationTests.Bottlecap.Net.GraphQL.Generation.csproj /p:CollectCoverage=true /p:CoverletOutput="../result/codecoverage/coverage.json" /p:MergeWith='../result/codecoverage/coverage.json'

# Define our environment variables so we can set our package information
ARG PACKAGE_VERSION
ARG NUGET_PACKAGE_API

# Build and pack attributes
RUN dotnet build ./Bottlecap.Net.GraphQL.Generation.Attributes/ -c Release -o ./Bottlecap.Net.GraphQL.Generation.Attributes/out /p:Version=$PACKAGE_VERSION
RUN dotnet pack ./Bottlecap.Net.GraphQL.Generation.Attributes/ -c Release -o ./Bottlecap.Net.GraphQL.Generation.Attributes/out /p:Version=$PACKAGE_VERSION

# Build and pack generator
RUN dotnet build ./Bottlecap.Net.GraphQL.Generation/ -c Release -o ./Bottlecap.Net.GraphQL.Generation/out /p:Version=$PACKAGE_VERSION
RUN dotnet pack ./Bottlecap.Net.GraphQL.Generation/ -c Release -o ./Bottlecap.Net.GraphQL.Generation/out /p:Version=$PACKAGE_VERSION

# Build and pack Cli
RUN dotnet build ./Bottlecap.Net.GraphQL.Generation.Cli/ -c Release -o ./Bottlecap.Net.GraphQL.Generation.Cli/out /p:Version=$PACKAGE_VERSION
RUN dotnet pack ./Bottlecap.Net.GraphQL.Generation.Cli/ -c Release -o ./Bottlecap.Net.GraphQL.Generation.Cli/out /p:Version=$PACKAGE_VERSION

# Push packages to nuget
RUN dotnet nuget push ./Bottlecap.Net.GraphQL.Generation/out/Bottlecap.Net.GraphQL.Generation.$PACKAGE_VERSION.nupkg -s https://api.nuget.org/v3/index.json -k $NUGET_PACKAGE_API
RUN dotnet nuget push ./Bottlecap.Net.GraphQL.Generation.Attributes/out/Bottlecap.Net.GraphQL.Generation.Attributes.$PACKAGE_VERSION.nupkg -s https://api.nuget.org/v3/index.json -k $NUGET_PACKAGE_API
RUN dotnet nuget push ./Bottlecap.Net.GraphQL.Generation.Cli/out/Bottlecap.Net.GraphQL.Generation.Cli.$PACKAGE_VERSION.nupkg -s https://api.nuget.org/v3/index.json -k $NUGET_PACKAGE_API