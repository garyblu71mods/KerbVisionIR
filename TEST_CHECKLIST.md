# TEST CHECKLIST - KerbVisionIR v0.4.1

## Przed testem:
1. Usu? stary `GameData/KerbVisionIR/Plugins/KerbVisionIR.dll`
2. Skopiuj nowy z `bin/Debug/KerbVisionIR.dll`
3. Uruchom KSP
4. Wejd? w Flight

## Sprawd? wersj?:
- Otwórz okno ustawie? (Alt + ` lub toolbar)
- **Pierwszy wiersz powinien pokazywa?:**
  ```
  Version: v0.4.1 | Build: 2025-01-09 20:45
  ```
- **Drugi wiersz powinien pokazywa?:**
  ```
  Mode: ACTIVE | Enabled: True/False
  ```

? **Je?li wersja si? nie zmieni?a - DLL si? nie zaktualizowa?o!**

## Test 1: Monochrome (czarno-bia?y)
1. W??cz efekt (Alt + `)
2. Ustaw Mode: **Monochrome**
3. **OCZEKIWANY WYNIK**: Obraz powinien by? czarno-bia?y (bez kolorów!)
4. Pokr?? Color Tint: 0% = czysta czer?/biel, 100% = lekko kolorowy

? **Je?li widzisz kolory - grayscale nie dzia?a!**

## Test 2: Contrast (kontrast)
1. Ustaw Contrast: **0.0** (minimum)
2. Ustaw Contrast: **1.0** (maximum)
3. **OCZEKIWANY WYNIK**: Widoczna ró?nica - cienie ciemniejsze, ?wiat?a ja?niejsze

? **Je?li nie widzisz ró?nicy - contrast nie dzia?a!**

## Test 3: Green NV (zielona noktowizja)
1. Zmie? Mode: **Green NV**
2. **OCZEKIWANY WYNIK**: 
   - Podstawa czarno-bia?a
   - Zielony odcie? na ca?o?ci
   - Wygl?da jak prawdziwa noktowizja

? **Je?li widzisz oryginalne kolory obiektu - grayscale nie dzia?a!**

## Test 4: Amber/Warm (termowizja)
1. Zmie? Mode: **Amber/Warm**
2. **OCZEKIWANY WYNIK**:
   - Podstawa czarno-bia?a
   - Pomara?czowo-?ó?ty odcie?
   - Wygl?da jak termowizja

## Test 5: Brightness (jasno??)
1. Ustaw Brightness: **0.0**
2. Ustaw Brightness: **1.0**
3. **OCZEKIWANY WYNIK**: Scena jest znacznie ja?niejsza (9x boost ambient light)

## Test 6: Grain (szum)
1. Ustaw Grain: **0.0**
2. Ustaw Grain: **1.0**
3. **OCZEKIWANY WYNIK**: Widoczny ruchomy szum na ekranie

## Sprawd? logi (KSP.log):
Szukaj co 60 klatek (sekunda):
```
[KerbVisionIR] Processing grayscale: 480x270, Mode: Monochrome, Contrast: 0.50
```

? **Je?li nie ma tych logów - OnRenderImage nie dzia?a!**

## Wydajno??:
- **32 FPS bazowo** ? powinno by? **~28-30 FPS** z efektem
- **60 FPS bazowo** ? powinno by? **~52-56 FPS** z efektem

? **Je?li spadek > 20% - co? jest ?le!**

## Najcz?stsze problemy:

### "Widz? kolory w Monochrome"
? Grayscale conversion nie dzia?a
? Sprawd? logi czy jest "Processing grayscale"

### "Contrast nic nie zmienia"
? Sprawd? warto?? w logu
? Contrast powinien by? 1.0 - 2.5x multiplier

### "Okno pokazuje star? wersj?"
? DLL si? nie zaktualizowa?o
? Usu? stary plik, skopiuj nowy, restart KSP

### "FPS drastycznie spadaj?"
? Sprawd? rozdzielczo?? w logu (powinna by? ~1/4)
? Sprawd? czy Parallel.For dzia?a

---

**Po testach daj zna? które testy przesz?y a które nie!**
