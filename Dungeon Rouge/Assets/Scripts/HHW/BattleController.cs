using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleController : MonoBehaviour
{
    [SerializeField] private CharacterStatHandler characterStatHandler; // ĳ���� ������ �����ϴ� �ڵ鷯
    [SerializeField] private CharacterStatHandler enemyStatHandler;

    public List<CharacterStat> statsModifiers = new List<CharacterStat>();

    public GameObject player;
    public GameObject enemy;
    public Text battleLog; //  �÷��̾�� ���� �ϰ� ��Ʋ �α׸� ǥ���ϴ� �ؽ�Ʈ UI
    public Button attackButton; // �÷��̾ ������ �� ������ ��ư
    public Slider playerHPBar; // �÷��̾��� ü���� ǥ���ϴ� UI �����̴� 
    public Slider enemyHPBar;// ���� ü���� ǥ���ϴ� UI �����̴�
    public Animator playerAnimator;
    public Animator enemyAnimator;
    public float typingSpeed = 0.1f;  // ��Ʋ �α� �ؽ�Ʈ�� ��µǴ� �ӵ�
    private int curHP;
    public int normalEnemyReward;
    public int EliteEnemyReward;
    public int playerHasGold;

    private bool isPlayerTurn = true; // ���� ���� �÷��̾��� ������ ���θ� ��Ÿ���� �÷���
    private bool playerActionCompleted = false; // �÷��̾��� �ൿ�� �Ϸ�Ǿ����� ���θ� ��Ÿ���� �÷���

    [SerializeField] private GameObject gameOverUI; // ���� ���� �� ǥ���� UI

    void Start()
    {
        player = FindObjectOfType<Player>().gameObject;
        enemy = FindObjectOfType<Enemy>().gameObject;
        characterStatHandler = player.GetComponent<CharacterStatHandler>();
        enemyStatHandler = enemy.GetComponent<CharacterStatHandler>();
        playerAnimator = player.GetComponentInChildren<Animator>();
        enemyAnimator = enemy.GetComponentInChildren<Animator>();
        attackButton.onClick.AddListener(OnPlayerAttack); // ���� ��ư�� Ŭ�� �̺�Ʈ�� �߰� -> ���ݴ��� ��Ʋ �ý��� �ȵ��ư�
        curHP = characterStatHandler.CurrentStat.statData.CurHealth;
        playerHPBar.maxValue = characterStatHandler.CurrentStat.statData.MaxHealth; // �÷��̾��� �ִ� ü���� �����̴��� �ִ� ������ ����
        playerHPBar.value = characterStatHandler.CurrentStat.statData.CurHealth; // �����̴��� �÷��̾��� ���� ü�� ������ �ʱ�ȭ
        enemyHPBar.maxValue = enemyStatHandler.CurrentStat.statData.MaxHealth; // ���� �ִ� ü���� �����̴��� �ִ� ������ ����
        enemyHPBar.value = enemyStatHandler.CurrentStat.statData.MaxHealth; // �����̴��� ���� ���� ü�� ������ �ʱ�ȭ
        playerHasGold = characterStatHandler.CurrentStat.statData.PlayerHaveGold;
        StartCoroutine(Battle()); // ��Ʋ �ڷ�ƾ ����
    }

    IEnumerator Battle()
    {
        // �÷��̾�� ���� ü���� ��� 0���� Ŭ ������ �ݺ�
        while (curHP > 0 && enemyStatHandler.CurrentStat.statData.MaxHealth > 0)
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

        if (curHP <= 0) // �÷��̾ �й��� ���
        {
            Debug.Log("Player is defeated");
            yield return StartCoroutine(TypeText("Player is defeated")); // "Player is defeated" �ؽ�Ʈ ���
            playerAnimator.SetTrigger("Death");
            yield return new WaitForSeconds(1f);
            GameOver(); // ���� ���� �Լ� ȣ��
        }
        else if (enemyStatHandler.CurrentStat.statData.MaxHealth <= 0)
        {
            enemyAnimator.SetTrigger("Death");
            Reward();
            Debug.Log("Enemy is defeated");
            yield return StartCoroutine(TypeText("Enemy is defeated")); // "Enemy is defeated" �ؽ�Ʈ ���
        }
    }

    void Reward()
    {
        if(DataManager.instance.styleIdx == 0)
        {
            playerHasGold += normalEnemyReward;
        }
        else if (DataManager.instance.styleIdx == 1)
        {
            playerHasGold += EliteEnemyReward;
        }
    }

    void OnPlayerAttack() 
    {
        if (isPlayerTurn && !playerActionCompleted) // �÷��̾� ���̰�, �÷��̾��� �ൿ�� �Ϸ���� �ʾҴٸ�
        {
            float damage = characterStatHandler.CurrentStat.statData.Atk; // �÷��̾��� ���ݷ��� ������
            enemyStatHandler.CurrentStat.statData.MaxHealth -= (int)damage; // ���� ü�¿��� �÷��̾��� ���ݷ¸�ŭ ��
            Debug.Log($"Player attacked the enemy for {damage} damage. Enemy health: {enemyStatHandler.CurrentStat.statData.MaxHealth}"); 
            string message = $"Player attacked the enemy for {damage} damage. Enemy health: {enemyStatHandler.CurrentStat.statData.MaxHealth}"; // ��Ʋ �α׿� ����� �޽��� ����
            StartCoroutine(TypeText(message)); // �޽����� Ÿ���� �ӵ��� ���
            playerActionCompleted = true; // �÷��̾��� �ൿ �Ϸ� �÷��׸� true�� ����
            playerAnimator.SetTrigger("Attack1");
            enemyAnimator.SetTrigger("Hurt");

            enemyHPBar.value = enemyStatHandler.CurrentStat.statData.MaxHealth; // ���� ü�� �����̴��� ������Ʈ
        }
    }

    void EnemyAttack()
    {
        float damage = enemyStatHandler.CurrentStat.statData.Atk; // ���� ���ݷ��� �������� ���� (100~200 ���� : ���� ������ �׽�ƮȮ���� ���� �÷��̾� ü�º��� ���� ������)
        curHP -= (int)damage; // �÷��̾��� ü�¿��� ���� ���ݷ¸�ŭ ��
        Debug.Log($"Enemy attacked the player for {damage} damage. Player health: {characterStatHandler.CurrentStat.statData.MaxHealth}");
        string message = $"Enemy attacked the player for {damage} damage. Player health: {curHP}"; // ��Ʋ �α׿� ����� �޽��� ����
        StartCoroutine(TypeText(message)); // �޽����� Ÿ���� �ӵ��� ���
        enemyAnimator.SetTrigger("Attack");
        playerAnimator.SetTrigger("Hurt");

        playerHPBar.value = curHP; // �÷��̾��� ü�� �����̴��� ������Ʈ
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
        playerAnimator.SetTrigger("Retry");
        SceneManager.LoadScene("StartScene"); //"StartScene" ������ ��ȯ�Ͽ� ������ �����

        foreach (CharacterStat modifier in statsModifiers)
        {
            characterStatHandler.RemoveStatModifier(modifier);
        }

        characterStatHandler.ApplyStatModifier(characterStatHandler.baseStats);
    }
}
