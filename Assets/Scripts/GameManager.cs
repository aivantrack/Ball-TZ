using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    [Header("Coin Controller")]
    public GameObject coinPref;
    public GameObject effectPref;
    public Transform[] positions;
    public Transform currentPosition;

    [Header("UI Coin")]
    public Text coinText;
    public int coin;

    [Header("UI Time")]
    public Text timeText;
    public int timer;
    public int maxTimer = 25;
    public int plusSeconds = 5;

    [Header("Булевые значения")]
    public bool isLost = false;
    public bool isWin = false;

    [Header("Панели")]
    public GameObject LostPanel;
    public GameObject GamePanel;
    public GameObject WinPanel;

    [Header("Панель проиграша")]
    public Text coinLostText;

    private void Start()
    {
        timer = maxTimer;
        coin = -1;

        SpanwCoin();
        ChangeCoinText();

        StartCoroutine(MinusOneSecond());
    }

    public void ChangeCoinText() 
    {
        if (coin >= 10)
        {
            isWin = true;

            WinPanel.SetActive(isWin);
            GamePanel.SetActive(!isWin);
        }

        coinText.text = coin.ToString();
    }

    public void ChangeTimeText()
    {
        timeText.text = timer.ToString();
    }

    public void PlusSeconds() 
    {
        timer += plusSeconds;
        if (timer >= maxTimer) timer = maxTimer;
        ChangeTimeText();
    }

    public void SpanwCoin() 
    {
        Transform newPos = positions[Random.Range(0, positions.Length)];

        if (newPos != currentPosition)
        {
            currentPosition = newPos;

            GameObject coinObj = 
                Instantiate(coinPref, newPos.position, Quaternion.identity);

            coin++;
            ChangeCoinText();
        }
        else 
        {
            SpanwCoin();
        }
    }

    public void CreateEffect(Vector3 position, float time) 
    {
        GameObject effectObj =
                    Instantiate(effectPref, position, Quaternion.identity);

        Destroy(effectObj, time);
    }
    public void TimeManager(int value) 
    {
        timer += value;
        ChangeTimeText();
    }
    IEnumerator MinusOneSecond() 
    {
        while (!isLost && !isWin)
        {
            yield return new WaitForSeconds(1);

            if (timer <= 0) 
            {
                YouLost();
            }
            
            TimeManager(-1);
        }
    }
    public void YouLost() 
    {
        isLost = true;
        Debug.Log("Ты продул");

        coinLostText.text = "Ты собрал: " + coin.ToString() + " монет";

        LostPanel.SetActive(true);
        GamePanel.SetActive(false);
    }

    public void RestsrtGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    public void NextScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    public void BackScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
    public void HomeScene() => SceneManager.LoadScene("Home");
    public void AnotherScene(int index) => SceneManager.LoadScene(index);
}
