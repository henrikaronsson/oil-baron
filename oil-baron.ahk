#Requires AutoHotkey v1.1
#NoEnv  ; Recommended for performance and compatibility with future AutoHotkey releases.
SendMode Input  ; Recommended for new scripts due to its superior speed and reliability.
SetWorkingDir %A_ScriptDir%  ; Ensures a consistent starting directory.

;	---==  oil-baron-web shell (Angular 20)  ==---
SetWorkingDir, C:\project\oil-baron\src\oil-baron-web\
Run, "C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe"

;	---==  OilBaron.Api shell (.NET)  ==---
SetWorkingDir, C:\project\oil-baron\src\OilBaron.Api\
Run, "C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe"

;	---== Open file explorer  ==---
Run, explorer.exe C:\project\oil-baron

;	---== MAP SHORTKEYS ==---
; oil-baron-web (Angular) – fokusera Angular-shellen
; ------------------------
F3::
Send npm start{Enter}
return

F4::
Send npm run build{Enter}
return

F6::
Run, http://localhost:4200
return

; OilBaron.Api – fokusera Api-shellen
; ------------------------
F5::
Send dotnet run --launch-profile http{Enter}
return

F7::
Run, http://localhost:5080/api/health
return
