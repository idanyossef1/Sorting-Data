using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainGame : MonoBehaviour
{
    private enum Screen
    {
        Intro, Game, Score
    }
    float startTime = 0;
    float seconds = 5;
    int pressed = 0;
    bool running = false;
    GameObject intro;
    GameObject game;
    GameObject finalScore;

    
    private Image filledImage;
    TMP_Text pressedText;
    TMP_Text finalScoreText;
    Button playButton;
    Button replayButton;
    private HighScore highScoreController;
    
    
    void Start()
    {   intro = transform.Find("Intro").gameObject;
        game = transform.Find("Game").gameObject;
        finalScore = transform.Find("Final Score").gameObject;
        playButton = intro.GetComponentInChildren<Button>(true);
        replayButton = finalScore.GetComponentInChildren<Button>(true);
        highScoreController = finalScore.transform.Find("highScoreController").GetComponent<HighScore>();
        
        filledImage = game.transform.Find("filled").GetComponent<Image>();
        pressedText = game.transform.Find("presses").GetComponent<TMP_Text>();
        finalScoreText = finalScore.transform.Find("FinalScore").GetComponent<TMP_Text>();
        
        
        filledImage.fillAmount = 0;
        playButton.onClick.AddListener(StartGame);
        replayButton.onClick.AddListener(StartGame);
        GoToScreen(Screen.Intro);
    }

    void GoToScreen(Screen screen)
    {
        intro.SetActive(screen == Screen.Intro);
        game.SetActive(screen == Screen.Game);
        finalScore.SetActive(screen == Screen.Score);
    }

    void StartGame()
    {
        startTime = Time.time;
        pressed = 0;
        running = true;
        filledImage.fillAmount = 0;
        pressedText.text = "0";
        GoToScreen(Screen.Game);

    }

    void Update()
    {
        if (running)
        {
            float passedTime = Time.time - startTime;
            if (passedTime >= seconds)
            {
                this.FinishGame();
                return;
            }
            
            filledImage.fillAmount = passedTime/seconds ;
            if (Input.GetKeyUp(KeyCode.Space))
            {
                pressed++;
                pressedText.text = pressed.ToString();
            }
            
        }
        
    }

    private void FinishGame()
    {
        running = false;
        finalScoreText.text = pressed.ToString();

        GoToScreen(Screen.Score);

        if (ScoreManager.IsHighScore(pressed))
        {
            finalScoreText.text += "(HIGH SCORE)";
            ScoreManager.SetHighScore(pressed);
        }
        
        // show high scores list here
        // List<int> highScore = new List<int>() { 1, 2, 3 };
        // highScoreController.showHighScore(highScore);
    }
}
