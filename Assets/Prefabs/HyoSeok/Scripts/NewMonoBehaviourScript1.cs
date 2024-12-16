using UnityEngine;
using System.Collections.Generic;
using Defend.Player;
using Defend.TestScript;
public class testlist: MonoBehaviour
{
    #region Variables
    //참조
    Health health;
    PlayerState playerState;
    public GameObject castle;
    #endregion
    [SerializeField] List<testdata> list = new List<testdata>();
    private void Awake()
    {
        //참조
        health = castle.GetComponent<Health>();
        playerState = FindAnyObjectByType<PlayerState>();

        testdata testdata = new testdata(health.maxHealth, playerState.money, playerState.tree, playerState.rock);
        list.Add(testdata);
    }


    public testdata GetTestdata(int i)
    {
        if(list.Count > i)
        {
            return list[i];

        }
        else
        {
            return null;
        }
    }

}
