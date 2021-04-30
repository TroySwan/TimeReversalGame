# TimeReversalGame 2020 - C# and Unity
![Gif showign time reversal mechanic](https://media.giphy.com/media/hMsT8dS2Bk0pPAgLEj/giphy.gif)

The player can rewind time for as long as there is time in thier bank up to the last 20 seconds.


Each object in the game that can be affected by the reversal of time inherits from the TimeReversal class, even the timer.
There is an object data store for each update.

An example for the timer object is below:
```c#
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
```
