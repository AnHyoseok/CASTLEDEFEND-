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
                enemyController.OnChanneling();
            }
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            EnemyController enemyController = animator.GetComponentInParent<EnemyController>();

            if (enemyController != null)
            {
                enemyController.OnChanneling();
            }
        }

        // OnStateMove is called right after Animator.OnAnimatorMove()
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that processes and affects root motion
        //}

        // OnStateIK is called right after Animator.OnAnimatorIK()
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that sets up animation IK (inverse kinematics)
        //}
    }
}