# ?? PODSUMOWANIE - KerbVisionIR gotowy na CKAN!

## ? Co zosta?o przygotowane:

### 1. Pliki CKAN
- **KerbVisionIR.netkan** - Automatyzacja dla CKAN bot'a
- **GameData/KerbVisionIR/KerbVisionIR.version** - Zaktualizowany do v0.5.0
- **LICENSE** - MIT License (wymagane przez CKAN)

### 2. Dokumentacja
- **CKAN-PUBLISHING-GUIDE.md** - Szczegó?owe instrukcje krok po kroku
- **CKAN-CHECKLIST.md** - Szybka checklist do odznaczania
- **CHANGELOG.md** - Historia zmian (best practice)
- **README.md** - Zaktualizowany z informacj? o CKAN

### 3. Skrypty budowania
- **MakeRelease.ps1** - Zaktualizowany z SHA256 i instrukcjami CKAN

---

## ?? CO TERAZ ZROBI?:

### Opcja A: Szybki start (5 minut)

```powershell
# 1. Zbuduj release
.\MakeRelease.ps1 -Version "0.5.0"

# 2. Sprawd? czy dzia?a
# Wypakuj ZIP z Release\ do testowego KSP i przetestuj

# 3. Push do GitHub
git add .
git commit -m "Prepare v0.5.0 for CKAN release"
git push

# 4. Stwórz GitHub Release na https://github.com/garyblu71mods/KerbVisionIR/releases/new
#    - Tag: v0.5.0
#    - Upload: KerbVisionIR-v0.5.0.zip
```

### Opcja B: Pe?ny proces (przeczytaj dokumentacj?)

Otwórz i przeczytaj:
1. **CKAN-CHECKLIST.md** - dla szybkiego overview
2. **CKAN-PUBLISHING-GUIDE.md** - dla szczegó?owych kroków

---

## ?? Najwa?niejsze informacje:

### Struktura .netkan
```json
{
  "identifier": "KerbVisionIR",
  "$kref": "#/ckan/github/garyblu71mods/KerbVisionIR",
  "$vref": "#/ckan/ksp-avc",
  "install": [
    {
      "find": "KerbVisionIR",
      "install_to": "GameData"
    }
  ]
}
```

**Co to znaczy:**
- `$kref` - CKAN automatycznie pobiera info z GitHub releases
- `$vref` - CKAN automatycznie czyta wersj? z .version file
- `install` - Mówi CKAN jak zainstalowa? mod

### GitHub Release MUSI:
- Mie? tag zaczynaj?cy si? od `v` (np. `v0.5.0`)
- Zawiera? ZIP z folderem GameData/KerbVisionIR/

### Po pierwszej publikacji:
CKAN bot sprawdza GitHub co godzin? - nowe wersje automatycznie si? pojawi?!

---

## ?? Wa?ne linki:

- **Twoje repo**: https://github.com/garyblu71mods/KerbVisionIR
- **NetKAN repo**: https://github.com/KSP-CKAN/NetKAN
- **CKAN-meta repo**: https://github.com/KSP-CKAN/CKAN-meta
- **CKAN Discord**: https://discord.gg/HYGz4Qv

---

## ? Najcz?stsze problemy:

**Q: Bot CKAN odrzuci? mój .netkan**
A: Sprawd? czy:
- GitHub release ma tag zaczynaj?cy si? od `v`
- ZIP zawiera folder GameData/KerbVisionIR/
- Plik .version jest w GameData/KerbVisionIR/

**Q: Mod nie pojawia si? w CKAN**
A: Poczekaj 30-60 minut po merge PR do NetKAN

**Q: Jak zaktualizowa? do nowej wersji?**
A: 
1. Zmie? wersj? w GameData/KerbVisionIR/KerbVisionIR.version
2. .\MakeRelease.ps1 -Version "nowa_wersja"
3. Stwórz GitHub Release
4. CKAN automatycznie wykryje!

---

## ?? Gotowe!

Masz wszystko czego potrzebujesz. Otwórz **CKAN-CHECKLIST.md** i wykonaj kroki po kolei.

Powodzenia! ??
