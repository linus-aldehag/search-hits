@echo off
cd api
dotnet build
cd ..\client
npm install
pause