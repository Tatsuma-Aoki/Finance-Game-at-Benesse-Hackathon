using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    [SerializeField] private GameObject count3Panel;
    [SerializeField] private GameObject count2Panel;
    [SerializeField] private GameObject count1Panel;
    [SerializeField] private List<GameObject> quizPanels;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject correctPanel;
    [SerializeField] private GameObject wrongPanel;
    [SerializeField] private GameObject waitingPanel;
    [SerializeField] private GameObject rankingPanel;
    [SerializeField] private GameObject rewardPanel;
    [SerializeField] private GameObject rewardPrefab;
    public bool isOnQuiz { get; private set;} = false;
    public bool isCorrected {private get; set;}
    private float timer = 0f;
    private bool isOnCountdown;
    private bool isCountdowned2 = false;
    private bool isCountdowned1 = false;
    private bool isOnWaiting = false;
    private bool isRewareded = false;
    private int quizCount = 1;
    private const int NUMBER_OF_QUIZ = 10;
    private const float REWARD_PANEL_SPEED = 0.2f;
    private int quizReward = 10000;
    // Start is called before the first frame update
    void Start()
    {
        count3Panel.SetActive(true);
        count2Panel.SetActive(false);
        count1Panel.SetActive(false);
        foreach(GameObject quizPanel in quizPanels)
        {
            quizPanel.SetActive(false);
        }
        correctPanel.SetActive(false);
        wrongPanel.SetActive(false);
        waitingPanel.SetActive(false);
        rankingPanel.SetActive(false);
        isOnCountdown = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isOnCountdown)
        {
            timer += Time.deltaTime;
            if(timer > 1f && !isCountdowned2)
            {
                ActivatePanel("Count2");
                isCountdowned2 = true;
            }
            if(timer > 2f && !isCountdowned1)
            {
                ActivatePanel("Count1");
                isCountdowned1 = true;
            }
            if(timer > 3f)
            {
                ActivatePanel("Quiz" + quizCount);
                isOnCountdown = false;
                isOnQuiz = true;
                timer = 0f;
            }
        }

        if(isCorrected)
        {
            quizCount++;
            if(quizCount < NUMBER_OF_QUIZ + 1)
            {
                ActivatePanel("Quiz" + quizCount);
            }
            else
            {
                ActivatePanel("WaitingPanel");
                timerText.text = "";
                isOnQuiz = false;
                isOnWaiting = true;
            }
            isCorrected = false;
        }

        if(isOnWaiting)
        {
            timer += Time.deltaTime;
            if(timer > 2f)
            {
                ActivatePanel("RankingPanel");
                isOnWaiting = false;
                isRewareded = true;
                PlayerPrefs.SetInt("EarnedReward", quizReward);
                PlayerPrefs.Save();
                timer = 0f;
            }
        }

        if(isRewareded)
        {
            timer += Time.deltaTime;
            if(timer < 4.0f && rewardPanel.transform.localPosition.y - REWARD_PANEL_SPEED > 150)
            {
                rewardPanel.transform.localPosition = new Vector3(0f, rewardPanel.transform.localPosition.y - REWARD_PANEL_SPEED, 0f);
            }
            else if(timer > 4.0f && rewardPanel.transform.localPosition.y + REWARD_PANEL_SPEED < 250)
            {
                rewardPanel.transform.localPosition = new Vector3(0f, rewardPanel.transform.localPosition.y + REWARD_PANEL_SPEED, 0f);
            }
            else
            {
                rewardPanel.transform.localPosition = new Vector3(0f, rewardPanel.transform.localPosition.y - timer*0, 0f);
            }
        }
    }

    private void ActivatePanel(string panelToBeActivated)
    {
        count3Panel.SetActive(panelToBeActivated.Equals(count3Panel.name));
        count2Panel.SetActive(panelToBeActivated.Equals(count2Panel.name));
        count1Panel.SetActive(panelToBeActivated.Equals(count1Panel.name));
        foreach(GameObject quizPanel in quizPanels)
        {
            quizPanel.SetActive(panelToBeActivated.Equals(quizPanel.name));
        }
        waitingPanel.SetActive(panelToBeActivated.Equals(waitingPanel.name));
        rankingPanel.SetActive(panelToBeActivated.Equals(rankingPanel.name));
    }
}
