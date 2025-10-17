using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ViewPlayer : MonoBehaviour
{
    public Collider _collider;
    public Rigidbody _rb;
    public GameObject _player;
    public Canvas ViewPlayerCanvas;
    public static bool isViewMode = false; 
    void Awake()
    {
        _player = GameObject.Find("FirstPersonController");
        if (_player == null)
        {
            return;
        }
        _collider = _player.GetComponent<Collider>();
        _rb = _player.GetComponent<Rigidbody>();
        if (_rb == null)
        {
            Debug.LogError("Player không có Rigidbody!");
        }
    }
    void Start()
    {
        // Cursor.lockState = CursorLockMode.None;
        // Cursor.visible = true;
        ViewPlayerCanvas.enabled = true;
        Time.timeScale = 0f;
        // View();
    }

    void Update()
    {

    }

    public void View()
    {
        isViewMode = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (_collider != null) _collider.enabled = false;
        if (_rb != null) _rb.constraints |= RigidbodyConstraints.FreezePositionY;
        Time.timeScale = 1f;
    }
    public void Player()
    {
        isViewMode = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (_collider != null) _collider.enabled = true;
        if (_rb != null) _rb.constraints |= RigidbodyConstraints.FreezePositionY;
        Time.timeScale = 1f;
    }

    public void Replay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Map2"); // Đổi "MainScene" thành tên scene của bạn
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Thoát Play Mode khi chạy trong Editor
        #else
            Application.Quit(); // Thoát game khi build
        #endif
    }
    }
