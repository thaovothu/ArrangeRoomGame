using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockController : MonoBehaviour
{
    public GameObject hourHand; // Đồng hồ kim giờ
    public GameObject minuteHand;
    public GameObject secondHand;
    string oldSeconds;

    private Vector3 offset;
    private float zCoord;
    private bool isDragging = false;

    void OnMouseDown()
    {
        zCoord = Camera.main.WorldToScreenPoint(transform.position).z;
        offset = transform.position - GetMouseWorldPos();
        isDragging = true;
    }
    void OnMouseUp()
    {
        isDragging = false;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        string seconds = System.DateTime.UtcNow.ToString("ss");
        if (seconds != oldSeconds)
        {
            UpdateTime();
        }
        oldSeconds = seconds;

        if (isDragging && Input.GetMouseButton(0))
        {
        float minX = 0.5f, maxX = 5f; // Giới hạn X (điều chỉnh theo ý bạn)
        float minY = 0.6f, maxY = 2.45f; // Giới hạn Y (điều chỉnh theo ý bạn)
        Vector3 targetPos = GetMouseWorldPos() + offset;
        targetPos.z = transform.position.z; // Giữ nguyên Z
        targetPos.x = Mathf.Clamp(targetPos.x, minX, maxX);
        targetPos.y = Mathf.Clamp(targetPos.y, minY, maxY);
        transform.position = targetPos;
        }
    }
    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    void UpdateTime()
    {
        int secondsInt = int.Parse(System.DateTime.UtcNow.ToString("ss"));
        int minutesInt = int.Parse(System.DateTime.UtcNow.ToString("mm"));
        int hoursInt = int.Parse(System.DateTime.UtcNow.ToLocalTime().ToString("hh"));
        Debug.Log(hoursInt + ":" + minutesInt + ":" + secondsInt);
        iTween.RotateTo(secondHand, iTween.Hash("z", secondsInt * 6, "time", 1, "easetype", "easeOutQuint"));
        iTween.RotateTo(minuteHand, iTween.Hash("z", minutesInt * 6, "time", 1, "easetype", "easeOutElastic"));
        float hourDistance = (float) (minutesInt)/60.0f; // Mỗi phút là 30 độ
        iTween.RotateTo(hourHand, iTween.Hash("z", (hoursInt + hourDistance) * 360 /12, "time", 1, "easetype", "easeInOutSine"));
    }
}
