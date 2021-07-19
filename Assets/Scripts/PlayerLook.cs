using Cinemachine;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [Header("Camera Objects")]
    [SerializeField] private Transform cameraTarget;
    
    [Header("Mouse Settings")]
    [SerializeField] private float mouseSensivity = 100f;
    [SerializeField] private Vector2 fovBounds;

    private Vector2 _mouse;
    private float _mouseScrollMultiplier = 10f;
    private CinemachineVirtualCamera _cameraComponent;

    private void Start()
    {
        _mouse = Vector2.zero;
        SetCursorActive(false);

        _cameraComponent = FindObjectOfType<CinemachineVirtualCamera>();
        _cameraComponent.Follow = cameraTarget;
        _cameraComponent.LookAt = cameraTarget;
    }
    
    private void Update()
    {
        _mouse += new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * (mouseSensivity * Time.deltaTime);
        _mouse.y = Mathf.Clamp(_mouse.y, -80f, 80f);
        
        transform.rotation = Quaternion.Euler(0f, _mouse.x, 0f);
        cameraTarget.localRotation = Quaternion.Euler(-_mouse.y, 0f, 0f);
        
        _cameraComponent.m_Lens.FieldOfView = 
            Mathf.Clamp(_cameraComponent.m_Lens.FieldOfView - Input.mouseScrollDelta.y * _mouseScrollMultiplier, 
                fovBounds.x, fovBounds.y);
    }

    public static void SetCursorActive(bool isActive)
    {
        Cursor.lockState = isActive ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isActive;
    }
}