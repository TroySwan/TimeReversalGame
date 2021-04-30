using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockTrigger : TimeReversal
    
{
    public Rock rock;

    // MARK:- TIME REVERSAL METHODS
    protected override void AdditionalUpdate()
    {
           GetComponent<Collider2D>().enabled = !isReversingTime;
    }

    // MARK:- COLLISION METHODS
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            rock.DropRock();
        }
    }
}
