using UnityEngine;


public class NewMonoBehaviourScript2 : MonoBehaviour
{
    [SerializeField]
    testlist testlist;
    [SerializeField]
    testdata a;
    private void Start()
    {
      a=  testlist.GetTestdata(1);
      //a=  testlist.GetTestdata(2);
      //a=  testlist.GetTestdata();

    }
}
