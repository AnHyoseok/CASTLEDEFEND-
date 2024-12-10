using UnityEngine;
using Defend.Interactive;
using System.Collections;
using Defend.Enemy;
using Defend.Tower;
using TMPro;
using UnityEngine.UI;
using Defend.Utillity;

namespace Defend.UI
{
    public class Skill : MonoBehaviour
    {
        #region Variables
        TowerBase[] towerbase;
        //Status[] status;
        //����
        private EnemyMoveController moveController;
        private BuffContents buffContents;
        //ĵ����
        public GameObject canvas;
        //
        public GameObject player;

        //��Ÿ��
        public GameObject[] coolTimeUI;        //��Ÿ���̹���
        public TextMeshProUGUI[] coolTimeText; //��Ÿ�� �ð�
        [SerializeField] float magnetCoolTime = 60f;
        [SerializeField] float timeStopCoolTime = 120f;
        [SerializeField] float atkSpeedUpCoolTime = 180f;

        private bool[] isCooldown;
        public Button[] skillButtons;
        //�ڼ��� ����
        private float originalMagnetSpeed;
        private float originalMagnetDistance;

        //Ÿ�ӽ�ž ����
        private GameObject[] enemies;
        private Vector3[] originalVelocities;

        //Ÿ�����Ӿ� ����
        private float originalshootDelay;

        //����
        public AudioClip magnetSound;
        private AudioSource audioSource;

        //����Ʈ
        public GameObject magnetEffectPrefab;   //�ڼ�����
        #endregion

        void Start()
        {

            moveController = FindFirstObjectByType<EnemyMoveController>();
            audioSource = player.AddComponent<AudioSource>();

            isCooldown = new bool[coolTimeUI.Length];
            audioSource.clip = magnetSound;
            audioSource.playOnAwake = false; // �ڵ� ��� ����

        }
        //�ʵ��� �ڿ� ��� ����ϴ� ��ų
        public void MagnetPlay()
        {

            StartCoroutine(Magnet());

        }


        public IEnumerator Magnet()
        {

            //canvas.SetActive(false);
            //������� 
            GameObject magnetEffect = Instantiate(magnetEffectPrefab, player.transform.position + transform.forward, Quaternion.identity);
            magnetEffect.transform.SetParent(transform);
            //���� ����
            audioSource.Play();
            //yield return new WaitForSeconds(1f);    //������ �ð�


            originalMagnetSpeed = ResourceManager.speed;
            originalMagnetDistance = ResourceManager.distance;
            ResourceManager.speed = 20f;
            ResourceManager.distance = 500f;
            yield return new WaitForSeconds(3f);
            //���� ����
            Destroy(magnetEffect);
            //���� ����
            audioSource.Stop();
            ResourceManager.speed = originalMagnetSpeed;
            ResourceManager.distance = originalMagnetDistance;
        }

        //���� ���� ��ų
        public void TimeStop()
        {

        }


        //Ÿ�� ���� ��
        public void TowerAtkSpeedPlay()
        {
            towerbase = FindObjectsByType<TowerBase>(FindObjectsSortMode.None);

            if (buffContents == null)
            {
                Debug.LogError("buffContents is null!");
                return;
            }

            buffContents.shootDelay = 1f;
            StartCoroutine(TowerAttakSpeed());


        }

        public IEnumerator TowerAttakSpeed()
        {

            originalshootDelay = buffContents.shootDelay;

            if (buffContents != null)
            {
                buffContents.shootDelay += 30f;
            }
            yield return new WaitForSeconds(3f);
            towerbase = FindObjectsByType<TowerBase>(FindObjectsSortMode.None);
            foreach (var tower in towerbase)
            {
                tower.BuffTower(buffContents, true);
            }

            buffContents.shootDelay = originalshootDelay;
        }

        //�����
        public void StartCooldown(int skillIndex)
        {
            if (isCooldown[skillIndex]) return;

            isCooldown[skillIndex] = true;
            skillButtons[skillIndex].interactable = false;
            StartCoroutine(SkillCoolDown(skillIndex));
        }

        //��ų ��ٿ�
        IEnumerator SkillCoolDown(int skillIndex)
        {
            float cooldownTime = 0f;

         
            switch (skillIndex)
            {
                case 0:
                    cooldownTime = magnetCoolTime;
                    break;
                case 1: 
                    cooldownTime = timeStopCoolTime;
                    break;
                case 2: 
                    cooldownTime = atkSpeedUpCoolTime;
                    break;
            }

            //UI ������Ʈ
            float elapsedTime = 0f;
            while (elapsedTime < cooldownTime)
            {
                elapsedTime += Time.deltaTime;
                float fillAmount = Mathf.Clamp01(1 - (elapsedTime / cooldownTime));
                coolTimeUI[skillIndex].GetComponent<Image>().fillAmount = fillAmount; 
                coolTimeText[skillIndex].text = Mathf.Ceil(cooldownTime - elapsedTime).ToString();

                yield return null;
            }

            //UI �ʱ�ȭ
            coolTimeUI[skillIndex].GetComponent<Image>().fillAmount = 1; 
            coolTimeText[skillIndex].text = "";
            skillButtons[skillIndex].interactable = true;
            isCooldown[skillIndex] = false; 
        }

        public void OnSkillButtonClick(int skillIndex)
        {
            StartCooldown(skillIndex);
           
        }
    }



}


