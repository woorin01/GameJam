using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//빨간색 : 1
//파란색 : 2
//노란색 : 3
//보라색 : 4
public class PlayerNotePad : MonoBehaviour
{
    public static PlayerNotePad instance;

    public Text comboTimeText;
    public Text scoreText;

    public int touchNum;
    public int nowCombo;
    public int score;

    public float comboTime;
    public float maxComboTime;

    public bool canTouch;
    public bool isChkComTim;

    private CharacterAction m_CharacterAction;
    public GameObject gameOver;
    public GameObject retryButton;
    public GameObject titleButton;
    private bool isGameOver;

    void Awake()
    {
        instance = this;

        canTouch = false;
        isChkComTim = false;

        nowCombo = 0;
        score = 0;

        comboTime = maxComboTime = 5f;
        m_CharacterAction = GetComponent<CharacterAction>();
        isGameOver = false;
    }

    void Update()
    {
        comboTimeText.text = string.Format("{0:N1}", comboTime);
        scoreText.text = score.ToString();

        if (Input.GetKeyDown(KeyCode.D)) TouchRedPad();
        else if (Input.GetKeyDown(KeyCode.F)) TouchYellowPad();
        else if (Input.GetKeyDown(KeyCode.J)) TouchBluePad();
        else if (Input.GetKeyDown(KeyCode.K)) TouchPurplePad();
    }

    public void TouchRedPad()//빨간색 노트를 눌렀을 때
    {
        if (!canTouch || isGameOver)//터치가 불가능한 상황이면 리턴
            return;

        touchNum = 1;

        CheckCombo();
    }
    public void TouchBluePad()//파란색 노트를 눌렀을 때
    {
        if (!canTouch || isGameOver)//터치가 불가능한 상황이면 리턴
            return;

        touchNum = 2;

        CheckCombo();
    }
    public void TouchYellowPad()//노란색 노트를 눌렀을 때
    {
        if (!canTouch || isGameOver)//터치가 불가능한 상황이면 리턴
            return;

        touchNum = 3;

        CheckCombo();
    }
    public void TouchPurplePad()//보라색 노트를 눌렀을 때
    {
        if (!canTouch || isGameOver)//터치가 불가능한 상황이면 리턴
            return;

        touchNum = 4;

        CheckCombo();
    }
    public void RetryButton()
    {
        SceneManager.LoadScene(1);
    }
    public void TitleButton()
    {
        SceneManager.LoadScene(0);
    }
    //스킬키 누르면 canTouch = true; nowCombo = 0; comboTime = 1.0f; StartCoroutine("CheckComboTime");
    public void CheckCombo()
    {
        if (GameManager.instance.buildingList[0] == null)//만약 건물이 생성 되어 있지 않다면
        {
            Debug.Log("sorry buildinginfo is null");
            return;
        }
        if (touchNum != GameManager.instance.buildingList[0].notePattern[nowCombo])//만약 콤보를 실패시켰다면
        {
            StartCoroutine("TouchFails");
            StopCoroutine("CheckComboTime");
            
            return;
        }
        else//만약 콤보를 성공시켰다면
        {
            if (touchNum == GameManager.instance.buildingList[0].notePattern[nowCombo])//만약 콤보를 시켰다면
            {
                nowCombo++;
            }
            comboTime = maxComboTime;
            if ((nowCombo).Equals(GameManager.instance.buildingList[0].maxPatternNum))//만약 콤보를 완성시켰다면
            {
                StartCoroutine("db");
                return;
            }
            
            GameManager.instance.buildingList[0].PatternToColor(GameManager.instance.buildingList[0].notePattern[nowCombo]);

            if (!isChkComTim)//코루틴 중복 실행 방지
                StartCoroutine("CheckComboTime");
        }
    }

    IEnumerator db()
    {
        canTouch = false;
        m_CharacterAction.Attack_All();
        
        yield return new WaitForSeconds(0.3f);

        Destroy(GameManager.instance.buildingList[0].gameObject);//건물을 삭제 시킨다.
        nowCombo = 0;
        score += 1;

        if (maxComboTime > 1.0f)
            maxComboTime -= 0.05f;

        if (!isChkComTim)//코루틴 중복 실행 방지
            StartCoroutine("CheckComboTime");

        GameManager.instance.AddBuilding(5);
        canTouch = true;

    }

    IEnumerator CheckComboTime()//x초 내에 콤보를 실행 하는 지 여부를 체크하는 함수
    {
        isChkComTim = true;

        for (; comboTime >= 0.0f; comboTime -= 0.1f)
        {
            yield return new WaitForSeconds(0.1f);
        }
        //만약 x초 내에 콤보를 실행 하지 못했다면?
        isChkComTim = false;//다시 이 코루틴을 실행 시킬 수 있게 false로 바꿔줌
                            //바로 뒤짐
        isGameOver = true;
    }

    IEnumerator TouchFails()//콤보가 틀렸을때 실행되는 함수
    {
        //바로 죽음
        gameOver.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
        titleButton.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        isGameOver = true;
    }
}
