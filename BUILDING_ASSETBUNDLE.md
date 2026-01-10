# Building AssetBundle for KerbVisionIR

## Why AssetBundle?
The GPU shader provides maximum performance (~0% FPS impact) compared to CPU pixel processing.

## Steps to Build:

### Option 1: Use Unity Editor (Recommended)

1. **Install Unity 2019.4 LTS** (same version as KSP 1.12)
   - Download from: https://unity.com/releases/editor/archive

2. **Create New Unity Project**
   - 3D Template
   - Name: KerbVisionIR_Assets

3. **Import Shader**
   - Copy `Assets/Shaders/NightVisionShader.shader` to Unity project
   - Place in `Assets/Shaders/` folder

4. **Create AssetBundle**
   - Select shader in Project window
   - In Inspector, set AssetBundle name: `nightvision.ksp`
   - Create folder: `Assets/Editor/`
   - Create script: `BuildAssetBundle.cs`

```csharp
using UnityEditor;
using System.IO;

public class BuildAssetBundle
{
    [MenuItem("Assets/Build KerbVisionIR Bundle")]
    static void BuildBundle()
    {
        string assetBundleDirectory = "Assets/AssetBundles";
        if (!Directory.Exists(assetBundleDirectory))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }
        
        BuildPipeline.BuildAssetBundles(
            assetBundleDirectory,
            BuildAssetBundleOptions.None,
            BuildTarget.StandaloneWindows64
        );
        
        UnityEngine.Debug.Log("AssetBundle built!");
    }
}
```

5. **Build Bundle**
   - In Unity: `Assets > Build KerbVisionIR Bundle`
   - Output: `Assets/AssetBundles/nightvision.ksp`

6. **Copy to KSP**
   - Copy bundle to: `GameData/KerbVisionIR/Shaders/nightvision.ksp`

### Option 2: Use Pre-built Bundle (If Available)

If a pre-built bundle is provided:
1. Download `nightvision.ksp`
2. Place in `GameData/KerbVisionIR/Shaders/`

### Option 3: GUI Overlay Fallback (No Build Required)

If no shader bundle is found, the mod automatically falls back to GUI overlay mode:
- **Pros**: Works without AssetBundle, no build needed
- **Cons**: ~5-10% FPS impact vs 0% with shader
- **Quality**: Good enough for night vision effect

## Testing

After placing the bundle:
1. Launch KSP
2. Check KSP.log for: `[KerbVisionIR] GPU shader loaded successfully!`
3. If you see: `[KerbVisionIR] Falling back to GUI overlay mode` - shader not found

## Performance Comparison

| Mode | FPS Impact | Quality | Build Required |
|------|-----------|---------|----------------|
| **GPU Shader** | ~0-1% | Excellent | Yes (Unity) |
| **GUI Overlay** | ~5-10% | Good | No |

## Troubleshooting

**Shader not loading?**
- Check file exists: `GameData/KerbVisionIR/Shaders/nightvision.ksp`
- Check KSP.log for error messages
- Verify Unity version matches KSP (2019.4 LTS)

**Wrong colors/artifacts?**
- Rebuild bundle with correct shader settings
- Check shader Properties in Unity Inspector
