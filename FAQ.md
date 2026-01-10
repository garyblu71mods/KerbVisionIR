# KerbVision IR - Frequently Asked Questions

## Installation & Setup

### Q: Where do I install the mod?
**A:** Extract the `GameData` folder from the zip into your KSP installation directory. The final path should be: `KSP/GameData/KerbVisionIR/`

### Q: Does this mod require any dependencies?
**A:** No! KerbVision IR is completely standalone and uses only stock KSP features.

### Q: Which KSP versions are supported?
**A:** KSP 1.8.0 through 1.12.5 are officially supported. It may work on other versions but hasn't been tested.

### Q: Can I use this with other visual mods?
**A:** Yes! KerbVision IR is compatible with most visual enhancement mods like Scatterer, EVE, Parallax, etc.

## Usage

### Q: How do I turn on night vision?
**A:** Press **Alt + I** (hold Alt, then press I). You can also click the toolbar icon and toggle it in the settings window.

### Q: Why can't I see the effect?
**A:** Make sure:
1. You're in Flight scene (not VAB/SPH/Menu)
2. The effect is enabled (press Alt + I)
3. Brightness is not set too low
4. The toolbar icon is visible (means mod is loaded)

### Q: The hotkey doesn't work!
**A:** Make sure you're holding **Alt** while pressing **I**. Both keys must be pressed together.

### Q: Can I change the hotkey?
**A:** Currently, you can only change the letter key (I) by editing the config file. The Alt modifier is required and cannot be changed.

### Q: Where is the settings window?
**A:** Click the green camera icon in the stock toolbar (top-right of screen in Flight).

## Settings & Configuration

### Q: What's the difference between the three modes?
**A:**
- **Grayscale** - Pure black and white, no color tint
- **Green NV** - Classic military night vision with green phosphor
- **IR High Contrast** - Infrared thermal style with warm colors

### Q: What does Tint Strength do?
**A:** It controls how much color is applied. At 0.0 you get pure grayscale, at 1.0 you get full mode color. Great for creating custom looks!

### Q: What's the difference between Brightness and Gamma?
**A:**
- **Brightness** - Overall exposure, how bright everything is
- **Gamma** - Tonal curve, how shadows and highlights are distributed

### Q: Should I enable Bloom?
**A:** Bloom adds a glow to bright objects and looks cool, but it can impact performance. Try it and see if you like it!

### Q: Why is there grain/noise?
**A:** It adds realism to simulate real night vision devices. You can disable it or reduce the intensity if you prefer a cleaner image.

### Q: My settings aren't saving!
**A:** Make sure to click the **Apply** button after changing settings. The mod auto-saves when you click Apply.

### Q: Where are settings stored?
**A:** In `GameData/KerbVisionIR/PluginData/settings.cfg`

### Q: Can I manually edit the settings?
**A:** Yes! Close KSP, edit the config file, then relaunch. See PRESETS.md for examples.

## Performance

### Q: Does this mod impact FPS?
**A:** Minimal impact (~1-3ms per frame). Most users see less than 5 FPS reduction. Disable Bloom if you need better performance.

### Q: My game is lagging with the effect enabled!
**A:** Try:
1. Disable Bloom effect
2. Reduce Grain intensity to 0
3. Close the settings window
4. Use Performance preset (see PRESETS.md)

### Q: Does it run on integrated graphics?
**A:** It should work, but performance will vary. Start with Bloom disabled.

## Troubleshooting

### Q: The shader isn't loading!
**A:** Check that `GameData/KerbVisionIR/Shaders/NightVision.shader` exists. If missing, reinstall the mod.

### Q: The effect looks wrong/broken!
**A:** Try resetting to defaults:
1. Open settings window
2. Click "Reset to Defaults"
3. Click "Apply"

### Q: I see a pink screen!
**A:** This means the shader failed to compile. Check your KSP logs in `KSP_Data/output_log.txt` for errors.

### Q: The mod disappeared after updating KSP!
**A:** KSP updates sometimes clear GameData. Reinstall the mod.

### Q: Settings window won't open!
**A:** Make sure you're in Flight scene. The toolbar button only appears during flight.

## Features & Limitations

### Q: Can I use this in IVA view?
**A:** The effect applies to all cameras in Flight scene, including IVA. Future versions may have separate IVA controls.

### Q: Does it work in Map View?
**A:** No, by design it only works in Flight scene. Night vision for map view doesn't make much sense!

### Q: Can I use it during launch?
**A:** Yes! It works throughout the entire flight, from launch to landing.

### Q: Will there be more vision modes?
**A:** Possibly! Check CHANGELOG.md for planned features. Suggestions are welcome on GitHub.

### Q: Can I create custom color modes?
**A:** Currently you need to edit the source code. A future version may allow custom color palettes via config.

### Q: Does it work with camera mods?
**A:** It should work with most camera mods that use Unity's standard camera system.

## Technical

### Q: How does it work?
**A:** It uses Unity's `OnRenderImage` to apply a post-processing shader to the camera output. See TECHNICAL.md for details.

### Q: Is it open source?
**A:** Check the GitHub repository for source code and license information.

### Q: Can I contribute?
**A:** Yes! See the GitHub repository for contribution guidelines.

### Q: Why Alt + I and not just I?
**A:** To avoid conflicts with other mods. Alt modifier makes it less likely to interfere.

### Q: Does it use ModuleManager?
**A:** No, it's a pure plugin with no MM configs needed.

## Getting Help

### Still having issues?

1. Check the logs: `KSP_Data/output_log.txt`
2. Look for lines containing `[KerbVisionIR]`
3. Read the full documentation:
   - [Quick Start](QUICKSTART.md)
   - [User Guide](USERGUIDE.md)
   - [Features](FEATURES.md)
   - [Technical Docs](TECHNICAL.md)
4. Post on the forum thread with:
   - KSP version
   - Mod version
   - Description of the issue
   - Relevant log excerpts

### Found a bug?

Report it on GitHub with:
- Steps to reproduce
- Expected vs actual behavior
- KSP version
- List of other installed mods
- Log file

### Have a suggestion?

Post on the forum thread or GitHub issues. Feature requests are welcome!

---

**Happy Night Flying!** ????
