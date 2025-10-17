using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private Camera Camera1;
    [SerializeField] private Camera Camera2;
    [SerializeField] private Camera CameraL1;
    [SerializeField] private Camera CameraL2;
    [SerializeField] private Camera CameraR1;
    [SerializeField] private Camera CameraR2;
    [SerializeField] private Canvas ViewPlayerCanvas;
    public static bool isViewCamera = false;
    private bool isViewCanvasActive = false;

    private bool isMainCamera = true;
    private int currentSideCam = 0;
    private Camera[] sideCams;

    // void Start()
    // {
    //     sideCams = new Camera[] { CameraL1, CameraL2, CameraR1, CameraR2 };
    //     UseMainCamera();
    // }
    void Start()
    {
        sideCams = new Camera[] { CameraL1, CameraL2, CameraR1, CameraR2 };
        UseMainCamera();
        
    }

    void Update()
    {
        if (ViewPlayer.isViewMode)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            UseMainCamera();
        }
        // Đổi giữa Camera1 và Camera2 bằng phím C
        if (Input.GetKeyDown(KeyCode.C))
        {
            //isViewCamera = false;

            if (!ViewPlayer.isViewMode)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                if (isMainCamera)
                    UseCamera2();
                else
                    UseMainCamera();
            }
        }

        // Đổi giữa 4 camera bên bằng phím V
        if (Input.GetKeyDown(KeyCode.V))
        {
            //isViewCamera = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            UseSideCamera((currentSideCam + 1) % sideCams.Length);
        }

        // if (Input.GetKeyDown(KeyCode.Escape))
        // {
        //     Cursor.lockState = CursorLockMode.None;
        //     Cursor.visible = true;
        //     ViewPlayerCanvas.gameObject.SetActive(true);
        //     Debug.Log("ViewPlayerCanvas enabled");
        //     Time.timeScale = 0f; // Dừng thời gian khi vào chế độ xem
        // }
        if (Input.GetKeyDown(KeyCode.Space))
        {
        isViewCanvasActive = !isViewCanvasActive;
        ViewPlayerCanvas.gameObject.SetActive(isViewCanvasActive);

        if (isViewCanvasActive)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Debug.Log("ViewPlayerCanvas enabled");
            Time.timeScale = 0f; // Dừng thời gian khi vào chế độ xem
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Debug.Log("ViewPlayerCanvas disabled");
            Time.timeScale = 1f; // Tiếp tục thời gian khi thoát chế độ xem
        }
        }
}
    public void UseMainCamera()
    {
        Camera1.enabled = true;
        Camera2.enabled = false;
        foreach (var cam in sideCams) cam.enabled = false;
        isMainCamera = true;
    }

    public void UseCamera2()
    {
        Camera1.enabled = false;
        Camera2.enabled = true;
        foreach (var cam in sideCams) cam.enabled = false;
        isMainCamera = false;
    }

    public void UseSideCamera(int idx)
    {
       
        Camera1.enabled = false;
        Camera2.enabled = false;
        for (int i = 0; i < sideCams.Length; i++)
            sideCams[i].enabled = (i == idx);
        currentSideCam = idx;
        isMainCamera = false;
    }

    public bool IsMainCamera => isMainCamera;
}