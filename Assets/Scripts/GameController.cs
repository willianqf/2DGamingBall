using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    GameObject ControlL;
    [SerializeField]
    GameObject ControlR;
    [SerializeField]
    GameObject gameOver;
    public static GameController instance;
    [SerializeField]
    public int pontuacao;
    [SerializeField]
    public Text Score;
    [SerializeField]
    public Text ScoreCanvas;
    public Text RecordScore;
    public int RecordScoreValor = 0;

    public Text InfoPause;

    public bool gameover = false;
    Scene scene;

    private void Awake() {
        Time.timeScale = 1;
        instance = this;
        scene = SceneManager.GetActiveScene();
        int RecordScor = PlayerPrefs.GetInt("RecordPontuacao");
        if (scene.name != "Inicio")
        {
            RecordScoreValor = RecordScor;
            RecordScore.text = "Recorde: " + RecordScor.ToString();
            pontuacao = 0;
        }
    }

    void Update()
    {
        RecordValor(RecordScoreValor);
    }

    void RecordValor(int valor)
    {
        if(valor > RecordScoreValor)
        {
            RecordScoreValor = valor;
            RecordScore.text = "Recorde: " + valor.ToString();
        }
    }
    private void Start() {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "GamePlay"){
            //PlayerPrefs.SetInt("RecordPontuacao", 0);
            ScoreCanvas.text = "0";
        }
    }

    public void ShowGameOver()
    {
        Time.timeScale = 0;
        DesableButton(false);
        gameOver.SetActive(true);
        ScoreCanvas.text = pontuacao.ToString();
        RecordValor(pontuacao);
    }

    public void RestartGame()
    {
        DesableButton(true);
        PlayerPrefs.SetInt("RecordPontuacao", RecordScoreValor);
        SceneManager.LoadScene(scene.name);
    }
    public void AddPontuacao(int p)
    {
        //pontuacao++;
        pontuacao += p;
        Score.text = pontuacao.ToString();
    }
    public void DesableButton(bool IsDesable)
    {
        ControlL.SetActive(IsDesable);
        ControlR.SetActive(IsDesable);
    }

    public void IniciarJogo()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void PauseGame()
    {
        if(!gameover){
            if(Time.timeScale == 0)
            {
                Time.timeScale  = 1;
                InfoPause.gameObject.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                InfoPause.gameObject.SetActive(true);
            }
        }
    }

}
