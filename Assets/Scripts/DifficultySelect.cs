using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySelect : MonoBehaviour
{
    public SceneFader sceneFader;
    public string Level = "NormalLevel";

    public void Easy()
    {
        sceneFader.FadeTo(Level);
        WaveSpawner.Difficulty = 0;
    }

    public void Normal()
    {
        sceneFader.FadeTo(Level);
        WaveSpawner.Difficulty = 1;
    }

    public void Hard()
    {
        sceneFader.FadeTo(Level);
        WaveSpawner.Difficulty = 2;
    }
}
