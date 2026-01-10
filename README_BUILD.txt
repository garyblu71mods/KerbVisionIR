================================================================================
  KERBVISION IR - BUILD INSTRUCTIONS
================================================================================

QUICK START:
------------

1. Znajd? swoj? instalacj? Kerbal Space Program
   
2. Uruchom Build.ps1 z parametrem:
   
   powershell -ExecutionPolicy Bypass -File Build.ps1 -KSPPath "C:\Your\Path\To\Kerbal Space Program"
   
   Przyk?ad:
   powershell -ExecutionPolicy Bypass -File Build.ps1 -KSPPath "C:\Steam\steamapps\common\Kerbal Space Program"

3. Je?li build si? powiedzie, skopiuj folder GameData\KerbVisionIR do swojego KSP\GameData\

4. Uruchom KSP i naci?nij Alt+I w locie


ALTERNATYWNIE (Edycja .csproj):
---------------------------------

1. Otwórz KerbVisionIR.csproj w edytorze tekstu

2. Znajd? lini?:
   <KSPRoot Condition=" '$(KSPRoot)' == '' ">C:\Program Files (x86)\Steam\steamapps\common\Kerbal Space Program</KSPRoot>

3. Zmie? ?cie?k? na swoj? instalacj? KSP

4. Zapisz plik

5. Uruchom:
   msbuild KerbVisionIR.csproj /p:Configuration=Release


TYPOWE LOKALIZACJE KSP:
-------------------------

Steam (Windows):
- C:\Program Files (x86)\Steam\steamapps\common\Kerbal Space Program
- D:\SteamLibrary\steamapps\common\Kerbal Space Program

GOG:
- C:\GOG Games\Kerbal Space Program

Custom:
- Sprawd? w w?a?ciwo?ciach skrótu do KSP


WYMAGANIA:
-----------

- Kerbal Space Program 1.8 lub nowszy zainstalowany
- Visual Studio 2019+ LUB MSBuild
- .NET Framework 4.8 SDK


ROZWI?ZYWANIE PROBLEMÓW:
-------------------------

B??d: "Could not find KSP installation"
? Podaj ?cie?k? do KSP r?cznie (zobacz QUICK START)

B??d: "Assembly-CSharp.dll not found"
? Sprawd? czy KSP jest zainstalowany i ?cie?ka jest poprawna

B??d: "msbuild not found"
? Zainstaluj Visual Studio lub Build Tools for Visual Studio


TESTOWANIE:
------------

Po zbudowaniu:
1. Skopiuj GameData\KerbVisionIR do KSP\GameData\
2. Uruchom KSP
3. Wejd? w lot (Flight)
4. Naci?nij Alt+I aby w??czy?/wy??czy? efekt
5. Kliknij zielon? ikon? kamery w toolbarze aby otworzy? ustawienia


POMOC:
-------

Je?li masz problemy, sprawd?:
- BUILDING.md - szczegó?owe instrukcje budowania
- FAQ.md - cz?sto zadawane pytania
- TECHNICAL.md - dokumentacja techniczna

================================================================================
