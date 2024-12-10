/// <summary>
/// 게임 내 고정된 상수를 정의 한 클래스
/// </summary>
namespace Defend.Utillity
{
    public static class Constants
    {
        #region Layer 
        public static string LAYER_ENEMY = "Enemy";                      //Layer - Enemy 
        public static string LAYER_BOSS = "Boss";                        //Layer - Boss
        #endregion

        #region Animation 
        public static string ANIM_SHOOTTRIGGER = "ShootTrigger";         //Anim - ShootTrigger
        #endregion

        #region EnemyAnimation
        public static string ENEMY_ANIM_ISARRIVE = "IsArrive";         //EnemyAnim - ArriveCheck
        public static string ENEMY_ANIM_ISDEATH = "IsDeath";         //EnemyAnim - DeathCheck
        public static string ENEMY_ANIM_ATTACKTRIGGER = "Attack";         //EnemyAnim - AttackTrigger
        public static string ENEMY_ANIM_SKILLTRIGGER = "Skill";         //EnemyAnim - ScreamTrigger

        #endregion
    }
}
