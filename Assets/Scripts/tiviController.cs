// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class tiviController : MonoBehaviour
// {
//     public bool isTiviOn = false;
//     //public GameObject tiviScreen; 
//     public GameObject lightScreen; 
//     public GameObject cyclinderScreen;
//     public GameObject videoScreen;
//     public void Start()
//     {
//         //tiviScreen.SetActive(isTiviOn);
//         lightScreen.SetActive(false);
//         cyclinderScreen.SetActive(false);
//         videoScreen.SetActive(false);
//     }

//     // Update is called once per frame
//     public void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.T))
//         {
//             isTiviOn = !isTiviOn;
//             // tiviScreen.SetActive(isTiviOn);
//             lightScreen.SetActive(isTiviOn);
//             cyclinderScreen.SetActive(isTiviOn);
//             videoScreen.SetActive(isTiviOn);
//         }

//     }
// }


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class tiviController : MonoBehaviour
// {
//     public bool isTiviOn = false;
//     //public GameObject tiviScreen; 
//     public GameObject lightScreen; 
//     public GameObject cyclinderScreen;
//     public GameObject videoScreen;
//     public Transform wallTransform; // Kéo tường vào đây trong Inspector
//     public Vector2 minMaxScale = new Vector2(0.5f, 3f); // scale nhỏ nhất/lớn nhất

//     public void Start()
//     {
//         //tiviScreen.SetActive(isTiviOn);
//         lightScreen.SetActive(false);
//         cyclinderScreen.SetActive(false);
//         videoScreen.SetActive(false);
//     }

//     // Update is called once per frame
//     public void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.T))
//         {
//             isTiviOn = !isTiviOn;
//             // tiviScreen.SetActive(isTiviOn);
//             lightScreen.SetActive(isTiviOn);
//             cyclinderScreen.SetActive(isTiviOn);
//             videoScreen.SetActive(isTiviOn);
//         }
//         if (videoScreen.activeSelf && wallTransform != null)
//     {
//         float distance = Vector3.Distance(transform.position, wallTransform.position);

//         // Quy đổi khoảng cách thành scale (bạn có thể điều chỉnh hệ số cho phù hợp)
//         float scale = Mathf.Clamp(distance, minMaxScale.x, minMaxScale.y);

//         // Scale theo X và Y (giả sử videoScreen là Quad/Plane nằm trên tường)
//         videoScreen.transform.localScale = new Vector3(scale, scale, 1f);
//     }
//     }
// }



using UnityEngine;

public class tiviController : MonoBehaviour
{
    public bool isTiviOn = false;
    public GameObject lightScreen; 
    public GameObject cyclinderScreen;
    public GameObject videoScreen;
    public Transform wallTransform; // Kéo tường vào đây trong Inspector
    public Vector2 minMaxScale = new Vector2(0.5f, 3f); // scale nhỏ nhất/lớn nhất

    private Vector3 offset;
    private float zCoord;
    private bool isDragging = false;
    AudioManager audioManager;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        lightScreen.SetActive(false);
        cyclinderScreen.SetActive(false);
        videoScreen.SetActive(false);
    }

    void OnMouseDown()
    {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        if (audioManager != null)
        {
            audioManager.PlayOnMouseDown();
        }
        if (ViewPlayer.isViewMode) return;
        zCoord = Camera.main.WorldToScreenPoint(transform.position).z;
        offset = transform.position - GetMouseWorldPos();
        isDragging = true;
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    void Update()
    {
        // Kéo máy chiếu bằng chuột trái
        if (isDragging && Input.GetMouseButton(0))
        {
            if (ViewPlayer.isViewMode) return;
            float minX = -3.8f, maxX = 4f;
            float minZ = -3.5f, maxZ = 3.75f;
            Vector3 targetPos = GetMouseWorldPos();
            targetPos.y = transform.position.y;
            targetPos.x = Mathf.Clamp(targetPos.x, minX, maxX);
            targetPos.z = Mathf.Clamp(targetPos.z, minZ, maxZ);
            transform.position = targetPos;
        }

        // Nhấn T để bật/tắt tivi và các màn hình liên quan
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (ViewPlayer.isViewMode) return;
            isTiviOn = !isTiviOn;
            lightScreen.SetActive(isTiviOn);
            cyclinderScreen.SetActive(isTiviOn);
            videoScreen.SetActive(isTiviOn);
        }

        // Tự động cập nhật videoScreen trên tường
        // if (videoScreen != null && videoScreen.activeSelf && wallTransform != null)
        // {
        //     // Lấy vị trí tường
        //     Vector3 wallPos = wallTransform.position;

        //     // Đặt videoScreen cùng Z với máy chiếu, X và Y giữ nguyên theo tường
        //     videoScreen.transform.position = new Vector3(
        //         wallPos.x,
        //         wallPos.y,
        //         wallPos.z // Z của máy chiếu
        //     );

        //     // Scale theo khoảng cách X từ máy chiếu đến tường
        //     float distanceX = Mathf.Abs(transform.position.x - wallPos.x);
        //     float scale = Mathf.Clamp(distanceX, minMaxScale.x, minMaxScale.y);
        //     videoScreen.transform.localScale = new Vector3(scale, scale, 1f);
        // }
        //     if (videoScreen != null && videoScreen.activeSelf && wallTransform != null && lightScreen != null)
        // {
        //     Vector3 wallPos = wallTransform.position;
        //     Vector3 lightPos = lightScreen.transform.position;

        //     // Đặt videoScreen trên tường, lấy Y,Z của lightScreen, X của tường
        //     videoScreen.transform.position = new Vector3(
        //         wallPos.x,           // X cố định trên tường
        //         lightPos.y,          // Y theo máy chiếu
        //         lightPos.z           // Z theo máy chiếu
        //     );

        //     Light spot = lightScreen.GetComponent<Light>();
        //     if (spot != null && spot.type == LightType.Spot)
        //     {
        //         float distance = Vector3.Distance(lightScreen.transform.position, wallPos);
        //         float angleRad = spot.spotAngle * Mathf.Deg2Rad;
        //         float size = 2f * distance * Mathf.Tan(angleRad / 2f);
        //         float scale = Mathf.Clamp(size, minMaxScale.x, minMaxScale.y);
        //         videoScreen.transform.localScale = new Vector3(scale, scale, 1f);
        //     }
        // }
if (videoScreen != null && videoScreen.activeSelf && wallTransform != null && lightScreen != null)
{
    Vector3 wallPos = wallTransform.position;
    Vector3 lightPos = lightScreen.transform.position;

    // Đặt videoScreen trên tường: X cố định, Y và Z theo lightScreen
    videoScreen.transform.position = new Vector3(
        wallPos.x,   // X cố định trên tường
        lightPos.y,  // Y theo máy chiếu
        lightPos.z   // Z theo máy chiếu
    );

    Light spot = lightScreen.GetComponent<Light>();
    if (spot != null && spot.type == LightType.Spot)
    {
        // Scale chỉ theo khoảng cách X (gần/xa tường)
        float distanceX = Mathf.Abs(lightPos.x - wallPos.x);
        float angleRad = spot.spotAngle * Mathf.Deg2Rad;
        float size = 2f * distanceX * Mathf.Tan(angleRad / 2f);
        float scale = Mathf.Clamp(size * 0.5f, minMaxScale.x, minMaxScale.y);
        videoScreen.transform.localScale = new Vector3(scale, scale, 1f);
    }
}
}
    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}