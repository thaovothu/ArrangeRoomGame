using UnityEngine;

public class RemoteButton : MonoBehaviour
{
    AudioManager audioManager;
    public AirconMaterialSwitcher[] aircon;

    void OnMouseDown()
    {
        audioManager = FindObjectOfType<AudioManager>();
        if (audioManager != null)
        {
            audioManager.PlayOnMouseDown();
        }
        if (aircon != null)
        {
            foreach (var ac in aircon)
            {
                ac.SwitchMaterial();
            }
        }
    }
}