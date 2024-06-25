using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleController : MonoBehaviour
{
    protected CharacterStatHandler characterStatHandler; // ĳ���� ������ �����ϴ� �ڵ鷯
    protected CharacterStatHandler enemyStatHandler;

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
    public int eliteEnemyReward;
    public int playerHasGold;
    public Text playerLevelText;
    public Text enemyLevelText;
    int enemyMaxHP;

    public GameObject loadScene;
    public Animator anim;
    public string animName;

    private bool isPlayerTurn = true; // ���� ���� �÷��̾��� ������ ���θ� ��Ÿ���� �÷���
    private bool playerActionCompleted = false; // �÷��̾��� �ൿ�� �Ϸ�Ǿ����� ���θ� ��Ÿ���� �÷���

    [SerializeField] private GameObject gameOverUI; // ���� ���� �� ǥ���� UI

    void Start()
    {
        loadScene.SetActive(true);
        anim.Play(animName);

        StartCoroutine(WaitForAnimation(anim, animName));
        
        player = FindObjectOfType<Player>().gameObject;
        enemy = FindObjectOfType<Enemy>().gameObject;
        characterStatHandler = player.GetComponent<CharacterStatHandler>();
        enemyStatHandler = enemy.GetComponent<CharacterStatHandler>();
        playerAnimator = player.GetComponentInChildren<Animator>();
        enemyAnimator = enemy.GetComponentInChildren<Animator>();
        attackButton.onClick.AddListener(OnPlayerAttack); // ���� ��ư�� Ŭ�� �̺�Ʈ�� �߰� -> ���ݴ��� ��Ʋ �ý��� �ȵ��ư�
        curHP = Player.instance.curHP;
        playerHPBar.maxValue = characterStatHandler.CurrentStat.statData.MaxHealth; // �÷��̾��� �ִ� ü���� �����̴��� �ִ� ������ ����
        playerHPBar.value = Player.instance.curHP; // �����̴��� �÷��̾��� ���� ü�� ������ �ʱ�ȭ
        enemyHPBar.maxValue = enemyStatHandler.CurrentStat.statData.MaxHealth; // ���� �ִ� ü���� �����̴��� �ִ� ������ ����
        enemyHPBar.value = enemyStatHandler.CurrentStat.statData.MaxHealth; // �����̴��� ���� ���� ü�� ������ �ʱ�ȭ
        playerHasGold = characterStatHandler.CurrentStat.statData.PlayerHaveGold;
        StartCoroutine(Battle()); // ��Ʋ �ڷ�ƾ ����
        enemyMaxHP=enemyStatHandler.CurrentStat.statData.MaxHealth;
        UpdataTxtUI();
    }

    public IEnumerator WaitForAnimation(Animator anim, string animName)
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        float animationDuration = stateInfo.length;

        yield return new WaitForSeconds(animationDuration);

        loadScene.SetActive(false);
    }

    IEnumerator Battle()
    {
        // �÷��̾�� ���� ü���� ��� 0���� Ŭ ������ �ݺ�
        while (Player.instance.curHP > 0 && enemyStatHandler.CurrentStat.statData.MaxHealth > 0)
        {
            if (isPlayerTurn) // �÷��̾��� ���̸�
            {
                Debug.Log("Player's Turn started");
                yield return StartCoroutine(TypeText("Player's Turn")); // ''Player's Turn' �ؽ�Ʈ ���
                playerActionCompleted = false; // �÷��̾��� �ൿ �Ϸ� �÷��׸� false�� ����
                yield return new WaitUntil(() => playerActionCompleted); // �÷��̾��� �ൿ�� �Ϸ�� ������ ���
                Debug.Log("Player's Turn completed"); 
                isPlayerTurn = false; // ���� ������ ��ȯ
                UpdataTxtUI();
            }
            else
            {
                Debug.Log("Enemy's Turn started");
                yield return StartCoroutine(TypeText("Enemy's Turn")); // "Enemy's Turn" �ؽ�Ʈ ���
                yield return new WaitForSeconds(1.0f); // 1�� ���
                EnemyAttack(); // ���� ���� �Լ� ȣ��
                Debug.Log("Enemy's Turn completed"); 
                isPlayerTurn = true; // �÷��̾��� ������ ��ȯ
                UpdataTxtUI();
            } 
            yield return new WaitForSeconds(2.0f); // �� ���̿� 4�� ��� -> �� ���ھ� Ÿ���� �ϴµ� �ð��� �ɷ� ���� ó�� �� ���ڰ� �� �����µ� 4������ �ɷ��� 4�ʴ�⼳��
        }

        if (Player.instance.curHP <= 0) // �÷��̾ �й��� ���
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
            yield return StartCoroutine(TypeText("Enemy is defeated")); // "Enemy is defeated" �ؽ�Ʈ ���
            if(DataManager.instance.styleIdx == 0)
            {
                yield return StartCoroutine(TypeText($"Player Get {normalEnemyReward} Gold"));
            }
            else if(DataManager.instance.styleIdx == 1)
            {
                yield return StartCoroutine(TypeText($"Player Get {eliteEnemyReward} Gold"));
            }
            Reward();
            yield return new WaitForSeconds(1f);

            loadScene.SetActive(true);
            Invoke("LoadScene", 1.5f);
            Debug.Log("Enemy is defeated");
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }
    void UpdataTxtUI()
    {
        playerLevelText.text=$"Player:  {Player.instance.curHP} / {characterStatHandler.CurrentStat.statData.MaxHealth}";
        enemyLevelText.text=$"Enemy:  {enemyStatHandler.CurrentStat.statData.MaxHealth} / {enemyMaxHP}";
    }

    void Reward()
    {
        if(DataManager.instance.styleIdx == 0)
        {
            Player.instance.curMoney += normalEnemyReward;
        }
        else if (DataManager.instance.styleIdx == 1)
        {
            Player.instance.curMoney += eliteEnemyReward;
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
        Player.instance.curHP -= (int)damage; // �÷��̾��� ü�¿��� ���� ���ݷ¸�ŭ ��
        Debug.Log($"Enemy attacked the player for {damage} damage. Player health: {characterStatHandler.CurrentStat.statData.MaxHealth}");
        string message = $"Enemy attacked the player for {damage} damage. Player health: {Player.instance.curHP}"; // ��Ʋ �α׿� ����� �޽��� ����
        StartCoroutine(TypeText(message)); // �޽����� Ÿ���� �ӵ��� ���
        enemyAnimator.SetTrigger("Attack");
        playerAnimator.SetTrigger("Hurt");

        playerHPBar.value = Player.instance.curHP; // �÷��̾��� ü�� �����̴��� ������Ʈ
    }

    IEnumerator TypeText(string message)
    {
        battleLog.text = ""; // ��Ʋ �α׸� �ʱ�ȭ
        foreach (char letter in message.ToCharArray()) // �޽����� �� ���ڸ� ����������
        {
            battleLog.text += letter; // ��Ʋ�α׿� �߰�
            yield return new WaitForSeconds(0.01f); //Ÿ���� �ӵ���ŭ ���
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
