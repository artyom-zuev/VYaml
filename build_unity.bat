@echo off
rem Builds the three DLLs a Unity project needs (runtime, annotations, source generator) in Release, collects them in
rem bin\unity and opens that folder. Copy them into the project's plugin folder from there; the source generator DLL
rem needs the RoslynAnalyzer label and no platforms in its import settings.
setlocal
cd /d "%~dp0"
set OUT=%~dp0bin\unity

if exist "%OUT%" rmdir /s /q "%OUT%"
mkdir "%OUT%"

dotnet build VYaml\VYaml.csproj -c Release -f netstandard2.1 -nologo -v q || goto :failed
dotnet build VYaml.Annotations\VYaml.Annotations.csproj -c Release -f netstandard2.1 -nologo -v q || goto :failed

rem The generator's Release build also copies itself into VYaml.Unity; that copy is pointed into bin to keep the tree clean
dotnet build VYaml.SourceGenerator\VYaml.SourceGenerator.csproj -c Release -nologo -v q "-p:UnityAssetsRoot=%OUT%\generator_copy" || goto :failed

copy /y VYaml\bin\Release\netstandard2.1\VYaml.dll "%OUT%" >nul || goto :failed
copy /y VYaml.Annotations\bin\Release\netstandard2.1\VYaml.Annotations.dll "%OUT%" >nul || goto :failed
copy /y VYaml.SourceGenerator\bin\Release\netstandard2.0\VYaml.SourceGenerator.dll "%OUT%" >nul || goto :failed
rmdir /s /q "%OUT%\generator_copy" 2>nul

echo Built to %OUT%
start "" explorer "%OUT%"
exit /b 0

:failed
echo Build failed
pause
exit /b 1
