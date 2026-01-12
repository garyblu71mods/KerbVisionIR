using UnityEngine;
using System.IO;
using System.Threading.Tasks;
using System.Threading;

namespace KerbVisionIR
{
    /// <summary>
    /// Night vision using grayscale conversion + ambient light manipulation
    /// </summary>
    [RequireComponent(typeof(Camera))]
    public class VisionShaderEffect : MonoBehaviour
    {
        private Material grayscaleMaterial;
        private Material effectMaterial;
        private Shader nightVisionShader;
        
        // Fallback textures
        private Texture2D grainTexture;
        private System.Random random = new System.Random();
        private bool useCustomShader = false;
        
        // Store original lighting
        private Color originalAmbientLight;
        private float originalAmbientIntensity;
        private Color originalAmbientEquator;
        private Color originalAmbientGround;
        private Color originalAmbientSky;
        private bool hasStoredLighting = false;
        
        private void Start()
        {
            Debug.Log("[KerbVisionIR] VisionShaderEffect Start");
            
            // Store original lighting settings
            StoreLightingSettings();
            
            // Try to create grayscale material
            CreateGrayscaleMaterial();
            
            // Try to load custom shader
            if (LoadShader())
            {
                useCustomShader = true;
                Debug.Log("[KerbVisionIR] Custom GPU shader loaded!");
            }
            else
            {
                Debug.Log("[KerbVisionIR] Using built-in grayscale shader");
                CreateGrainTexture();
            }
        }
        
        private void CreateGrayscaleMaterial()
        {
            // Find built-in Grayscale Effect shader
            Shader grayscaleShader = Shader.Find("Hidden/Grayscale Effect");
            
            if (grayscaleShader == null)
            {
                // Fallback - try to create simple grayscale shader
                grayscaleShader = Shader.Find("Sprites/Default");
            }
            
            if (grayscaleShader != null)
            {
                grayscaleMaterial = new Material(grayscaleShader);
                grayscaleMaterial.hideFlags = HideFlags.DontSave;
                Debug.Log("[KerbVisionIR] Grayscale material created");
            }
            else
            {
                Debug.LogWarning("[KerbVisionIR] Could not find grayscale shader!");
            }
        }
        
        private void StoreLightingSettings()
        {
            if (!hasStoredLighting)
            {
                originalAmbientLight = RenderSettings.ambientLight;
                originalAmbientIntensity = RenderSettings.ambientIntensity;
                
                try
                {
                    originalAmbientEquator = RenderSettings.ambientEquatorColor;
                    originalAmbientGround = RenderSettings.ambientGroundColor;
                    originalAmbientSky = RenderSettings.ambientSkyColor;
                }
                catch { }
                
                hasStoredLighting = true;
                Debug.Log($"[KerbVisionIR] Stored original ambient: {originalAmbientLight}, intensity: {originalAmbientIntensity}");
            }
        }
        
        private void Update()
        {
            if (KerbVisionIR.Instance != null && KerbVisionIR.Instance.Settings != null)
            {
                VisionSettings settings = KerbVisionIR.Instance.Settings;
                
                if (settings.IsEnabled)
                {
                    ApplyEnhancedLighting(settings);
                }
                else
                {
                    // When disabled, just update our stored values (don't restore every frame!)
                    // This allows KSP to change ambient light naturally
                    UpdateStoredLighting();
                }
            }
        }
        
        private void UpdateStoredLighting()
        {
            if (hasStoredLighting)
            {
                originalAmbientLight = RenderSettings.ambientLight;
                originalAmbientIntensity = RenderSettings.ambientIntensity;
                
                try
                {
                    originalAmbientEquator = RenderSettings.ambientEquatorColor;
                    originalAmbientGround = RenderSettings.ambientGroundColor;
                    originalAmbientSky = RenderSettings.ambientSkyColor;
                }
                catch { }
            }
        }
        
        private void ApplyEnhancedLighting(VisionSettings settings)
        {
            // Brightness 0-2: much more aggressive scaling
            // 0?0.1x, 1?9x, 2?25x
            float brightnessBoost = 0.1f + (settings.Brightness * 12.0f);
            
            // Contrast boost for ambient intensity (-0.5 to 0.5 range)
            // -0.5 = 0.25x, 0 = 1.0x, 0.5 = 1.75x
            float contrastBoost = 1.0f + (settings.Contrast * 1.5f);
            contrastBoost = Mathf.Max(0.25f, contrastBoost);
            
            Color boostedAmbient = originalAmbientLight * brightnessBoost;
            
            // Higher clamp for brighter scenes
            boostedAmbient.r = Mathf.Clamp(boostedAmbient.r, 0f, 5.0f);
            boostedAmbient.g = Mathf.Clamp(boostedAmbient.g, 0f, 5.0f);
            boostedAmbient.b = Mathf.Clamp(boostedAmbient.b, 0f, 5.0f);
            
            RenderSettings.ambientLight = boostedAmbient;
            RenderSettings.ambientIntensity = originalAmbientIntensity * contrastBoost;
            
            try
            {
                RenderSettings.ambientEquatorColor = originalAmbientEquator * brightnessBoost;
                RenderSettings.ambientGroundColor = originalAmbientGround * brightnessBoost;
                RenderSettings.ambientSkyColor = originalAmbientSky * brightnessBoost;
            }
            catch { }
        }
        
        private void RestoreLighting()
        {
            if (hasStoredLighting)
            {
                RenderSettings.ambientLight = originalAmbientLight;
                RenderSettings.ambientIntensity = originalAmbientIntensity;
                
                try
                {
                    RenderSettings.ambientEquatorColor = originalAmbientEquator;
                    RenderSettings.ambientGroundColor = originalAmbientGround;
                    RenderSettings.ambientSkyColor = originalAmbientSky;
                }
                catch { }
            }
        }
        
        private bool LoadShader()
        {
            nightVisionShader = Shader.Find("KerbVisionIR/NightVision");
            if (nightVisionShader != null)
            {
                effectMaterial = new Material(nightVisionShader);
                effectMaterial.hideFlags = HideFlags.DontSave;
                return true;
            }
            
            string bundlePath = Path.Combine(KSPUtil.ApplicationRootPath, "GameData", "KerbVisionIR", "Shaders", "nightvision.ksp");
            if (File.Exists(bundlePath))
            {
                try
                {
                    AssetBundle bundle = AssetBundle.LoadFromFile(bundlePath);
                    if (bundle != null)
                    {
                        nightVisionShader = bundle.LoadAsset<Shader>("NightVisionShader");
                        if (nightVisionShader != null)
                        {
                            effectMaterial = new Material(nightVisionShader);
                            effectMaterial.hideFlags = HideFlags.DontSave;
                            return true;
                        }
                        bundle.Unload(false);
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogWarning($"[KerbVisionIR] Failed to load AssetBundle: {e.Message}");
                }
            }
            
            return false;
        }
        
        private void CreateGrainTexture()
        {
            grainTexture = new Texture2D(256, 256, TextureFormat.ARGB32, false);
            grainTexture.wrapMode = TextureWrapMode.Repeat;
            
            Color[] pixels = new Color[256 * 256];
            for (int i = 0; i < pixels.Length; i++)
            {
                float noise = (float)random.NextDouble();
                pixels[i] = new Color(noise, noise, noise, 0.1f);
            }
            
            grainTexture.SetPixels(pixels);
            grainTexture.Apply();
        }
        
        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if (KerbVisionIR.Instance == null || KerbVisionIR.Instance.Settings == null)
            {
                Graphics.Blit(source, destination);
                return;
            }
            
            VisionSettings settings = KerbVisionIR.Instance.Settings;
            
            if (!settings.IsEnabled)
            {
                Graphics.Blit(source, destination);
                return;
            }
            
            if (useCustomShader && effectMaterial != null)
            {
                // Custom shader with everything built-in
                UpdateShaderProperties(settings);
                Graphics.Blit(source, destination, effectMaterial);
            }
            else
            {
                // Fallback: Manual grayscale + color tint
                ApplyGrayscaleEffect(source, destination, settings);
            }
        }
        
        private void ApplyGrayscaleEffect(RenderTexture source, RenderTexture destination, VisionSettings settings)
        {
            // Quality setting: 1=Full, 2=High, 3=Medium, 4=Low (internal divisor)
            int divisor = settings.ProcessingQuality;
            int processWidth = source.width / divisor;
            int processHeight = source.height / divisor;
            
            // Log once every 60 frames for debugging
            if (Time.frameCount % 60 == 0)
            {
                Debug.Log($"[KerbVisionIR] Processing: {processWidth}x{processHeight} (Quality: 1/{divisor}), Mode: {settings.Mode}, Contrast: {settings.Contrast:F2}");
            }
            
            RenderTexture tempLowRes = RenderTexture.GetTemporary(processWidth, processHeight, 0, RenderTextureFormat.ARGB32);
            
            // Downsample
            Graphics.Blit(source, tempLowRes);
            
            // Convert to grayscale on CPU (low res = fast)
            Texture2D tempTex = new Texture2D(processWidth, processHeight, TextureFormat.RGB24, false);
            tempTex.filterMode = FilterMode.Bilinear;
            
            RenderTexture.active = tempLowRes;
            tempTex.ReadPixels(new Rect(0, 0, processWidth, processHeight), 0, 0);
            tempTex.Apply(false);
            
            Color[] pixels = tempTex.GetPixels();
            Color tintColor = settings.GetCurrentTint();
            float tintStrength = settings.TintStrength;
            
            // Contrast now ranges from -0.5 to 0.5 (displayed as -1 to 1 in UI)
            // -0.5 = very flat (0.1x), 0 = normal (1.0x), 0.5 = high (2.0x)
            float contrastMult = 1.0f + settings.Contrast * 2.0f; // -0.5?0.0, 0?1.0, 0.5?2.0
            contrastMult = Mathf.Max(0.1f, contrastMult); // Prevent going to zero
            
            // Parallel grayscale + tint for speed
            Parallel.For(0, pixels.Length, i =>
            {
                // Grayscale
                float gray = pixels[i].r * 0.299f + pixels[i].g * 0.587f + pixels[i].b * 0.114f;
                
                // Contrast
                gray = (gray - 0.5f) * contrastMult + 0.5f;
                gray = Mathf.Clamp01(gray);
                
                // Apply tint
                pixels[i] = new Color(
                    Mathf.Lerp(gray, gray * tintColor.r, tintStrength),
                    Mathf.Lerp(gray, gray * tintColor.g, tintStrength),
                    Mathf.Lerp(gray, gray * tintColor.b, tintStrength),
                    1f
                );
            });
            
            tempTex.SetPixels(pixels);
            tempTex.Apply(false);
            
            // Upscale back to full resolution
            Graphics.Blit(tempTex, destination);
            
            Destroy(tempTex);
            RenderTexture.ReleaseTemporary(tempLowRes);
        }
        
        private void UpdateShaderProperties(VisionSettings settings)
        {
            effectMaterial.SetFloat("_Brightness", 1.0f + settings.Brightness * 5.0f);
            effectMaterial.SetFloat("_Contrast", 1.0f + settings.Contrast * 2.0f);
            effectMaterial.SetColor("_TintColor", settings.GetCurrentTint());
            effectMaterial.SetFloat("_TintStrength", settings.TintStrength);
            effectMaterial.SetFloat("_GrainIntensity", settings.GrainIntensity);
        }
        
        private void OnGUI()
        {
            if (useCustomShader)
                return;
            
            if (KerbVisionIR.Instance == null || KerbVisionIR.Instance.Settings == null)
                return;
            
            VisionSettings settings = KerbVisionIR.Instance.Settings;
            
            if (!settings.IsEnabled)
                return;
            
            // Don't draw grain when UI is active (pause, dialogs)
            if (InputLockManager.IsAllLocked(ControlTypes.All))
                return;
            
            // Draw grain overlay - always in deepest background
            if (settings.GrainIntensity > 0.01f && grainTexture != null)
            {
                // Draw at maximum depth (behind everything)
                GUI.depth = int.MaxValue;
                
                // Base grain effect
                float baseAlpha = Mathf.Min(settings.GrainIntensity * 0.5f, 1.0f);
                GUI.color = new Color(1, 1, 1, baseAlpha);
                
                // Draw grain texture tiled across entire screen
                GUI.DrawTextureWithTexCoords(new Rect(0, 0, Screen.width, Screen.height), grainTexture, 
                    new Rect(0, 0, Screen.width / 256f, Screen.height / 256f));
                
                // Night vision artifacts for high grain (> 2.0)
                if (settings.GrainIntensity > 2.0f)
                {
                    float artifactIntensity = Mathf.Min((settings.GrainIntensity - 2.0f) * 0.5f, 1.0f); // 0.0 to 1.0, slower ramp
                    
                    // 1. Scanlines effect (horizontal lines)
                    DrawScanlines(artifactIntensity);
                    
                    // 2. Vignette darkening (edges darker)
                    DrawVignette(artifactIntensity);
                    
                    // 3. Random bright spots (phosphor burn-in effect) - MORE!
                    DrawPhosphorSpots(artifactIntensity);
                    
                    // 4. Rolling bars (like old CRT TV interference)
                    DrawRollingBars(artifactIntensity);
                }
                
                GUI.color = Color.white;
                GUI.depth = 0;
            }
        }
        
        private void DrawScanlines(float intensity)
        {
            // Horizontal scanlines like CRT/NV display
            int lineSpacing = 4; // Every 4 pixels
            GUI.color = new Color(0, 0, 0, intensity * 0.15f);
            
            for (int y = 0; y < Screen.height; y += lineSpacing)
            {
                GUI.DrawTexture(new Rect(0, y, Screen.width, 1), Texture2D.whiteTexture);
            }
        }
        
        private void DrawVignette(float intensity)
        {
            // Darken edges like real night vision optics - rectangular style
            int vignetteSize = Mathf.RoundToInt(Screen.height * 0.15f);
            GUI.color = new Color(0, 0, 0, intensity * 0.3f);
            
            // Top edge
            GUI.DrawTexture(new Rect(0, 0, Screen.width, vignetteSize), Texture2D.whiteTexture);
            // Bottom edge
            GUI.DrawTexture(new Rect(0, Screen.height - vignetteSize, Screen.width, vignetteSize), Texture2D.whiteTexture);
            // Left edge
            GUI.DrawTexture(new Rect(0, 0, vignetteSize, Screen.height), Texture2D.whiteTexture);
            // Right edge
            GUI.DrawTexture(new Rect(Screen.width - vignetteSize, 0, vignetteSize, Screen.height), Texture2D.whiteTexture);
        }
        
        private void DrawPhosphorSpots(float intensity)
        {
            // Random bright spots (phosphor artifacts in tube-based NV) - visible from grain 2.0+
            if (Time.frameCount % 3 == 0) // Update every 3 frames = even more frequent
            {
                // Start with more spots at lower intensity
                int spotCount = Mathf.RoundToInt(intensity * 25); // 0-25 spots (was 15) = MUCH MORE!
                GUI.color = new Color(1, 1, 1, intensity * 0.6f); // Brighter (was 0.5f)
                
                for (int i = 0; i < spotCount; i++)
                {
                    float x = (float)random.NextDouble() * Screen.width;
                    float y = (float)random.NextDouble() * Screen.height;
                    float size = 2f + (float)random.NextDouble() * 5f; // Larger sizes 2-7 pixels (was 1-5)
                    
                    GUI.DrawTexture(new Rect(x, y, size, size), Texture2D.whiteTexture);
                }
            }
        }
        
        private void DrawRollingBars(float intensity)
        {
            // White rolling bars falling down screen like CRT interference
            // More random, slower, more subtle
            
            int barCount = Mathf.RoundToInt(intensity * 2); // 0-2 bars at once (reduced from 3)
            
            for (int i = 0; i < barCount; i++)
            {
                // Much more random positioning and speed
                float randomOffset = (float)(random.NextDouble() * 1000); // Random seed per bar
                float speed = 50f + (float)(random.NextDouble() * 100f); // 50-150px/s (was 200-300)
                float yPos = ((Time.time + randomOffset) * speed) % (Screen.height + 200);
                
                // Add some horizontal drift for more randomness
                float xDrift = Mathf.Sin(Time.time * 0.5f + i) * 50f;
                
                int barHeight = 2 + random.Next(4); // Random height 2-6 pixels (was 3-8)
                float alpha = intensity * (0.15f + (float)random.NextDouble() * 0.2f); // 0.15-0.35 alpha (was 0.3-0.6) - more subtle
                
                // Draw more subtle horizontal bar with slight horizontal offset
                GUI.color = new Color(1, 1, 1, alpha);
                GUI.DrawTexture(new Rect(xDrift, yPos, Screen.width, barHeight), Texture2D.whiteTexture);
                
                // Draw very faint trailing edge
                GUI.color = new Color(1, 1, 1, alpha * 0.2f); // Even more subtle trail
                GUI.DrawTexture(new Rect(xDrift, yPos - barHeight * 3, Screen.width, barHeight), Texture2D.whiteTexture);
            }
        }
        
        private void OnDestroy()
        {
            RestoreLighting();
            
            if (effectMaterial != null)
                Destroy(effectMaterial);
            
            if (grayscaleMaterial != null)
                Destroy(grayscaleMaterial);
            
            if (grainTexture != null)
                Destroy(grainTexture);
            
            Debug.Log("[KerbVisionIR] VisionShaderEffect destroyed");
        }
        
        private void OnDisable()
        {
            RestoreLighting();
        }
    }
}
