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
        private float rockmultiplier = 1.0f;
        private float treemultiplier = 1.0f;
        private float moneymultiplier = 1.0f;
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
                  
                    playerState.AddRock(amount* rockmultiplier);
                    break;
                case "tree":
                 
                    playerState.AddTree(amount* treemultiplier);
                    break;
                case "money":
                 
                    playerState.AddMoney(amount * moneymultiplier);
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
                    rockmultiplier *= multiplier;
                    Debug.Log($"rockamout = {moneymultiplier}");
                    break;
                case "tree":
                    treemultiplier *= multiplier;
                    Debug.Log($"treeAmount = {moneymultiplier}");
                    break;
                case "money":
                    moneymultiplier *= multiplier;
                    Debug.Log($"moneyAmount = {moneymultiplier}");
                    break;
            }
        }
    }
}