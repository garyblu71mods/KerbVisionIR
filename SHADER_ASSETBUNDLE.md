# Creating Shader AssetBundle for KerbVision IR

The mod requires a compiled shader to work at full performance. Here's how to create it:

## Quick Solution (Temporary)

For now, the mod will work without the shader but with reduced performance. To get full GPU acceleration, you need to create an AssetBundle.

## Creating the AssetBundle (Unity Required)

### Requirements:
- Unity 2019.2.2f1 (same version as KSP 1.12)
- KerbVision IR shader file

### Steps:

1. **Install Unity 2019.2.2f1**
   - Download from Unity Archive
   - Install with Windows/Mac/Linux build support

2. **Create New Project**
   - Open Unity
   - Create new 3D project
   - Name it "KerbVisionIR_Shaders"

3. **Import Shader**
   - Copy `GameData/KerbVisionIR/Shaders/NightVision.shader` to `Assets/Shaders/`
   - Unity will automatically compile it

4. **Create AssetBundle**
   - Select the shader file in Project window
   - In Inspector, set AssetBundle name to "nightvision"
   - Create folder: `Assets/Editor/`
   - Create script `BuildAssetBundles.cs`:

```csharp
using UnityEditor;
using System.IO;

public class BuildAssetBundles
{
    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        string assetBundleDirectory = "Assets/AssetBundles";
        if(!Directory.Exists(assetBundleDirectory))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }
        
        BuildPipeline.BuildAssetBundles(assetBundleDirectory, 
                                        BuildAssetBundleOptions.None, 
                                        BuildTarget.StandaloneWindows64);
    }
}
```

5. **Build**
   - In Unity menu: Assets ? Build AssetBundles
   - Find bundle in `Assets/AssetBundles/nightvision`

6. **Copy to Mod**
   - Copy `nightvision` file (no extension) to:
   - `GameData/KerbVisionIR/Shaders/nightvision`

## Without AssetBundle

The mod will fall back to CPU processing which works but is slow (0.2 FPS).

## Alternative: Use KSP's Built-in Shaders

Another option is to use KSP's existing post-processing if available, but this requires more research into KSP's shader system.

## Need Help?

If you can't create the AssetBundle, you can:
1. Request it in the mod thread
2. Use the mod with reduced performance
3. Wait for a pre-built release with the bundle included

The shader source is in `GameData/KerbVisionIR/Shaders/NightVision.shader` and is ready to be compiled.
