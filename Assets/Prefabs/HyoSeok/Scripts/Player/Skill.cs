using UnityEngine;
using Defend.Interactive;
using System.Collections;
using Defend.Enemy;
using Defend.Tower;
using TMPro;
using UnityEngine.UI;
using Defend.Utillity;
using Defend.Player;
using Unity.VisualScripting;
using Defend.TestScript;
using System.Net;

namespace Defend.UI
{
    public class Skill : MonoBehaviour
    {
        #region Variables
        TowerBase[] towerbase;
        //Status[] status;
        //참조
        private EnemyMoveController moveController;

        //캔버스
        public GameObject canvas;
        //
        public GameObject player;

        //쿨타임
        public GameObject[] coolTimeUI;        //쿨타임이미지
        public TextMeshProUGUI[] coolTimeText; //쿨타임 시간
        [SerializeField] float magnetCoolTime = 60f;
        [SerializeField] float timeStopCoolTime = 120f;
        [SerializeField] float atkSpeedUpCoolTime = 180f;

        private bool[] isCooldown;
        public Button[] skillButtons;
        //자석값 변수
        private float originalMagnetSpeed;
        private float originalMagnetDistance;

        //타임스탑 변수
        private GameObject[] enemies;

        EnemyState[] enemyState;

        //타워공속업 변수
        //private float originalshootDelay;

        //사운드
        public AudioClip magnetSound;
        private AudioSource audioSource;

        //이펙트
        public GameObject magnetEffectPrefab;   //자석연출
        #endregion

        void Start()
        {

            moveController = FindFirstObjectByType<EnemyMoveController>();
            audioSource = player.AddComponent<AudioSource>();

            isCooldown = new bool[coolTimeUI.Length];
            audioSource.clip = magnetSound;
            audioSource.playOnAwake = false; // 자동 재생 방지

        }
        //필드위 자원 모두 흡수하는 스킬
        public void MagnetPlay()
        {

            StartCoroutine(Magnet());

        }


        public IEnumerator Magnet()
        {

            //canvas.SetActive(false);
            //연출시작 
            GameObject magnetEffect = Instantiate(magnetEffectPrefab, player.transform.position + transform.forward, Quaternion.identity);
            magnetEffect.transform.SetParent(transform);
            //사운드 시작
            audioSource.Play();
            //yield return new WaitForSeconds(1f);    //연출할 시간


            originalMagnetSpeed = ResourceManager.speed;
            originalMagnetDistance = ResourceManager.distance;
            ResourceManager.speed = 20f;
            ResourceManager.distance = 500f;
            yield return new WaitForSeconds(3f);
            //연출 종료
            Destroy(magnetEffect);
            //사운드 종료
            audioSource.Stop();
            ResourceManager.speed = originalMagnetSpeed;
            ResourceManager.distance = originalMagnetDistance;
        }

        //몬스터 정지 스킬
        public void TimeStopPlay()
        {
            StartCoroutine(TimeStop());
        }

        public IEnumerator TimeStop()
        {
            EnemyState[] enemys = FindObjectsByType<EnemyState>(FindObjectsSortMode.None);

            
            foreach (EnemyState e in enemys)
            {
                if (e == null)
                {
                    continue; 
                }
                e.gameObject.GetComponent<EnemyMoveController>().enabled = false;
              
                e.gameObject.GetComponent<Animator>().speed=0.01f;
                Debug.Log($"e={e.gameObject}");
                ////연출시작 
                GameObject magnetEffect = Instantiate(magnetEffectPrefab, e.transform.position + transform.forward, Quaternion.identity);
                magnetEffect.transform.SetParent(transform);
                //연출 종료
                Destroy(magnetEffect, 3f);
            }
         
            yield return new WaitForSeconds(3f);

            foreach (EnemyState e in enemys)
            {
                if (e == null)
                {
                    continue;
                }
                e.gameObject.GetComponent<EnemyMoveController>().enabled = true;
              
                e.gameObject.GetComponent<Animator>().speed = 1f;

            }
     
            //사운드 종료
            audioSource.Stop();
        }
        //타워 공속 업
        public void TowerAtkSpeedPlay()
        {

            StartCoroutine(TowerAttakSpeed());

        }

        public IEnumerator TowerAttakSpeed()
        {
            //연출시작 
            GameObject magnetEffect = Instantiate(magnetEffectPrefab, player.transform.position + transform.forward, Quaternion.identity);
            magnetEffect.transform.SetParent(transform);
            //사운드 시작
            audioSource.Play();
            if (CastleUpgrade.buffContents != null)
            {
                CastleUpgrade.buffContents.duration = 5f;
                CastleUpgrade.buffContents.atk = 0f;
                CastleUpgrade.buffContents.armor = 0f;
                CastleUpgrade.buffContents.healthRegen = 1f;
                CastleUpgrade.buffContents.manaRegen = 1f;
                CastleUpgrade.buffContents.atkRange = 1f;
                CastleUpgrade.buffContents.shootDelay = 5f;
            }

            towerbase = FindObjectsByType<TowerBase>(FindObjectsSortMode.None);
            foreach (var tower in towerbase)
            {
                tower.BuffTower(CastleUpgrade.buffContents, false);
            }
            yield return new WaitForSeconds(3f);

            //연출 종료
            Destroy(magnetEffect);
            //사운드 종료
            audioSource.Stop();
        }

        //쿨시작
        public void StartCooldown(int skillIndex)
        {
            if (isCooldown[skillIndex]) return;

            isCooldown[skillIndex] = true;
            skillButtons[skillIndex].interactable = false;
            StartCoroutine(SkillCoolDown(skillIndex));
        }

        //스킬 쿨다운
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

            //UI 업데이트
            float elapsedTime = 0f;
            while (elapsedTime < cooldownTime)
            {
                elapsedTime += Time.deltaTime;
                float fillAmount = Mathf.Clamp01(1 - (elapsedTime / cooldownTime));
                coolTimeUI[skillIndex].GetComponent<Image>().fillAmount = fillAmount;
                coolTimeText[skillIndex].text = Mathf.Ceil(cooldownTime - elapsedTime).ToString();

                yield return null;
            }

            //UI 초기화
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


