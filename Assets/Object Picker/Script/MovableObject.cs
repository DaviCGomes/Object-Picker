using UnityEngine;

public class MovableObject : MonoBehaviour {
    [SerializeField] private Transform finalPlace;
    [SerializeField] private int coins;

    public int GetCoins(){
        return coins;
    }
    public GameObject GetInteractiveObject(){
        return gameObject;
    }
    public Transform GetFinalPlace(){
        return finalPlace;
    }
}
