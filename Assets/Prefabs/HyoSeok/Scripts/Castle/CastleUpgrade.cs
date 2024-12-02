using Defend.TestScript;
using Mono.Cecil;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using Defend.Interactive;
namespace Defend.Player
{
    /// <summary>
    /// Castle���� ����(���׷��̵�)
    /// </summary>
    public class CastleUpgrade : MonoBehaviour
    {
        #region Variables
        //����
        HealthBasedCastle HealthBasedCastle;
        PlayerState playerState;
        Health health;
        public GameObject castle;
        //ü���ӵ�
        private float interval;

        //��Ż Ȱ��ȭ
        public GameObject potal;
        public Renderer[] potalsColor;
        public GameObject[] potalsEffect;

        //���� ���ſ���
        private bool isPotalActive = false;
        private bool isMoveSpeedUp = false;
        private bool isMoneyUp = false;
        private bool isTreeUp = false;
        private bool isRockUp = false;

        //��ư ��Ȱ��ȭ
        public Button[] btnList;

        //�̵��ӵ� ����
        public DynamicMoveProvider dynamicMove;
        #endregion
        private void Start()
        {

            //����
            health = castle.GetComponent<Health>();

            for (int i = 0; i < potalsColor.Length; i++)
            {
                potalsColor[i] = GetComponent<Renderer>();
            }

            //btn = item.transform.Find("Button").GetComponent<Button>();

            //�ʱ�ȭ
            interval = health.Rginterval;

            health.HPTime(interval, 1f);//1�ʸ��� 1�� ü���� ȸ��

        }


        //ü�� ��� ȸ��
        public void FullonHP()
        {

            //100�� �Ҹ�� Ǯ�� //Ǯ���϶� ���� ���� 
            if (health != null && health.CurrentHealth != health.maxHealth)
            {
                health.CurrentHealth = health.maxHealth;
                playerState.SpendMoney(100f);
            }
            else
            {
                //��ư ��Ȱ��ȭ 
                offBuyButton();
            }

        }

        //ü�� �ø���
        public void HPUpgrade()
        {
            if (health != null && health.maxHealth <= 1000f)
            {
                //100�� �ø���
                health.IncreaseMaxHealth(100f);
                //100��� �Һ�
                playerState.SpendMoney(100f);
            }
            else
            {
                //��ư ��Ȱ��ȭ
                offBuyButton();
            }
        }

        //ü��ȸ���� 1�ʿ� interval ��ŭ ȸ��
        public void HPTimeUpgrade()
        {

            if (health != null && interval <= 5f)
            {
                interval++;
            }
            else
            {
                //��ư ��Ȱ��ȭ
                offBuyButton();
            }

        }

        //�ڿ�ŉ�淮
        public void MoneyGain()
        {
            if(isMoneyUp == false)
            {
                ResourceManager.Instance.UpgradeResourceGain("money", 1.2f); //20% �� ���
                playerState.SpendMoney(100f);
                isMoneyUp = true;
            }
            else
            {
                //��ư ��Ȱ��ȭ
                offBuyButton();
            }
        }
        public void TreeGain()
        {
            if (isTreeUp==false)
            {
                ResourceManager.Instance.UpgradeResourceGain("tree", 1.5f); //50% �� ���
                playerState.SpendMoney(100f);
                isTreeUp = true;
            }
            else
            {
                //��ư ��Ȱ��ȭ
                offBuyButton();
            }
        }
        public void RockGain()
        {
            if (isRockUp ==false)
            {
                ResourceManager.Instance.UpgradeResourceGain("rock", 1.5f); //50% �� ���
                playerState.SpendMoney(100f);
                isRockUp = true;
            }
            else
            {
                //��ư ��Ȱ��ȭ
                offBuyButton();
            }

        }



        //�÷��̾� �̵��ӵ���(�Ź߸��)
        public void MoveSpeed()
        {
            if (isMoveSpeedUp == false)
            {
                dynamicMove.moveSpeed = 10f;
                playerState.SpendMoney(100f);
                isMoveSpeedUp = true;
                //��ư ��Ȱ��ȭ
                offBuyButton();
            }

        }

        //��Ż Ȱ��ȭ
        public void PotalActivate()
        {
            if (isPotalActive == false)
            {
                for (int i = 0; i < potalsColor.Length; i++)
                {
                    potalsColor[i].material.color = Color.red;
                    potalsEffect[i].SetActive(true);
                }
                XRSimpleInteractable xRSimpleInteractable = potal.GetComponent<XRSimpleInteractable>();
                xRSimpleInteractable.enabled = true;
                playerState.SpendMoney(100f);
                isPotalActive = true;
                //��ư ��Ȱ��ȭ
                offBuyButton();
            }

        }

        //������ ŉ�� ���� ����
        public void AutoGain()
        {

        }


        //��ư ��Ȱ��ȭ
        private void offBuyButton()
        {
            btnList[1].interactable = false; // ��ư Ŭ���� ��Ȱ��
        }

        //��ư Ȱ��ȭ
        private void onBuyButton()
        {
            btnList[1].interactable = true; //��ư Ŭ�� Ȱ��ȭ
        }

    }
}