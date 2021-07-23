using UnityEngine;

public class Show3dModel : MonoBehaviour
{
    [SerializeField] private Vector3 pos3DView;
    [SerializeField] private float cameraDistance = 1f;
    [SerializeField] private Transform lookRef;
    [SerializeField][Range(0, 0.9f)] private float scaleView;
    private bool isShowingIn = false;
    private bool isShowingOut = false;
    private Vector3 initialPos;
    private Vector3 initialRot;
    private Vector3 direction;

    public void ShowObject(){
        isShowingIn = true;
        initialPos = transform.position;
        initialRot = transform.rotation.eulerAngles;
    }
    public void ReturnPosition(){
        isShowingOut = true;
    }
    private void Start(){
        initialPos = transform.position;
        initialRot = transform.rotation.eulerAngles;
    }
    
    private void Update() {
        if(isShowingIn){
            direction = GetComponent<RecieveDirection>().GetDirection();
            direction *= cameraDistance;
            
            Vector3 endPos = new Vector3(direction.x + lookRef.position.x, direction.y + lookRef.position.y, direction.z + lookRef.position.z);
            endPos += pos3DView;
            
            transform.SetPositionAndRotation(endPos, new Quaternion());
            
            Vector3 endScale = new Vector3(scaleView, scaleView, scaleView);
            transform.localScale -= endScale;

            isShowingIn = false;
        }
        if(isShowingOut){
            Quaternion endRot = new Quaternion(initialRot.x, initialRot.y, initialRot.z, 0);
            transform.SetPositionAndRotation(initialPos, endRot);

            Vector3 endScale = new Vector3(scaleView, scaleView, scaleView);
            transform.localScale += endScale;

            isShowingOut = false;
        }
    }
}
