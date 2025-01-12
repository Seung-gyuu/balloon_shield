using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public GameObject square;
    public Text timeTxt;
    float alive  = 0f;
    //여러군데에서 불릴 수있는 게임매니저 I
    public static gameManager I;
    public GameObject endPanel;
    public Animator anim;
    public Text thisScoreTxt;
    public Text maxScoreTxt;

    bool isRunning = true;


    //불리면 이 게임매니저만 넣기(싱글톤)
    void Awake()
    {
        I = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //게임이 다시 실행될때 원래대로 돌아가게 만들기
        Time.timeScale = 1.0f;
        InvokeRepeating("makeSquare", 0.0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        //버티는 시간만큼 시간 늘게하기
        if (isRunning)
        {
        alive += Time.deltaTime;
        timeTxt.text = alive.ToString("N2");
        }
      
    }

    void makeSquare()
    {
        Instantiate(square);
    }
    public void gameOver()
    {
        anim.SetBool("isDie", true);
        isRunning = false;

        Invoke("timeStop", 0.5f);
        thisScoreTxt.text = alive.ToString("N2");
        endPanel.SetActive(true);

        if (PlayerPrefs.HasKey("bestscore") == false)
        {
            PlayerPrefs.SetFloat("bestscore", alive);
        }
        else { 
            float maxS = PlayerPrefs.GetFloat("bestscore");
   
            if (maxS < alive)
            {
                PlayerPrefs.SetFloat("bestscore", alive);
            }
        }
        float maxScore = PlayerPrefs.GetFloat("bestscore");
        maxScoreTxt.text = maxScore.ToString("N2");
    }
    

    public void retry()
    {
        SceneManager.LoadScene("MainScene");

    }
    void timeStop()
    {
        Time.timeScale = 0.0f;
    }

  
}

