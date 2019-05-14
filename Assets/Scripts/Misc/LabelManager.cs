using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabelManager : MonoBehaviour
{
    public float charactersPerSecond = 3.0f;
    Text label;
    string text;

    void Start()
    {
        label = GetComponent<Text>();
    }

    public void Type(string s)
    {
        text = s;
        StartCoroutine(TyperCoroutine());
    }

    IEnumerator TyperCoroutine()
    {
        label.text = "";
        foreach (char c in text)
        {
            label.text += c;
            yield return new WaitForSeconds(1.0f / charactersPerSecond);
        }
    }
}
