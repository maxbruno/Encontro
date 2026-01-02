@echo off
set PHP_BIN=C:\xampp\php\php.exe



echo Setting up Environment variables for MySQL...
set DB_CONNECTION=mysql
set DB_HOST=127.0.0.1
set DB_PORT=3306
set DB_DATABASE=encontro
set DB_USERNAME=root
set "DB_PASSWORD==Katano+2007"

echo Verifying PHP...
%PHP_BIN% -v >nul 2>&1
if %errorlevel% neq 0 (
    echo PHP not found at %PHP_BIN%.
    exit /b
)

if not exist vendor (
    echo Installing dependencies...
    if not exist composer.phar copy ..\composer.phar composer.phar
    %PHP_BIN% composer.phar install --no-interaction
)

echo Generating Key...
%PHP_BIN% artisan key:generate --force

echo Running Migrations...
%PHP_BIN% artisan migrate --force

echo Linking Storage...
%PHP_BIN% artisan storage:link

echo Starting Server...
echo Access at http://127.0.0.1:8000
%PHP_BIN% artisan serve
