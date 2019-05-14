using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAnimation : MonoBehaviour
{
    public LabelManager stemGames;
    public LabelManager techArena;
    public GameObject[] loginFields;

    void Start()
    {
        StartCoroutine(RunAnimation());
    }

    IEnumerator RunAnimation()
    {
        yield return new WaitForSeconds(1.0f);
        stemGames.Type("STEM Games");
        yield return new WaitForSeconds(0.2f + (((float)"STEM Games".Length) / stemGames.charactersPerSecond));
        techArena.Type("Technology Arena");
        yield return new WaitForSeconds(0.2f + (((float)"Technology Arena".Length) / techArena.charactersPerSecond));
        foreach (GameObject obj in loginFields)
        {
            obj.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
