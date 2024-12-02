using Defend.Player;
using UnityEngine;

namespace Defend.Interactive
{
    public class ResourceManager : MonoBehaviour
    {
        #region
        public static ResourceManager Instance;

        private PlayerState playerState;

        // �ڿ� �߰� �̺�Ʈ
        public delegate void ResourceAddedHandler(float amount, string resourceType);
        public event ResourceAddedHandler OnResourceAdded;

        // �⺻ �ڿ� ȹ�淮
        private float rockAmount = 1.0f;
        private float treeAmount = 1.0f;
        private float moneyAmount = 1.0f;
        #endregion
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                playerState = Object.FindAnyObjectByType<PlayerState>(); // PlayerState�� ã��
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // �ڿ� �߰�
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
            // �ڿ� �߰� �̺�Ʈ �߻�
            OnResourceAdded?.Invoke(amount, resourceType);
        }

        // �ڿ� ȹ�淮 ���׷��̵�
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