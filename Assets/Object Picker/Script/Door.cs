using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private bool isOpen = false;
    private Animator anim;

    void Awake () {
		anim = GetComponent <Animator> ();
		anim.speed = 0.35f;
	}

    public bool GetIsOpen(){
        return isOpen;
    }
    
    public void Interact(){
        isOpen = !isOpen;
        anim.SetBool ("Open", isOpen);
    }
}
