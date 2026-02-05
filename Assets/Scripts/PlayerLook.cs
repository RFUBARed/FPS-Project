using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerLook : MonoBehaviour
{
    public static PlayerLook Instance;
    
    public float mouseSensitivity = 50f;
    public Transform cam;

    private float xRotation=0f;
    private Vector2 lookInput;

    private float shakeDuration = 0f;
    private float shakeMagnitude = 0.1f;
    private float shakeFadeSpeed = 1.5f;
    private Vector3 initialCamPos;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        initialCamPos = cam.localPosition;
    }

    void OnLook(InputValue value)
    { 
        lookInput=value.Get<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMouseLook();
        HandleShake();
    }

    void HandleMouseLook()
    {
        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY= lookInput.y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cam.localRotation=Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);

    }
    void HandleShake()
    {
        if (shakeDuration > 0)
        {
            cam.localPosition = initialCamPos + Random.insideUnitSphere * shakeMagnitude;
            shakeDuration -= Time.deltaTime * shakeFadeSpeed;
        }
        else 
        {
            cam.localPosition = initialCamPos;
        }
    }
    public void AddShake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude; 
    }

}
