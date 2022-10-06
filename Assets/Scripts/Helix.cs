using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Helix:MonoBehaviour
{
    public Canvas GUI;
    private Dictionary<string,GameObject> _panels = new Dictionary<string,GameObject>();
    private bool isPlaying=false;
    private int score = 0;
    private int level = 1;
    GameObject gm;

    private void Awake()
    {
        gm = GameObject.Find("GameManager");
        //�������� ������� � ��������
        _panels.Add("Main",GameObject.Find("Main"));
        _panels.Add("Game",GameObject.Find("Game"));
        _panels.Add("Restart",GameObject.Find("Restart"));
        _panels.Add("Win",GameObject.Find("Win"));
        //������� ������
        foreach (KeyValuePair<string,GameObject> pair in _panels)
        {
            pair.Value.gameObject.SetActive(false);
        }
    }
    void Start()
    {
        //������� ������� ����
        GameObject mainGUI = _panels["Main"];
        mainGUI.gameObject.SetActive(true);
        //�������� ������� ��������
        //..�����
        Button startButton = mainGUI.gameObject.transform.Find("Btn_NewGame").GetComponent<Button>();
        startButton.onClick.RemoveAllListeners();
        startButton.onClick.AddListener(StartNewGame);
        //..�����
        Button button = mainGUI.gameObject.transform.Find("Btn_Quit").GetComponent<Button>();
        button.onClick.AddListener(Quit);

    }

    void StartNewGame()
    {
        gm.GetComponent<Controls>().iCanMove = true;
        gm.GetComponent<Jump>().StartGame();
        //�������� ������
        _panels["Main"].gameObject.SetActive(false);
        _panels["Restart"].gameObject.SetActive(false);
        _panels["Win"].gameObject.SetActive(false);
        //������� ������� gui
        GameObject gameGUI = _panels["Game"];
        gameGUI.gameObject.SetActive(true);
        gameGUI.gameObject.transform.Find("Status").gameObject.SetActive(true);
        //����������� ������� ������
        isPlaying = true;
        //��������
        SetText(gameGUI.gameObject.transform.Find("Score").GetComponent<TMPro.TextMeshProUGUI>(),"Score: " + score);
        SetText(gameGUI.gameObject.transform.Find("Level").GetComponent<TMPro.TextMeshProUGUI>(),"Level: " + level);
        SetText(gameGUI.gameObject.transform.Find("Status").GetComponent<TMPro.TextMeshProUGUI>(),"");
        gameObject.GetComponent<Game>().CurrentState = Game.State.Playing;
    }

    private void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    void SetText(TMPro.TextMeshProUGUI txt, string text)
    {
        txt.text = text;
    }

    public void AddScore(int value)
    {
        score = score + value;
        SetText(_panels["Game"].gameObject.transform.Find("Score").GetComponent<TMPro.TextMeshProUGUI>(), "Score: " + score);
    }

    public void BallDies()
    {
        Reload();
    }
    void Reload()
    {
        //�������� ������� ������
        _panels["Game"].gameObject.SetActive(false);
        //������� ������
        GameObject restartGUI = _panels["Restart"];
        restartGUI.gameObject.SetActive(true);
        restartGUI.gameObject.transform.Find("Status").gameObject.SetActive(true);
        //������ ���������
        SetText(restartGUI.gameObject.transform.Find("Status").GetComponent<TMPro.TextMeshProUGUI>(),"YOU LOSE! \n \n Score: "+score+ "\n Level:  "+ level);
        //������� ����������
        score = 0;
        level = 1;
        //�������� ������� gui 
        Button button = restartGUI.gameObject.transform.Find("Btn_Restart").GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(StartNewGame);
        //..�����
        Button buttonQ = restartGUI.gameObject.transform.Find("Btn_Quit").GetComponent<Button>();
        buttonQ.onClick.RemoveAllListeners();
        buttonQ.onClick.AddListener(Quit);
        
    }
    public void GoNext()
    {
        Win();
    }
    void Win()
    {
        //�������� ������� ������
        _panels["Game"].gameObject.SetActive(false);
        //������� ������
        GameObject winGUI = _panels["Win"];
        winGUI.gameObject.SetActive(true);
        winGUI.gameObject.transform.Find("Achivment").gameObject.SetActive(true);
        //������ ���������
        level++;
        SetText(winGUI.gameObject.transform.Find("Achivment").GetComponent<TMPro.TextMeshProUGUI>(),"YOU WIN! \n \n Score: " + score + "\n Next level: " + level);
        //�������� ��������� �������
        Button button = winGUI.gameObject.transform.Find("Btn_Next").GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(NextLevel);
        //..�����
        Button buttonQ = winGUI.gameObject.transform.Find("Btn_Quit").GetComponent<Button>();
        buttonQ.onClick.RemoveAllListeners();
        buttonQ.onClick.AddListener(Quit);
    }

    void NextLevel()
    {
        StartNewGame();
    }
}
