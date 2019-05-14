using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

using System.Collections;
using System.Collections.Generic;

public class SessionManager : MonoBehaviour
{
    public LoginButton loginButton;
    public LabelManager textData;
    public ExperienceManager expManager;
    public GameObject taskInput;
    public GameObject up;
    public GameObject down;
    public GameObject left;
    public GameObject right;
    public GameObject taskCommands;

    [SerializeField]
    string BASE_URL;
    string LOGIN_URL;
    string ACTION_URL;
    string ATTACK_URL;
    string STATS_URL;

    public LoginData loginData = new LoginData();
    public PositionData positionData = new PositionData();
    public AttackResponse attackResponse = new AttackResponse();
    public StatsData statsData = new StatsData();
    public Text pointsLabel;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Text coordsLabel;

    public Hearbeat heartbeat;

    public int TIMEOUT = 5; //seconds

    void Start()
    {
        LOGIN_URL = BASE_URL + "/teams/login";
        ACTION_URL = BASE_URL + "/action/move";
        ATTACK_URL = BASE_URL + "/attack";
        STATS_URL = BASE_URL + "/action/stats";
    }

    public void Login(string username, string password)
    {
        loginButton.gameObject.SetActive(false);
        StartCoroutine(LoginCoroutine(username, password));
    }

    public IEnumerator LoginCoroutine(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        using (UnityWebRequest webRequest = UnityWebRequest.Post(LOGIN_URL, form))
        {
            webRequest.timeout = TIMEOUT;
            yield return webRequest.SendWebRequest();
            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.Log(": Error: " + webRequest.error);
                loginButton.gameObject.SetActive(true);
            }
            else
            {
                string json = webRequest.downloadHandler.text;
                JsonUtility.FromJsonOverwrite(json, loginData);
                if(loginData.token != "")
                {
                    loginButton.gameObject.SetActive(true);
                    loginButton.LoginSuccessful();
                    DoAction("");
                    heartbeat.token = loginData.token;
                    //heartbeat.StartHeartbeat();
                }
                else{
                    loginButton.gameObject.SetActive(true);
                }
            }
        }
    }

    public void DoAction(string action)
    {
        StartCoroutine(DoActionCoroutine(action));
    }

    public IEnumerator DoActionCoroutine(string action)
    {
        WWWForm form = new WWWForm();
        form.AddField("token", loginData.token);
        form.AddField("action", action);
        using (UnityWebRequest webRequest = UnityWebRequest.Post(ACTION_URL, form))
        {
            webRequest.timeout = TIMEOUT;
            webRequest.SetRequestHeader("x-team-token", loginData.token);
            yield return webRequest.SendWebRequest();
            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                textData.Type(": Error: " + webRequest.error);
            }
            else
            {
                string json = webRequest.downloadHandler.text;
                JsonUtility.FromJsonOverwrite(json, positionData);
                UpdatePositionData();
            }
        }
    }

    public void UpdatePositionData()
    {
        taskCommands.SetActive(false);
        taskInput.SetActive(false);

        textData.Type(positionData.display_metadata);
        up.SetActive(positionData.actions.up);
        down.SetActive(positionData.actions.down);
        left.SetActive(positionData.actions.left);
        right.SetActive(positionData.actions.right);
        expManager.UpdateExperience(positionData.points);
        coordsLabel.text = positionData.y.ToString() + ", " + positionData.x.ToString();
        if (!positionData.task.url.Equals(""))
        {
            taskCommands.SetActive(true);
            taskInput.SetActive(true);
        }

        if (positionData.task.healthbar.Length > 0)
        {
            SetUpHearts(positionData.task.healthbar);
        }
    }

    public void DoAttack(string caseID, string solution)
    {
        StartCoroutine(DoAttackCoroutine(caseID, solution));
    }

    public IEnumerator DoAttackCoroutine(string caseID, string solution)
    {
        WWWForm form = new WWWForm();
        form.AddField("token", loginData.token);
        form.AddField("problem_id", positionData.task.id);
        form.AddField("case_id", caseID);
        form.AddField("submission", solution);
        using (UnityWebRequest webRequest = UnityWebRequest.Post(ATTACK_URL, form))
        {
            webRequest.timeout = TIMEOUT;
            webRequest.SetRequestHeader("x-team-token", loginData.token);
            yield return webRequest.SendWebRequest();
            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                textData.Type(": Error: " + webRequest.error);
            }
            else
            {
                string json = webRequest.downloadHandler.text;
                try{
                    JsonUtility.FromJsonOverwrite(json, attackResponse);
                    HandleAttackResponse();
                }
                catch
                {
                    
                }
            }
        }
    }

    public void HandleAttackResponse()
    {
        bool taskSolved = true;
        foreach (bool caseStatus in attackResponse.healthbar)
        {
            if (caseStatus == false)
            {
                taskSolved = false;
                break;
            }
        }
        if (taskSolved)
        {
            string s = MessagePools.GetMsg(MessagePools.mobDead) + "TASK SOLVED COMPLETELY.";
            textData.Type(s);
            StartCoroutine(FirstTaskHandler());
        }
        else if (!attackResponse.solvedBefore && attackResponse.solved)
        {
            string s = MessagePools.GetMsg(MessagePools.mobHit) + "CASE SOLVED CORRECTLY.";
            textData.Type(s);
        }
        else if (attackResponse.solvedBefore)
        {
            textData.Type("You already solved this test case correctly.");
        }
        else
        {
            string s = MessagePools.GetMsg(MessagePools.mobMiss) + "WRONG ANSWER.";
            textData.Type(s);
        }
        SetUpHearts(attackResponse.healthbar);
        expManager.UpdateExperience(attackResponse.points);
    }

    public void SetUpHearts(bool[] status)
    {
        for (int i = 0; i < status.Length; i++)
        {
            if (status[i] == true)
            {
                hearts[i].sprite = emptyHeart;
            }
            else
            {
                hearts[i].sprite = fullHeart;
            }
        }
    }

    public void GetStats()
    {
        StartCoroutine(GetStatsCoroutine());
    }

    public IEnumerator GetStatsCoroutine()
    {
        WWWForm form = new WWWForm();
        form.AddField("token", loginData.token);
        using (UnityWebRequest webRequest = UnityWebRequest.Post(STATS_URL, form))
        {
            webRequest.timeout = TIMEOUT;
            webRequest.SetRequestHeader("x-team-token", loginData.token);
            yield return webRequest.SendWebRequest();
            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                textData.Type(": Error: " + webRequest.error);
            }
            else
            {
                string json = webRequest.downloadHandler.text;
                Debug.Log(json);
                JsonUtility.FromJsonOverwrite(json, statsData);
                PrintStats();
            }
        }
    }

    public void PrintStats()
    {
        string s = "Your stats:" + "\n";
        s += "Points earned: " + statsData.points + "\n";
        s += "Distance moved: " + statsData.moves + "m" + "\n";
        s += "Attacks used: " + statsData.attacks + "\n";
        s += "Tasks completely solved: " + statsData.tasksSolved + "\n";
        textData.Type(s);
    }

    IEnumerator FirstTaskHandler()
    {
        yield return new WaitForSecondsRealtime(5.0f);
        DoAction("");
    }
}