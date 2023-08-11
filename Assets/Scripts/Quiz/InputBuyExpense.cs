using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBuyExpense : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadInputBuyExpense()
    {
        PlayerPrefs.SetInt("BuyingExpense", int.Parse(this.GetComponent<UnityEngine.UI.InputField>().text));
        PlayerPrefs.SetInt("BuyingExpenseValue", int.Parse(this.GetComponent<UnityEngine.UI.InputField>().text));
        PlayerPrefs.Save();
    }
}
