using UnityEngine;

public class MoveObject : MonoBehaviour {
    [SerializeField] private Vector3 pos3DView;
    [SerializeField] private float cameraDistance = 1.05f;
    [SerializeField] [Range(0.0f, 0.5f)] private float smoothMoveTime = 0.3f;
    [SerializeField] private Transform lookRef;
    private bool canMove = false;
    private bool isHolding = false;
    private Vector3 direction;
    private Vector3 currentDir;
    private Vector3 currentDirSpeed = Vector3.zero;
    private Transform cameraTransform;
    public bool IsMoving(){
        return canMove;
    }
    public bool IsHolding(){
        return isHolding;
    }
    public void SetMoving(bool canMove){
        this.canMove = canMove;
    }
    public void CanMove(){
        canMove = true;
        isHolding = true;
        currentDir = transform.position;
    }
    public void StopMove(){
        canMove = false;
        isHolding = false;
        Vector3 endPos = new Vector3(transform.position.x, 0, transform.position.z);
        transform.position = endPos;
    }

    private void Start(){
        cameraTransform = Camera.main.transform;
    }
    private void Update(){
        if(isHolding && canMove) {
            direction = GetComponent<RecieveDirection>().GetDirection();
            direction *= cameraDistance;
            
            float angle = Camera.main.transform.rotation.eulerAngles.y/57.4f;
            float xPos = (cameraDistance * Mathf.Sin(angle)) + lookRef.position.x;
            float zPos = (cameraDistance * Mathf.Cos(angle)) + lookRef.position.z;
            float yPos = direction.y + lookRef.position.y;
            
            Vector3 endPos = new Vector3(xPos, yPos, zPos);
            endPos += pos3DView;
            
            currentDir = Vector3.SmoothDamp(currentDir, endPos, ref currentDirSpeed, smoothMoveTime);
            
            transform.SetPositionAndRotation(currentDir, new Quaternion());
        } else if(isHolding && !canMove){
            GetComponent<PlaceInTable>().MoveToTable();
        }
    }
}
