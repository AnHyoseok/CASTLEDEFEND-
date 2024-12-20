using Defend.TestScript;
using UnityEngine;

public class HealthBasedCastle : MonoBehaviour
{
    //[SerializeField]
    //[Range(0f, 100f)]
    public float castleHealth;

    private Health health;  // 용훈님 스크립트 'Health' 참조
    private float maxCastleHealth;  // 최대체력

    public GameObject castleState01;
    public GameObject castleState02;
    public GameObject castleState03;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = GetComponent<Health>();
        maxCastleHealth = health.maxHealth; // 최대체력 불러오기

        health.OnDamaged += ChangeHealth;
        health.OnHeal += ChangeHealth;

        //castleState01 = transform.GetChild(1).gameObject;
        //castleState02 = transform.GetChild(2).gameObject;
        //castleState03 = transform.GetChild(3).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        maxCastleHealth = health.maxHealth; // 업데이트에서 최대체력 항상 체크 (체력 업그레이드 대비)

        if (health.GetRatio() >= 0.7f)  // 현재HP가 최대체력의 70퍼센트와 같거나 높을때
        {
            castleState01.SetActive(true);
            castleState02.SetActive(false);
            castleState03.SetActive(false);
        }
        else if (health.GetRatio() >= 0.3f) // 현재HP가 최대체력의 30퍼센트와 같거나 높을때
        {
            castleState01.SetActive(false);
            castleState02.SetActive(true);
            castleState03.SetActive(false);
        }
        else
        {
            castleState01.SetActive(false);
            castleState02.SetActive(false);
            castleState03.SetActive(true);
        }
        
    }

    private void ChangeHealth(float amount)
    {
        castleHealth += amount;
    }
}
