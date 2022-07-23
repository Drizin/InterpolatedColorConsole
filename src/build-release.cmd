if not exist packages-local mkdir packages-local

dotnet clean
dotnet build -c release InterpolatedColorConsole\InterpolatedColorConsole.csproj

"C:\Program Files\Microsoft Visual Studio\2022\Professional\MSBuild\Current\Bin\MSBuild.exe" /t:Pack InterpolatedColorConsole\ /p:targetFrameworks="netstandard2.0;net472" /p:Configuration=Release /p:IncludeSymbols=true /p:SymbolPackageFormat=snupkg /p:PackageOutputPath=..\packages-local\ /verbosity:minimal /p:ContinuousIntegrationBuild=true