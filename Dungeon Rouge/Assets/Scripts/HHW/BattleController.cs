using System.Collections;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleController : MonoBehaviour
{
    [SerializeField] private CharacterStatHandler characterStatHandler;

    public Text battleLog;
    public Button attackButton;
    public Slider playerHPBar;
    public Slider enemyHPBar;
    public float typingSpeed = 0.05f;

    private bool isPlayerTurn = true;
    private bool playerActionCompleted = false;


    private float enemyHealth = 100f;

    void Start()
    {
        attackButton.onClick.AddListener(OnPlayerAttack);
        playerHPBar.maxValue = characterStatHandler.CurrentStat.statData.MaxHealth;
        playerHPBar.value = characterStatHandler.CurrentStat.statData.MaxHealth;
        enemyHPBar.maxValue = enemyHealth;
        enemyHPBar.value = enemyHealth;
        StartCoroutine(Battle());
    }

    IEnumerator Battle()
    {
        while (characterStatHandler.CurrentStat.statData.MaxHealth > 0 && enemyHealth > 0)
        {
            if (isPlayerTurn)
            {
                Debug.Log("Player's Turn started");
                yield return StartCoroutine(TypeText("Player's Turn"));
                playerActionCompleted = false;
                yield return new WaitUntil(() => playerActionCompleted);
                Debug.Log("Player's Turn completed");
                isPlayerTurn = false;
            }
            else
            {
                Debug.Log("Enemy's Turn started");
                yield return StartCoroutine(TypeText("Enemy's Turn"));
                yield return new WaitForSeconds(1.0f);
                EnemyAttack();
                Debug.Log("Enemy's Turn completed");
                isPlayerTurn = true;
            }
            yield return new WaitForSeconds(4.0f);
        }

        if (characterStatHandler.CurrentStat.statData.MaxHealth <= 0)
        {
            Debug.Log("Player is defeated");
            yield return StartCoroutine(TypeText("Player is defeated"));
        }
        else if (enemyHealth <= 0)
        {
            Debug.Log("Enemy is defeated");
            yield return StartCoroutine(TypeText("Enemy is defeated"));
        }
    }

    void OnPlayerAttack()
    {
        if (isPlayerTurn && !playerActionCompleted)
        {
            float damage = characterStatHandler.CurrentStat.statData.Atk;
            enemyHealth -= damage;
            Debug.Log($"Player attacked the enemy for {damage} damage. Enemy health: {enemyHealth}");
            string message = $"Player attacked the enemy for {damage} damage. Enemy health: {enemyHealth}";
            StartCoroutine(TypeText(message));
            playerActionCompleted = true;

            enemyHPBar.value = enemyHealth;
        }
    }

    void EnemyAttack()
    {
        int damage = Random.Range(10, 20);
        characterStatHandler.CurrentStat.statData.MaxHealth -= damage;
        Debug.Log($"Enemy attacked the player for {damage} damage. Player health: {characterStatHandler.CurrentStat.statData.MaxHealth}");
        string message = $"Enemy attacked the player for {damage} damage. Player health: {characterStatHandler.CurrentStat.statData.MaxHealth}";
        StartCoroutine(TypeText(message));

        playerHPBar.value = characterStatHandler.CurrentStat.statData.MaxHealth;
    }

    IEnumerator TypeText(string message)
    {
        battleLog.text = "";
        foreach (char letter in message.ToCharArray())
        {
            battleLog.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void OnBattleExit()
    {
        SceneManager.LoadScene("KKEScene");
    }
}
