# KerbVision IR - Preset Examples

Here are some ready-to-use presets for different scenarios. Copy the values into your settings window or manually edit the config file.

## Night Landing Presets

### Classic Green NV (Default)
Best for general night operations with authentic military NV look.
```
mode = GreenNV
brightness = 0.600
contrast = 0.550
gamma = 0.500
tintStrength = 0.700
enableGrain = True
grainIntensity = 0.200
enableBloom = False
```

### High Visibility Green
Maximum brightness for very dark landings.
```
mode = GreenNV
brightness = 0.750
contrast = 0.600
gamma = 0.550
tintStrength = 0.700
enableGrain = True
grainIntensity = 0.150
enableBloom = False
```

### Subtle Green Enhancement
Less intrusive, more natural look.
```
mode = GreenNV
brightness = 0.550
contrast = 0.450
gamma = 0.600
tintStrength = 0.500
enableGrain = True
grainIntensity = 0.100
enableBloom = False
```

## Grayscale Presets

### Pure Monochrome
Clean black and white with no color tint.
```
mode = Grayscale
brightness = 0.600
contrast = 0.550
gamma = 0.500
tintStrength = 0.000
enableGrain = True
grainIntensity = 0.200
enableBloom = False
```

### High Contrast BW
Dramatic contrast for terrain definition.
```
mode = Grayscale
brightness = 0.650
contrast = 0.700
gamma = 0.450
tintStrength = 0.000
enableGrain = True
grainIntensity = 0.250
enableBloom = False
```

### Soft Grayscale
Gentle enhancement, good for daytime overcast.
```
mode = Grayscale
brightness = 0.500
contrast = 0.400
gamma = 0.600
tintStrength = 0.000
enableGrain = False
grainIntensity = 0.000
enableBloom = False
```

## IR Thermal Presets

### Standard IR
Balanced infrared thermal imaging style.
```
mode = IRHighContrast
brightness = 0.600
contrast = 0.600
gamma = 0.500
tintStrength = 0.800
enableGrain = True
grainIntensity = 0.150
enableBloom = False
```

### Hot IR
Strong contrast with full warm color palette.
```
mode = IRHighContrast
brightness = 0.700
contrast = 0.700
gamma = 0.400
tintStrength = 1.000
enableGrain = True
grainIntensity = 0.200
enableBloom = True
bloomIntensity = 0.400
```

### Subtle IR
Less saturated for a more subtle thermal effect.
```
mode = IRHighContrast
brightness = 0.550
contrast = 0.500
gamma = 0.550
tintStrength = 0.500
enableGrain = True
grainIntensity = 0.100
enableBloom = False
```

## Specialized Presets

### Docking Mode
Clean, high contrast for precision docking.
```
mode = Grayscale
brightness = 0.650
contrast = 0.650
gamma = 0.500
tintStrength = 0.000
enableGrain = False
grainIntensity = 0.000
enableBloom = False
```

### Cave/Canyon Flying
Maximum visibility with bloom for highlights.
```
mode = GreenNV
brightness = 0.800
contrast = 0.600
gamma = 0.600
tintStrength = 0.700
enableGrain = True
grainIntensity = 0.150
enableBloom = True
bloomIntensity = 0.300
```

### Cinematic
High grain, dramatic look for screenshots/videos.
```
mode = GreenNV
brightness = 0.600
contrast = 0.650
gamma = 0.450
tintStrength = 0.900
enableGrain = True
grainIntensity = 0.350
enableBloom = True
bloomIntensity = 0.500
```

### Performance Mode
Minimal effects for lower-end systems.
```
mode = Grayscale
brightness = 0.600
contrast = 0.550
gamma = 0.500
tintStrength = 0.000
enableGrain = False
grainIntensity = 0.000
enableBloom = False
```

### Old Film Look
Heavy grain, low contrast, retro aesthetic.
```
mode = Grayscale
brightness = 0.550
contrast = 0.400
gamma = 0.650
tintStrength = 0.200
enableGrain = True
grainIntensity = 0.450
enableBloom = False
```

## Custom Tint Examples

### Blue Tint (Manual Edit)
Change the tint in VisionSettings.cs:
```csharp
public Color CustomBlueTint => new Color(0.3f, 0.6f, 1.0f, 1f);
```
Then use Grayscale mode with tintStrength = 0.7

### Red Tint (Manual Edit)
```csharp
public Color CustomRedTint => new Color(1.0f, 0.3f, 0.3f, 1f);
```

## How to Apply Presets

### Method 1: In-Game
1. Open settings window (click toolbar icon)
2. Adjust sliders to match preset values
3. Click "Apply"

### Method 2: Config File
1. Close KSP
2. Open `GameData/KerbVisionIR/PluginData/settings.cfg`
3. Replace KERBVISION section with preset values
4. Save file
5. Launch KSP

## Tips for Creating Your Own Presets

1. **Start with a base preset** that's close to what you want
2. **Adjust one parameter at a time** to see its effect
3. **Brightness first** - get the overall exposure right
4. **Then contrast** - adjust the punch
5. **Then gamma** - fine-tune the tonal curve
6. **Tint strength last** - dial in the color intensity
7. **Grain optional** - add for realism or remove for clarity
8. **Save variations** - create presets for different scenarios

## Sharing Presets

Found a great preset? Share it with the community!
Post your settings on the forum thread or GitHub issues.

Format:
```
Name: Your Preset Name
Description: What it's good for
Settings:
  mode = YourMode
  brightness = X.XXX
  contrast = X.XXX
  gamma = X.XXX
  tintStrength = X.XXX
  enableGrain = True/False
  grainIntensity = X.XXX
  enableBloom = True/False
  bloomIntensity = X.XXX
```

Happy flying! ??
