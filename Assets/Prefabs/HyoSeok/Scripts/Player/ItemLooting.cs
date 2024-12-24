
using UnityEngine;



namespace Defend.item
{
    public class ItemLooting : MonoBehaviour
    {
        #region Variables
 

        [SerializeField] private float distance; //아이템 루팅 거리
        [SerializeField] private float speed;  //습득속도
        #endregion
      
        public ItemLooting()
        {
            //swich로 구분 ㄱㄱ
            //ResourceTypeEnum.Tree

            ////태그 resource인 놈들 플레이어쪽으로 끌고오기 

            //// 플레이어에게 전달
            //ResourceManager.Instance.AddResources(currentResourceType.amount, currentResourceType.name.ToString());
        }
    }
}