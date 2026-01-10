# Changelog

All notable changes to KerbVision IR will be documented in this file.

## [1.0.0] - 2025-01-09

### Added
- Initial release of KerbVision IR
- Three vision modes: Grayscale, Green NV, and IR High Contrast
- Adjustable brightness control (0-1 range)
- Adjustable contrast control (0-1 range)
- Adjustable gamma correction (0-1 range)
- Adjustable tint strength control (0-1 range) - blend between grayscale and colored modes
- Optional grain/noise effect for realism
- Optional bloom effect for bright areas
- Hotkey toggle support (default: **Alt + I**)
- In-game settings window with sliders
- Mode cycling button (click to cycle through modes)
- Stock toolbar integration
- Persistent settings storage in config file
- Vignette effect for immersion
- Animated scanlines effect
- Dark area enhancement (simulates IR sensitivity)
- Real-time shader-based post-processing

### Technical
- Built on .NET Framework 4.8
- Compatible with KSP 1.8+
- Uses Unity's OnRenderImage for camera effects
- Custom shader with gamma and tint strength control
- ModuleManager-free implementation
- Single-pass shader rendering

## [Unreleased]

### Planned Features
- In-game key rebinding
- Additional vision modes (FLIR, white-hot, etc.)
- Per-camera settings for IVA vs external views
- Toggle effect per-vessel
- Integration with camera mods
- Performance optimization for potato PCs
- More sophisticated bloom implementation
- Heat signature detection for thermal mode
- Customizable color palettes

### Known Issues
- Bloom effect may impact performance on lower-end systems
- Shader requires AssetBundle for full functionality (fallback available)
- Key binding can only be changed manually in config file
- Effect only active in Flight scene

---

## Version Format

[Major.Minor.Patch]

- **Major**: Breaking changes or major feature additions
- **Minor**: New features, backward compatible
- **Patch**: Bug fixes and minor improvements
