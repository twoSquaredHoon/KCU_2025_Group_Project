using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeToStage3()
    {
        SceneManager.LoadScene("Stage3");
    }

    public void ChangeToStage2()
    {
        SceneManager.LoadScene("Stage2");
    }
}

