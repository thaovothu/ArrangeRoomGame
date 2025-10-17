using UnityEngine;

public class DragTable : MonoBehaviour
{
    private Vector3 offset;

    private float zCoord;
    private bool isDragging = false;
    private bool isHovered = false;

    public float liftHeight = 0f;  // Độ nâng bàn khi nhấn
    private Rigidbody rb;

    public float rotationSpeed = 500f; // Tăng tốc độ xoay để cảm nhận rõ hơn

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogWarning("Table is missing Rigidbody component.");
        }
        else if (gameObject.CompareTag("keepY"))
        {
            // Đóng băng xoay và vị trí Y để bàn không bị lún theo vật khác
            rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    void OnMouseEnter()
    {
        isHovered = true;
    }

    void OnMouseExit()
    {
        isHovered = false;
    }
    void OnMouseDown()
    {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        if (audioManager != null)
        {
            audioManager.PlayOnMouseDown();
        }
        if (ViewPlayer.isViewMode) return;
        if (gameObject.CompareTag("changeY")) rb.isKinematic = true;
        
        zCoord = Camera.main.WorldToScreenPoint(transform.position).z;
        offset = transform.position - GetMouseWorldPos();
        isDragging = true;
    }
    void OnMouseUp()
    {
        if (ViewPlayer.isViewMode) return;
        isDragging = false;

        // Không cần đổi constraints, luôn để FreezeRotation
        if (gameObject.CompareTag("changeY"))
        {
            AudioManager audioManager = FindObjectOfType<AudioManager>();
            if (audioManager != null)
            {
                audioManager.PlayOnMouseUp();
            }
        }
        rb.isKinematic = false;
        rb.velocity = Vector3.zero;      // Dừng bàn lại
        rb.angularVelocity = Vector3.zero;
    }
    void Update()
    {
    // Kéo object bằng chuột trái
    if (isDragging && Input.GetMouseButton(0))
    {
        float minX = -3.8f, maxX = 5f;
        float minZ = -3.5f, maxZ = 3.75f;
        float minY = 0.58f, maxY = 2.67f;
        Vector3 targetPos = GetMouseWorldPos();

        if (gameObject.CompareTag("keepY"))
        {
            targetPos.y = transform.position.y;
        }
        else targetPos.y = Mathf.Clamp(targetPos.y, minY, maxY);

        targetPos.x = Mathf.Clamp(targetPos.x, minX, maxX);
        targetPos.z = Mathf.Clamp(targetPos.z, minZ, maxZ);

        transform.position = targetPos;
    }
    if (isHovered && Input.GetMouseButtonDown(1))
    {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        if (audioManager != null)
        {
            audioManager.PlayOnMouseDown();
        }
    }
    if (isHovered && Input.GetMouseButton(1))
    {
        if (ViewPlayer.isViewMode) return;
        float mouseX = Input.GetAxis("Mouse X");
        float rotationY = mouseX * -rotationSpeed * Time.deltaTime;
        transform.Rotate(0f, rotationY, 0f, Space.World);
    }
    }
    Vector3 GetMouseWorldPos()
    {
        // Lấy vị trí chuột trên màn hình (pixel)
        Vector3 mousePoint = Input.mousePosition;
        // Đặt khoảng cách từ camera đến object (theo trục Z)
        mousePoint.z = zCoord;
        // Chuyển sang tọa độ 3D
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.CompareTag("changeY") && collision.gameObject.CompareTag("keepY"))
        {
            // Kiểm tra va chạm từ phía dưới (bề mặt dưới của changeY chạm keepY)
            // Giả sử hướng lên là Vector3.up
            foreach (ContactPoint contact in collision.contacts)
            {
                if (Vector3.Dot(contact.normal, Vector3.up) > 0.1f)
                {
                    transform.SetParent(collision.transform);
                    break;
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (gameObject.CompareTag("changeY") && collision.gameObject.CompareTag("keepY"))
        {
            // Bỏ parent khi không còn chạm
            transform.SetParent(null);
        }
    }
}