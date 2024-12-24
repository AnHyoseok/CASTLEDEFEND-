using UnityEngine;

namespace Defend.Enemy
{
    public class AttackControlStateMachine : StateMachineBehaviour
    {
        private EnemyAttackController attackController;
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (attackController == null)
            {
                attackController = animator.GetComponentInParent<EnemyAttackController>();
            }
            if (attackController != null)
            {
                attackController.ChangeAttackingStatus();
            }
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (attackController != null)
            {
                attackController.ChangeAttackingStatus();
                attackController.StartAttackCooldown();
            }
        }
    }
}