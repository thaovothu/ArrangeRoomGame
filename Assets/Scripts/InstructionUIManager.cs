using UnityEngine;
using UnityEngine.UI;

public class InstructionUIManager : MonoBehaviour
{
    public Canvas instructionPanel;
    public Button startButton;
    public FirstPersonController playerController;

    void Awake()
    {
        
        // Cursor.lockState = CursorLockMode.None;
        // Cursor.visible = true;
        instructionPanel.enabled = true;
       

        if (playerController != null)
        {
            playerController.cameraCanMove = false;
            playerController.playerCanMove = false;
        }
        
        startButton.onClick.AddListener(OnStartGame);
    }

    public void OnStartGame()
    {
        instructionPanel.enabled = false;
        Invoke(nameof(EnableControl), 0.1f);
    }

    public void EnableControl()
    {
        if (playerController != null)
        {
            playerController.cameraCanMove = true;
            playerController.playerCanMove = true;
        }
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
    }
    
    public void ShowInstruction()
{
    instructionPanel.enabled = true;
   
    // Cursor.lockState = CursorLockMode.None;
    // Cursor.visible = true;
    if (playerController != null)
    {
        playerController.cameraCanMove = false;
        playerController.playerCanMove = false;
    }
}

public void HideInstruction()
{
    instructionPanel.enabled = false;
    // Có thể bật MenuCanvas nếu muốn, hoặc giữ nguyên
}
}

