using UnityEngine;
using System;
namespace Defend.Manager
{
    /// <summary>
    /// ������ ������
    /// </summary>
    [SerializeField]
    public class Data 
    {
     
        //�������� ī��Ʈ
        public int isClear ;
        //Ÿ�� �ر� ���� 0�϶� ��� 1�϶� �ر�
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
        //Ÿ�� �ر� ���� 0�϶� ��� 1�϶� �ر�
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

        //���׷��̵� ����
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
        //�÷��̾� �ڿ� ����
        public float money;
        public float health;
        public float tree;
        public float rock;
        //Ÿ����ġ - ���ϸ� �����ϴ½����� �� �Ⱦƹ�����(��ġ��������) �ڿ� �߰�
    }
}