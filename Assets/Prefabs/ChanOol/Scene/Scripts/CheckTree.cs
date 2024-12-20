using UnityEngine;

public class CheckTree : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Tree")
        { 
            Destroy(other.gameObject);
        }
    }
}
