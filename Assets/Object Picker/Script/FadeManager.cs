using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour {
    public static FadeManager Instance {
        get; set;
    }

    [SerializeField] private Image fadeImage;
    private bool isInTransition;
    private bool isShowing;
    private float transition;
    private float duration;
    
    private void Awake() {
        Instance = this;
    }

    public void Fade (bool showing, float duration){
        isShowing = showing;
        isInTransition = true;
        this.duration = duration;
        transition = (isShowing) ? 0 : 1;
    }

    private void Update(){
        /*if(Input.GetButtonDown("Action1")){
            Fade(true, 3.0f);
        }
        if(Input.GetButtonDown("Fire2")){
            Fade(false, 3.0f);
        }*/
        
        if(!isInTransition)
            return;

        transition += (isShowing) ? Time.deltaTime * (1/duration) : -Time.deltaTime * (1/duration);
        fadeImage.color = Color.Lerp(new Color(0,0,0,0), Color.black, transition);

        if(transition > 1 || transition < 0)
            isInTransition = false;
    }

    public bool GetIsInTransition(){
        return isInTransition;
    }
}
