using UnityEngine;
using System.Collections;

public class DetectionCheck : MonoBehaviour
{
    private BoxCollider boxCollider;

    public bool isRockInside = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Rock")
        {
            isRockInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Rock")
        {
            isRockInside = false;
        }
    }
}
