using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private PanelManager panelManager;
    [SerializeField] private TextMeshProUGUI timerText;
    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(panelManager.isOnQuiz)
        {
            timer += Time.deltaTime;
            timerText.text = String.Format("{0:00.00}", timer);
        }
    }
}
