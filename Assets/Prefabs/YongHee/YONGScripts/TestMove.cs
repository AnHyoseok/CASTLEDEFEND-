using Defend.Tower;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    #region Variables
    public Transform target;
    public TowerBase[] towers;
    #endregion
    void Start()
    {
        towers = FindObjectsByType<TowerBase>(FindObjectsSortMode.None);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            // 각 TowerBase 오브젝트에 대해 BuffTower 호출
            foreach (TowerBase tower in towers)
            {
                tower.BuffTower(
                    duration: 5f,
                    atk: 10f,
                    armor: 5f,
                    shootDelay: 1.2f,
                    atkRange: 2f,
                    healthRegen: 2f,
                    manaRegen: 3f
                );
            }
        }
    }
}
