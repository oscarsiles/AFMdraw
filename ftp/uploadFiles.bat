REM @echo off
REM read parameters from text file
for /f "usebackq tokens=1,2,3 delims=," %%1 in (parameters.txt) do (
	set server=%%1
	set username=%%2
	set password=%%3
)

echo user %username%> ftpcmd.dat
echo %password%>> ftpcmd.dat
echo bin>> ftpcmd.dat
echo put .\..\bin\Release\AFMdraw.exe>> ftpcmd.dat
echo cd updater>> ftpcmd.dat
echo put .\updater\autoupdater.xml>> ftpcmd.dat
echo put .\updater\releasenotes.html>> ftpcmd.dat
echo quit>> ftpcmd.dat
ftp -n -s:ftpcmd.dat %server%
del ftpcmd.dat