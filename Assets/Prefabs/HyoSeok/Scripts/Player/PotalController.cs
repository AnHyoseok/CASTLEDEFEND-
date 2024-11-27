using System;
using UnityEngine;

namespace Defend.UI
{
    public class PotalController : MonoBehaviour
    {
        #region Variables

        //포탈 버튼
        public GameObject[] potalButton;
        public Transform[] potalsTransform;
        //플레이어 위치값
        public Transform playerTransform;
        #endregion

   

        //포탈 이동 버튼
        public void Teleportation(int index)
        {
            if (index >= 0 && index < potalsTransform.Length)
            {
                playerTransform.position = potalsTransform[index].position;
            }
        }

        
    }
}