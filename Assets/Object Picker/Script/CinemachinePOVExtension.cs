using UnityEngine;
using Cinemachine;

public class CinemachinePOVExtension : CinemachineExtension {
    [SerializeField] private float clampAgle = 80f;
    [SerializeField] private float rotationSpeed = 10f;
    private Vector3 startingRotation;
    private Vector2 currentAng;
    [SerializeField]private Transform endLook;

    private bool lockCursor = false;
    private bool isPause = false;
    [SerializeField] private bool lookend = false;

    public void SetLockCursor(bool lockCursor){
        this.lockCursor = lockCursor;
    }
    public void SetIsPause(bool isPause){
        this.isPause = isPause;
        lockCursor = isPause;
    }
    public void LookAt(Transform lookAt){
        lookend = true;
        endLook = lookAt;
        //currentAng = new Vector2(-Camera.main.transform.eulerAngles.y, Camera.main.transform.eulerAngles.x);
    }
    
    protected override void Awake(){
        if(startingRotation == null)
            startingRotation = transform.localRotation.eulerAngles;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        base.Awake();
    }
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vCam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime){
        if(vCam.Follow){
            if(stage == CinemachineCore.Stage.Aim){
                if(!lockCursor){
                    Vector2 deltaInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
                    startingRotation.x += deltaInput.x * rotationSpeed * Time.deltaTime;
                    startingRotation.y += deltaInput.y * rotationSpeed * Time.deltaTime;
                    startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAgle, clampAgle);
                    state.RawOrientation = Quaternion.Euler(-startingRotation.y, startingRotation.x, 0f);
                }
                if(!isPause){
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }else{
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    state.RawOrientation = Quaternion.Euler(-startingRotation.y, startingRotation.x, 0f);
                }
                if(lookend){
                    vCam.LookAt = endLook;
                    /*Vector2 o = new Vector2(transform.position.x, transform.position.z);
                    Vector2 d = new Vector2(endLook.position.x, endLook.position.z);
                    float angle = Vector2.Angle(o, d);
                    Debug.Log(angle);
                    
                    float xAngle = currentAng.x - Mathf.Cos(angle);
                    float yAngle = currentAng.y - Mathf.Sin(angle);

                    //Vector2 deltaInput = new Vector2(xAngle, yAngle);
                    //Debug.Log(deltaInput.ToString());
                    if(Mathf.Cos(angle) > Mathf.Cos(startingRotation.x))
                        startingRotation.x += Mathf.Cos(angle) * rotationSpeed * Time.deltaTime;
                    else if(Mathf.Cos(angle) < Mathf.Cos(startingRotation.x))
                        startingRotation.x -= Mathf.Cos(angle) * rotationSpeed * Time.deltaTime;

                    o = new Vector2(transform.position.x, transform.position.y);
                    d = new Vector2(endLook.position.x, endLook.position.y);
                    angle = Vector2.Angle(o, d);

                    if(Mathf.Sin(angle) > Mathf.Sin(startingRotation.x))
                        startingRotation.y -= Mathf.Sin(angle) * rotationSpeed * Time.deltaTime;
                    else if(Mathf.Sin(angle) < Mathf.Sin(startingRotation.x))
                        startingRotation.y += Mathf.Sin(angle) * rotationSpeed * Time.deltaTime;
                    //Debug.Log(startingRotation.x);
                    state.RawOrientation = Quaternion.Euler(-startingRotation.y, startingRotation.x, 0f);*/
                }
                 
            }
        }
    }
}
