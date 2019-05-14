using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceManager : MonoBehaviour
{
    public int level = 1;
    public int experience = 0;
    public Text expLabel;
    public Text lvlLabel;

    public void UpdateExperience(float points)
    {
        experience = (int)(points * 1000);
        level = 1 + (int)(Mathf.Sqrt(points * 1000) / 100.0f);
        expLabel.text = experience.ToString();
        lvlLabel.text = level.ToString();
    }
}
