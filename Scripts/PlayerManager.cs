using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{public static bool gameover;
    public GameObject GameOverPanel;
    public static bool isGameStarted;
    public GameObject StartingText;
    public static int numberOfCoins;
    public Text coinsText;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        gameover = false;
        isGameStarted = false;
        numberOfCoins = 0;
    }

    // Update is called once per frame
    void Update()
    {
        coinsText.text = "Coins:" + numberOfCoins;

        if(gameover)
        {
            GameOverPanel.SetActive(true);
            Time.timeScale = 0;
            
        }
       if(SwipeManager.tap)
        {
            isGameStarted = true;
            Destroy(StartingText);


        }
    }
}
