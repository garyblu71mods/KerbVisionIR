# KerbVision IR - Testing Checklist

Use this checklist before releasing a new version or after making changes.

## Pre-Build Testing

### Code Quality
- [ ] All files compile without errors
- [ ] No compiler warnings
- [ ] Code follows consistent style
- [ ] All public methods have XML documentation
- [ ] No hardcoded paths (except KSP references)

### Version Check
- [ ] Version number updated in AssemblyInfo.cs
- [ ] Version number updated in KerbVisionIR.version
- [ ] CHANGELOG.md updated with changes
- [ ] README.md version badge updated (if present)

## Build Testing

### Compilation
- [ ] Debug build succeeds
- [ ] Release build succeeds
- [ ] No post-build errors
- [ ] DLL copied to GameData correctly
- [ ] File size reasonable (~50-200 KB)

### File Structure
- [ ] GameData/KerbVisionIR/ exists
- [ ] Plugins/ folder contains DLL
- [ ] Shaders/ folder contains .shader file
- [ ] PluginData/ folder exists
- [ ] KerbVisionIR.version file present

## In-Game Testing

### Initial Load
- [ ] KSP launches without errors
- [ ] Mod loads successfully (check logs)
- [ ] No errors in output_log.txt
- [ ] Toolbar button appears in Flight scene

### Basic Functionality
- [ ] Alt + I toggles effect on/off
- [ ] Effect is visible when enabled
- [ ] Effect is off by default
- [ ] Toolbar button opens settings window
- [ ] Settings window is draggable

### Settings Window
- [ ] All sliders work correctly
- [ ] Mode button cycles through all modes
- [ ] Values display correctly
- [ ] Apply button saves settings
- [ ] Reset button restores defaults
- [ ] Close button closes window

### Vision Modes
- [ ] Grayscale mode works
- [ ] Green NV mode works
- [ ] IR High Contrast mode works
- [ ] Colors look correct for each mode
- [ ] Mode changes apply immediately

### Parameter Controls
- [ ] Brightness slider works (0.0 - 1.0)
- [ ] Contrast slider works (0.0 - 1.0)
- [ ] Gamma slider works (0.0 - 1.0)
- [ ] Tint Strength slider works (0.0 - 1.0)
- [ ] Grain toggle works
- [ ] Grain intensity slider works
- [ ] Bloom toggle works
- [ ] Bloom intensity slider works

### Visual Effects
- [ ] Grayscale conversion looks correct
- [ ] Color tinting applies properly
- [ ] Brightness boost affects dark areas
- [ ] Contrast adjustment is visible
- [ ] Gamma correction works as expected
- [ ] Tint strength blends correctly
- [ ] Grain/noise is visible when enabled
- [ ] Grain animates over time
- [ ] Bloom glows on bright objects (when enabled)
- [ ] Vignette visible at edges
- [ ] Scanlines visible (subtle)

### Configuration
- [ ] Settings save when Apply is clicked
- [ ] Settings persist after restarting KSP
- [ ] Config file created in PluginData
- [ ] Config file format is correct
- [ ] Manual config edits load correctly

### Scene Transitions
- [ ] Effect persists when switching vessels
- [ ] Effect state preserved during scene changes
- [ ] No errors when leaving Flight scene
- [ ] Toolbar button reappears in new flight
- [ ] Settings retained between flights

## Compatibility Testing

### Stock KSP
- [ ] Works in stock KSP (no mods)
- [ ] No conflicts with stock features
- [ ] Camera system works correctly
- [ ] Performance acceptable (>30 FPS)

### With Popular Mods
Test with commonly used mods:
- [ ] Scatterer (visual)
- [ ] EVE (clouds/visual)
- [ ] Parallax (terrain)
- [ ] Tufx (post-processing)
- [ ] MechJeb (autopilot)
- [ ] KER (engineering)
- [ ] Transfer Window Planner (navigation)

### Different Scenarios
- [ ] Launch from Kerbin
- [ ] Landing on Mun (dark side)
- [ ] Docking in orbit
- [ ] Space station operations
- [ ] Atmospheric flight at night
- [ ] IVA view
- [ ] External camera
- [ ] Map view (should not affect)

## Performance Testing

### FPS Impact
- [ ] Measure baseline FPS (effect off)
- [ ] Measure FPS with effect on
- [ ] FPS drop less than 10% (target)
- [ ] No stuttering or frame drops
- [ ] Stable frame times

### Settings Impact
Test performance with different settings:
- [ ] Grain enabled vs disabled
- [ ] Bloom enabled vs disabled
- [ ] Different resolutions (1080p, 1440p, 4K)
- [ ] Different quality settings (Low, High, Ultra)

### Memory
- [ ] No memory leaks over time
- [ ] RAM usage stable
- [ ] VRAM usage reasonable
- [ ] No allocation spikes

## Edge Cases

### Unusual Situations
- [ ] Very bright scenes (Kerbol close)
- [ ] Completely dark scenes (deep space)
- [ ] Rapid on/off toggling
- [ ] Multiple quick setting changes
- [ ] Window dragged off-screen
- [ ] Alt-tab while effect active

### Error Handling
- [ ] Missing shader file (fallback)
- [ ] Corrupted config file (defaults)
- [ ] Invalid config values (clamped)
- [ ] Shader compilation failure (logged)

## Platform Testing

### Operating Systems
- [ ] Windows 10/11
- [ ] Linux (if available)
- [ ] macOS (if available)

### KSP Versions
- [ ] KSP 1.8.x
- [ ] KSP 1.9.x
- [ ] KSP 1.10.x
- [ ] KSP 1.11.x
- [ ] KSP 1.12.x (primary target)

## Documentation Review

### Completeness
- [ ] README.md accurate
- [ ] QUICKSTART.md tested
- [ ] USERGUIDE.md complete
- [ ] FEATURES.md up to date
- [ ] TECHNICAL.md accurate
- [ ] PRESETS.md tested
- [ ] FAQ.md answers common issues
- [ ] CHANGELOG.md updated
- [ ] BUILDING.md instructions work

### Accuracy
- [ ] All keybindings documented correctly
- [ ] All features listed
- [ ] Screenshots up to date (if present)
- [ ] No broken links
- [ ] Version numbers consistent

## Release Package

### File Contents
- [ ] GameData folder structured correctly
- [ ] All necessary files included
- [ ] No debug files (*.pdb in Release)
- [ ] No source code in release
- [ ] Documentation included
- [ ] LICENSE file present

### Package Creation
- [ ] MakeRelease.ps1 runs successfully
- [ ] Zip file created
- [ ] Zip extracts correctly
- [ ] File structure correct in zip
- [ ] File size reasonable (~1-2 MB)

### Installation Test
- [ ] Extract to fresh KSP install
- [ ] Launch and verify it works
- [ ] No missing dependencies
- [ ] No installation issues

## Final Checks

### Code Review
- [ ] No commented-out debug code
- [ ] No temporary test code
- [ ] No profanity in comments/logs
- [ ] Consistent naming conventions
- [ ] No sensitive information

### Legal
- [ ] LICENSE file included
- [ ] Copyright notices present
- [ ] No third-party code without license
- [ ] Attribution given where needed

### Communication
- [ ] Forum post ready
- [ ] GitHub release notes ready
- [ ] Known issues documented
- [ ] Support channels ready

## Post-Release

### Monitoring
- [ ] Watch for bug reports
- [ ] Check forum thread regularly
- [ ] Monitor GitHub issues
- [ ] Respond to user questions

### Feedback Collection
- [ ] Note feature requests
- [ ] Track common issues
- [ ] Collect preset suggestions
- [ ] Gather performance data

---

## Testing Tools

### KSP Logs
Location: `KSP_x64_Data/output_log.txt` or `Player.log`
Look for: `[KerbVisionIR]` entries

### FPS Counter
Use: F12 in KSP (if enabled) or FRAPS/MSI Afterburner

### Performance Profiler
Consider: Unity Profiler (if attached) or in-game stats

### Test Save
Create a dedicated test save with:
- Various vessels
- Different celestial bodies
- Day/night scenarios
- Space station for docking tests

---

## Notes

- Not all tests need to be run for minor changes
- Focus testing on affected areas
- Document any new issues found
- Keep test results for reference
- Update checklist as needed

**Last Updated:** 2025-01-09
