using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BattleController : MonoBehaviour
{
    public Text battleLog;
    public Button attackButton;

    private bool isPlayerTurn = true;
    private bool playerActionCompleted = false;

    private int playerHealth = 100;
    private int enemyHealth = 100;

    void Start()
    {
        attackButton.onClick.AddListener(OnPlayerAttack);
        StartCoroutine(Battle());
    }

    IEnumerator Battle()
    {
        while (playerHealth > 0 && enemyHealth > 0)
        {
            if (isPlayerTurn)
            {
                Debug.Log("Player's Turn started");
                battleLog.text = "Player's Turn";
                playerActionCompleted = false;
                yield return new WaitUntil(() => playerActionCompleted);
                Debug.Log("Player's Turn completed");
                isPlayerTurn = false;
            }
            else
            {
                Debug.Log("Enemy's Turn started");
                battleLog.text = "Enemy's Turn";
                yield return new WaitForSeconds(2.0f);
                EnemyAttack();
                Debug.Log("Enemy's Turn completed");
                isPlayerTurn = true;
            }
        }

        if (playerHealth <= 0)
        {
            Debug.Log("Player is defeated");
            battleLog.text = "Player is defeated";
        }
        else if (enemyHealth <= 0)
        {
            Debug.Log("Enemy is defeated");
            battleLog.text = "Enemy is defeated";
        }
    }

    void OnPlayerAttack()
    {
        if (isPlayerTurn && !playerActionCompleted)
        {
            int damage = Random.Range(10, 20);
            enemyHealth -= damage;
            Debug.Log($"Player attacked the enemy for {damage} damage. Enemy health: {enemyHealth}");
            battleLog.text = $"Player attacked the enemy for {damage} damage. Enemy health: {enemyHealth}";
            playerActionCompleted = true;
            StartCoroutine(UpdateUI());
        }
    }
    IEnumerator UpdateUI()
    {
        yield return null;
    }

    void EnemyAttack()
    {
        int damage = Random.Range(10, 20);
        playerHealth -= damage;
        Debug.Log($"Enemy attacked the player for {damage} damage. Player health: {playerHealth}");
        battleLog.text = $"Enemy attacked the player for {damage} damage. Player health: {playerHealth}";
    }
}
