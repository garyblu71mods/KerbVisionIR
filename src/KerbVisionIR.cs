using UnityEngine;

namespace KerbVisionIR
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class KerbVisionIR : MonoBehaviour
    {
        public static KerbVisionIR Instance { get; private set; }
        
        public VisionSettings Settings { get; private set; }
        
        private VisionShaderEffect shaderEffect;
        private VisionSettingsWindow settingsWindow;
        private ToolbarButton toolbarButton;
        
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }
            
            Instance = this;
            Debug.Log($"[KerbVisionIR] Initializing... Version: {VisionSettings.VERSION} Build: {VisionSettings.BUILD_DATE}");
            
            // Load settings
            Settings = VisionConfig.Load();
            
            // Debug log to verify hotkey
            Debug.Log($"[KerbVisionIR] Loaded hotkey: {Settings.ToggleKey} (RequireAlt: {Settings.RequireAlt})");
            
            // Initialize UI components on this GameObject
            settingsWindow = gameObject.AddComponent<VisionSettingsWindow>();
            toolbarButton = gameObject.AddComponent<ToolbarButton>();
            
            Debug.Log("[KerbVisionIR] Initialized successfully");
        }
        
        private void Start()
        {
            // Add shader effect to main camera (must be done in Start, not Awake)
            Camera mainCamera = Camera.main;
            if (mainCamera != null)
            {
                shaderEffect = mainCamera.gameObject.AddComponent<VisionShaderEffect>();
                Debug.Log("[KerbVisionIR] Shader effect added to camera");
            }
            else
            {
                Debug.LogError("[KerbVisionIR] Could not find main camera!");
            }
        }
        
        private void Update()
        {
            // Handle hotkey - Alt + backtick
            if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
            {
                if (Input.GetKeyDown(Settings.ToggleKey))
                {
                    ToggleEffect();
                }
            }
        }
        
        private void OnDestroy()
        {
            Debug.Log("[KerbVisionIR] Shutting down...");
            
            // Save settings
            if (Settings != null)
            {
                VisionConfig.Save(Settings);
            }
            
            if (Instance == this)
            {
                Instance = null;
            }
        }
        
        /// <summary>
        /// Toggle the night vision effect on/off
        /// </summary>
        public void ToggleEffect()
        {
            bool wasEnabled = Settings.IsEnabled;
            Settings.IsEnabled = !Settings.IsEnabled;
            
            // Play sound when turning ON
            if (!wasEnabled && Settings.IsEnabled)
            {
                PlayNVOnSound();
            }
            
            // If turning OFF, restore lighting immediately
            if (wasEnabled && !Settings.IsEnabled && shaderEffect != null)
            {
                // Force restore lighting when disabling
                var restoreMethod = shaderEffect.GetType().GetMethod("RestoreLighting", 
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                restoreMethod?.Invoke(shaderEffect, null);
            }
            
            Debug.Log($"[KerbVisionIR] Effect {(Settings.IsEnabled ? "enabled" : "disabled")}");
        }
        
        private void PlayNVOnSound()
        {
            // Use KSP's audio system via GameDatabase and standard Unity AudioSource
            string soundPath = "KerbVisionIR/Sounds/NVon";
            
            try
            {
                // Check if sound exists in GameDatabase
                if (GameDatabase.Instance != null && GameDatabase.Instance.ExistsAudioClip(soundPath))
                {
                    // Get the audio clip from GameDatabase
                    var clip = GameDatabase.Instance.GetAudioClip(soundPath);
                    
                    if (clip != null)
                    {
                        // Create a temporary GameObject with AudioSource
                        GameObject audioObject = new GameObject("KerbVisionIR_Audio");
                        var audioSource = audioObject.AddComponent(typeof(AudioSource)) as AudioSource;
                        
                        if (audioSource != null)
                        {
                            audioSource.clip = clip;
                            audioSource.volume = GameSettings.UI_VOLUME;
                            audioSource.spatialBlend = 0f; // 2D sound
                            audioSource.playOnAwake = false; // Don't auto-play
                            audioSource.PlayOneShot(clip); // Immediate playback
                            
                            // Destroy immediately after sound finishes
                            Destroy(audioObject, clip.length);
                            
                            Debug.Log("[KerbVisionIR] Playing NV activation sound");
                        }
                    }
                }
                else
                {
                    Debug.LogWarning($"[KerbVisionIR] Sound not found in GameDatabase: {soundPath}");
                    Debug.LogWarning("[KerbVisionIR] Make sure NVon.wav is in GameData/KerbVisionIR/Sounds/");
                }
            }
            catch (System.Exception e)
            {
                Debug.LogWarning($"[KerbVisionIR] Failed to play sound: {e.Message}");
            }
        }
        
        /// <summary>
        /// Toggle settings window visibility
        /// </summary>
        public void ToggleSettingsWindow()
        {
            if (settingsWindow != null)
            {
                settingsWindow.ToggleWindow();
            }
        }
    }
}
