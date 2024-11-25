using Defend.Player;
using TMPro;
using Unity.Cinemachine;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Defend.UI
{
    public class PlayerHandController : MonoBehaviour
    {
        #region Variables
        //����
        PlayerState playerState;
        //�׽�Ʈ�� �ð�
        public TextMeshProUGUI timeText;
        public GameObject viewButton;
        public GameObject uiOnButton;
        public GameObject viewCanvas;

        //�ڿ�����
        public TextMeshProUGUI rockheld;
        public TextMeshProUGUI treeheld;
        public TextMeshProUGUI moneyheld;

        private float time;

        //��ư �����¿���
        private bool isOnUi;

        //ž��
        private bool isViewChange = false;
        private Transform currentTransform;
        private Vector3 originalPosition; // ���� ��ġ
        private Quaternion originalRotation; // ���� ȸ��
        #endregion

        private void Start()
        {
            playerState = GetComponent<PlayerState>();
            //�ʱ�ȭ
            currentTransform = gameObject.transform;
            originalPosition = currentTransform.position;
            originalRotation = currentTransform.rotation;
        }

        private void Update()
        {

            time += Time.deltaTime;
            timeText.text = time.ToString("F2");
            rockheld.text = playerState.rock.ToString();
            treeheld.text = playerState.tree.ToString();
            moneyheld.text = playerState.money.ToString() + "G";

        }

        public void ViewChange()
        {
           
            CinemachineBrain cinemachineBrain = gameObject.GetComponent<CinemachineBrain>();
            if (isViewChange)
            {
                // ž�信�� ���� ��ġ�� ���ư���
                currentTransform.position = originalPosition;
                currentTransform.rotation = originalRotation;
                cinemachineBrain.enabled = false; 
            }
            else
            {
                // ���� ��ġ ����
                originalPosition = currentTransform.position;
                originalRotation = currentTransform.rotation;
                cinemachineBrain.enabled = true;
            }

            isViewChange = !isViewChange;
        }

        //ui �ѱ�
        public void ShowButton()
        {
            if (isOnUi == true)
            {
                uiOnButton.SetActive(true);
                viewCanvas.SetActive(false);
            }
            else
            {
                uiOnButton.SetActive(false);
                viewCanvas.SetActive(true);
            }
        }

        //ui ����
        public void hideButton()
        {
            if (isOnUi == true)
            {
                uiOnButton.SetActive(false);
                viewCanvas.SetActive(true);
            }
            else
            {
                uiOnButton.SetActive(true);
                viewCanvas.SetActive(false);
            }
        }



    }
}