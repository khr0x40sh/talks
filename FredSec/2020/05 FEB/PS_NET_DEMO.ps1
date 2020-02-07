$a = "C:\Users\ktolb\OneDrive\Desktop\TALK\talks-master\FredSec\2020\05 FEB\exposing_demo\exposing_demo\bin\Release\exposing_demo.exe"

$bytes = [System.IO.File]::ReadAllBytes($a)
[System.Reflection.Assembly]::Load($bytes)
[exposing_demo.Program]::Main({""})
#[System.Reflection.Assembly]::Load($a)


