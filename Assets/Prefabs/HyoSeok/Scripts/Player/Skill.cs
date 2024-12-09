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
        //����
        private EnemyMoveController moveController;
        private BuffContents buffContents;
        //ĵ����
        public GameObject canvas;
        //
        public GameObject player;

      
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
            audioSource.clip = magnetSound;
            audioSource.playOnAwake = false; // �ڵ� ��� ����

        }

        public void MagnetPlay()
        {

            StartCoroutine(Magnet());

        }

        //�ʵ��� �ڿ� ��� ����ϴ� ��ų
        public IEnumerator Magnet()
        {
    
           canvas.SetActive(false);
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
            Destroy(magnetEffect );
            //���� ����
            audioSource.Stop();
            ResourceManager.speed = originalMagnetSpeed;
            ResourceManager.distance = originalMagnetDistance;
        }

        //���� ���� ��ų
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
        //Ÿ�� ���� ��
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

        //��ų ��ٿ�
        IEnumerator SkillCoolDown()
        {
            yield return null;
        }
    }



}


