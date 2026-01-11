using UnityEngine;

namespace KerbVisionIR
{
    /// <summary>
    /// Settings data model for Night Vision/IR effects
    /// </summary>
    public class VisionSettings
    {
        // Version info
        public const string VERSION = "v0.4.4";
        public const string BUILD_DATE = "2025-01-09";
        public const string TEST_BUILD_FOR = "bane_iz_missing";
        
        // Main toggle
        public bool IsEnabled = false;
        
        // Vision mode
        public enum VisionMode
        {
            Monochrome,
            GreenNV,
            AmberWarm
        }
        public VisionMode Mode = VisionMode.GreenNV;
        
        // Effect parameters
        public float Brightness = 1.0f;    // Brightness multiplier (0-2, default 1.0 = normal)
        public float Contrast = 0.0f;      // Contrast adjustment (-0.5 to 0.5 actual, displayed as -1 to 1)
        public float TintStrength = 0.8f;  // Color overlay strength (0-1)
        public float GrainIntensity = 0.7f; // Noise/grain amount (0-3, increased range)
        
        // Performance setting
        public int ProcessingQuality = 4;  // 1=full res, 2=1/2, 3=1/3, 4=1/4 (default)
        
        // Hotkey - Alt + `
        public KeyCode ToggleKey = KeyCode.BackQuote;
        public bool RequireAlt = true;
        
        // Color tints for different modes
        public Color MonochromeTint => new Color(1f, 1f, 1f, 1f);
        public Color GreenNVTint => new Color(0.2f, 1.0f, 0.2f, 1f);
        public Color AmberWarmTint => new Color(1.0f, 0.8f, 0.4f, 1f);
        
        /// <summary>
        /// Get current tint color based on mode
        /// </summary>
        public Color GetCurrentTint()
        {
            switch (Mode)
            {
                case VisionMode.Monochrome:
                    return MonochromeTint;
                case VisionMode.GreenNV:
                    return GreenNVTint;
                case VisionMode.AmberWarm:
                    return AmberWarmTint;
                default:
                    return Color.white;
            }
        }
        
        /// <summary>
        /// Create default settings
        /// </summary>
        public static VisionSettings CreateDefault()
        {
            return new VisionSettings
            {
                IsEnabled = false,
                Mode = VisionMode.GreenNV,
                Brightness = 1.0f,
                Contrast = 0.0f,
                TintStrength = 0.8f,
                GrainIntensity = 0.7f,
                ProcessingQuality = 4,
                ToggleKey = KeyCode.BackQuote,
                RequireAlt = true
            };
        }
        
        /// <summary>
        /// Clone settings
        /// </summary>
        public VisionSettings Clone()
        {
            return (VisionSettings)this.MemberwiseClone();
        }
    }
}
