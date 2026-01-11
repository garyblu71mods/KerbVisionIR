# KerbVision IR

Night Vision and Infrared camera mod for Kerbal Space Program.

## Features

- **Performance-optimized** - Uses low-resolution grayscale conversion with ambient light boost
- **Three vision modes:**
  - Monochrome - Classic black & white
  - Green NV - Traditional night vision (green tint)
  - Amber/Warm - Infrared/thermal style (orange tint)
- **Customizable settings:**
  - Brightness (0-2) - Ambient light boost
  - Contrast (-1 to 1) - Image contrast adjustment
  - Color Tint (0-1) - Tint strength
  - Grain (0-1) - Static noise overlay
- **Hotkey:** Alt + ` (backtick/gravis)
- **Stock toolbar integration** - No dependencies required

## Installation

1. Download the latest release
2. Extract to `GameData/KerbVisionIR/`
3. Launch KSP

## Usage

- Press **Alt + `** to toggle night vision on/off
- Click the toolbar button to open settings
- Adjust sliders in real-time - changes are immediate
- Settings are saved automatically

## Requirements

- Kerbal Space Program 1.8+
- .NET Framework 4.8

## Building from Source

1. Clone this repository
2. Open `KerbVisionIR.csproj` in Visual Studio
3. Add reference to KSP assemblies:
   - `Assembly-CSharp.dll`
   - `UnityEngine.dll`
   - `UnityEngine.CoreModule.dll`
   - `UnityEngine.UI.dll`
4. Build (Debug or Release)
5. Copy `bin/[Debug|Release]/KerbVisionIR.dll` to `GameData/KerbVisionIR/Plugins/`

## License

MIT License - See LICENSE file for details

## Credits

Created by garyblu71mods

## Version

Current: v0.5.0 (2026-01-11)
