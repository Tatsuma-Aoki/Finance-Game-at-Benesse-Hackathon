using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Reward : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rewardText;
    private int reward = 0;
    // Start is called before the first frame update
    void Start()
    {
        reward += PlayerPrefs.GetInt("EarnedReward", 0);
        rewardText.text = reward.ToString();
        PlayerPrefs.SetInt("Reward", reward);
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
