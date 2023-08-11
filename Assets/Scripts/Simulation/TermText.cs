using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TermText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI termText;
    [SerializeField] private TermManager termManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        termText.text = ((termManager.termCount / 4) + 1).ToString() + "年目 " + termManager.termString;
    }
}
