using Defend.Player;
using Defend.Interactive;
using UnityEngine;

namespace Defend.item
{
    public class DropItem : MonoBehaviour
    {
        #region Variables
        public float amount;       // 자원 양
        //상점에서 업그레이드 할것
        public float distance = 5f; // 플레이어에게 가는 거리
        public float speed = 1.0f; // 플레이어에게 가는 속도
        private PlayerState playerState;
        #endregion

        private void Awake()
        {
            playerState = Object.FindAnyObjectByType<PlayerState>(); // PlayerState를 찾기
        }

        private void Update()
        {
            MagnetItem();
        }

        //플레이어가 거리에 있으면 플레이어에게 이동
        void MagnetItem()
        {

        }

        //닿으면 제거
        private void OnTriggerEnter(Collider other)
        {
            OnDestroy();
        }

        //제거시 이펙트나오고 자원넘기기
        private void OnDestroy()
        {
            //흭득 이펙트

            //자원넘기기
            //ResourceManager.Instance.AddResources(currentResourceType.amount, currentResourceType.name.ToString());
        }
    }
}