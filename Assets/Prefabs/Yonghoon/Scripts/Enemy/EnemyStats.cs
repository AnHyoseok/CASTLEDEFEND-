using UnityEngine;

/// <summary>
/// Enemy의 기본정보를 정의하는 클래스
/// </summary>
namespace Defend.Enemy
{
    public enum EnemyType
    {
        Buffer,
        Warrior,
        Tanker,
        Boss
    }

    [System.Serializable]
    public class EnemyStats : MonoBehaviour
    {
        //Animator
        private Animator animator;

        //이동관련
        public float baseSpeed = 5f;

        //공격 관련
        public float baseAttackDamage = 10f;
        public float baseAttackDelay = 2f;

        //체력관련
        public float baseHealth = 50f;

        //방어관련
        public float baseArmor = 5f;

        //보상관련
        public int rewardGold = 1;

        //살아있는지 여부
        public bool isDeath = false;

        //적 타입
        public EnemyType type;
    }
}