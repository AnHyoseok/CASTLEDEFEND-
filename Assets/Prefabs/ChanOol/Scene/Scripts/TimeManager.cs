using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public Light sunLight;
    private Vector3 initAngle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initAngle = sunLight.transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
