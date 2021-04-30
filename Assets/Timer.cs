using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : TimeReversal
{ 
    public Text timeInBank;
    public float timePassed;

    // MARK:- TIME REVERSAL METHODS
    protected override void StoreData()
    {
        if (timePassed < 20)
        {
            timePassed += Time.deltaTime;
            timeInBank.text = timePassed.ToString("00.00");
            objectDataStore.Insert(0, new ObjectData(timePassed));
        }
    }

    protected override void ApplyData(ObjectData objectData)
    {
        timeInBank.text = objectData.timePassed.ToString("00.00");
        timePassed = objectData.timePassed;
    }

}
