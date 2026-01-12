# KerbVisionIR - CKAN Checklist

## ? Przygotowane pliki

- [x] LICENSE (MIT)
- [x] README.md (z instrukcj? CKAN)
- [x] GameData/KerbVisionIR/KerbVisionIR.version (v0.5.0)
- [x] KerbVisionIR.netkan (dla automatyzacji)
- [x] build-release.ps1 (skrypt budowania)
- [x] CKAN-PUBLISHING-GUIDE.md (szczegó?owe instrukcje)

## ?? Nast?pne kroki (wykonaj po kolei)

### 1. Zbuduj release package
```powershell
.\MakeRelease.ps1 -Version "0.5.0"
```
- [ ] Sprawd? czy ZIP zosta? utworzony w katalogu Release/
- [ ] Zapisz SHA256 hash (wy?wietlony po buildzie)
- [ ] Nazwa ZIP powinna by?: KerbVisionIR-v0.5.0.zip

### 2. Przetestuj lokalnie
- [ ] Wypakuj ZIP do testowego KSP
- [ ] Uruchom gr? i sprawd? czy mod dzia?a
- [ ] Przetestuj wszystkie 3 tryby wizji
- [ ] Sprawd? czy ustawienia si? zapisuj?
- [ ] Sprawd? hotkey (Alt + `)

### 3. Commit i push do GitHub
```bash
git add .
git commit -m "Prepare v0.5.0 for CKAN release"
git push origin main
```

### 4. Stwórz GitHub Release
- [ ] Id? do: https://github.com/garyblu71mods/KerbVisionIR/releases/new
- [ ] Tag: `v0.5.0`
- [ ] Title: `KerbVision IR v0.5.0`
- [ ] Description: Skopiuj features z README
- [ ] Upload: `KerbVisionIR-0.5.0.zip`
- [ ] Publish release

### 5. Fork NetKAN repo
- [ ] Fork: https://github.com/KSP-CKAN/NetKAN
- [ ] Clone lokalnie: `git clone https://github.com/garyblu71mods/NetKAN.git`

### 6. Dodaj .netkan
```bash
cd NetKAN
git checkout -b add-kerbvisionir
cp ../KerbVisionIR/KerbVisionIR.netkan NetKAN/KerbVisionIR.netkan
git add NetKAN/KerbVisionIR.netkan
git commit -m "Add KerbVisionIR - Night Vision mod"
git push origin add-kerbvisionir
```

### 7. Stwórz Pull Request
- [ ] Id? do: https://github.com/KSP-CKAN/NetKAN/pulls
- [ ] Kliknij "New pull request"
- [ ] Compare: `add-kerbvisionir`
- [ ] Wype?nij opis (u?yj template z CKAN-PUBLISHING-GUIDE.md)
- [ ] Submit PR

### 8. Poczekaj na walidacj?
- [ ] Bot CKAN sprawdzi .netkan (5-10 minut)
- [ ] Je?li s? b??dy - popraw i push do tego samego brancha
- [ ] Maintainer zaakceptuje PR
- [ ] Bot automatycznie doda mod do CKAN-meta

### 9. Sprawd? w CKAN client
- [ ] Otwórz CKAN (30-60 min po merge)
- [ ] Refresh list
- [ ] Wyszukaj "KerbVision"
- [ ] Mod powinien by? dost?pny! ??

## ?? Przysz?e aktualizacje

Dla kolejnych wersji wystarczy:
1. Zaktualizuj numer wersji w `GameData/KerbVisionIR/KerbVisionIR.version`
2. Zaktualizuj CHANGELOG.md
3. Uruchom `.\MakeRelease.ps1 -Version "x.y.z"` z now? wersj?
4. Stwórz GitHub Release
5. **CKAN automatycznie wykryje aktualizacj?!**

## ?? Pomoc

Je?li co? nie dzia?a:
- Discord CKAN: https://discord.gg/HYGz4Qv
- Forum KSP: https://forum.kerbalspaceprogram.com/
- CKAN Wiki: https://github.com/KSP-CKAN/CKAN/wiki
