using System;
using System.IO;
using UnityEngine;

namespace KerbVisionIR
{
    /// <summary>
    /// Configuration persistence for VisionSettings
    /// </summary>
    public static class VisionConfig
    {
        private static readonly string ConfigPath = Path.Combine(
            KSPUtil.ApplicationRootPath,
            "GameData", "KerbVisionIR", "PluginData", "settings.cfg"
        );
        
        /// <summary>
        /// Load settings from config file
        /// </summary>
        public static VisionSettings Load()
        {
            VisionSettings settings = VisionSettings.CreateDefault();
            
            try
            {
                if (!File.Exists(ConfigPath))
                {
                    Debug.Log("[KerbVisionIR] Config file not found, using defaults");
                    return settings;
                }
                
                ConfigNode node = ConfigNode.Load(ConfigPath);
                if (node == null)
                {
                    Debug.LogWarning("[KerbVisionIR] Failed to load config, using defaults");
                    return settings;
                }
                
                ConfigNode visionNode = node.GetNode("KERBVISION");
                if (visionNode == null)
                {
                    Debug.LogWarning("[KerbVisionIR] Invalid config structure, using defaults");
                    return settings;
                }
                
                // Parse settings
                settings.IsEnabled = ParseBool(visionNode, "isEnabled", false);
                settings.Mode = ParseEnum<VisionSettings.VisionMode>(visionNode, "mode", VisionSettings.VisionMode.GreenNV);
                settings.Brightness = ParseFloatRange(visionNode, "brightness", 1.0f, 0f, 2f);
                settings.Contrast = ParseFloatRange(visionNode, "contrast", 0.0f, -0.5f, 0.5f);
                settings.TintStrength = ParseFloat(visionNode, "tintStrength", 0.8f);
                settings.GrainIntensity = ParseFloatRange(visionNode, "grainIntensity", 0.7f, 0f, 3f);
                settings.ProcessingQuality = ParseInt(visionNode, "processingQuality", 4);
                settings.ToggleKey = ParseEnum<KeyCode>(visionNode, "toggleKey", KeyCode.BackQuote);
                
                Debug.Log("[KerbVisionIR] Settings loaded successfully");
            }
            catch (Exception ex)
            {
                Debug.LogError($"[KerbVisionIR] Error loading config: {ex.Message}");
            }
            
            return settings;
        }
        
        /// <summary>
        /// Save settings to config file
        /// </summary>
        public static void Save(VisionSettings settings)
        {
            try
            {
                // Ensure directory exists
                string dir = Path.GetDirectoryName(ConfigPath);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                
                ConfigNode rootNode = new ConfigNode();
                ConfigNode visionNode = rootNode.AddNode("KERBVISION");
                
                visionNode.AddValue("isEnabled", settings.IsEnabled);
                visionNode.AddValue("mode", settings.Mode.ToString());
                visionNode.AddValue("brightness", settings.Brightness.ToString("F3"));
                visionNode.AddValue("contrast", settings.Contrast.ToString("F3"));
                visionNode.AddValue("tintStrength", settings.TintStrength.ToString("F3"));
                visionNode.AddValue("grainIntensity", settings.GrainIntensity.ToString("F3"));
                visionNode.AddValue("processingQuality", settings.ProcessingQuality.ToString());
                visionNode.AddValue("toggleKey", settings.ToggleKey.ToString());
                
                rootNode.Save(ConfigPath);
                Debug.Log("[KerbVisionIR] Settings saved successfully");
            }
            catch (Exception ex)
            {
                Debug.LogError($"[KerbVisionIR] Error saving config: {ex.Message}");
            }
        }
        
        // Helper parsing methods
        private static bool ParseBool(ConfigNode node, string key, bool defaultValue)
        {
            if (node.HasValue(key) && bool.TryParse(node.GetValue(key), out bool result))
                return result;
            return defaultValue;
        }
        
        private static float ParseFloat(ConfigNode node, string key, float defaultValue)
        {
            if (node.HasValue(key) && float.TryParse(node.GetValue(key), out float result))
                return Mathf.Clamp01(result);
            return defaultValue;
        }
        
        private static float ParseFloatRange(ConfigNode node, string key, float defaultValue, float min, float max)
        {
            if (node.HasValue(key) && float.TryParse(node.GetValue(key), out float result))
                return Mathf.Clamp(result, min, max);
            return defaultValue;
        }
        
        private static int ParseInt(ConfigNode node, string key, int defaultValue)
        {
            if (node.HasValue(key) && int.TryParse(node.GetValue(key), out int result))
                return Mathf.Clamp(result, 1, 4);
            return defaultValue;
        }
        
        private static T ParseEnum<T>(ConfigNode node, string key, T defaultValue) where T : struct
        {
            if (node.HasValue(key) && Enum.TryParse<T>(node.GetValue(key), out T result))
                return result;
            return defaultValue;
        }
    }
}
