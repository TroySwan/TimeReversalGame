using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TimeReversal : MonoBehaviour
{
    protected List<ObjectData> objectDataStore;
    protected Rigidbody2D objectRigidbody;
    protected bool isReversingTime = false;

    private float reversalLimit = 20f;
    private GameObject player;

    // PROTECTED METHODS
    protected void MakeKinematic(bool isKinematic)
    {
        if (objectRigidbody != null)
        {
            objectRigidbody.isKinematic = isKinematic;
        }
    }

    // OVERRIDABLE METHODS

    protected virtual void AdditionalStart()
    {
        // Additional actions to be called before the first frame update go here
    }

    protected virtual void AdditionalUpdate()
    {
        // Additional actions to be called once per frame go here
    }

    protected virtual void ReversalStopped()
    {
        isReversingTime = false;
        MakeKinematic(false);
    } 

    protected virtual void StoreData()
    {
        objectDataStore.Insert(0, new ObjectData(transform.position, transform.rotation));
    }

    protected virtual void ApplyData(ObjectData objectData)
    {
        transform.position = objectData.position;
        transform.rotation = objectData.rotation;
    }

    // PRIVATE METHODS
    private void Start()
    {
        objectRigidbody = GetComponent<Rigidbody2D>();
        objectDataStore = new List<ObjectData>();
        player = GameObject.Find("Ninja");
        AdditionalStart();
    }

    private void Update()
    {
        if (!GameObject.Find("UI Manager").GetComponent<UIManager>().isPaused)
        {
            AdditionalUpdate();

            if (Input.GetKeyDown(KeyCode.R))
            {
                isReversingTime = true;
                MakeKinematic(true);
            }
            if (Input.GetKeyUp(KeyCode.R))
            {
                ReversalStopped();
            }
        }

    }

    private void FixedUpdate()
    {
        if (isReversingTime)
        {
            ReverseTime();
        }
        else
        {
            if (player.GetComponent<PlayerCharacterController>().isDead == false)
            {
                StoreObjectData();
            }
        }
    }

    private void StoreObjectData()
    {
        if (objectDataStore.Count > Mathf.Round(reversalLimit / Time.fixedDeltaTime))
        {
            objectDataStore.RemoveAt(objectDataStore.Count - 1);
        }
        StoreData();
    }

    private void ReverseTime()
    {
        if (objectDataStore.Count > 0)
        {
            ObjectData objectData = objectDataStore[0];

            ApplyData(objectData);

            objectDataStore.RemoveAt(0);
        }
        else
        {
            ReversalStopped();
        }
    }
}
