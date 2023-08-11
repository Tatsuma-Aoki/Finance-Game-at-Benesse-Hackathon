using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TermManager : MonoBehaviour
{
    public int termCount {get; set;} = 0;
    public string termString {get; set;}
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update()
    {
        if(termCount % 4 == 0)
        {
            termString = "春";
        }
        else if(termCount % 4 == 1)
        {
            termString = "夏";
        }
        else if(termCount % 4 == 2)
        {
            termString = "秋";
        }
        else if(termCount % 4 == 3)
        {
            termString = "冬";
        }
    }
}
