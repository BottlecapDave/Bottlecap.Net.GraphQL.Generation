FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY src/Bottlecap.Net.GraphQL.Generation/*.csproj ./Bottlecap.Net.GraphQL.Generation/
COPY src/Bottlecap.Net.GraphQL.Generation.Attributes/*.csproj ./Bottlecap.Net.GraphQL.Generation.Attributes/
COPY src/Bottlecap.Net.GraphQL.Generation.Cli/*.csproj ./Bottlecap.Net.GraphQL.Generation.Cli/

COPY src/Tests/UnitTests.Bottlecap.Net.GraphQL.Generation/*.csproj ./Tests/UnitTests.Bottlecap.Net.GraphQL.Generation/

COPY src/Examples/GraphQLExample/*.csproj ./Examples/GraphQLExample/
COPY src/Examples/GraphQLExample.Data/*.csproj ./Examples/GraphQLExample.Data/

COPY src/*.sln ./
RUN dotnet restore

# Define our environment variables so we can set our package information
ARG PACKAGE_VERSION
ARG NUGET_PACKAGE_API
ARG MYGET_PACKAGE_API

# Copy attributes and build
COPY src/Bottlecap.Net.GraphQL.Generation.Attributes/ ./Bottlecap.Net.GraphQL.Generation.Attributes/
RUN dotnet build ./Bottlecap.Net.GraphQL.Generation.Attributes/ -c Release -o out /p:Version=$PACKAGE_VERSION

# Pack attributes
RUN dotnet pack ./Bottlecap.Net.GraphQL.Generation.Attributes/ -c Release -o out /p:Version=$PACKAGE_VERSION

# Copy generator and build
COPY src/Bottlecap.Net.GraphQL.Generation/ ./Bottlecap.Net.GraphQL.Generation/
RUN dotnet build ./Bottlecap.Net.GraphQL.Generation/ -c Release -o out /p:Version=$PACKAGE_VERSION

# Pack generator
RUN dotnet pack ./Bottlecap.Net.GraphQL.Generation/ -c Release -o out /p:Version=$PACKAGE_VERSION

# Copy Cli and build
COPY src/Bottlecap.Net.GraphQL.Generation.Cli/ ./Bottlecap.Net.GraphQL.Generation.Cli/
RUN dotnet build ./Bottlecap.Net.GraphQL.Generation.Cli/ -c Release -o out /p:Version=$PACKAGE_VERSION

# Pack Cli
RUN dotnet pack ./Bottlecap.Net.GraphQL.Generation.Cli/ -c Release -o out /p:Version=$PACKAGE_VERSION

# Push packages to nuget
RUN dotnet nuget push ./Bottlecap.Net.GraphQL.Generation/out/Bottlecap.Net.GraphQL.Generation.$PACKAGE_VERSION.nupkg -s https://api.nuget.org/v3/index.json -k $NUGET_PACKAGE_API
RUN dotnet nuget push ./Bottlecap.Net.GraphQL.Generation.Attributes/out/Bottlecap.Net.GraphQL.Generation.Attributes.$PACKAGE_VERSION.nupkg -s https://api.nuget.org/v3/index.json -k $NUGET_PACKAGE_API
RUN dotnet nuget push ./Bottlecap.Net.GraphQL.Generation.Cli/out/Bottlecap.Net.GraphQL.Generation.Cli.$PACKAGE_VERSION.nupkg -s https://api.nuget.org/v3/index.json -k $NUGET_PACKAGE_API

# RUN dotnet nuget push ./Bottlecap.Net.GraphQL.Generation/out/Bottlecap.Net.GraphQL.Generation.$PACKAGE_VERSION.nupkg -s https://www.myget.org/F/bottlecap-net/api/v2/package -k $MYGET_PACKAGE_API
# RUN dotnet nuget push ./Bottlecap.Net.GraphQL.Generation.Attributes/out/Bottlecap.Net.GraphQL.Generation.Attributes.$PACKAGE_VERSION.nupkg -s https://www.myget.org/F/bottlecap-net/api/v2/package -k $MYGET_PACKAGE_API
# RUN dotnet nuget push ./Bottlecap.Net.GraphQL.Generation.Cli/out/Bottlecap.Net.GraphQL.Generation.Cli.$PACKAGE_VERSION.nupkg -s https://www.myget.org/F/bottlecap-net/api/v2/package  -k $MYGET_PACKAGE_API