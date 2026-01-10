# KerbVision IR - User Guide

## Quick Start

1. **Installation**
   - Copy the `GameData/KerbVisionIR` folder to your KSP installation's `GameData` directory
   - Launch KSP

2. **First Use**
   - During flight, look for the green camera icon in the toolbar
   - Click it to open the settings window
   - Press **N** key to toggle night vision on/off

## Controls

### Keyboard
- **Alt + I** - Toggle night vision effect (hold Alt and press I)

### Toolbar Button
- Click the green camera icon to open/close settings window
- Available only during flight

## Settings Explained

### Vision Mode
Choose between three different vision modes (click to cycle):

- **Grayscale** - Pure monochrome night vision, no color tint
- **Green NV** - Classic military night vision with green phosphor screen effect
- **IR High Contrast** - Simulates infrared thermal imaging with warm tones

### Brightness (0.0 - 1.0)
- Controls overall image brightness and exposure
- Higher values = brighter image
- Also enhances dark areas more (IR sensitivity simulation)
- **Recommended:** 0.5 - 0.7

### Contrast (0.0 - 1.0)
- Adjusts the difference between light and dark areas
- Higher values = more dramatic contrast
- Lower values = flatter, more uniform image
- **Recommended:** 0.5 - 0.6

### Gamma (0.0 - 1.0)
- Controls gamma correction curve
- Lower values = darker shadows, more dramatic
- Higher values = brighter, more even distribution
- **Recommended:** 0.4 - 0.6

### Tint Strength (0.0 - 1.0)
- Controls the intensity of the color tint
- 0.0 = pure grayscale (no tint)
- 1.0 = full color tint based on mode
- Allows blending between grayscale and colored modes
- **Recommended:** 0.6 - 0.8

### Grain/Noise
- **Enable Grain** - Adds realistic sensor noise/grain
- **Grain Intensity** - Controls how visible the noise is
- Adds authenticity to the night vision effect
- **Recommended:** Enabled with intensity 0.15 - 0.25

### Bloom
- **Enable Bloom** - Adds glow/bloom to bright objects
- **Bloom Intensity** - Controls strength of the glow
- May impact performance
- Creates a more "overexposed" NV look for bright light sources
- **Recommended:** Disabled for better performance, or 0.2-0.4 if enabled

## Tips & Tricks

1. **Best Settings for Dark Areas**
   - Mode: Green NV
   - Brightness: 0.7
   - Contrast: 0.6
   - Gamma: 0.5
   - Tint Strength: 0.7
   - Grain: Enabled (0.2)

2. **Best Settings for Space**
   - Mode: Grayscale or IR High Contrast
   - Brightness: 0.5
   - Contrast: 0.5
   - Gamma: 0.5
   - Tint Strength: 0.5
   - Grain: Enabled (0.15)

3. **Custom Grayscale from Any Mode**
   - Choose any mode
   - Set Tint Strength to 0.0
   - Adjust brightness, contrast, gamma to taste

4. **Performance**
   - Disable Bloom for better FPS
   - Lower Grain Intensity slightly if you experience stuttering

5. **Realism**
   - Enable Grain for authentic look
   - Use Green NV mode with high Tint Strength (0.7+)
   - Keep Brightness around 0.6
   - Moderate Contrast (0.55)

## Troubleshooting

### Effect not showing
- Make sure the effect is enabled (toggle with Alt + I)
- Check that you're in Flight scene
- Try adjusting Brightness higher

### Image too dark/bright
- Adjust Brightness slider
- Try different Gamma settings
- Lower Contrast if too extreme

### Colors too strong/weak
- Adjust Tint Strength slider
- Try different vision mode

### Performance issues
- Disable Bloom effect
- Lower Grain Intensity
- Close settings window when not needed

### Settings not saving
- Check that `GameData/KerbVisionIR/PluginData/` folder exists
- Ensure KSP has write permissions to the folder

### Shader not loading
- Verify `GameData/KerbVisionIR/Shaders/NightVision.shader` exists
- Check KSP logs for shader compilation errors

## Technical Notes

### Settings File Location
`GameData/KerbVisionIR/PluginData/settings.cfg`

### Manual Configuration
You can manually edit the settings file while KSP is closed:
```
KERBVISION
{
    isEnabled = False
    mode = GreenNV
    brightness = 0.600
    contrast = 0.550
    gamma = 0.500
    tintStrength = 0.700
    enableBloom = False
    bloomIntensity = 0.300
    enableGrain = True
    grainIntensity = 0.200
    toggleKey = I
}
```

### Changing the Toggle Key
Currently, the toggle requires Alt + the specified key.
To change the key, edit `toggleKey` in the settings file.
Valid values include: `I`, `V`, `B`, `N`, etc.
(Any Unity KeyCode value)
Alt is always required.

## Known Limitations

- Effect only works in Flight scene
- Hotkey requires Alt modifier (cannot be changed)
- Bloom effect may impact performance on lower-end systems
- Some visual enhancement mods may conflict with the shader
- Mode cycling only available via button (no dropdown yet)

## Support

For issues, suggestions, or feedback, please visit:
- GitHub: [Your Repository URL]
- Forum: [KSP Forum Thread URL]

## Credits

Developed by garyblu71mods
Night vision shader based on real-world NV/IR camera principles

Enjoy your enhanced night operations! ???
