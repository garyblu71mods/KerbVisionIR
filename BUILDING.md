# Building KerbVision IR

## Prerequisites

- Visual Studio 2019 or later (Community Edition is fine)
- .NET Framework 4.8 SDK
- Kerbal Space Program installed

## Setup

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/KerbVisionIR.git
   cd KerbVisionIR
   ```

2. **Configure KSP paths**
   
   Edit `KerbVisionIR.csproj` and update the paths to your KSP installation:
   
   ```xml
   <Reference Include="Assembly-CSharp">
       <HintPath>C:\Path\To\Your\KSP\KSP_x64_Data\Managed\Assembly-CSharp.dll</HintPath>
   </Reference>
   ```
   
   Update all references:
   - Assembly-CSharp
   - Assembly-CSharp-firstpass
   - UnityEngine
   - UnityEngine.CoreModule
   - UnityEngine.UI
   - UnityEngine.IMGUIModule

## Building

### Using Visual Studio

1. Open `KerbVisionIR.csproj` in Visual Studio
2. Select **Release** configuration
3. Build ? Build Solution (or press Ctrl+Shift+B)
4. The DLL will be automatically copied to `GameData/KerbVisionIR/Plugins/`

### Using MSBuild (Command Line)

```bash
# From the project directory
msbuild KerbVisionIR.csproj /p:Configuration=Release
```

### Using dotnet CLI

```bash
dotnet build KerbVisionIR.csproj -c Release
```

## Post-Build

The post-build event automatically:
1. Copies `KerbVisionIR.dll` to `GameData/KerbVisionIR/Plugins/`
2. Copies the `.pdb` debug symbols (in Debug builds)

## Installing for Testing

After building, copy the entire `GameData` folder to your KSP installation:

```bash
# Windows PowerShell
Copy-Item -Recurse -Force "GameData\KerbVisionIR" "C:\Path\To\KSP\GameData\"

# Linux/Mac
cp -r GameData/KerbVisionIR /path/to/KSP/GameData/
```

## Shader Setup (Advanced)

The shader file `GameData/KerbVisionIR/Shaders/NightVision.shader` needs to be compiled into an AssetBundle for full functionality:

1. Create a Unity project (version matching KSP's Unity version)
2. Import the shader file
3. Build an AssetBundle containing the shader
4. Place the AssetBundle in `GameData/KerbVisionIR/Shaders/`

For now, the mod uses a fallback approach that works without the AssetBundle.

## Creating a Release

1. Build in Release configuration
2. Create a zip file with this structure:
   ```
   KerbVisionIR-v1.0.0.zip
   ??? GameData/
       ??? KerbVisionIR/
           ??? Plugins/
           ?   ??? KerbVisionIR.dll
           ??? Shaders/
           ?   ??? NightVision.shader
           ??? PluginData/
           ?   ??? settings.cfg (example)
           ??? KerbVisionIR.version
   ```

3. Include README.md and USERGUIDE.md in the root of the zip

## Troubleshooting Build Issues

### "Assembly reference not found"
- Check that your KSP path is correct in the .csproj file
- Verify that the DLL files exist in your KSP installation

### "Could not copy file"
- Make sure KSP is not running
- Check that you have write permissions to the GameData folder

### "Unknown language version: 7.3"
- Update your Visual Studio to the latest version
- Or change `<LangVersion>7.3</LangVersion>` to `<LangVersion>7.0</LangVersion>` in the .csproj

## Debug Build

For debugging:

1. Build in Debug configuration
2. The .pdb files will be included
3. Attach Visual Studio debugger to KSP process
4. Set breakpoints in your code

## Clean Build

To clean all build outputs:

```bash
msbuild KerbVisionIR.csproj /t:Clean
```

Or in Visual Studio: Build ? Clean Solution
