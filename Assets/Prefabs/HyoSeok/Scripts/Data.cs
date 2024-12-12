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
        //각 스테이지 잠금여부
        public bool[] isClear = new bool[5];
        //타워 해금 여부
        public bool[] isTowerUnlock = new bool[7];
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