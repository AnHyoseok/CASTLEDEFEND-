using UnityEngine;

public class LightController : MonoBehaviour
{
    private Light[] lights; // 자식들에게 달린 Light 컴포넌트를 받아올 변수
    public bool isOn = false; // 램프의 현재 상태
    private float[] originalIntensities; // 각 라이트의 초기 밝기 저장
    private bool isUpdating = false; // 밝기 업데이트 여부
    [SerializeField] private float timerControl = 5f;

    private void Start()
    {
        // 모든 자식 라이트 가져오기
        lights = GetComponentsInChildren<Light>();

        // 초기 밝기를 저장할 배열 생성
        originalIntensities = new float[lights.Length];

        for (int i = 0; i < lights.Length; i++)
        {
            originalIntensities[i] = lights[i].intensity; // 초기 밝기를 저장
            lights[i].intensity = 0f; // 시작 시 꺼진 상태로 설정
        }
    }

    public void SetLampState(bool turnOn)
    {
        isOn = turnOn;
        isUpdating = true; // 상태 변경 후 업데이트 활성화
    }

    private void Update()
    {
        if (!isUpdating) return; // 업데이트가 필요하지 않으면 종료

        bool allLightsUpdated = true;

        for (int i = 0; i < lights.Length; i++)
        {
            float targetIntensity = isOn ? originalIntensities[i] : 0f; // 켜질 때는 원래 밝기, 꺼질 때는 0
            float currentIntensity = lights[i].intensity;

            // 밝기를 점진적으로 변경
            lights[i].intensity = Mathf.MoveTowards(currentIntensity, targetIntensity, Time.deltaTime * timerControl);

            // 목표 밝기에 도달하지 않았으면 계속 업데이트
            if (!Mathf.Approximately(lights[i].intensity, targetIntensity))
            {
                allLightsUpdated = false;
            }
        }

        // 모든 라이트가 목표 밝기에 도달하면 업데이트 종료
        if (allLightsUpdated)
        {
            isUpdating = false;
        }
    }
}
