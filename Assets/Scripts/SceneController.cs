using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // HomeSceneのボタンを押したときの処理
    public void OnClickToQuizGameStartSceneButton()
    {
        SceneManager.LoadScene("QuizGameLobbyScene");
    }
    public void OnClickToSimulationGameStartSceneButton()
    {
        SceneManager.LoadScene("SimulationGameStartScene");
    }
}

