# File Monitoring Service
A Windows service that monitors a source folder. When a file is added to the source folder, its name is replaced with a GUID, and it is moved to the destination folder.
# Build Instructions (Release Mode):
1. Save the source code.
2. Build the solution (ctrl + shift + b).
# Deployment Instructions:
## Installation (Using InstallUtil):
1. Open the solution's folder in file explorer.
2. Open bin -> release -> copy file path **(service's file path)**.
3. Open the command prompt in **administrator mode**.
4. Change the current directory to the **service's file path**.
5. Use the following command:
 -  for 64-bit systems: C:\Windows\Microsoft.NET\Framework64\v4.0.30319\InstallUtil.exe service'sFilePath.exe
 -  for 32-bit systems: C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe service'sFilePath.exe.


