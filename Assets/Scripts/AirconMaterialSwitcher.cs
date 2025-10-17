using UnityEngine;

public class AirconMaterialSwitcher : MonoBehaviour
{
    public Material materialA;
    public Material materialB;
    private bool isAltMaterial = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            SwitchMaterial();
        }
    }

    // Hàm này gọi từ UI Button hoặc object 3D
    public void SwitchMaterial()
    {
        isAltMaterial = !isAltMaterial;
        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
        {
            rend.material = isAltMaterial ? materialB : materialA;
        }
    }
}