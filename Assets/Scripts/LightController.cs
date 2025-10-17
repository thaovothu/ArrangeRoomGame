// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class On : MonoBehaviour
// {
//     [SerializeField] private Light lightObject; 
//     [SerializeField] private GameObject glassObject; 
//     AudioManager audioManager;
//     void Start()
//     {
//         lightObject = GetComponent<Light>();
//         audioManager = FindObjectOfType<AudioManager>();
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if (ViewPlayer.isViewMode )
//         {
//             return; // Không thực hiện gì nếu đang ở chế độ xem camera
//         }
//         if (Input.GetKeyDown(KeyCode.O))
//         {
//             if (audioManager != null)
//             {
//                 audioManager.PlayLight(); // Phát âm thanh khi bật/tắt đèn
//             }
//             lightObject.enabled = !lightObject.enabled;
//             glassObject.SetActive(!glassObject.activeSelf);
//         }
//     }
// }


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class On : MonoBehaviour
{
    [SerializeField] private Light[] lightObject; 
    [SerializeField] private GameObject[] glassObject; 
    [SerializeField] private Renderer buttonRenderer;
    [SerializeField] private Material buttonMaterialA;
    [SerializeField] private Material buttonMaterialB;
    private bool isAltMaterial = false;

    AudioManager audioManager;

    void Start()
{
    audioManager = FindObjectOfType<AudioManager>();
    // Tắt tất cả light và glass khi bắt đầu
    foreach (var light in lightObject)
    {
        light.enabled = false;
    }
    foreach (var glass in glassObject)
    {
        glass.SetActive(false);
    }
    // Đặt material mặc định cho button nếu muốn
    if (buttonRenderer != null && buttonMaterialA != null)
    {
        buttonRenderer.material = buttonMaterialA;
    }
}
    void Update()
    {
        if (ViewPlayer.isViewMode)
            return;

        if (Input.GetKeyDown(KeyCode.O))
        {
            ToggleLight();
        }
    }

    // Hàm này gọi từ UI Button hoặc object 3D
    public void ToggleLight()
{
    if (audioManager != null)
    {
        audioManager.PlayLight();
    }
    foreach (var light in lightObject)
    {
        light.enabled = !light.enabled;
    }
    foreach (var glass in glassObject)
    {
        glass.SetActive(!glass.activeSelf);
    }

    // Đổi material của button
    isAltMaterial = !isAltMaterial;
    if (buttonRenderer != null)
    {
        buttonRenderer.material = isAltMaterial ? buttonMaterialB : buttonMaterialA;
    }
}

// Nếu là object 3D có Collider, dùng OnMouseDown
void OnMouseDown()
{
    if (audioManager != null)
    {
        audioManager.PlayOnMouseDown();
    }
    if (!ViewPlayer.isViewMode)
    {
        ToggleLight();
    }
}}