using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    public float smoothingSpeed = 0.5f;

    private Vector3 velocity;

    public float minX;
    public float minY;
    public float maxX;
    public float maxY;

    // MARK:- PUBLIC METHODS
    void Update()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = player.transform.position;

        transform.position = Vector3.SmoothDamp(startPosition, endPosition, ref velocity, smoothingSpeed);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY), transform.position.z);

    }
}
