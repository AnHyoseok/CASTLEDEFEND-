using UnityEngine;
using Defend.Enemy;
using System.Collections.Generic;
using Defend.item;
using Defend.Utillity;
using Defend.Enemy.Skill;
using UnityEngine.Events;
using System;

namespace Defend.TestScript
{
    public enum EnemyType
    {
        Buffer,
        Warrior,
        Tanker,
        Boss
    }

    // Renderer와 머티리얼 인덱스를 관리하기 위한 구조체
    [System.Serializable]
    public struct RendererIndexData
    {
        public Renderer renderer;
        public int metarialIndx;

        public RendererIndexData(Renderer _renderer, int index)
        {
            renderer = _renderer;
            metarialIndx = index;
        }
    }

    public class EnemyController : MonoBehaviour
    {
        #region Variables
        //Enemy IMG Sprite
        public Sprite sprite;

        //애니메이터
        private Animator animator;
        private float animatorSpeed;

        //스킬을 받아올 컴포넌트
        private SkillBase skill; // 공통 스킬 참조
        private bool channeling = false; //스킬을 사용 중인지 체크
        public UnityAction OnChanneling;


        //버프와 디버프 VFX
        public ParticleSystem buffParticleSystem;
        public ParticleSystem debuffParticleSystem;
        public ParticleSystem healParticleSystem;

        //SFX
        public AudioClip buffAudioClip;
        public AudioClip debuffAudioClip;
        public AudioClip healAudioClip;
        public AudioClip dieAudioClip;

        private Health health;//체력담당 컴포넌트
        private EnemyMoveController moveController;//이동담당 컴포넌트

        private EnemyAttackController attackController;//공격담당 컴포넌트
        private bool isAttacking = false;

        public Vector3 positionOffset;//미사일이 날아와서 부딪힐 곳을 offset으로 할당
        public float scaleOffset;//몬스터마다 타워쪽 이펙트에 스케일을 조절하기위한 scaleOffset

        //떨어뜨릴 골드 개수
        public int rewardGoldCount;
        public int RewardGoldCount { get { return rewardGoldCount; } private set { rewardGoldCount = value; } } //참조가 필요시 사용할 레퍼런스
        public GameObject goldPrefab;           //코인 프리팹
        public Transform offsetTransform;       //생성될 위치 (위로 조정)

        // VFX 관련 변수
        public Material bodyMaterial; // 데미지를 줄 머티리얼
        [GradientUsage(true)] public Gradient hitEffectGradient; // 데미지 컬러 그라디언트 효과
        [GradientUsage(true)] public Gradient healEffectGradient; // 데미지 컬러 그라디언트 효과
        private List<RendererIndexData> bodyRenderers = new List<RendererIndexData>();
        private MaterialPropertyBlock bodyFlashMaterialPropertyBlock;
        private float lastTimeEffect; // 마지막으로 효과가 발생한 시간

        [SerializeField] private float flashDuration = 0.5f;
        private bool isFlashing; // 반짝거림 상태
        private Gradient currentEffectGradient; // 현재 적용 중인 그라디언트

        public EnemyType type;


        #endregion
        void Start()
        {
            //참조
            animator = GetComponent<Animator>();
            health = GetComponent<Health>();
            moveController = GetComponent<EnemyMoveController>();
            attackController = GetComponent<EnemyAttackController>();


            //UnityAction
            health.OnDie += OnDie;
            health.OnHeal += OnHeal;
            health.OnDamaged += OnDamaged;

            //버프와 디버프관련 UnityAction
            health.Armorchange += UpdateArmor;
            moveController.MoveSpeedChanged += UpdateSpeed;
            attackController.AttackDamageChanged += UpdateAttactDamage;
            attackController.OnAttacking += OnAttacking;

            // 반짝임 효과 초기화
            bodyFlashMaterialPropertyBlock = new MaterialPropertyBlock();
            Renderer[] renderers = GetComponentsInChildren<Renderer>(true);
            foreach (var renderer in renderers)
            {
                for (int i = 0; i < renderer.sharedMaterials.Length; i++)
                {
                    if (renderer.sharedMaterials[i] == bodyMaterial)
                    {
                        bodyRenderers.Add(new RendererIndexData(renderer, i));
                    }
                }
            }

            //초기화
            animatorSpeed = animator.speed;

            buffParticleSystem.Stop();
            debuffParticleSystem.Stop();
            healParticleSystem.Stop();

            switch (type)
            {
                case EnemyType.Tanker:
                    skill = gameObject.GetComponent<TankerSkill>();
                    break;
                case EnemyType.Warrior:
                    skill = gameObject.GetComponent<WarriorSkill>();
                    break;
                case EnemyType.Buffer:
                    skill = gameObject.GetComponent<WizardSkill>();
                    break;
                case EnemyType.Boss:
                    skill = gameObject.GetComponent<BossSkill>();
                    break;
                default:
                    Debug.LogWarning("Unknown EnemyType. No skill assigned.");
                    break;
            }
        }



        void Update()
        {
            // 데미지 효과 업데이트
            if (isFlashing)
            {
                UpdateEffect();
            }

            if (skill.CanActivateSkill(health.GetRatio()) && !channeling && !isAttacking)
            {
                if (type == EnemyType.Boss)
                {
                    skill.SoundPlay(2, 30);
                }
                else
                {
                    skill.SoundPlay();
                }
                animator.SetTrigger(Constants.ENEMY_ANIM_SKILLTRIGGER);
                ChangeChannelingStatus();
            }
        }

        public void ChangeChannelingStatus()
        {
            channeling = !channeling;
            OnChanneling?.Invoke();
        }

        private void OnDamaged(float amount)
        {
            //Debug.Log("공격 받음");
            TriggerEffect(hitEffectGradient); // 데미지 효과 적용
        }

        private void OnHeal(float amount)
        {
            //Debug.Log("힐 받음");
            healParticleSystem.Play();
            if (healAudioClip != null)
            {
                AudioUtility.CreateSFX(healAudioClip, transform.position, AudioUtility.AudioGroups.BuffAndDebuff, 1);
            }
        }

        private void OnDie()
        {
            AudioUtility.CreateSFX(dieAudioClip, transform.position, AudioUtility.AudioGroups.EFFECT);

            //살아있는 에너미 수 감소
            ListSpawnManager.enemyAlive--;

            animator.SetBool(Constants.ENEMY_ANIM_ISDEATH, true);

            //죽으면 코인 흩뿌리기
            ScatterCoins();

            // 애니메이션 종료 후 제거되는 로직은 StateMachineBehaviour에서 처리
        }

        private void TriggerEffect(Gradient effectGradient)
        {
            lastTimeEffect = Time.time; // 효과 시작 시간 기록
            currentEffectGradient = effectGradient; // 현재 적용 중인 그라디언트 설정
            isFlashing = true; // 반짝거림 시작
        }

        private void UpdateEffect()
        {
            // 반짝거림 지속 시간 계산
            float elapsed = Time.time - lastTimeEffect;

            if (elapsed > flashDuration)
            {
                // 반짝거림 효과 종료
                ResetMaterialProperties();
                return;
            }

            // 현재 시간에 따른 색상 계산
            Color currentColor = currentEffectGradient.Evaluate(elapsed / flashDuration);
            bodyFlashMaterialPropertyBlock.SetColor("_EmissionColor", currentColor);

            // 각 렌더러에 효과 적용
            foreach (var data in bodyRenderers)
            {
                data.renderer.SetPropertyBlock(bodyFlashMaterialPropertyBlock, data.metarialIndx);
            }
        }

        private void ResetMaterialProperties()
        {
            // 반짝거림 효과를 초기화하여 원래 상태로 되돌림
            bodyFlashMaterialPropertyBlock.SetColor("_EmissionColor", Color.black);
            foreach (var data in bodyRenderers)
            {
                data.renderer.SetPropertyBlock(bodyFlashMaterialPropertyBlock, data.metarialIndx);
            }
            isFlashing = false;
        }

        private void ScatterCoins()
        {
            for (int i = 0; i < rewardGoldCount; i++)
            {
                // 코인 생성
                Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                GameObject coin = Instantiate(goldPrefab, spawnPosition, Quaternion.identity);
                DropItem item = coin.GetComponent<DropItem>();
                item.amount = 1f;
                item.resourceName = "Money";

            }
        }

        private void UpdateSpeed(float value, float rate)
        {
            // rate에 따라 버프 또는 디버프 효과 실행
            PlayEffect(rate);

            animator.speed = animatorSpeed * (1.0f + rate);
        }

        private void UpdateArmor(float amount)
        {
            //Debug.Log($"{amount}만큼 방어력 증/감소됨!");
            PlayEffect(amount);
        }

        private void UpdateAttactDamage(float amount)
        {
            //Debug.Log($"{amount}만큼 공격력 증/감소됨!");
            PlayEffect(amount);
        }

        private void PlayEffect(float amount)
        {
            if (amount > 0)
            {
                AudioUtility.CreateSFX(buffAudioClip, transform.position, AudioUtility.AudioGroups.BuffAndDebuff, 1);
                buffParticleSystem.Play();
            }
            else if (amount < 0)
            {
                AudioUtility.CreateSFX(debuffAudioClip, transform.position, AudioUtility.AudioGroups.BuffAndDebuff, 1);
                debuffParticleSystem.Play();
            }
        }

        //공격중인지 확인하는 UnityAction
        private void OnAttacking()
        {
            isAttacking = !isAttacking;
        }
    }
}