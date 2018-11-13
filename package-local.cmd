dotnet restore ./src --force

dotnet build ./src/Bottlecap.Net.GraphQL.Generation.Attributes/ -c Release -o out /p:Version=0.6.0-alpha
dotnet pack ./src/Bottlecap.Net.GraphQL.Generation.Attributes/ -c Release -o out /p:Version=0.6.0-alpha

dotnet build ./src/Bottlecap.Net.GraphQL.Generation/ -c Release -o out /p:Version=0.6.0-alpha
dotnet pack ./src/Bottlecap.Net.GraphQL.Generation/ -c Release -o out /p:Version=0.6.0-alpha

dotnet build ./src/Bottlecap.Net.GraphQL.Generation.Console/ -c Release -o out /p:Version=0.6.0-alpha
dotnet pack ./src/Bottlecap.Net.GraphQL.Generation.Console/ -c Release -o out /p:Version=0.6.0-alpha

XCOPY "src/Bottlecap.Net.GraphQL.Generation.Attributes/out" "C:\nuget.local" /s
XCOPY "src/Bottlecap.Net.GraphQL.Generation/out" "C:\nuget.local" /s
XCOPY "src/Bottlecap.Net.GraphQL.Generation.Console/out" "C:\nuget.local" /s