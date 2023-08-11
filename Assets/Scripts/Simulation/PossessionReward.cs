using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PossessionReward : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI possessionRewardText;
    // Start is called before the first frame update
    void Start()
    {
        possessionRewardText.text = PlayerPrefs.GetInt("Reward", 0).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
