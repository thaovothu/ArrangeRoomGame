using UnityEngine;
using UnityEngine.UI;

public class ReticleController : MonoBehaviour
{
    public Image dotCanvas;   // Image nút chấm
    public Image handCanvas;  // Image nút tay
    public float rayDistance = 5f;

    private Outline lastOutline;

    // void Update()
    // {
    //     if (ViewPlayer.isViewMode)
    //     {
    //         dotCanvas.enabled = false;
    //         handCanvas.enabled = false;
    //         if (lastOutline != null)
    //         {
    //             lastOutline.enabled = false;
    //             lastOutline = null;
    //         }

    //     }
    //     Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));
    //     RaycastHit hit;
    //     if (Physics.Raycast(ray, out hit, rayDistance))
    //     {
    //         if (hit.collider.CompareTag("keepY") || hit.collider.CompareTag("changeY"))
    //         {
    //             dotCanvas.enabled = false;
    //             handCanvas.enabled = true;

    //             // Bật Outline cho object đang hover
    //             Outline outline = hit.collider.GetComponent<Outline>();
    //             if (outline != null)
    //             {
    //                 outline.enabled = true;
    //                 // Tắt Outline object cũ nếu khác object mới
    //                 if (lastOutline != null && lastOutline != outline)
    //                     lastOutline.enabled = false;
    //                 lastOutline = outline;
    //             }
    //             else if (lastOutline != null)
    //             {
    //                 lastOutline.enabled = false;
    //                 lastOutline = null;
    //             }
    //         }
    //         else
    //         {
    //             dotCanvas.enabled = true;
    //             handCanvas.enabled = false;
    //             // Tắt Outline nếu không hover object đúng tag
    //             if (lastOutline != null)
    //             {
    //                 lastOutline.enabled = false;
    //                 lastOutline = null;
    //             }
    //         }
    //     }
    //     else
    //     {
    //         dotCanvas.enabled = true;
    //         handCanvas.enabled = false;
    //         // Tắt Outline nếu không raycast trúng gì
    //         if (lastOutline != null)
    //         {
    //             lastOutline.enabled = false;
    //             lastOutline = null;
    //         }
    //     }
    // }
    void Update()
{
    if (ViewPlayer.isViewMode)
    {
        dotCanvas.enabled = false;
        handCanvas.enabled = false;
    }

    // Raycast theo vị trí con trỏ chuột thay vì tâm màn hình
    Vector3 rayOrigin = ViewPlayer.isViewMode ? Input.mousePosition : new Vector3(Screen.width / 2f, Screen.height / 2f);
    Ray ray = Camera.main.ScreenPointToRay(rayOrigin);
    RaycastHit hit;
    if (Physics.Raycast(ray, out hit, rayDistance))
    {
        if (hit.collider.CompareTag("keepY") || hit.collider.CompareTag("changeY") || hit.collider.CompareTag("Tivi") ||hit.collider.CompareTag("Clock"))
        {
            // Ở View mode: không hiện handCanvas, chỉ bật Outline
            if (!ViewPlayer.isViewMode)
            {
                dotCanvas.enabled = false;
                handCanvas.enabled = true;
            }

            // Bật Outline cho object đang hover
            Outline outline = hit.collider.GetComponent<Outline>();
            if (outline != null)
            {
                outline.enabled = true;
                if (lastOutline != null && lastOutline != outline)
                    lastOutline.enabled = false;
                lastOutline = outline;
            }
            else if (lastOutline != null)
            {
                lastOutline.enabled = false;
                lastOutline = null;
            }
        }
        else
        {
            if (!ViewPlayer.isViewMode)
            {
                dotCanvas.enabled = true;
                handCanvas.enabled = false;
            }
            if (lastOutline != null)
            {
                lastOutline.enabled = false;
                lastOutline = null;
            }
        }
    }
    else
    {
        if (!ViewPlayer.isViewMode)
        {
            dotCanvas.enabled = true;
            handCanvas.enabled = false;
        }
        if (lastOutline != null)
        {
            lastOutline.enabled = false;
            lastOutline = null;
        }
    }
}
}