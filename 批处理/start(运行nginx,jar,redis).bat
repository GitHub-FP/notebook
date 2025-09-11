@echo off

REM ---
taskkill /f /t /im nginx.exe
cd /d C:\inetpub\nginx-1.24.0
start "" nginx.exe

REM ---
cd /d C:\inetpub\nginx-1.24.0\xdf\service
start "" java -jar biportal-module-system-0.0.1-SNAPSHOT.jar

REM ---
cd /d C:\inetpub\nginx-1.24.0\Redis-7.4.2-Windows-x64-cygwin-with-Service
start "" start.bat