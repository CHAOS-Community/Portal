@echo off

rem Needed to update variable in loop
setlocal enabledelayedexpansion

for %%i in (.\bin\PortalTest\*.dll) do (set files=!files!%%~i )

echo Merging Portal Test SDK

tools\ILMerge\ILMerge.exe /out:build\PortalTest.dll /lib:build /targetplatform:v4,C:\Windows\Microsoft.NET\Framework64\v4.0.30319 /lib:tools\nunit\ /lib:lib\ %files%

echo Done

pause