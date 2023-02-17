using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySelect : MonoBehaviour
{
    public string EasyLevel = "Scenes/EasyLevel";
    public string NormalLevel = "Scenes/NormalLevel";
    public string HardLevel = "Scenes/HardLevel";

    public SceneFader sceneFader;

    public void Easy()
    {
        sceneFader.FadeTo(EasyLevel);
    }

    public void Normal()
    {
        sceneFader.FadeTo(NormalLevel);
    }

    public void Hard()
    {
        sceneFader.FadeTo(HardLevel);
    }
}
