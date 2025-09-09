# Missing Scripts Finder

A Unity Editor tool designed to find and remove missing scripts from GameObjects in the scene hierarchy.

## Features

- **Menu Integration**: Accessible via `Tools > Zero > Open Missing Scripts Finder`
- **Scene Hierarchy Scanning**: Only scans objects in the current scene hierarchy
- **Batch Operations**: Remove all missing scripts with one click
- **Individual Fixes**: Fix specific objects one at a time
- **Safety Features**: Confirmation dialogs and undo support
- **Clean UI**: Intuitive interface with scrollable results

## How to Use

1. Open the tool via `Tools > Zero > Open Missing Scripts Finder`
2. Click "Scan for Missing Scripts" to find all objects with missing scripts
3. Either:
   - Click "Auto Fix" to remove all missing scripts at once
   - Use individual "Fix" buttons for specific objects
4. Confirm the action in the dialog that appears

## Requirements

- Unity 2019.4 or later
- Compatible with VRChat Creator Companion

## Installation

Install this package through VRChat Creator Companion or manually place it in your project's Packages folder.

## Version History

### 1.0.0
- Initial release
- Basic missing script detection and removal
- Scene hierarchy support only
