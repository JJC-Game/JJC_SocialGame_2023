@ECHO OFF
SET BAT_PATH=%~dp0
SET POWERSHELL_PATH=%BAT_PATH%ConvertInGameText_SkillName.ps1
PowerShell -Command "Set-ExecutionPolicy RemoteSigned -Scope CurrentUser;"
ECHO %POWERSHELL_PATH%Çé¿çsÇµÇ‹Ç∑.
PowerShell -File %POWERSHELL_PATH%
pause