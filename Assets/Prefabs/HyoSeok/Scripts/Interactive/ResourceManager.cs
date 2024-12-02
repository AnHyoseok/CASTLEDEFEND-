using Defend.Player;
using UnityEngine;

namespace Defend.Interactive
{
    public class ResourceManager : MonoBehaviour
    {
        #region
        public static ResourceManager Instance;

        private PlayerState playerState;

        // 자원 추가 이벤트
        public delegate void ResourceAddedHandler(float amount, string resourceType);
        public event ResourceAddedHandler OnResourceAdded;

        // 기본 자원 획득량
        private float rockAmount = 1.0f;
        private float treeAmount = 1.0f;
        private float moneyAmount = 1.0f;
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
                case "money":
                    playerState.AddMoney(amount);
                    break;

            }
            // 자원 추가 이벤트 발생
            OnResourceAdded?.Invoke(amount, resourceType);
        }

        // 자원 획득량 업그레이드
        public void UpgradeResourceGain(string resourceType, float multiplier)
        {
            switch (resourceType.ToLower())
            {
                case "rock":
                    rockAmount *= multiplier;
                    break;
                case "tree":
                    treeAmount *= multiplier;
                    break;
                case "money":
                    moneyAmount *= multiplier;
                    break;
            }
        }
    }
}