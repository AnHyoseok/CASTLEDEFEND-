using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Turning;

namespace MyVrSample
{
    /// <summary>
    /// ������ �޴� UI �����ϴ� Ŭ����
    /// </summary>
    public class GameMenuManager : MonoBehaviour
    {
        #region Variables

        [SerializeField] private float distance = 1.5f;
        //��Ż
        public GameObject potalMenu;
        //��Ż ��ư
        public GameObject[] potalButton;
        public Transform[] potalsTransform;
        //�÷��̾� ��ġ��
        public Transform playerTransform;
        #endregion

        //�տ�����
        void Update()
        {
        
            //show ����
            if(potalMenu.activeSelf)
            {
                potalMenu.transform.position = playerTransform.position + new Vector3(playerTransform.forward.x, 0f, playerTransform.forward.z).normalized * distance;
                potalMenu.transform.LookAt(new Vector3(playerTransform.position.x, potalMenu.transform.position.y, playerTransform.position.z));
                potalMenu.transform.forward *= -1;
            }
        }
        //��Ż UI ����
        public void OnPotalUI()
        {
            potalMenu.SetActive(true);
           
        }

        //��Ż �̵� ��ư
        public void Teleportation(int index)
        {
            if (index >= 0 && index < potalsTransform.Length)
            {
                playerTransform.position = potalsTransform[index].position;
            }
        }

        //UI �ݱ�
        public void OffPotalUI()
        {
            potalMenu.SetActive(false); 
        }
    }
}