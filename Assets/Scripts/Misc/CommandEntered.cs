using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandEntered : MonoBehaviour
{

    public SessionManager sessionManager;
    public InputField commandInput;
    public AttackEntered attackManager;
    public AudioSource audioManager;
    public CommandHistory cmdHistory;

    public GameObject egg;

    string goUp = "GO UP";
    string goDown = "GO DOWN";
    string goLeft = "GO LEFT";
    string goRight = "GO RIGHT";
    string attack = "ATTACK";
    string openTask = "OPEN TASK";
    string getStats = "GET STATS";

    string musicOff = "MUSIC OFF";
    string musicOn = "MUSIC ON";
    string exitApp = "EXIT";
    string help = "HELP";

    string upCmd = "up";
    string downCmd = "down";
    string leftCmd = "left";
    string rightCmd = "right";
    string statsCmd = "stats";

    public bool pretty = false;

    public bool delay = false;

    public void SendCommand()
    {
        if (delay)
        {
            StartCoroutine(InputLocker());
        }
        if (!commandInput.text.Equals(""))
        {
            cmdHistory.AddToHistory(commandInput.text);
        }
        string cmd = "";
        //Izlaz iz aplikacije
        if (commandInput.text.ToUpper().StartsWith(exitApp))
        {
            Application.Quit();
        }

        else if (commandInput.text.ToUpper().StartsWith(help))
        {
            sessionManager.textData.Type(MessagePools.helpMessage);
        }

        //Iskljuci muziku
        else if (commandInput.text.ToUpper().StartsWith(musicOff))
        {
            audioManager.volume = 0.0f;
        }

        //Ukljuci muziku
        else if (commandInput.text.ToUpper().StartsWith(musicOn))
        {
            audioManager.volume = 1.0f;
        }

        //Otvori task
        else if (commandInput.text.ToUpper().StartsWith(openTask))
        {
            if (sessionManager.positionData.task.url.Equals(""))
            {
                sessionManager.textData.Type(MessagePools.GetMsg(MessagePools.emptyTask));
            }
            else
            {
                Application.OpenURL(sessionManager.positionData.task.url);
            }
        }

        //Posalji solution za test case
        else if (commandInput.text.ToUpper().StartsWith(attack))
        {
            if (sessionManager.positionData.task.url.Equals(""))
            {
                sessionManager.textData.Type(MessagePools.GetMsg(MessagePools.emptyAttack));
            }
            else
            {
                attackManager.SendAttack();
            }
        }

        //Zatrazi statistiku za team
        else if (commandInput.text.ToUpper().StartsWith(getStats))
        {
            sessionManager.GetStats();
        }

        //Odi gore
        else if (sessionManager.positionData.actions.up && commandInput.text.ToUpper().StartsWith(goUp))
        {
            cmd = upCmd;
        }

        //Odi dolje
        else if (sessionManager.positionData.actions.down && commandInput.text.ToUpper().StartsWith(goDown))
        {
            cmd = downCmd;
        }

        //odi lijevo
        else if (sessionManager.positionData.actions.left && commandInput.text.ToUpper().StartsWith(goLeft))
        {
            cmd = leftCmd;
        }

        //Odi desno
        else if (sessionManager.positionData.actions.right && commandInput.text.ToUpper().StartsWith(goRight))
        {
            cmd = rightCmd;
        }

        else if (commandInput.text.ToUpper().StartsWith("PRETTY"))
        {
            pretty = !pretty;
            egg.SetActive(pretty);
        }

        //Ako je bila naredba za kretanje posalji
        if (!cmd.Equals(""))
        {
            sessionManager.DoAction(cmd);
        }
        commandInput.text = "";
    }

    public bool allowEnter = false;

    void Update()
    {
        if (allowEnter)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                SendCommand();
                commandInput.ActivateInputField();
            }
        }
        allowEnter = commandInput.isFocused && !commandInput.readOnly;
    }

    IEnumerator InputLocker()
    {
        commandInput.readOnly = true;
        yield return new WaitForSecondsRealtime(0.5f);
        commandInput.readOnly = false;
    }
}
