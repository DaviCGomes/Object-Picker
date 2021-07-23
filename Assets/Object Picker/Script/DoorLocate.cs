using UnityEngine;

public class DoorLocate : MonoBehaviour {
    
	private void Update() {
		if(Input.GetButtonDown("Action1")){
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            if (Physics.Raycast (ray, out hit)) {
                if (hit.distance < 1.5) {
                    if (hit.collider.tag == "Door") {
                        hit.collider.gameObject.GetComponent<Door>().Interact();
                    }
                }
            }
        }
	}

	private void OnGUI(){
		GUIStyle myLabelStyle = new GUIStyle (GUI.skin.label);
		myLabelStyle.alignment = TextAnchor.UpperLeft;
		myLabelStyle.fontSize = (Screen.width / 28 + Screen.height / 28) / 2;
        
        RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray, out hit)) {
			if (hit.distance < 1.5 && !(Cursor.lockState == CursorLockMode.None)) {
				if (hit.collider.tag == "Door") {
                    bool isOpen = hit.collider.gameObject.GetComponent<Door>().GetIsOpen();
                    if(isOpen){
    					GUI.Label (new Rect (Screen.width/1.99f, Screen.height/2f, Screen.width/4f, Screen.height/4f),
	    					"Close Door", myLabelStyle);
                    } else {
                        GUI.Label (new Rect (Screen.width/1.99f, Screen.height/2f, Screen.width/4f, Screen.height/4f),
	    					"Open Door", myLabelStyle);
                    }
				}
			}
		}
	}
}
