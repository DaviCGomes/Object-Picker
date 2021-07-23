using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController _instance;
    public static GameController Instance{
        get {return _instance;}
    }

    [SerializeField] private FadeManager fadeManager;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject cinemachine;
    [SerializeField] private Transform endLook;
    [SerializeField] private MovableObject[] movableObject = new MovableObject[3];
    [SerializeField][Range(1.0f, 5.0f)] private float fadeDuration = 3.0f;
    [SerializeField] private Canvas playerUI;
    [SerializeField] private Canvas viewExitUI;
    [SerializeField] private Canvas restartUI;

    private CinemachinePOVExtension cineExt;
    private PlayerMovimentController p_mController;
    private Text textCoins;
    private int numbCoins = 0;
    
    private bool isStart = false;
    [SerializeField]private bool isGameOver = false;
    private bool isObjectView = false;
    private bool restart;
    private int numObjectInPlace = 0;

    public void viewExit() {
        isObjectView = false;
        p_mController.SetLockMoviment(false);
        cineExt.SetIsPause(false);
        player.GetComponent<ObjectLocate>().enabled = true;
        playerUI.enabled = true;
        viewExitUI.enabled = false;
    }
    public void ObjectView() {
        isObjectView = true;
    }
    public void GiveReward(int reward){
        numbCoins += reward;
        textCoins.text = "" + numbCoins;
        numObjectInPlace++;
    }
    public MovableObject[] GetMovableObject(){
        return movableObject;
    }
    public void Restart(){
        SceneManager.LoadScene("Menu");
    }

    private void Awake(){
        if(_instance != null && _instance != this){
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
        cineExt = cinemachine.GetComponent<CinemachinePOVExtension>();
        p_mController = player.GetComponent<PlayerMovimentController>();
        playerUI.enabled = true;
        viewExitUI.enabled = false;
        restartUI.enabled = false;
    }

    private void Start() {
        
        fadeManager.Fade(false, fadeDuration);
        p_mController.SetLockMoviment(true);
        cineExt.SetLockCursor(true);

        textCoins = playerUI.GetComponentInChildren<Text>();
    }

    private void Update() {
        //Start Fade in
        if(!isStart && !fadeManager.GetIsInTransition()){
            isStart = true;
            p_mController.SetLockMoviment(false);
            cineExt.SetLockCursor(false);
        }
        //Game Over Fade out
        if(isGameOver){
            if(!restart){
                p_mController.SetLockMoviment(true);
                cineExt.SetIsPause(true);
                cinemachine.GetComponent<CinemachinePOVExtension>().LookAt(endLook);
                fadeManager.Fade(true, fadeDuration);
                restart = true;
            } else {
                restartUI.enabled = true;
            }
        } else {
            //Object 3d View
            if(isObjectView){
                p_mController.SetLockMoviment(true);
                cineExt.SetIsPause(true);
                player.GetComponent<ObjectLocate>().enabled = false;
                playerUI.enabled = false;
                viewExitUI.enabled = true;
            }
            
            //If all object is in the right place end the game
            if(numObjectInPlace == movableObject.Length){
                isGameOver = true;
            }
        }
    }
}
