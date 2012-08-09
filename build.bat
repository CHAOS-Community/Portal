@echo off

rem Needed to update variable in loop
rem setlocal enabledelayedexpansion

rem for %%i in (bin\IndexExtension\*.dll) do (set files=!files!%%~i )

rem echo Merging IndexExtension

rem tools\ILMerge\ILMerge.exe /out:build\CHAOS.Portal.IndexExtension.dll /lib:C:\Windows\Microsoft.NET\Framework64\v4.0.30319 /targetplatform:v4,C:\Windows\Microsoft.NET\Framework64\v4.0.30319 /lib:lib\ %files%

rem echo Done

tools\nant-0.92\bin\nant.exe -buildfile:build\Portal.build