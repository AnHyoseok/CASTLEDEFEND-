
using UnityEngine;

namespace Defend.Player
{
    public class PlayerState : MonoBehaviour
    {
        #region Variables
        public float money = 100; //돈
        public float health = 20; //체력(성)
        public float tree; //자원(나무)
        public float rock; //자원(돌)
        #endregion

        //돈 , 나무 , 돌 개수 체크
        public void ShowStatus()
        {
            //단축키누르면 보이게하거나 창 항상띄우던가
            Debug.Log($"Money = {money} Tree = {tree} Rock = {rock}");
        }

        //데미지 입기
        public void TakeDamage(float amount)
        {
            if (health <= amount)
            {
                health = 0;

                //GameOver
                GameOver();
            }
        }

        //GameOver
        public void GameOver()
        {
            //게임오버 만들어야됌
            Debug.Log("GameOver");
        }

        //돈 습득
        public void AddMoney(float amount)
        {
            money += amount;
        }

        //돈 소비
        public bool SpendMoney(float amount)
        {
            if (money >= amount)
            {
                money -= amount;    //돈 충분하면 소비
                return true;
            }
            else
            {
                //구매 불가 ui띄우기
                Debug.Log("Not Enough Money");
                return false;       //돈 부족하면 구매불가
            }
        }
        //나무 습득
        public void AddTree(float amount)
        {
            tree += amount;
        }
        //돌 습득
        public void AddRock(float amount)
        {
            rock += amount;
        }


        // 자원(나무 , 돌 소비)
        public bool SpendResources(float requiredTree = 0, float requiredRock = 0)
        {
            // 자원이 충분한지 확인
            if (tree >= requiredTree && rock >= requiredRock)
            {
                // 자원 소비
                tree -= requiredTree;
                rock -= requiredRock;
                return true;
            }
            else
            {
                //구매 불가 ui띄우기
                Debug.Log("Not Enough Money");
                return false;
            }
        }
    }
}