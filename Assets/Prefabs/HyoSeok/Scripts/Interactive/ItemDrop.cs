using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;


namespace Defend.item
{
    /// <summary>
    /// 아이템 드랍 
    /// </summary>
    public class ItemDrop : MonoBehaviour
    {
       
        #region Variables
        public List<Item> items = new List<Item>();
        #endregion

        //아이템 랜덤 선택해서 픽업(드랍율)
        protected Item PickItem()
        {
            int sum = 0;
            foreach (var item in items)
            {
                //모든 아이템 비율 더하기
                sum += item.weight;
            }

            var rnd = Random.Range(0, sum);

            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];
                if (item.weight > rnd) return item;
                else rnd -= item.weight;
            }

            return null;
        }

        ////아이템 생성
        //public void DropItem(Vector3 pos)
        //{
        //    var item = PickItem();
        //    if (item == null) return;

        //    Instantiate(item.prefab, pos, Quaternion.identity);
        //}
    }
}