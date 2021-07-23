using UnityEngine;

public class PlayerCameraController : MonoBehaviour {
    [SerializeField] private float mouseSensitivity = 3.5f;
    [SerializeField] private Transform playerCamera;
    [SerializeField] [Range(0.0f, 0.5f)] private float smoothMouseTime = 0.3f;

    private bool lockCursor = false;
    private bool isPause = false;
    private float cameraPitch = 0.0f;
    private Vector2 currentMouseDelta = Vector2.zero;
    private Vector2 currentMouseDeltaSpeed = Vector2.zero;

    public void SetLockCursor(bool lockCursor){
        this.lockCursor = lockCursor;
    }
    public void SetIsPause(bool isPause){
        this.isPause = isPause;
        lockCursor = isPause;
    }

    private void Awake(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() {
        if(!lockCursor){
            MouseLook();
        }
        if(!isPause){
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }else{
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        
    }

    private void MouseLook(){
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaSpeed, smoothMouseTime);
        
        cameraPitch -= currentMouseDelta.y * mouseSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);
        playerCamera.localEulerAngles = Vector3.right * cameraPitch;
        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }
}
