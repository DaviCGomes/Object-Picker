using UnityEngine;

public class TableCollider : MonoBehaviour {
    private MovableObject[] movableObject;
    private bool startMoving = false;
    private int idObject = -1;
    
    private void Start(){
        movableObject = GameController._instance.GetMovableObject();
    }

    private void Update(){
        LocateObject();
        MoveObject();
    }

    private void LocateObject(){
        if(idObject == -1){
            for(int i = 0; i < movableObject.Length; i++){
                float distance = Vector3.Distance(transform.position, movableObject[i].transform.position);
                if(!startMoving && distance >= 2f && distance <=2.5f){
                    idObject = i;
                }
            }
        }
    }
    private void MoveObject(){
        if(idObject >= 0){
            float distance = Vector3.Distance(transform.position, movableObject[idObject].transform.position);
            
            if(!startMoving && distance >= 2f && distance <=2.5f){
                if(movableObject[idObject].GetComponent<MoveObject>().IsMoving()){
                    movableObject[idObject].GetComponent<PlaceInTable>().StartToMove();
                }
            } else if(!startMoving && distance <2f && distance >= 1.5f){
                if(movableObject[idObject].GetComponent<MoveObject>().IsMoving()){
                    movableObject[idObject].GetComponent<MoveObject>().SetMoving(false);
                    startMoving = true;
                }
            } else if(startMoving && distance < 1.1f){
                movableObject[idObject].GetComponent<PlaceInTable>().Complete();
                idObject = -1;
                startMoving = false;
            }else if(!startMoving && distance > 2.5f && distance < 2.6f){
                if(movableObject[idObject].GetComponent<MoveObject>().IsMoving()){
                    movableObject[idObject].GetComponent<PlaceInTable>().ExitToMove();
                    idObject = -1;
                }
            }
        }
    }
}
