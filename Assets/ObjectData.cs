using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectData
{
    // General Rigidbody Data
    public Vector3 position;
    public Quaternion rotation;
    public Vector2 velocity;

    // Player Specific Data 
    public Sprite sprite;
    public bool isDead;
    public bool isFacingRight;
    public float horizontalMovement;

    // Rock Specific Data
    public bool isDropped;

    // Timer Data
    public float timePassed;

    // MARK:- CONSTRUCTORS
    // General Constructor
    public ObjectData (Vector3 _position, Quaternion _rotation)
    {
        position = _position;
        rotation = _rotation;
    }

    // Player Data Constructor
    public ObjectData(Vector3 _position, Quaternion _rotation, Vector2 _velocity, Sprite _sprite, bool _isDead, bool _isFacingRight)
    {
        position = _position;
        rotation = _rotation;
        sprite = _sprite;
        isDead = _isDead;
        velocity = _velocity;
        isFacingRight = _isFacingRight;
    }

    // Rock Data Constructor

    public ObjectData(Vector3 _position, Quaternion _rotation, bool _isDropped, Vector2 _velocity)
    {
        position = _position;
        rotation = _rotation;
        isDropped = _isDropped;
        velocity = _velocity;
    }

    // Timer Constructor 
    public ObjectData(float _timePassed)
    {
        timePassed = _timePassed;
    }
}
