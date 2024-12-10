using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class BuildArea : MonoBehaviour
{
    public Material AreaBlack;
    public Material AreaRed;
    private Renderer renderer;
    private BoxCollider boxCollider;
    

    void Start()
    {
        renderer = GetComponent<Renderer>();
        boxCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        
    }

    /*private void OnTriggerEnter(Collider other)
    {
        renderer.material = AreaRed;
    }*/

    private void OnTriggerStay(Collider other)
    {
        renderer.material = AreaRed;
    }

    private void OnTriggerExit(Collider other)
    {
        renderer.material = AreaBlack;
    }


}