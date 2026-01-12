# Changelog

All notable changes to KerbVisionIR will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [0.5.0] - 2025-01-09

### Added
- Initial public release
- 3 Vision Modes: Monochrome, Green Night Vision, Amber/Warm
- Adjustable brightness (0-2) and contrast (-1 to 1)
- Adjustable color tint with RGB sliders
- Grain/noise control (0-4)
- Processing quality settings (1/1, 1/2, 1/3, 1/4 resolution)
- CRT artifacts at high grain levels:
  - Scanlines
  - Phosphor spots
  - Rolling interference bars
  - Vignette effect
- Audio feedback on activation (NVon.wav)
- Hotkey support (Alt + `)
- Toolbar integration
- Settings window with real-time preview
- Automatic settings persistence
- AVC version file support
- CKAN compatibility

### Technical
- Built for KSP 1.8.0 - 1.12.99
- .NET Framework 4.8
- Custom shader implementation
- Optimized rendering pipeline

## [Unreleased]

### Planned
- Additional vision modes (thermal, UV)
- Per-camera settings
- Configurable hotkeys
- VR support improvements
- Additional audio feedback options

[0.5.0]: https://github.com/garyblu71mods/KerbVisionIR/releases/tag/v0.5.0
