@echo off
@setlocal
set ERROR_CODE=0

echo Building Dint projects..

dotnet restore src\Dint.sln
if not %ERRORLEVEL%==0  (
    echo Error restoring NuGet packages for Dint.sln.
    set ERROR_CODE=1
    goto End
)

dotnet build src\Dint.CLI\Dint.CLI.csproj /p:Configuration=Release
if not %ERRORLEVEL%==0  (
    echo Error building Dint projects.
    set ERROR_CODE=2
    goto End
)

cd ..\..\
echo Building Dint projects complete.

:End
@endlocal
exit /B %ERROR_CODE%

