# HOTKEY FIX - Why it's still Alt+I

## Problem
Your old config file has `toggleKey = I` saved.

## Solution

### Option 1: Delete config (RECOMMENDED)
```
DELETE: GameData/KerbVisionIR/PluginData/settings.cfg
```
Mod will create new config with `toggleKey = BackQuote`

### Option 2: Edit config manually
Open: `GameData/KerbVisionIR/PluginData/settings.cfg`

Change:
```
KERBVISION
{
    toggleKey = I        ? OLD
}
```

To:
```
KERBVISION
{
    toggleKey = BackQuote   ? NEW
}
```

### Option 3: Check what's loaded
Open `KSP.log` and search for:
```
[KerbVisionIR] Loaded hotkey: I (RequireAlt: True)
```

If it says **`I`** - your config is old!
If it says **`BackQuote`** - it's correct!

---

## Why this happens
- v0.4.0 and earlier: default was `KeyCode.I`
- v0.4.1+: default changed to `KeyCode.BackQuote`
- Your config was saved with old value
- Code loads from config, not defaults

---

## After fixing
1. Delete/edit config
2. Restart KSP
3. Check log: should say `Loaded hotkey: BackQuote`
4. Test: Press **Alt + `** (key under ESC)

? Should work now!
