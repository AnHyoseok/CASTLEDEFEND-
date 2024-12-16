using UnityEngine;
using System;
namespace Defend.Manager
{
    /// <summary>
    /// 저장할 데이터
    /// </summary>
    [SerializeField]
    public class Data 
    {
     
        //스테이지 카운트
        public int isClear ;
        //타워 해금 여부 0일때 잠금 1일때 해금
        public bool isTowerUnlock1;
        public bool isTowerUnlock2;
        public bool isTowerUnlock3;
        public bool isTowerUnlock4;
        public bool isTowerUnlock5;
        public bool isTowerUnlock6;
        public bool isTowerUnlock7;
        public bool isTowerUnlock8;
        public bool isTowerUnlock9;
        public bool isTowerUnlock10;
        public bool isTowerUnlock11;
        public bool isTowerUnlock12;

        //
        //타워 해금 여부 0일때 잠금 1일때 해금
        public bool isTowerUnlocked1;
        public bool isTowerUnlocked2;
        public bool isTowerUnlocked3;
        public bool isTowerUnlocked4;
        public bool isTowerUnlocked5;
        public bool isTowerUnlocked6;
        public bool isTowerUnlocked7;
        public bool isTowerUnlocked8;
        public bool isTowerUnlocked9;
        public bool isTowerUnlocked10;
        public bool isTowerUnlocked11;
        public bool isTowerUnlocked12;

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
        public bool isPotalActive ;
        public bool isMoveSpeedUp;
        public bool isAutoGain;
        //플레이어 자원 여부
        public float money;
        public float health;
        public float tree;
        public float rock;
        //타워설치 - 못하면 저장하는시점에 다 팔아버리고(설치가격으로) 자원 추가
    }
}