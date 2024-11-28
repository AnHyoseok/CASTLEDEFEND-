using MyVrSample;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

namespace Defend.UI
{
    /// <summary>
    /// ui 위치를 플레이어 앞에 뛰우는 용도
    /// </summary>
    public class UITransformSet : MonoBehaviour
    {
        #region Variables
        public GameObject[] Canvases;
        public Transform player;
        [SerializeField] private float distance = 1.5f;
        #endregion

        //앞에띄우기
        void Update()
        {
            for(int i = 0; i < Canvases.Length; i++)
            {
                //show 설정
                if (Canvases[i].activeSelf)
                {
                    Canvases[i].transform.position = player.position + new Vector3(player.forward.x, 0f, player.forward.z).normalized * distance;
                    Canvases[i].transform.LookAt(new Vector3(player.position.x, Canvases[i].transform.position.y, player.position.z));
                    Canvases[i].transform.forward *= -1;
                }
            }
         
        }
    }


}