using UnityEngine;
using Defend.Interactive;
using System.Collections;
using Defend.Enemy;
using static UnityEngine.XR.OpenXR.Features.Interactions.HTCViveControllerProfile;
using Defend.Tower;
using System.Linq;
using Defend.UI;

namespace Defend.Player
{
    public class Skill : MonoBehaviour
    {
        #region Variables
        //참조
        private EnemyMoveController moveController;
        private BuffContents buffContents;
        //캔버스
        public GameObject canvas;
        //
        public GameObject player;

      
        //자석값 변수
        private float originalMagnetSpeed;
        private float originalMagnetDistance;

        //타임스탑 변수
        private GameObject[] enemies;
        private Vector3[] originalVelocities;

        //타워공속업 변수
        private float originalshootDelay;

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
            audioSource.clip = magnetSound;
            audioSource.playOnAwake = false; // 자동 재생 방지

        }

        public void MagnetPlay()
        {

            StartCoroutine(Magnet());

        }

        //필드위 자원 모두 흡수하는 스킬
        public IEnumerator Magnet()
        {
    
           canvas.SetActive(false);
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
            Destroy(magnetEffect );
            //사운드 종료
            audioSource.Stop();
            ResourceManager.speed = originalMagnetSpeed;
            ResourceManager.distance = originalMagnetDistance;
        }

        //몬스터 정지 스킬
        public void TimeStop()
        {

        }
        public void TowerAtkSpeedPlay()
        {
            if (buffContents == null)
            {
                Debug.LogError("buffContents is null!");
                return;
            }

            StartCoroutine(TowerAttakSpeed());
        }
        //타워 공속 업
        public IEnumerator TowerAttakSpeed()
        {

            originalshootDelay = buffContents.shootDelay;

            if (buffContents != null)
            {
                buffContents.shootDelay += 30f;
            }
            yield return new WaitForSeconds(3f);
            buffContents.shootDelay = originalshootDelay;
        }

        //스킬 쿨다운
        IEnumerator SkillCoolDown()
        {
            yield return null;
        }
    }



}


