using UnityEngine;

public class ClampPosition : MonoBehaviour
{
    public float minX = -3.8f, maxX = 5f;
    public float minZ = -3.5f, maxZ = 3.75f;
    public float minY = 1f, maxY = 2.75f;
    void LateUpdate()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);
        if (gameObject.CompareTag("changeY")) pos.y = Mathf.Clamp(pos.y, minY, maxY);
        else  pos.y = transform.position.y; // Giữ nguyên Y nếu là keepY
        transform.position = pos;
    }
}