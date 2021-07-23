using UnityEngine;

public class RecieveDirection : MonoBehaviour
{
    Vector3 direction;
    public void SetDirection(Vector3 direction){
        this.direction = direction;
    }
    public Vector3 GetDirection(){
        return direction;
    }
}
