using UnityEngine;
using KSP.UI.Screens;

namespace KerbVisionIR
{
    /// <summary>
    /// Toolbar button for KerbVision IR
    /// </summary>
    public class ToolbarButton : MonoBehaviour
    {
        private ApplicationLauncherButton button;
        private Texture2D buttonTexture;
        
        private void Start()
        {
            // Wait for toolbar to be ready
            GameEvents.onGUIApplicationLauncherReady.Add(OnGUIAppLauncherReady);
        }
        
        private void OnDestroy()
        {
            GameEvents.onGUIApplicationLauncherReady.Remove(OnGUIAppLauncherReady);
            
            if (button != null)
            {
                ApplicationLauncher.Instance.RemoveModApplication(button);
            }
        }
        
        private void OnGUIAppLauncherReady()
        {
            // Create button texture
            buttonTexture = CreateButtonTexture();
            
            // Add button to toolbar
            button = ApplicationLauncher.Instance.AddModApplication(
                OnButtonTrue,      // onTrue
                OnButtonFalse,     // onFalse
                null,              // onHover
                null,              // onHoverOut
                null,              // onEnable
                null,              // onDisable
                ApplicationLauncher.AppScenes.FLIGHT,
                buttonTexture
            );
        }
        
        private void OnButtonTrue()
        {
            KerbVisionIR.Instance?.ToggleSettingsWindow();
        }
        
        private void OnButtonFalse()
        {
            // Settings window handles its own visibility
        }
        
        private Texture2D CreateButtonTexture()
        {
            // Create a simple icon texture programmatically
            int size = 38;
            Texture2D texture = new Texture2D(size, size, TextureFormat.RGBA32, false);
            
            // Fill with dark background
            Color darkGreen = new Color(0.1f, 0.3f, 0.1f, 1f);
            Color brightGreen = new Color(0.3f, 1f, 0.3f, 1f);
            
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    // Create a simple "eye" or camera icon
                    float centerX = size / 2f;
                    float centerY = size / 2f;
                    float dx = x - centerX;
                    float dy = y - centerY;
                    float distance = Mathf.Sqrt(dx * dx + dy * dy);
                    
                    // Outer circle (lens)
                    if (distance < size * 0.4f && distance > size * 0.3f)
                    {
                        texture.SetPixel(x, y, brightGreen);
                    }
                    // Inner dot (lens center)
                    else if (distance < size * 0.15f)
                    {
                        texture.SetPixel(x, y, brightGreen);
                    }
                    else
                    {
                        texture.SetPixel(x, y, darkGreen);
                    }
                }
            }
            
            texture.Apply();
            return texture;
        }
    }
}
