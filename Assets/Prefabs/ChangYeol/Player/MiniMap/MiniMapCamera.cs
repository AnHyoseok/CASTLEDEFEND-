using UnityEngine;

namespace Defend.UI
{
    public class MiniMapCamera : MonoBehaviour
    {
        #region Variables
        //player ��ġ
        public Transform player;
        //�̴ϸ� UI
        public GameObject miniMapUI;
        //���� ��ġ
        private Transform currentPosition;

        //�̴ϸ� ī�޶�
        private Camera miniMapcamera;
        #endregion
        private void Start()
        {
            //����
            miniMapcamera = GetComponent<Camera>();
            //���� player ��ġ ����
            currentPosition.position = Camera.main.transform.position;
        }

        private void Update()
        {
            //�̴ϸʿ��� �������� �̴ϸ� ī�޶� player ��ġ�� ����ٴѴ�
            miniMapcamera.transform.position = new Vector3(player.transform.position.x, miniMapcamera.transform.position.y, player.transform.position.z);
            //���� player ��ġ ����
            //player.position = currentPosition.position;
        }
        //������ ���� UI�� ��Ȱ��ȭ �������� ���� ���� Ȱ��ȭ
        void PlayerMoveUI()
        {
            //player�� �����̰� �ִ��� �ƴ���
            if (this.transform.localPosition == currentPosition.position)
            {
                miniMapUI.SetActive(false);
            }
            else
            {
                miniMapUI.SetActive(true);
            }
        }
    }
}