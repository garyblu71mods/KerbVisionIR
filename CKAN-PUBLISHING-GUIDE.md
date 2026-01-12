# Instrukcje publikacji na CKAN

## Przygotowanie

### 1. Niezb?dne pliki (? Wszystkie gotowe)

- ? `LICENSE` - Licencja MIT
- ? `GameData/KerbVisionIR/KerbVisionIR.version` - Plik wersji (AVC format)
- ? `KerbVisionIR.netkan` - Automatyzacja CKAN
- ? `README.md` - Dokumentacja

## Proces publikacji

### Krok 1: Zbuduj release package

```powershell
.\MakeRelease.ps1 -Version "0.5.0"
```

Ten skrypt:
- Buduje projekt w trybie Release  
- Tworzy struktur? katalogów zgodn? z KSP
- Kopiuje wszystkie potrzebne pliki (DLL, shadery, version, dokumentacj?)
- Tworzy plik ZIP gotowy do publikacji
- Generuje SHA256 hash dla CKAN

### Krok 2: Przetestuj lokalnie

1. Wypakuj utworzony ZIP do katalogu testowego
2. Skopiuj zawarto?? do `KSP/GameData/`
3. Uruchom KSP i przetestuj wszystkie funkcje
4. Sprawd? czy:
   - Mod si? ?aduje bez b??dów
   - Wszystkie funkcje dzia?aj?
   - Nie ma konfliktów z innymi modami

### Krok 3: Stwórz GitHub Release

1. Id? do: https://github.com/garyblu71mods/KerbVisionIR/releases/new

2. Wype?nij formularz:
   - **Tag version**: `v0.5.0` (WA?NE: musi zaczyna? si? od 'v')
   - **Release title**: `KerbVision IR v0.5.0`
   - **Description**: Skopiuj changelog z README.md

3. Za??cz pliki:
   - Upload `KerbVisionIR-0.5.0.zip`

4. Zaznacz opcje:
   - [ ] Set as a pre-release (je?li wersja beta/RC)
   - [x] Set as the latest release

5. Kliknij **Publish release**

### Krok 4: Fork i przygotuj CKAN-meta

1. Id? do: https://github.com/KSP-CKAN/CKAN-meta
2. Kliknij **Fork** (je?li nie masz ju? forka)
3. Sklonuj swój fork lokalnie:

```bash
git clone https://github.com/garyblu71mods/CKAN-meta.git
cd CKAN-meta
```

### Krok 5: Dodaj .netkan do NetKAN

1. Fork repozytorium NetKAN: https://github.com/KSP-CKAN/NetKAN
2. Sklonuj swój fork:

```bash
git clone https://github.com/garyblu71mods/NetKAN.git
cd NetKAN
```

3. Utwórz branch:

```bash
git checkout -b add-kerbvisionir
```

4. Skopiuj plik `KerbVisionIR.netkan` do katalogu `NetKAN/`:

```bash
cp ../KerbVisionIR/KerbVisionIR.netkan NetKAN/KerbVisionIR.netkan
```

5. Commit i push:

```bash
git add NetKAN/KerbVisionIR.netkan
git commit -m "Add KerbVisionIR - Night Vision mod"
git push origin add-kerbvisionir
```

### Krok 6: Stwórz Pull Request do NetKAN

1. Id? do: https://github.com/KSP-CKAN/NetKAN/pulls
2. Kliknij **New pull request**
3. Wybierz swój branch `add-kerbvisionir`
4. Wype?nij opis PR:

```markdown
# Add KerbVisionIR - Night Vision mod

## Mod Information
- **Name**: KerbVision IR - Night Vision
- **Version**: 0.5.0
- **Author**: garyblu71mods
- **License**: MIT
- **KSP Version**: 1.8.0 - 1.12.99

## Description
Night Vision mod for Kerbal Space Program with old-school CRT artifacts.

Features:
- 3 Vision Modes: Monochrome, Green NV, Amber/Warm
- Adjustable Settings: Brightness, Contrast, Color Tint, Grain
- CRT Artifacts: Scanlines, phosphor spots, rolling bars
- Audio feedback

## Links
- Repository: https://github.com/garyblu71mods/KerbVisionIR
- Release: https://github.com/garyblu71mods/KerbVisionIR/releases/tag/v0.5.0

## Checklist
- [x] .netkan file follows CKAN spec v1.4
- [x] GitHub release exists with proper tag
- [x] .version file included in mod
- [x] LICENSE file included
- [x] Tested locally
```

5. Kliknij **Create pull request**

### Krok 7: Poczekaj na walidacj?

1. Bot CKAN automatycznie zwaliduje twój .netkan
2. Je?li s? b??dy, popraw je w swoim branchu
3. Gdy walidacja przejdzie, maintainerzy CKAN zaakceptuj? PR
4. Po merge, bot automatycznie wygeneruje .ckan i doda do CKAN-meta

### Krok 8: Weryfikacja

Po oko?o 30-60 minutach od merge:

1. Otwórz CKAN client
2. Od?wie? list? modów (Refresh)
3. Wyszukaj "KerbVision" lub "Night Vision"
4. Twój mod powinien by? dost?pny do instalacji!

## ?? Przysz?e aktualizacje

Dla kolejnych wersji wystarczy:
1. Zaktualizuj numer wersji w `GameData/KerbVisionIR/KerbVisionIR.version`
2. Zaktualizuj CHANGELOG.md
3. Uruchom `.\MakeRelease.ps1 -Version "x.y.z"` z now? wersj?
4. Stwórz GitHub Release
5. **CKAN automatycznie wykryje aktualizacj?!**

Nie musisz ju? tworzy? PR - bot NetKAN sprawdza GitHub releases co godzin?.

## Wsparcie

Je?li masz problemy:

1. Dokumentacja CKAN: https://github.com/KSP-CKAN/CKAN/wiki
2. Discord CKAN: https://discord.gg/HYGz4Qv
3. Forum KSP: https://forum.kerbalspaceprogram.com/

## Przydatne linki

- CKAN Metadata Spec: https://github.com/KSP-CKAN/CKAN/blob/master/Spec.md
- NetKAN Spec: https://github.com/KSP-CKAN/CKAN/wiki/NetKAN.md
- Walidator .netkan: https://netkan.ksp-ckan.space/
