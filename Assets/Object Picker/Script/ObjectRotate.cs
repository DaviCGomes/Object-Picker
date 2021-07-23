using UnityEngine;

public class ObjectRotate : MonoBehaviour {
    [SerializeField] private float rotationSpeed = 2f;
    protected Vector3 posLastFrame;
    private bool isRotateLock = true;

    public void UnlockRotate(){
        isRotateLock = false;
    }
    public void LockRotate(){
        isRotateLock = true;
    }
    
    private void Update() {
        if(!isRotateLock){
            if(Input.GetButtonDown("Action1")){
                posLastFrame = Input.mousePosition;
            }
            if(Input.GetButton("Action1")){
                Vector3 delta = Input.mousePosition - posLastFrame;
                posLastFrame = Input.mousePosition;

                Vector3 axis = Quaternion.AngleAxis(-90f, Vector3.forward) * delta;
                transform.rotation = Quaternion.AngleAxis(delta.magnitude * rotationSpeed, axis) * transform.rotation;
            }
        }
    }
}
