# Zero's Unity Tools VCC Repository

This repository contains Unity Editor tools designed for VRChat development, compatible with VRChat Creator Companion.

## How to Add This Repository to VCC

1. Open VRChat Creator Companion
2. Go to Settings (gear icon)
3. Click on "Packages" tab
4. Click "Add Repository"
5. Enter this URL: `https://raw.githubusercontent.com/YOUR_USERNAME/vcc-repository/main/index.json`
6. Click "Add"

## Available Packages

### Missing Scripts Finder v1.0.0
- **Description**: Find and remove missing scripts from GameObjects in the scene hierarchy
- **Menu Location**: Tools > Zero > Open Missing Scripts Finder
- **Features**: 
  - Scan scene hierarchy for missing scripts
  - Auto-fix all missing scripts with one click
  - Individual object fixes
  - Undo support and confirmation dialogs

## Installation Instructions

After adding the repository to VCC:

1. Create a new VRChat project or open an existing one in VCC
2. Go to "Manage Project"
3. Find "Missing Scripts Finder" in the available packages
4. Click the "+" button to install it
5. The tool will be available under Tools > Zero in Unity

## GitHub Setup Instructions

To host this repository on GitHub:

1. Create a new GitHub repository named `vcc-repository`
2. Upload the contents of the `vcc-repository` folder
3. Create a release for each package version:
   - Tag: `missing-scripts-finder-1.0.0`
   - Upload the zipped package as an asset
4. Update the URLs in `index.json` to point to your GitHub username
5. The repository URL will be: `https://raw.githubusercontent.com/YOUR_USERNAME/vcc-repository/main/index.json`

## Package Development

To add new packages or update existing ones:

1. Update the package files in the appropriate folder
2. Create a new zip file of the package
3. Create a GitHub release with the zip file
4. Update `index.json` with the new version information
5. Users with the repository added will see updates in VCC

## Support

For issues or feature requests, please use the GitHub Issues page.
