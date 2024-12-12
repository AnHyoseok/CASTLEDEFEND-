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
        //�� �������� ��ݿ���
        public bool[] isClear = new bool[5];
        //Ÿ�� �ر� ����
        public bool[] isTowerUnlock = new bool[7];
        //���׷��̵� ����
        public bool[] isHPUpgrade = new bool[3];
        public bool[] isHPTimeUpgrade = new bool[3];
        public bool[] isArmorUpgrade = new bool[3];
        public bool[] isATKUpgrade = new bool[50];
        public bool[] isATKSpeedUpgrade = new bool[50];
        public bool[] isATKRangeUpgrade = new bool[50];
        public bool[] isMoneyGain = new bool[3];
        public bool[] isTreeGain = new bool[3];
        public bool[] isRockGain = new bool[3];
        //�÷��̾� �ڿ� ����
        public float money;
        public float health;
        public float tree;
        public float rock;
        //Ÿ����ġ - ���ϸ� �����ϴ½����� �� �Ⱦƹ�����(��ġ��������) �ڿ� �߰�
    }
}