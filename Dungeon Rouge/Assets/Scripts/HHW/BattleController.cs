using System.Collections;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleController : MonoBehaviour
{
    [SerializeField] private CharacterStatHandler characterStatHandler; // ĳ���� ������ �����ϴ� �ڵ鷯

    public Text battleLog; //  �÷��̾�� ���� �ϰ� ��Ʋ �α׸� ǥ���ϴ� �ؽ�Ʈ UI
    public Button attackButton; // �÷��̾ ������ �� ������ ��ư
    public Slider playerHPBar; // �÷��̾��� ü���� ǥ���ϴ� UI �����̴� 
    public Slider enemyHPBar;// ���� ü���� ǥ���ϴ� UI �����̴�
    public float typingSpeed = 0.05f;  // ��Ʋ �α� �ؽ�Ʈ�� ��µǴ� �ӵ�

    private bool isPlayerTurn = true; // ���� ���� �÷��̾��� ������ ���θ� ��Ÿ���� �÷���
    private bool playerActionCompleted = false; // �÷��̾��� �ൿ�� �Ϸ�Ǿ����� ���θ� ��Ÿ���� �÷���

    [SerializeField] private GameObject gameOverUI; // ���� ���� �� ǥ���� UI


    private float enemyHealth = 100f; // ���� ü��, ���Ƿ� 100���� ����

    void Start()
    {
        attackButton.onClick.AddListener(OnPlayerAttack); // ���� ��ư�� Ŭ�� �̺�Ʈ�� �߰� -> ���ݴ��� ��Ʋ �ý��� �ȵ��ư�
        playerHPBar.maxValue = characterStatHandler.CurrentStat.statData.MaxHealth; // �÷��̾��� �ִ� ü���� �����̴��� �ִ� ������ ����
        playerHPBar.value = characterStatHandler.CurrentStat.statData.MaxHealth; // �����̴��� �÷��̾��� ���� ü�� ������ �ʱ�ȭ
        enemyHPBar.maxValue = enemyHealth; // ���� �ִ� ü���� �����̴��� �ִ� ������ ����
        enemyHPBar.value = enemyHealth; // �����̴��� ���� ���� ü�� ������ �ʱ�ȭ
        StartCoroutine(Battle()); // ��Ʋ �ڷ�ƾ ����
    }

    IEnumerator Battle()
    {
        // �÷��̾�� ���� ü���� ��� 0���� Ŭ ������ �ݺ�
        while (characterStatHandler.CurrentStat.statData.MaxHealth > 0 && enemyHealth > 0)
        {
            if (isPlayerTurn) // �÷��̾��� ���̸�
            {
                Debug.Log("Player's Turn started");
                yield return StartCoroutine(TypeText("Player's Turn")); // ''Player's Turn' �ؽ�Ʈ ���
                playerActionCompleted = false; // �÷��̾��� �ൿ �Ϸ� �÷��׸� false�� ����
                yield return new WaitUntil(() => playerActionCompleted); // �÷��̾��� �ൿ�� �Ϸ�� ������ ���
                Debug.Log("Player's Turn completed"); 
                isPlayerTurn = false; // ���� ������ ��ȯ
            }
            else
            {
                Debug.Log("Enemy's Turn started");
                yield return StartCoroutine(TypeText("Enemy's Turn")); // "Enemy's Turn" �ؽ�Ʈ ���
                yield return new WaitForSeconds(1.0f); // 1�� ���
                EnemyAttack(); // ���� ���� �Լ� ȣ��
                Debug.Log("Enemy's Turn completed"); 
                isPlayerTurn = true; // �÷��̾��� ������ ��ȯ
            } 
            yield return new WaitForSeconds(4.0f); // �� ���̿� 4�� ��� -> �� ���ھ� Ÿ���� �ϴµ� �ð��� �ɷ� ���� ó�� �� ���ڰ� �� �����µ� 4������ �ɷ��� 4�ʴ�⼳��
        }

        if (characterStatHandler.CurrentStat.statData.MaxHealth <= 0) // �÷��̾ �й��� ���
        {
            Debug.Log("Player is defeated");
            yield return StartCoroutine(TypeText("Player is defeated")); // "Player is defeated" �ؽ�Ʈ ���
            GameOver(); // ���� ���� �Լ� ȣ��
        }
        else if (enemyHealth <= 0)
        {
            Debug.Log("Enemy is defeated");
            yield return StartCoroutine(TypeText("Enemy is defeated")); // "Enemy is defeated" �ؽ�Ʈ ���
        }
    }

    void OnPlayerAttack() 
    {
        if (isPlayerTurn && !playerActionCompleted) // �÷��̾� ���̰�, �÷��̾��� �ൿ�� �Ϸ���� �ʾҴٸ�
        {
            float damage = characterStatHandler.CurrentStat.statData.Atk; // �÷��̾��� ���ݷ��� ������
            enemyHealth -= damage; // ���� ü�¿��� �÷��̾��� ���ݷ¸�ŭ ��
            Debug.Log($"Player attacked the enemy for {damage} damage. Enemy health: {enemyHealth}"); 
            string message = $"Player attacked the enemy for {damage} damage. Enemy health: {enemyHealth}"; // ��Ʋ �α׿� ����� �޽��� ����
            StartCoroutine(TypeText(message)); // �޽����� Ÿ���� �ӵ��� ���
            playerActionCompleted = true; // �÷��̾��� �ൿ �Ϸ� �÷��׸� true�� ����

            enemyHPBar.value = enemyHealth; // ���� ü�� �����̴��� ������Ʈ
        }
    }

    void EnemyAttack()
    {
        int damage = Random.Range(100, 200); // ���� ���ݷ��� �������� ���� (100~200 ���� : ���� ������ �׽�ƮȮ���� ���� �÷��̾� ü�º��� ���� ������)
        characterStatHandler.CurrentStat.statData.MaxHealth -= damage; // �÷��̾��� ü�¿��� ���� ���ݷ¸�ŭ ��
        Debug.Log($"Enemy attacked the player for {damage} damage. Player health: {characterStatHandler.CurrentStat.statData.MaxHealth}");
        string message = $"Enemy attacked the player for {damage} damage. Player health: {characterStatHandler.CurrentStat.statData.MaxHealth}"; // ��Ʋ �α׿� ����� �޽��� ����
        StartCoroutine(TypeText(message)); // �޽����� Ÿ���� �ӵ��� ���

        playerHPBar.value = characterStatHandler.CurrentStat.statData.MaxHealth; // �÷��̾��� ü�� �����̴��� ������Ʈ
    }

    IEnumerator TypeText(string message)
    {
        battleLog.text = ""; // ��Ʋ �α׸� �ʱ�ȭ
        foreach (char letter in message.ToCharArray()) // �޽����� �� ���ڸ� ����������
        {
            battleLog.text += letter; // ��Ʋ�α׿� �߰�
            yield return new WaitForSeconds(typingSpeed); //Ÿ���� �ӵ���ŭ ���
        }
    }

    public void OnBattleExit()
    {
        SceneManager.LoadScene("KKEScene"); // "KKEScene"������ ��ȯ
    }

    private void GameOver()
    {
        gameOverUI.SetActive(true); // ���� ���� UI�� Ȱ��ȭ
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("StartScene"); //"StartScene" ������ ��ȯ�Ͽ� ������ �����
    }
}
