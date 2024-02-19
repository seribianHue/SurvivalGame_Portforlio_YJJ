using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommMath
{
    static CommMath _instance;
    public static CommMath Instance
    {
        get 
        { 
            if(_instance == null) 
                _instance = new CommMath(); 
            return _instance; 
        }
    }

    public bool ProbabilityMethod(float percent)
    {
        int randomNum = Random.Range(0, 100);
        if (randomNum < percent) { return true; }
        else { return false; }
    }
}
