using UnityEngine;

public class ObjectInteract : MonoBehaviour {
    
    private int counter = 0;
    private bool isInteract = false;
    private bool inView = false;
    private bool notView = true;
    
    public bool GetInView() {
        return inView;
    }
    public void ExitInteract(){
        inView = false;
        notView = true;
        GetComponent<ObjectRotate>().LockRotate();
        GetComponent<Show3dModel>().ReturnPosition();
    }
    public void Interact(){
        isInteract = true;
    }

    private void Update() {
        if(isInteract && !GetComponent<PlaceInTable>().RewardIsGiven()){
            if(!inView) {
                if(notView){
                    if(Input.GetButton("Action1")){
                        ++counter;
                        if(counter > 50){
                            notView = false;
                            GetComponent<MoveObject>().CanMove();
                        }
                    }
                    if(Input.GetButtonUp("Action1")){
                        counter = 0;
                        inView = true;
                    }
                } else {
                    if(Input.GetButtonUp("Action1")){
                        counter = 0;
                        notView = true;
                        isInteract = false;
                        GetComponent<MoveObject>().StopMove();
                    }
                }
            //Start Object Inspect
            } else {
                GameController._instance.ObjectView();
                GetComponent<ObjectRotate>().UnlockRotate();
                GetComponent<Show3dModel>().ShowObject();
                isInteract = false;
            }
        }
    }
    
}
