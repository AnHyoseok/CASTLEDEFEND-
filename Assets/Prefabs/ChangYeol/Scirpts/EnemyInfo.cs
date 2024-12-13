using Defend.Enemy;
using Defend.TestScript;
using UnityEngine;

namespace Defend.UI
{
    [System.Serializable]
    public class EnemyInfo
    {
        #region Variables
        public string Enemyname;
        public Sprite enemySprite;
        //체력,방어력
        public Health Health;
        //공격력,공격 스피드
        public EnemyAttackController Attack;
        //이동속도
        public EnemyMoveController Move;
        //얻는 돈
        public EnemyController Money;
        #endregion
    }
}