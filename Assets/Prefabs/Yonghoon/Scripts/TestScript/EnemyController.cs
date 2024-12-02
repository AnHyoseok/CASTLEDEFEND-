using UnityEngine;
using Defend.Enemy;
using System.Collections.Generic;

namespace Defend.TestScript
{
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
        //애니메이터
        private Animator animator;

        //체력담당 컴포넌트
        private Health health;

        public Vector3 offset;

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
        #endregion
        void Start()
        {
            //참조
            animator = GetComponent<Animator>();
            health = GetComponent<Health>();

            //UnityAction
            health.OnDie += OnDie;
            health.OnHeal += OnHeal;
            health.OnDamaged += OnDamaged;

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

        }

        void Update()
        {
            // 데미지 효과 업데이트
            if (isFlashing)
            {
                UpdateEffect();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                OnHeal(1);
            }
        }

        private void OnDamaged(float arg0)
        {
            Debug.Log("공격 받음");
            TriggerEffect(hitEffectGradient); // 데미지 효과 적용

        }

        private void OnHeal(float arg0)
        {
            TriggerEffect(healEffectGradient); // 힐 효과 적용
            Debug.Log("힐 받음");
        }

        private void OnDie()
        {
            //살아있는 에너미 수 감소
            ListSpawnManager.enemyAlive--;

            animator.SetBool("IsDeath", true);

            //Enemy 킬
            Destroy(gameObject, 2f);
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
    }
}