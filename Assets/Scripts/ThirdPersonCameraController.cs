// using UnityEngine;

// public class ThirdPersonCameraController : MonoBehaviour
// {
//     public Transform target; // Kéo Player vào đây
//     public float distance = 2f;
//     public float mouseSensitivity = 1f;
//     public float minY = -30f;
//     public float maxY = 10f;
//     public float heightOffset = 1.7f; // Thêm biến này, chỉnh cho phù hợp với chiều cao đầu player

//     private float yaw = 0f;
//     private float pitch = 10f;

//     void Start()
//     {
//         Vector3 angles = transform.eulerAngles;
//         yaw = angles.y;
//         pitch = angles.x;
//         Cursor.lockState = CursorLockMode.Locked;
//         Cursor.visible = false;
//     }

//     void LateUpdate()
//     {
//         yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
//         pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
//         pitch = Mathf.Clamp(pitch, minY, maxY);

//         // Xoay camera quanh target, cộng thêm offset chiều cao
//         Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
//         Vector3 targetPos = target.position + Vector3.up * heightOffset;
//         Vector3 position = targetPos - (rotation * Vector3.forward * distance);

//         transform.position = position;
//         transform.LookAt(targetPos);
//     }
// }

using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    public Transform target; // Player
    public float distance = 2f;
    public float mouseSensitivity = 1f;
    public float maxLookAngle = 50f;
    public float minLookAngle = -50f;
    public float heightOffset = 1.7f;

    private float yaw = 0f;
    private float pitch = 0f;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;
        
    }

    void LateUpdate()
    {
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, minLookAngle, maxLookAngle);

        // Tính vị trí camera phía sau player
        // Vector3 targetPos = target.position + Vector3.up * heightOffset;
        Vector3 targetPos = target.position + Vector3.up * heightOffset;
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 camOffset = rotation * new Vector3(0, 0, -distance);
        transform.position = targetPos + camOffset;
        transform.LookAt(targetPos);
    }
}