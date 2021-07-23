using UnityEngine;

public class PlaceInTable : MonoBehaviour
{
    [SerializeField] [Range(0.0f, 0.5f)] private float smoothMoveTime = 0.3f;
    private Vector3 initPos;
    private Transform destiny;
    private Vector3 currentDir;
    private Vector3 currentDirSpeed = Vector3.zero;
    private bool complete = false;
    private bool isRewardGiven = false;

    public bool RewardIsGiven(){
        return isRewardGiven;
    }
    public void Complete(){
        complete = true;
    }
    public void StartToMove(){
        initPos = transform.position;
        currentDir = transform.position;
    }
    public void MoveToTable(){
        Vector3 endPos = destiny.position;
        
        currentDir = Vector3.SmoothDamp(currentDir, endPos, ref currentDirSpeed, smoothMoveTime);
            
        transform.SetPositionAndRotation(currentDir, new Quaternion());
    }
    public void ExitToMove(){
        transform.SetPositionAndRotation(initPos, new Quaternion());
    }

    private void Start(){
        destiny = GetComponent<MovableObject>().GetFinalPlace();
    }
    private void Update(){
        if(complete && !isRewardGiven){
            initPos = transform.position;
            Debug.Log("Reward");
            int reward = GetComponent<MovableObject>().GetCoins();
            GameController._instance.GiveReward(reward);
            isRewardGiven =  true;
        }
        if(isRewardGiven){
            GetComponent<ObjectInteract>().enabled = false;
            GetComponent<PlaceInTable>().enabled = false;
            GetComponent<Show3dModel>().enabled = false;
            GetComponent<ObjectRotate>().enabled = false;
            GetComponent<MoveObject>().enabled = false;
            gameObject.tag = "Untagged";
        }
    }
}
