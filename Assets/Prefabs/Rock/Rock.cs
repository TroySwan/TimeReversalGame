using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : TimeReversal
{
    private bool isDropped = false;

    // MARK:- PUBLIC METHODS
    public void DropRock()
    {
        isDropped = true;
        MakeKinematic(false);
    }

    // MARK:- TIME REVERSAL METHODS
    protected override void StoreData()
    {
        objectDataStore.Insert(0, new ObjectData(transform.position, transform.rotation, isDropped, objectRigidbody.velocity));
    }

    protected override void ApplyData(ObjectData objectData)
    {
        transform.position = objectData.position;
        transform.rotation = objectData.rotation;
        isDropped = objectData.isDropped;
        objectRigidbody.velocity = objectData.velocity;
    }

    protected override void ReversalStopped()
    {
        isReversingTime = false;
        objectRigidbody.isKinematic = !isDropped;
    }
}
