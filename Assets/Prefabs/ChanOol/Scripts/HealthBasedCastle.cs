using Defend.TestScript;
using UnityEngine;

public class HealthBasedCastle : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 100f)]
    public float castleHealth;

    private Health health;

    private GameObject castleState01;
    private GameObject castleState02;
    private GameObject castleState03;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = GetComponent<Health>();

        health.OnDamaged += ChangeHealth;
        health.OnHeal += ChangeHealth;

        castleState01 = transform.GetChild(1).gameObject;
        castleState02 = transform.GetChild(2).gameObject;
        castleState03 = transform.GetChild(3).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (castleHealth > 69)
        {
            castleState01.SetActive(true);
            castleState02.SetActive(false);
            castleState03.SetActive(false);
        }
        else if (castleHealth > 29)
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
