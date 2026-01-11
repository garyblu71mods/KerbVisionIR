using UnityEngine;

namespace KerbVisionIR
{
    /// <summary>
    /// Settings window UI - real-time updates
    /// </summary>
    public class VisionSettingsWindow : MonoBehaviour
    {
        private bool windowVisible = false;
        private Rect windowRect = new Rect(100, 100, 350, 520);
        private int windowId;
        
        private void Start()
        {
            windowId = GetInstanceID();
        }
        
        private void OnGUI()
        {
            if (!windowVisible)
                return;
            
            GUI.skin = HighLogic.Skin;
            windowRect = GUILayout.Window(windowId, windowRect, DrawWindow, "KerbVision IR Settings", GUILayout.MinWidth(350));
        }
        
        private void DrawWindow(int id)
        {
            VisionSettings settings = KerbVisionIR.Instance.Settings;
            
            GUILayout.BeginVertical();
            
            // TEST BUILD INFO
            GUILayout.BeginHorizontal();
            GUILayout.Label($"TEST BUILD FOR: {VisionSettings.TEST_BUILD_FOR}", HighLogic.Skin.label);
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.Label($"Version: {VisionSettings.VERSION} | {VisionSettings.BUILD_DATE}", HighLogic.Skin.label);
            GUILayout.EndHorizontal();
            
            GUILayout.Space(10);
            
            // Main toggle
            GUILayout.BeginHorizontal();
            GUILayout.Label("Effect Enabled:", GUILayout.Width(120));
            bool enabled = GUILayout.Toggle(settings.IsEnabled, "");
            if (enabled != settings.IsEnabled)
            {
                KerbVisionIR.Instance.ToggleEffect();
            }
            GUILayout.EndHorizontal();
            
            GUILayout.Space(10);
            
            // Vision mode dropdown
            GUILayout.Label("Vision Mode:", HighLogic.Skin.label);
            
            string[] modeNames = { "Monochrome", "Green NV", "Amber/Warm" };
            int currentMode = (int)settings.Mode;
            
            GUILayout.BeginHorizontal();
            if (GUILayout.Button(modeNames[currentMode], HighLogic.Skin.button, GUILayout.Width(330)))
            {
                // Cycle through modes
                currentMode = (currentMode + 1) % modeNames.Length;
                settings.Mode = (VisionSettings.VisionMode)currentMode;
                SaveSettings();
            }
            GUILayout.EndHorizontal();
            
            GUILayout.Space(15);
            
            // Brightness slider - REAL TIME (0 to 2)
            GUILayout.BeginHorizontal();
            GUILayout.Label($"Brightness: {settings.Brightness:F2}", GUILayout.Width(150));
            float newBrightness = GUILayout.HorizontalSlider(settings.Brightness, 0f, 2f, GUILayout.Width(180));
            if (Mathf.Abs(newBrightness - settings.Brightness) > 0.001f)
            {
                settings.Brightness = newBrightness;
            }
            GUILayout.EndHorizontal();
            GUILayout.Label("  0=dark, 1=normal, 2=very bright", HighLogic.Skin.label);
            
            // Contrast slider - REAL TIME (-1 to 1 displayed, -0.5 to 0.5 actual)
            GUILayout.BeginHorizontal();
            float displayContrast = settings.Contrast * 2f; // -0.5?-1, 0.5?1
            GUILayout.Label($"Contrast: {displayContrast:F2}", GUILayout.Width(150));
            float newDisplayContrast = GUILayout.HorizontalSlider(displayContrast, -1f, 1f, GUILayout.Width(180));
            float newContrast = newDisplayContrast * 0.5f; // -1?-0.5, 1?0.5
            if (Mathf.Abs(newContrast - settings.Contrast) > 0.001f)
            {
                settings.Contrast = newContrast;
            }
            GUILayout.EndHorizontal();
            GUILayout.Label("  -1=flat, 0=normal, 1=high contrast", HighLogic.Skin.label);
            
            // Tint Strength slider - REAL TIME
            GUILayout.BeginHorizontal();
            GUILayout.Label($"Color Tint: {settings.TintStrength:F2}", GUILayout.Width(150));
            float newTint = GUILayout.HorizontalSlider(settings.TintStrength, 0f, 1f, GUILayout.Width(180));
            if (Mathf.Abs(newTint - settings.TintStrength) > 0.001f)
            {
                settings.TintStrength = newTint;
            }
            GUILayout.EndHorizontal();
            
            // Grain/Noise slider - REAL TIME (0 to 3)
            GUILayout.BeginHorizontal();
            GUILayout.Label($"Grain/Noise: {settings.GrainIntensity:F2}", GUILayout.Width(150));
            float newGrain = GUILayout.HorizontalSlider(settings.GrainIntensity, 0f, 3f, GUILayout.Width(180));
            if (Mathf.Abs(newGrain - settings.GrainIntensity) > 0.001f)
            {
                settings.GrainIntensity = newGrain;
            }
            GUILayout.EndHorizontal();
            
            GUILayout.Space(20);
            
            // === PERFORMANCE SECTION ===
            GUILayout.Label("=== PERFORMANCE ===", HighLogic.Skin.label);
            
            // Processing Quality slider
            GUILayout.BeginHorizontal();
            string[] qualityLabels = { "Full (1/1)", "High (1/2)", "Med (1/3)", "Low (1/4)" };
            int qualityIndex = settings.ProcessingQuality - 1; // 1-4 to 0-3
            GUILayout.Label($"Quality: {qualityLabels[qualityIndex]}", GUILayout.Width(150));
            int newQualityIndex = Mathf.RoundToInt(GUILayout.HorizontalSlider(qualityIndex, 0f, 3f, GUILayout.Width(180)));
            if (newQualityIndex != qualityIndex)
            {
                settings.ProcessingQuality = newQualityIndex + 1; // 0-3 to 1-4
            }
            GUILayout.EndHorizontal();
            GUILayout.Label("  ?? Higher quality = MUCH lower FPS!", HighLogic.Skin.label);
            
            GUILayout.Space(10);
            
            // Hotkey info
            string keyNameBottom = settings.ToggleKey == KeyCode.BackQuote ? "`" : settings.ToggleKey.ToString();
            GUILayout.Label($"Toggle Hotkey: Alt + {keyNameBottom}", HighLogic.Skin.label);
            
            GUILayout.Space(20);
            
            // Bottom buttons
            GUILayout.BeginHorizontal();
            
            if (GUILayout.Button("Reset to Defaults", GUILayout.Height(30)))
            {
                VisionSettings defaults = VisionSettings.CreateDefault();
                settings.Mode = defaults.Mode;
                settings.Brightness = defaults.Brightness;
                settings.Contrast = defaults.Contrast;
                settings.TintStrength = defaults.TintStrength;
                settings.GrainIntensity = defaults.GrainIntensity;
                settings.ProcessingQuality = defaults.ProcessingQuality;
                SaveSettings();
                ScreenMessages.PostScreenMessage("Settings reset to defaults", 2f, ScreenMessageStyle.UPPER_CENTER);
            }
            
            if (GUILayout.Button("Save & Close", GUILayout.Height(30)))
            {
                SaveSettings();
                windowVisible = false;
                ScreenMessages.PostScreenMessage("Settings saved", 2f, ScreenMessageStyle.UPPER_CENTER);
            }
            
            GUILayout.EndHorizontal();
            
            GUILayout.EndVertical();
            
            GUI.DragWindow();
        }
        
        private void SaveSettings()
        {
            VisionConfig.Save(KerbVisionIR.Instance.Settings);
        }
        
        public void ToggleWindow()
        {
            windowVisible = !windowVisible;
        }
        
        public bool IsVisible()
        {
            return windowVisible;
        }
    }
}
