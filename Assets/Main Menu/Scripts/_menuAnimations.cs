using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class _menuAnimations : MonoBehaviour
{
    [SerializeField] MainMenuManager mainMenuManager;

    public void _fadingAnimationIsDone()
    {
        mainMenuManager.main_animator.enabled = false;
        mainMenuManager.homePanel.blocksRaycasts = true;
    }
}

