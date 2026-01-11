using UnityEngine;

namespace KerbVisionIR
{
    /// <summary>
    /// Settings window UI - real-time updates
    /// </summary>
    public class VisionSettingsWindow : MonoBehaviour
    {
        private bool windowVisible = false;
        private Rect windowRect = new Rect(100, 100, 380, 420);
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
            
            // Header: Version and build date
            GUILayout.BeginHorizontal();
            GUILayout.Label($"KerbVision IR - {VisionSettings.VERSION}", HighLogic.Skin.label);
            GUILayout.FlexibleSpace();
            GUILayout.Label(VisionSettings.BUILD_DATE, HighLogic.Skin.label);
            GUILayout.EndHorizontal();
            
            GUILayout.Space(5);
            
            // Hotkey info
            string keyName = settings.ToggleKey == KeyCode.BackQuote ? "`" : settings.ToggleKey.ToString();
            GUILayout.Label($"Toggle: Alt + {keyName}", HighLogic.Skin.label);
            
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
            GUILayout.BeginHorizontal();
            GUILayout.Label("Vision Mode:", GUILayout.Width(120));
            
            string[] modeNames = System.Enum.GetNames(typeof(VisionSettings.VisionMode));
            int currentIndex = (int)settings.Mode;
            int newIndex = GUILayout.SelectionGrid(currentIndex, modeNames, 1, GUILayout.Width(200));
            
            if (newIndex != currentIndex)
            {
                settings.Mode = (VisionSettings.VisionMode)newIndex;
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
            
            // Contrast slider - REAL TIME (-1 to 1 displayed, -0.5 to 0.5 actual)
            GUILayout.BeginHorizontal();
            float displayContrast = settings.Contrast * 2f;
            GUILayout.Label($"Contrast: {displayContrast:F2}", GUILayout.Width(150));
            float newDisplayContrast = GUILayout.HorizontalSlider(displayContrast, -1f, 1f, GUILayout.Width(180));
            float newContrast = newDisplayContrast * 0.5f;
            if (Mathf.Abs(newContrast - settings.Contrast) > 0.001f)
            {
                settings.Contrast = newContrast;
            }
            GUILayout.EndHorizontal();
            
            // Tint Strength slider - REAL TIME
            GUILayout.BeginHorizontal();
            GUILayout.Label($"Color Tint: {settings.TintStrength:F2}", GUILayout.Width(150));
            float newTint = GUILayout.HorizontalSlider(settings.TintStrength, 0f, 1f, GUILayout.Width(180));
            if (Mathf.Abs(newTint - settings.TintStrength) > 0.001f)
            {
                settings.TintStrength = newTint;
            }
            GUILayout.EndHorizontal();
            
            // Grain/Noise slider - REAL TIME (0 to 4)
            GUILayout.BeginHorizontal();
            GUILayout.Label($"Grain/Noise: {settings.GrainIntensity:F2}", GUILayout.Width(150));
            float newGrain = GUILayout.HorizontalSlider(settings.GrainIntensity, 0f, 4f, GUILayout.Width(180));
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
            int qualityIndex = settings.ProcessingQuality - 1;
            GUILayout.Label($"Quality: {qualityLabels[qualityIndex]}", GUILayout.Width(150));
            int newQualityIndex = Mathf.RoundToInt(GUILayout.HorizontalSlider(qualityIndex, 0f, 3f, GUILayout.Width(180)));
            if (newQualityIndex != qualityIndex)
            {
                settings.ProcessingQuality = newQualityIndex + 1;
            }
            GUILayout.EndHorizontal();
            GUILayout.Label("  WARNING: Higher quality = MUCH lower FPS!", HighLogic.Skin.label);
            
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
            
            GUILayout.FlexibleSpace();
            
            if (GUILayout.Button("Save & Close", GUILayout.Height(30)))
            {
                SaveSettings();
                ToggleWindow();
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
