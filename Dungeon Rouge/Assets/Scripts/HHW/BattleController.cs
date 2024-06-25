using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleController : MonoBehaviour
{
    [SerializeField] private CharacterStatHandler characterStatHandler; // 캐릭터 스탯을 관리하는 핸들러
    [SerializeField] private CharacterStatHandler enemyStatHandler;

    public List<CharacterStat> statsModifiers = new List<CharacterStat>();

    public GameObject player;
    public GameObject enemy;
    public Text battleLog; //  플레이어와 적의 턴과 배틀 로그를 표시하는 텍스트 UI
    public Button attackButton; // 플레이어가 공격할 때 누르는 버튼
    public Slider playerHPBar; // 플레이어의 체력을 표시하는 UI 슬라이더 
    public Slider enemyHPBar;// 적의 체력을 표시하는 UI 슬라이더
    public Animator playerAnimator;
    public Animator enemyAnimator;
    public float typingSpeed = 0.1f;  // 배틀 로그 텍스트가 출력되는 속도
    private int curHP;
    public int normalEnemyReward;
    public int EliteEnemyReward;
    public int playerHasGold;

    private bool isPlayerTurn = true; // 현재 턴이 플레이어의 턴인지 여부를 나타내는 플래그
    private bool playerActionCompleted = false; // 플레이어의 행동이 완료되었는지 여부를 나타내는 플래그

    [SerializeField] private GameObject gameOverUI; // 게임 오버 시 표시할 UI

    void Start()
    {
        player = FindObjectOfType<Player>().gameObject;
        enemy = FindObjectOfType<Enemy>().gameObject;
        characterStatHandler = player.GetComponent<CharacterStatHandler>();
        enemyStatHandler = enemy.GetComponent<CharacterStatHandler>();
        playerAnimator = player.GetComponentInChildren<Animator>();
        enemyAnimator = enemy.GetComponentInChildren<Animator>();
        attackButton.onClick.AddListener(OnPlayerAttack); // 공격 버튼에 클릭 이벤트를 추가 -> 없앴더니 배틀 시스템 안돌아감
        curHP = characterStatHandler.CurrentStat.statData.CurHealth;
        playerHPBar.maxValue = characterStatHandler.CurrentStat.statData.MaxHealth; // 플레이어의 최대 체력을 슬라이더의 최대 값으로 설정
        playerHPBar.value = characterStatHandler.CurrentStat.statData.CurHealth; // 슬라이더를 플레이어의 현재 체력 값으로 초기화
        enemyHPBar.maxValue = enemyStatHandler.CurrentStat.statData.MaxHealth; // 적의 최대 체력을 슬라이더의 최대 값으로 설정
        enemyHPBar.value = enemyStatHandler.CurrentStat.statData.MaxHealth; // 슬라이더를 적의 현재 체력 값으로 초기화
        playerHasGold = characterStatHandler.CurrentStat.statData.PlayerHaveGold;
        StartCoroutine(Battle()); // 배틀 코루틴 시작
    }

    IEnumerator Battle()
    {
        // 플레이어와 적의 체력이 모두 0보다 클 때까지 반복
        while (curHP > 0 && enemyStatHandler.CurrentStat.statData.MaxHealth > 0)
        {
            if (isPlayerTurn) // 플레이어의 턴이면
            {
                Debug.Log("Player's Turn started");
                yield return StartCoroutine(TypeText("Player's Turn")); // ''Player's Turn' 텍스트 출력
                playerActionCompleted = false; // 플레이어의 행동 완료 플래그를 false로 설정
                yield return new WaitUntil(() => playerActionCompleted); // 플레이어의 행동이 완료될 때까지 대기
                Debug.Log("Player's Turn completed"); 
                isPlayerTurn = false; // 적의 턴으로 전환
            }
            else
            {
                Debug.Log("Enemy's Turn started");
                yield return StartCoroutine(TypeText("Enemy's Turn")); // "Enemy's Turn" 텍스트 출력
                yield return new WaitForSeconds(1.0f); // 1초 대기
                EnemyAttack(); // 적의 공격 함수 호출
                Debug.Log("Enemy's Turn completed"); 
                isPlayerTurn = true; // 플레이어의 턴으로 전환
            } 
            yield return new WaitForSeconds(4.0f); // 턴 사이에 4초 대기 -> 한 글자씩 타이핑 하는데 시간이 걸려 공격 처리 때 글자가 다 나오는데 4초정도 걸려서 4초대기설정
        }

        if (curHP <= 0) // 플레이어가 패배한 경우
        {
            Debug.Log("Player is defeated");
            yield return StartCoroutine(TypeText("Player is defeated")); // "Player is defeated" 텍스트 출력
            playerAnimator.SetTrigger("Death");
            yield return new WaitForSeconds(1f);
            GameOver(); // 게임 오버 함수 호출
        }
        else if (enemyStatHandler.CurrentStat.statData.MaxHealth <= 0)
        {
            enemyAnimator.SetTrigger("Death");
            Reward();
            Debug.Log("Enemy is defeated");
            yield return StartCoroutine(TypeText("Enemy is defeated")); // "Enemy is defeated" 텍스트 출력
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
        if (isPlayerTurn && !playerActionCompleted) // 플레이어 턴이고, 플레이어의 행동이 완료되지 않았다면
        {
            float damage = characterStatHandler.CurrentStat.statData.Atk; // 플레이어의 공격력을 가져옴
            enemyStatHandler.CurrentStat.statData.MaxHealth -= (int)damage; // 적의 체력에서 플레이어의 공격력만큼 뺌
            Debug.Log($"Player attacked the enemy for {damage} damage. Enemy health: {enemyStatHandler.CurrentStat.statData.MaxHealth}"); 
            string message = $"Player attacked the enemy for {damage} damage. Enemy health: {enemyStatHandler.CurrentStat.statData.MaxHealth}"; // 배틀 로그에 출력할 메시지 생성
            StartCoroutine(TypeText(message)); // 메시지를 타이핑 속도로 출력
            playerActionCompleted = true; // 플레이어의 행동 완료 플래그를 true로 설정
            playerAnimator.SetTrigger("Attack1");
            enemyAnimator.SetTrigger("Hurt");

            enemyHPBar.value = enemyStatHandler.CurrentStat.statData.MaxHealth; // 적의 체력 슬라이더를 업데이트
        }
    }

    void EnemyAttack()
    {
        float damage = enemyStatHandler.CurrentStat.statData.Atk; // 적의 공격력을 랜덤으로 설정 (100~200 사이 : 게임 오버씬 테스트확인을 위해 플레이어 체력보다 높게 설정함)
        curHP -= (int)damage; // 플레이어의 체력에서 적의 공격력만큼 뺌
        Debug.Log($"Enemy attacked the player for {damage} damage. Player health: {characterStatHandler.CurrentStat.statData.MaxHealth}");
        string message = $"Enemy attacked the player for {damage} damage. Player health: {curHP}"; // 배틀 로그에 출력할 메시지 생성
        StartCoroutine(TypeText(message)); // 메시지를 타이핑 속도로 출력
        enemyAnimator.SetTrigger("Attack");
        playerAnimator.SetTrigger("Hurt");

        playerHPBar.value = curHP; // 플레이어의 체력 슬라이더를 업데이트
    }

    IEnumerator TypeText(string message)
    {
        battleLog.text = ""; // 베틀 로그를 초기화
        foreach (char letter in message.ToCharArray()) // 메시지의 각 문자를 순차적으로
        {
            battleLog.text += letter; // 배틀로그에 추가
            yield return new WaitForSeconds(typingSpeed); //타이핑 속도만큼 대기
        }
    }

    public void OnBattleExit()
    {
        SceneManager.LoadScene("KKEScene"); // "KKEScene"씬으로 전환
    }

    private void GameOver()
    {
        
        gameOverUI.SetActive(true); // 게임 오버 UI를 활성화
    }

    public void RestartGame()
    {
        playerAnimator.SetTrigger("Retry");
        SceneManager.LoadScene("StartScene"); //"StartScene" 씬으로 전환하여 게임을 재시작

        foreach (CharacterStat modifier in statsModifiers)
        {
            characterStatHandler.RemoveStatModifier(modifier);
        }

        characterStatHandler.ApplyStatModifier(characterStatHandler.baseStats);
    }
}
