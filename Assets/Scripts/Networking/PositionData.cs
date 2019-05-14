using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PositionData
{
    public Actions actions;
    public float points;
    public Task task;
    public string display_metadata;
    public int x;
    public int y;

    override
    public string ToString()
    {
        string s = "actions = " + actions.ToString() + "\n";
        s += "points = " + points + "\n";
        s += "task = " + task.ToString() + "\n";
        s += "display metadata = " + display_metadata + "\n";
        s += x.ToString() + " " + y.ToString();
        return s;
    }
}

[Serializable]
public struct Actions
{
    public bool up;
    public bool down;
    public bool left;
    public bool right;

    override
    public string ToString()
    {
        string s = "up: " + up + "; down: " + down + "; left: " + left + "; right: " + right;
        return s;
    }
}

[Serializable]
public struct Task
{
    public int id;
    public string url;
    public bool[] healthbar;

    override
    public string ToString()
    {
        string s = "id: " + id + "; url: " + url;
        return s;
    }
}
