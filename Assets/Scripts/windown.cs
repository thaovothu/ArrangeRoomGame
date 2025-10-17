// using UnityEngine;

// public class windown : MonoBehaviour
// {
//     public float _speed = 0.5f;
//     public float maxY = 2.85f;

//     private bool isRollingUp = false; // true: cuốn lên, false: cuốn xuống
//     private bool isMoving = false;    // Để kiểm tra trạng thái chuyển động

//     private AudioSource windowAudioSource;

//     void Start()
//     {
//         transform.position = new Vector3(0, 0, 0); // Đặt vị trí ban đầu của rèm
//         // Lấy AudioSource từ AudioManager (hoặc gán trực tiếp nếu bạn muốn)
//         windowAudioSource = FindObjectOfType<AudioManager>()?.GetComponent<AudioSource>();
//     }

//     void Update()
//     {
//         if (ViewPlayer.isViewMode)
//         {
//             return;
//         }
//         if (Input.GetKeyDown(KeyCode.Q))
//         {
//             // Nếu đang ở dưới cùng thì cuốn lên, nếu đang ở trên cùng thì cuốn xuống, nếu đang cuốn thì đảo chiều
//             if (Mathf.Approximately(transform.position.y, 0f))
//                 isRollingUp = true;
//             else if (Mathf.Approximately(transform.position.y, maxY))
//                 isRollingUp = false;
//             else
//                 isRollingUp = !isRollingUp;

//             // Bắt đầu phát âm thanh khi bắt đầu cuốn
//             AudioManager audioManager = FindObjectOfType<AudioManager>();
//             if (audioManager != null)
//             {
//                 audioManager.PlayWindow();
//             }
//             isMoving = true;
//         }

//         bool wasMoving = isMoving;

//         if (isRollingUp && transform.position.y < maxY)
//         {
//             float newY = Mathf.MoveTowards(transform.position.y, maxY, _speed * Time.deltaTime);
//             transform.position = new Vector3(transform.position.x, newY, transform.position.z);
//             isMoving = true;
//         }
//         else if (!isRollingUp && transform.position.y > 0f)
//         {
//             float newY = Mathf.MoveTowards(transform.position.y, 0f, _speed * Time.deltaTime);
//             transform.position = new Vector3(transform.position.x, newY, transform.position.z);
//             isMoving = true;
//         }
//         else
//         {
//             isMoving = false;
//         }

//         // Khi dừng lại thì tắt âm thanh
//         if (wasMoving && !isMoving)
//         {
//             if (windowAudioSource != null)
//             {
//                 windowAudioSource.Stop();
//             }
//         }
//     }
// }

using UnityEngine;

public class windown : MonoBehaviour
{
    public float _speed = 0.5f;
    public float maxY = 2.85f;

    private bool isRollingUp = false; // true: cuốn lên, false: cuốn xuống
    private bool isMoving = false;    // Để kiểm tra trạng thái chuyển động

    public AudioSource windowAudioSource;

    void Start()
    {
        // Gán AudioSource riêng cho object này (nên kéo file sound vào AudioClip của AudioSource này trên Inspector)
        // windowAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (ViewPlayer.isViewMode)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Nếu đang ở dưới cùng thì cuốn lên, nếu đang ở trên cùng thì cuốn xuống, nếu đang cuốn thì đảo chiều
            if (Mathf.Approximately(transform.position.y, 0f))
                isRollingUp = true;
            else if (Mathf.Approximately(transform.position.y, maxY))
                isRollingUp = false;
            else
                isRollingUp = !isRollingUp;

            // Bắt đầu phát âm thanh lặp lại khi bắt đầu cuốn
            if (windowAudioSource != null)
            {
                windowAudioSource.loop = true;
                if (!windowAudioSource.isPlaying)
                    windowAudioSource.Play();
            }
            isMoving = true;
        }

        bool wasMoving = isMoving;

        if (isRollingUp && transform.position.y < maxY)
        {
            float newY = Mathf.MoveTowards(transform.position.y, maxY, _speed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            isMoving = true;
        }
        else if (!isRollingUp && transform.position.y > 0f)
        {
            float newY = Mathf.MoveTowards(transform.position.y, 0f, _speed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        // Khi dừng lại thì tắt lặp và dừng âm thanh
        if (wasMoving && !isMoving)
        {
            if (windowAudioSource != null)
            {
                windowAudioSource.loop = false;
                windowAudioSource.Stop();
            }
        }
    }
}