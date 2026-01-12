# CKAN Validation Helper

## Szybka walidacja przed publikacj?

### 1. Sprawd? plik .netkan lokalnie

```powershell
# Sprawd? czy JSON jest poprawny
Get-Content .\KerbVisionIR.netkan | ConvertFrom-Json
```

Je?li nie ma b??dów - JSON jest poprawny! ?

### 2. Sprawd? plik .version

```powershell
# Sprawd? czy JSON jest poprawny
Get-Content .\GameData\KerbVisionIR\KerbVisionIR.version | ConvertFrom-Json
```

### 3. Sprawd? struktur? ZIP (po zbudowaniu)

```powershell
# Poka? zawarto?? ZIP
Add-Type -Assembly System.IO.Compression.FileSystem
$zip = [System.IO.Compression.ZipFile]::OpenRead(".\Release\KerbVisionIR-v0.5.0.zip")
$zip.Entries | Select-Object FullName
$zip.Dispose()
```

**Poprawna struktura ZIP powinna zawiera?:**
```
GameData/
  KerbVisionIR/
    Plugins/
      KerbVisionIR.dll
    Shaders/
      NightVision.shader
    PluginData/
      settings.cfg
    KerbVisionIR.version
README.md
CHANGELOG.md
LICENSE
```

### 4. Walidacja online

Po stworzeniu GitHub Release mo?esz sprawdzi? .netkan online:
https://netkan.ksp-ckan.space/

Wklej zawarto?? swojego .netkan i kliknij "Inflate"

### 5. Checklist przed PR do NetKAN

- [ ] GitHub Release istnieje
- [ ] Tag zaczyna si? od 'v' (np. v0.5.0)
- [ ] ZIP jest za??czony do release
- [ ] .version file ma poprawny URL do raw.githubusercontent.com
- [ ] .netkan ma poprawny JSON
- [ ] `identifier` jest unikalny (sprawd? na https://github.com/KSP-CKAN/CKAN-meta)
- [ ] `install` sekcja jest poprawna
- [ ] LICENSE istnieje w repo
- [ ] Mod dzia?a lokalnie po rozpakowaniu ZIP

### 6. Testowanie instalacji CKAN (opcjonalne)

Je?li chcesz przetestowa? przed oficjaln? publikacj?:

```bash
# Zainstaluj CKAN command line tool
# https://github.com/KSP-CKAN/CKAN/releases

# Inflate .netkan lokalnie
netkan inflate KerbVisionIR.netkan

# To stworzy .ckan file który mo?esz przetestowa?
```

### 7. Cz?ste b??dy

**B??d: "Could not find release"**
- Sprawd? czy GitHub Release jest publiczny (nie draft)
- Sprawd? czy tag jest w formacie vX.Y.Z

**B??d: "Invalid install path"**
- Sprawd? czy ZIP ma folder GameData/KerbVisionIR/
- Nie powinno by? KerbVisionIR/GameData/KerbVisionIR/

**B??d: "Version file not found"**
- Sprawd? czy .version jest w GameData/KerbVisionIR/
- Sprawd? czy .version jest prawid?owym JSON

**B??d: "License not found"**
- Dodaj plik LICENSE w root repozytorium

### 8. Po walidacji

Je?li wszystko przesz?o - mo?esz ?mia?o tworzy? PR do NetKAN! ??
