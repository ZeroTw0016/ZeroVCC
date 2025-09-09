# Changelog

All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [1.0.0] - 2025-09-09

### Added
- Initial release of Missing Scripts Finder
- Editor window accessible via Tools > Zero > Open Missing Scripts Finder
- Scanning functionality for missing scripts in scene hierarchy
- Auto Fix button to remove all missing scripts at once
- Individual fix buttons for specific objects
- Confirmation dialogs for safety
- Undo support for all operations
- Clean, scrollable UI interface

### Features
- Only scans objects in the current scene hierarchy (not prefab assets)
- Shows count of missing scripts per object
- Batch removal of all missing scripts
- Individual object fixes
- Proper error handling and user feedback
