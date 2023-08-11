using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizGameSceneController : MonoBehaviour
{
    [SerializeField] private GameObject loginUIPanel;
    [SerializeField] private GameObject gameOptionsUIPanel;
    [SerializeField] private GameObject createRoomUIPanel;
    [SerializeField] private GameObject insideRoomUIPanel;
    [SerializeField] private GameObject roomListUIPanel;
    [SerializeField] private GameObject gamePanel;

    // QuizGameのloginUIPanelでLoginボタンを押したときの処理

    private void Start() {
        loginUIPanel.SetActive(true);
        gameOptionsUIPanel.SetActive(false);
        createRoomUIPanel.SetActive(false);
        insideRoomUIPanel.SetActive(false);
        gamePanel.SetActive(false);
    }
    
    // public void OnClickToLoginButton()
    // {
    //     loginUIPanel.SetActive(false);
    //     gameOptionsUIPanel.SetActive(true);
    // }

    // // QuizGameのgameOptionsUIPanelでCreateRoomボタンを押したときの処理
    // public void OnClickToCreateRoomButton()
    // {
    //     gameOptionsUIPanel.SetActive(false);
    //     createRoomUIPanel.SetActive(true);
    // }

    // // QuizGameのgameOptionsUIPanelでRoomListボタンを押したときの処理
    // public void OnClickToRoomListButton()
    // {
    //     gameOptionsUIPanel.SetActive(false);
    //     roomListUIPanel.SetActive(true);
    // }

    // // QuizGameのcreateRoomUIPanelでCreateボタンを押したときの処理
    // public void OnClickToCreateButton()
    // {
    //     createRoomUIPanel.SetActive(false);
    //     insideRoomUIPanel.SetActive(true);
    // }

    // // QuizGameのcreateRoomUIPanelでCancelボタンを押したときの処理
    // public void OnClickToCancelButton()
    // {
    //     createRoomUIPanel.SetActive(false);
    //     gameOptionsUIPanel.SetActive(true);
    // }

    // // QuizGameのinsideRoomUIPanelでLeaveRoomボタンを押したときの処理
    // public void OnClickToLeaveRoomButton()
    // {
    //     insideRoomUIPanel.SetActive(false);
    //     gameOptionsUIPanel.SetActive(true);
    // }

    // // QuizGameのinsideRoomUIPanelでGameStartボタンを押したときの処理
    // public void OnClickToGameStartButton()
    // {
    //     insideRoomUIPanel.SetActive(false);
    //     gamePanel.SetActive(true);
    // }

    // // QuizGameのinsideRoomUIPanelでGameStartボタンを押したときの処理
    // public void OnClickToBackButton()
    // {
    //     roomListUIPanel.SetActive(false);
    //     gameOptionsUIPanel.SetActive(true);
    // }
}
