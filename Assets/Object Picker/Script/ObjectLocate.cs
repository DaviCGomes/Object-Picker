using UnityEngine;

public class ObjectLocate : MonoBehaviour
{
	private void Update(){
		if(Input.GetButton("Action1")){
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            if (Physics.Raycast (ray, out hit)) {
                if (hit.distance < 1.5) {
                    if (hit.collider.tag == "Movable") {
                        hit.collider.gameObject.GetComponent<ObjectInteract>().Interact();
						hit.collider.gameObject.GetComponent<RecieveDirection>().SetDirection(ray.direction);
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
				if (hit.collider.tag == "Movable") {
                    GUI.Label (new Rect (Screen.width/1.99f, Screen.height/2f, Screen.width/4f, Screen.height/4f),
	    					"Move Object", myLabelStyle);
				}
			}
		}
	}
}
