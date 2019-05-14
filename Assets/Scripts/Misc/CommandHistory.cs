using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandHistory : MonoBehaviour
{
    public List<string> history = new List<string>();
    public InputField commandInput;
    public int i = 0;

    void Update()
    {
        if (commandInput.isFocused && !commandInput.readOnly)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (i != 0)
                {
                    --i;
                    commandInput.text = history[i];
                }
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (i < history.Count - 1)
                {
                    ++i;
                    commandInput.text = history[i];
                }
                else if (i == history.Count - 1)
                {
                    ++i;
                    commandInput.text = "";
                }
            }
        }
    }

    public void AddToHistory(string s)
    {
        if (history.Count == 128)
        {
            history.RemoveAt(0);
        }
        history.Add(s);
        i = history.Count;
    }
}
