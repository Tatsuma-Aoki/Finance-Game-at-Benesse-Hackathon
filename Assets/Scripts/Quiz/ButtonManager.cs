using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject correctPanel;
    [SerializeField] private GameObject wrongPanel;
    [SerializeField] private PanelManager panelManager;
    public bool isCorrect = false;
    private float timer = 0f;
    private bool isPanelActive = false;
    private bool isCorrected = false;
    // Start is called before the first frame update
    
    private void Update()
    {
        if(isPanelActive)
        {
            timer += Time.deltaTime;
            if(timer > 0.3f)
            {
                ActivatePanel("None");
                timer = 0f;
                isPanelActive = false;
                if(isCorrected)
                {
                    panelManager.isCorrected = true;
                }
            }
        }
    }
    public void OnClickToQuizButton()
    {
        if(isCorrect)
        {
            ActivatePanel("CorrectPanel");
            isCorrected = true;
        }
        else
        {
            ActivatePanel("WrongPanel");
        }
        isPanelActive = true;
    }

    public void OnClickToBackHomeButton()
    {
        SceneManager.LoadScene("HomeScene");
    }



    private void ActivatePanel(string panelToBeActivated)
    {
        correctPanel.SetActive(panelToBeActivated.Equals(correctPanel.name));
        wrongPanel.SetActive(panelToBeActivated.Equals(wrongPanel.name));
        if(panelToBeActivated == "None")
        {
            correctPanel.SetActive(false);
            wrongPanel.SetActive(false);
        }
    }
}
