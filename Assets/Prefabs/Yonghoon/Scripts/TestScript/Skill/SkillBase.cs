using UnityEngine;

namespace Defend.Enemy.Skill
{
    public abstract class SkillBase : MonoBehaviour
    {
        public abstract void ActivateSkill(Transform activator, float range);
    }
}