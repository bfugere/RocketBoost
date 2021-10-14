using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This is friendly.");
                break;
            
            case "Fuel":
                Debug.Log("Refueling...");
                break;
            
            case "Finish":
                Debug.Log("Finish!");
                break;
            
            default:
                Debug.Log("Dead");
                break;
        }
    }
}
