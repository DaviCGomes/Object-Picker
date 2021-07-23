using UnityEngine;

public class PlayerMovimentController : MonoBehaviour
{
    [SerializeField] private float speed = 6.0f;
    [SerializeField] private float gravit = -9.8f;
    [SerializeField] [Range(0.0f, 0.5f)] private float smoothMoveTime = 0.3f;
    
    private bool lockMoviment = false;
    private	float speedY = 0.0f;
    private CharacterController controller;
    private Vector2 currentDir = Vector2.zero;
    private Vector2 currentDirSpeed = Vector2.zero;
    private Transform cameraTransform;

    public void SetLockMoviment(bool lockMoviment){
        this.lockMoviment = lockMoviment;
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
    }

    private void Update(){
        if(!lockMoviment){
            Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
            targetDir.Normalize();

            currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirSpeed, smoothMoveTime);
            
            if(controller.isGrounded)
                speedY = 0.0f;

            speedY += gravit * Time.deltaTime; 
            Vector3 velocity = (cameraTransform.forward * currentDir.y + cameraTransform.right * currentDir.x) * speed + Vector3.down * speedY;
            
            controller.Move(velocity * Time.deltaTime);
        }
    }
}
