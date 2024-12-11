using UnityEngine;

namespace Defend.Enemy.Skill
{
    public abstract class SkillBase : MonoBehaviour
    {
        /// <summary>
        /// 스킬을 활성화하는 메서드
        /// </summary>
        /// <param name="activator">스킬을 발동하는 적의 Transform</param>
        /// <param name="range">스킬 발동 범위</param>
        public abstract void ActivateSkill();

        /// <summary>
        /// 스킬을 활성화할 수 있는지 여부를 반환하는 메서드
        /// </summary>
        /// <param name="currentHealth">현재 체력</param>
        /// <param name="maxHealth">최대 체력</param>
        /// <param name="elapsedTime">경과 시간 (필요한 경우)</param>
        /// <returns>스킬 활성화 가능 여부</returns>
        public abstract bool CanActivateSkill(float healthRatio);

        #region Variables
        public float amount = 5f;
        public float skillDuration = 5f;
        public float skillCooldown = 5f;
        public float range = 5f;
        
        [HideInInspector] protected bool hasSkill = false;
        #endregion

    }
}