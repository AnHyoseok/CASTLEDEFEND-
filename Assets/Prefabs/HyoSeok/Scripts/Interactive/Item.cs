using UnityEngine;

namespace Defend.item
{
    [System.Serializable]
    public class Item
    {
        #region Variables
        public string itemName;
        public Sprite itemIcon;
        //개수
        public float count;
        //아이템 프리팹
        public Transform prefab;
        //설명
        [Multiline]
        public string description;
        //각 아이템의 등장 비율
        public int weight;  
        #endregion
    }
}