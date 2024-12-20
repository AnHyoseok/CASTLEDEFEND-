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
        public static string TOWER_ANIM_SHOOTTRIGGER = "ShootTrigger";   //TOWERAnim - ShootTrigger
        #endregion

        #region EnemyAnimation
        public static string ENEMY_ANIM_ISARRIVE = "IsArrive";          //EnemyAnim - ArriveCheck
        public static string ENEMY_ANIM_ISDEATH = "IsDeath";            //EnemyAnim - DeathCheck
        public static string ENEMY_ANIM_ATTACKTRIGGER = "Attack";       //EnemyAnim - AttackTrigger
        public static string ENEMY_ANIM_SKILLTRIGGER = "Skill";         //EnemyAnim - SkillTrigger
        /**********************************************   EnemyAnimationState    ************************************************/
        public static string ENEMY_ANIM_STATE_DEATH = "Death";          //EnemyAnimState - Death
        public static string ENEMY_ANIM_STATE_IDLE = "Idle";            //EnemyAnimState - Idle
        public static string ENEMY_ANIM_STATE_ATTACK = "Attack";        //EnemyAnimState - Attack
        public static string ENEMY_ANIM_STATE_SKILL = "Skill";          //EnemyAnimState - Skill
        #endregion

        #region SoundParameter
        public static string AUDIO_UTIL_MASTER = "MASTER";
        public static string AUDIO_UTIL_BGM = "BGM";
        public static string AUDIO_UTIL_EFFECT = "EFFECT";
        #endregion
    }
}
