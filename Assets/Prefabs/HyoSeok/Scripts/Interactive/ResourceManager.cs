using Defend.Player;
using UnityEngine;

namespace Defend.Interactive
{
    public class ResourceManager : MonoBehaviour
    {
        #region
        public static ResourceManager Instance;

        private PlayerState playerState;
        #endregion
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                playerState = Object.FindAnyObjectByType<PlayerState>(); // PlayerState를 찾기
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // 자원 추가
        public void AddResources(float amount, string resourceType)
        {

            switch (resourceType.ToLower())
            {
                case "rock":
                    playerState.AddRock(amount);
                    break;
                case "tree":
                    playerState.AddTree(amount);
                    break;
              
            }
        }
    }
}