using System;
using System.Collections;
using System.Collections.Generic;
using ToolsAndMechanics.UserInterfaceManager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public enum SceneName
{
    Menu,
    Game
}

public class LoadSceneButton : AbstractButton
{
    [SerializeField]
    private SceneName sceneName;

    public override void OnButtonClick()
    {
        SceneManager.LoadSceneAsync((int)sceneName);
    }
}
