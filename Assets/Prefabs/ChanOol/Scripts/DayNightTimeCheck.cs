using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Defend.Enemy;
public class DayNightTimeCheck : MonoBehaviour
{
    [Header("스카이 박스3개, Light 넣기")]
    [SerializeField] Material skybox1; // 아침
    [SerializeField] Material skybox2; // 새벽/해지기 전
    [SerializeField] Material skybox3; // 밤
    [SerializeField] Material skybox4; // 

    //[SerializeField] GameObject Lamp;

    [SerializeField] GameObject directionalLight; // 아침에는 활성화

    [SerializeField] int stageWave = 1; // 스테이지 웨이브 (1, 2, 3 중 하나로 설정)

    [SerializeField] private GameObject listSpawnManagerObject; // ListSpawnManager가 붙은 GameObject를 드래그로 연결
    private ListSpawnManager listSpawnManager;

    void Start()
    {
        if (listSpawnManagerObject != null)
        {
            // 특정 GameObject에서 ListSpawnManager를 가져옴
            listSpawnManager = listSpawnManagerObject.GetComponent<ListSpawnManager>();

            if (listSpawnManager == null)
            {
                Debug.LogError("ListSpawnManager 컴포넌트를 찾을 수 없습니다!");
            }
        }
        else
        {
            Debug.LogError("ListSpawnManager가 붙은 GameObject를 설정하지 않았습니다!");
        }
    }

    private void Update()
    {
        stageWave = listSpawnManager.waveCount;
        UpdateEnvironment(stageWave);
    }

    /*// 스테이지 웨이브에 따라 환경 변경
    public void SetStageWave(int newStageWave)
    {
        stageWave = newStageWave;
        UpdateEnvironment();
    }*/

    void UpdateEnvironment(int stageWave)
    {
        switch (stageWave)
        {
            case 1: // 아침
                //Lamp.SetActive(false);
                directionalLight.SetActive(true);
                RenderSettings.skybox = skybox1;
                break;

            case 2: // 노을
                //Lamp.SetActive(false);
                directionalLight.SetActive(true);
                RenderSettings.skybox = skybox2;
                break;

            case 3: // 밤
                //Lamp.SetActive(true);
                directionalLight.SetActive(false);
                RenderSettings.skybox = skybox3;
                break;

            case 4: // 밤
                //Lamp.SetActive(true);
                directionalLight.SetActive(false);
                RenderSettings.skybox = skybox4;
                break;

            default:
                Debug.LogWarning("Invalid stageWave value: " + stageWave);
                break;
        }
    }
}
