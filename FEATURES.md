# KerbVision IR - Complete Feature List

## Overview
KerbVision IR is a comprehensive night vision and infrared camera mod for Kerbal Space Program 1.8+. It provides realistic image enhancement effects for flying and landing in dark conditions.

## Vision Modes

### 1. Grayscale
Pure monochrome night vision without color tinting.
- Classic black and white enhancement
- High clarity for instrument reading
- Best for precision landing

### 2. Green NV (Night Vision)
Traditional military-style night vision with green phosphor display.
- Authentic green tint
- Classic NV aesthetic
- Most popular for general use

### 3. IR High Contrast
Infrared thermal imaging style with warm color palette.
- Yellow/orange tones
- High contrast visualization
- Great for spotting terrain features

## Image Controls

### Brightness (Exposure)
- Range: 0.0 - 1.0
- Controls overall image brightness
- Automatically boosts dark areas (IR sensitivity simulation)
- Higher values increase visibility in shadows
- **Default:** 0.6

### Contrast
- Range: 0.0 - 1.0
- Adjusts difference between light and dark areas
- Higher values = more dramatic, punchy image
- Lower values = flatter, more even image
- **Default:** 0.55

### Gamma Correction
- Range: 0.0 - 1.0
- Controls midtone distribution
- Lower = darker shadows, more dramatic
- Higher = lifted shadows, more detail
- Affects overall tonal curve
- **Default:** 0.5

### Tint Strength
- Range: 0.0 - 1.0
- Controls color intensity of the vision mode
- 0.0 = pure grayscale (no color)
- 1.0 = full mode color
- Allows smooth blending between modes
- Create custom looks by mixing modes
- **Default:** 0.7

## Visual Effects

### Grain/Noise
- Simulates sensor noise from real night vision devices
- Animated, time-based grain for realism
- Adjustable intensity (0.0 - 1.0)
- Adds authenticity and "analog" feel
- **Default:** Enabled at 0.2

### Bloom
- Glow effect on bright light sources
- Simulates overexposure typical of NV systems
- Optional (can disable for performance)
- Adjustable intensity (0.0 - 1.0)
- **Default:** Disabled

### Vignette
- Subtle darkening at screen edges
- Always active
- Enhances focus on center of view
- Adds depth and realism

### Scanlines
- Subtle horizontal scan lines
- Animated movement
- Simulates CRT/analog display
- Always active at low intensity

## User Interface

### Toolbar Button
- Stock ApplicationLauncher integration
- Green camera icon
- Single click to open settings window
- Available only in Flight scene
- No dependencies required

### Settings Window
- Compact, draggable window
- Real-time preview of changes
- Apply button to save settings
- Reset to defaults option
- All sliders with numeric display
- Mode cycling button

### Controls
- **Alt + I** - Toggle effect on/off
- **Toolbar Icon** - Open/close settings
- Settings persist between sessions

## Technical Features

### Rendering
- Real-time post-processing shader
- Single-pass efficient rendering
- GPU-accelerated effects
- Minimal CPU overhead
- ~1-3ms per frame on modern hardware

### Shader Details
- Custom HLSL/Cg shader
- Luminance-based processing
- Proper gamma correction
- Efficient noise generation
- Optimized bloom (when enabled)

### Configuration
- Automatic saving to config file
- KSP ConfigNode format
- Manual editing supported
- Settings location: `GameData/KerbVisionIR/PluginData/settings.cfg`

### Compatibility
- Works with stock KSP (1.8 - 1.12.5)
- No mod dependencies
- Compatible with visual enhancement mods
- Flight scene only (by design)
- .NET Framework 4.8

## Use Cases

### Night Landing
Enable night vision for:
- Landing on dark side of planets
- Approach in low light conditions
- Terrain visualization without sunlight
- Instrument reading enhancement

### Space Operations
Useful for:
- Docking in shadow
- Rendezvous operations
- Satellite deployment
- Station assembly

### Atmospheric Flight
Benefits:
- Night-time flight operations
- Canyon flying in darkness
- Low-altitude terrain following
- Emergency landings

### IVA Enhancement
(Future feature):
- Cockpit instrument reading
- Window view enhancement
- Realistic pilot experience

## Performance

### System Requirements
- Mid-range GPU (2GB+ VRAM)
- KSP 1.8 or newer
- .NET Framework 4.8

### Performance Impact
- **Minimal** (~1-2ms): Base effect with grain
- **Low** (~2-3ms): With bloom enabled
- **FPS Impact**: Typically <5 FPS on 60 FPS systems

### Optimization Tips
- Disable bloom for best performance
- Reduce grain intensity slightly
- Close settings window when not in use
- Effect has no overhead when disabled

## Customization

### Create Your Own Look
Examples:

**Pure White NV:**
- Mode: Any
- Tint Strength: 0.0
- Brightness: 0.7
- Contrast: 0.6
- Gamma: 0.4

**Subtle Enhancement:**
- Mode: Grayscale
- Tint Strength: 0.0
- Brightness: 0.5
- Contrast: 0.4
- Gamma: 0.6

**Dramatic IR:**
- Mode: IR High Contrast
- Tint Strength: 1.0
- Brightness: 0.7
- Contrast: 0.7
- Gamma: 0.4

**Realistic Military NV:**
- Mode: Green NV
- Tint Strength: 0.8
- Brightness: 0.6
- Contrast: 0.55
- Gamma: 0.5
- Grain: 0.25

## Future Enhancements

Planned features:
- Per-camera settings (IVA vs External)
- Additional vision modes (FLIR, White Hot, etc.)
- Heat signature overlay for Thermal mode
- Performance scaling options
- In-game key rebinding
- Custom color palette creator
- Integration with camera mods
- Scope/binocular mode

## Philosophy

KerbVision IR aims to:
- ? Enhance gameplay, not replace core mechanics
- ? Maintain performance and stability
- ? Stay true to real-world NV/IR technology
- ? Provide flexibility without overwhelming users
- ? Work out-of-the-box with sensible defaults
- ? Remain simple and dependency-free

---

**For more information:**
- [Quick Start Guide](QUICKSTART.md)
- [User Guide](USERGUIDE.md)
- [Technical Documentation](TECHNICAL.md)
- [Building Instructions](BUILDING.md)
