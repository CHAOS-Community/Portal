@echo off

rem Needed to update variable in loop
setlocal enabledelayedexpansion

for %%i in (.\bin\Portal\*.dll) do (set files=!files!%%~i )

echo Merging Portal SDK

tools\ILMerge\ILMerge.exe /out:build\Portal.dll /lib:C:\Windows\Microsoft.NET\Framework64\v4.0.30319 /targetplatform:v4,C:\Windows\Microsoft.NET\Framework64\v4.0.30319 /lib:"C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET MVC 3\Assemblies" /lib:lib\ %files%

set files=

for %%i in (.\bin\PortalTest\*.dll) do (set files=!files!%%~i )

echo Merging Portal Test SDK

tools\ILMerge\ILMerge.exe /out:build\PortalTest.dll /lib:build /targetplatform:v4,C:\Windows\Microsoft.NET\Framework64\v4.0.30319 /lib:tools\nunit\ /lib:lib\ %files%

echo Done

pause