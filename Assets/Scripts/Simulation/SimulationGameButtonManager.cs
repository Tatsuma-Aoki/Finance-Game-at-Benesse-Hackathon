using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SimulationGameButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject gameStartPanel;
    [SerializeField] private List<GameObject> panels;
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private TermManager termManager;
    [SerializeField] private InputField inputBuyExpense;
    [SerializeField] private TextMeshProUGUI usedMoneyText;
    [SerializeField] private TextMeshProUGUI earnedMoneyText;
    [SerializeField] private TextMeshProUGUI profitMoneyText;
    public int valueOfEvent;
    private int profit;
    private bool isBuyButtonPushed = false;
    private bool isChangeNextTerm = true;
    private int totalUsedMoney = 0;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject panel in panels)
        {
            panel.SetActive(false);
            resultPanel.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickToGameStartButton()
    {
        // ActivatePanel(panels[termManager.termCount].name);
        panels[termManager.termCount].SetActive(true);
        panels[termManager.termCount].transform.Find("SellButton").gameObject.GetComponent<Button>().interactable = false;
        panels[termManager.termCount].transform.Find("InputBuyExpense").gameObject.SetActive(false);
    }

    public void OnClickToBackHomeButton()
    {
        SceneManager.LoadScene("HomeScene");
    }
    public void OnClickToBuyButton()
    {
        inputBuyExpense.gameObject.SetActive(true);
        isBuyButtonPushed = true;
    }

    public void OnClickToSellButton()
    {
        if(PlayerPrefs.GetInt("BuyingExpense", 0) != 0)
        {
            profit = PlayerPrefs.GetInt("BuyingExpenseValue", 0) - PlayerPrefs.GetInt("BuyingExpense", 0);
            PlayerPrefs.SetInt("Profit", PlayerPrefs.GetInt("Profit", 0) + profit);
            PlayerPrefs.Save();
            PlayerPrefs.DeleteKey("BuyingExpense");
            PlayerPrefs.DeleteKey("BuyingExpenseValue");
        }
    }

    public void OnClickToNextTermButton()
    {
        termManager.termCount++;
        panels[termManager.termCount].SetActive(true);
        if(isBuyButtonPushed)
        {
            totalUsedMoney += int.Parse(inputBuyExpense.text);
            if(totalUsedMoney <= PlayerPrefs.GetInt("Reward", 0))
            {
                PlayerPrefs.SetInt("BuyingExpense", int.Parse(inputBuyExpense.text));
                PlayerPrefs.SetInt("BuyingExpenseValue", int.Parse(inputBuyExpense.text));
                PlayerPrefs.Save();
                isBuyButtonPushed = false;
            }
            else
            {
                inputBuyExpense.text = "所持金を超えています";
                totalUsedMoney -= int.Parse(inputBuyExpense.text);
                isChangeNextTerm = false;
            }
        }

        if(PlayerPrefs.GetInt("BuyingExpense", 0) != 0)
        {
            PlayerPrefs.SetInt("BuyingExpenseValue", PlayerPrefs.GetInt("BuyingExpenseValue", 0) + valueOfEvent);
            PlayerPrefs.Save();
        }

        if(isChangeNextTerm)
        {
            termManager.termCount++;
            if(termManager.termCount < 12)
            {
                // ActivatePanel(panels[termManager.termCount].name);
                Debug.Log(panels[termManager.termCount].name);
                // panels[termManager.termCount].SetActive(true);
                if(PlayerPrefs.GetInt("BuyingExpense", 0) == 0)
                {
                    panels[termManager.termCount].transform.Find("SellButton").gameObject.GetComponent<Button>().interactable = false;
                    panels[termManager.termCount].transform.Find("InputBuyExpense").gameObject.SetActive(false);
                }
                else
                {
                    panels[termManager.termCount].transform.Find("BuyButton").gameObject.GetComponent<Button>().interactable = false;
                    panels[termManager.termCount].transform.Find("InputBuyExpense").gameObject.SetActive(false);
                }
            }
            else
            {
                // ActivatePanel(resultPanel.name);
                resultPanel.SetActive(true);
                usedMoney();
                profitMoney();
                earnedMoney();
                PlayerPrefs.SetInt("EarnedReward", PlayerPrefs.GetInt("Profit", 0));
                PlayerPrefs.Save();
            }
        }
    }

    public void usedMoney()
    {
        usedMoneyText.text = "投資した金額" + totalUsedMoney.ToString();
    }

    public void profitMoney()
    {
        profitMoneyText.text = "利益" + PlayerPrefs.GetInt("Profit", 0).ToString();
    }

    public void earnedMoney()
    {
        earnedMoneyText.text ="回収した金額" + (totalUsedMoney + PlayerPrefs.GetInt("Profit", 0)).ToString();
    }

    private void ActivatePanel(string panelToBeActivated)
    {
        foreach(GameObject panel in panels)
        {
            panel.SetActive(panelToBeActivated.Equals(panel.name));
        }
        resultPanel.SetActive(panelToBeActivated.Equals(resultPanel.name));
    }
}
