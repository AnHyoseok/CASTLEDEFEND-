using Defend.TestScript;
using UnityEngine;

namespace Defend.Enemy.Skill
{
    public class SkillControlStateMachine : StateMachineBehaviour
    {
        private EnemyController enemyController;
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (enemyController == null)
            {
                enemyController = animator.GetComponentInParent<EnemyController>();
            }
            if (enemyController != null)
            {
                enemyController.ChangeChannelingStatus();
            }
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (enemyController != null)
            {
                enemyController.ChangeChannelingStatus();
            }
        }
    }
}