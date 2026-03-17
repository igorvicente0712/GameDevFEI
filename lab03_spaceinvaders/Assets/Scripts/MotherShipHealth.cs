using UnityEngine;

public class MotherShipHealth : MonoBehaviour
{
    public int hitsToKill = 3;  // quantos tiros aguenta
    private int currentHits = 0;
    private EnemyController enemyController;

    void Awake()
    {
        enemyController = GetComponent<EnemyController>();
    }

    public void TakeHit()
    {
        currentHits++;

        Debug.Log("MotherShip levou hit: " + currentHits + "/" + hitsToKill);

        if (currentHits >= hitsToKill)
        {
            if (enemyController != null)
            {
                enemyController.OnDeath();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}