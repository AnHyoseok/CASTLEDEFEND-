using UnityEngine;

[System.Serializable]
public class testdata
{

    //플레이어 자원 여부
    public float money;
    public float health;
    public float tree;
    public float rock;
  
    //타워 해금 여부 0일때 잠금 1일때 해금
    public int isTowerUnlock1;
    public int isTowerUnlock2;
    public int isTowerUnlock3;
    public int isTowerUnlock4;
    public int isTowerUnlock5;
    public int isTowerUnlock6;
    public int isTowerUnlock7;
    public int isTowerUnlock8;
    public int isTowerUnlock9;
    public int isTowerUnlock10;
    public int isTowerUnlock11;
    public int isTowerUnlock12;
    //업그레이드 여부
    public int isHPUpgradelevel;
    public int isHPTimeUpgradelevel;
    public int isArmorUpgradelevel;
    public int isATKUpgradelevel;
    public int isATKSpeedUpgradelevel;
    public int isATKRangeUpgradelevel;
    public int isMoneyGainlevel;
    public int isTreeGainlevel;
    public int isRockGainlevel;
    public bool isPotalActive;
    public bool isMoveSpeedUp;
    public bool isAutoGain;
    //각 스테이지 잠금여부
    public bool[] isClear = new bool[5];
    //타워설치 - 못하면 저장하는시점에 다 팔아버리고(설치가격으로) 자원 추가


    public testdata(float money, float health, float tree, float rock)
    {
        this.money = money;
        this.health = health;
        this.tree = tree;
        this.rock = rock;
    }
}
