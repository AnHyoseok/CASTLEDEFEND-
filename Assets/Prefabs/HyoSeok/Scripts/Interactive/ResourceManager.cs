using Defend.Player;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    private PlayerState playerState;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            playerState = Object.FindAnyObjectByType<PlayerState>(); // PlayerState를 찾기
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 자원 추가
    public void AddResources(int amount)
    {
       

        //돌이면
        playerState.AddRock(amount);
        
        //나무면
        playerState.AddTree(amount);

        //보유자원 표시
        playerState.ShowStatus();
    }
}