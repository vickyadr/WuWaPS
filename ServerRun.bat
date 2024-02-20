@echo off
Title Wuthering Waves PS Build and Run Program

:: Check if .NET 8.0 SDK has been installed or not
for /f "delims=" %%a in ('dotnet --version 2^>^&1') do (
    set "dotnetVersion=%%a"
    echo Found .NET SDK version %%a
)

if not defined dotnetVersion (
    echo .NET SDK version 8 is not installed.
    echo Please download and install it, and try again...
	
    :: Go to .NET 8.0 SDK download page in the default browser
    start https://dotnet.microsoft.com/download/dotnet/8.0
	
    pause
    exit /b
)

:: Check if the found version is equal to "8."
if not "%dotnetVersion:~0,2%"=="8." (
    echo Required .NET SDK version 8.
    echo Please install the correct version and try again.
    
    pause
    exit /b
)

:: Building Wuwa server
if not exist "%~dp0GameServer\bin\Debug\net8.0\" (
    echo Building the GameServer application...
    powershell dotnet build
) else if not exist "%~dp0SDKServer\bin\Debug\net8.0\" (
    echo Building the SDKServer application...
    powershell dotnet build
) else (
    echo Applications already built. Skipping build process.
)

:: Running Wuwa server
cd /d "%~dp0GameServer\bin\Debug\net8.0" && start GameServer.exe
cd /d "%~dp0SDKServer\bin\Debug\net8.0" && start SDKServer.exe"