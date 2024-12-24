using UnityEngine;

public class ClickTest : MonoBehaviour
{
    public FaderTest faderTest;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        faderTest = new FaderTest();
        faderTest.FadeTo();
        //faderTest.FromFade();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
