@echo off

rem Needed to update variable in loop
setlocal enabledelayedexpansion

for %%i in (.\bin\Portal\*.dll) do (set files=!files!%%~i )

echo Merging Portal SDK

tools\ILMerge\ILMerge.exe /out:build\Portal.dll /lib:C:\Windows\Microsoft.NET\Framework64\v4.0.30319 /lib:tools\nunit\ /targetplatform:v4,C:\Windows\Microsoft.NET\Framework64\v4.0.30319 /lib:lib\ %files%

echo Done