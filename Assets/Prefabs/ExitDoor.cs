using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    private GameObject pauseMenu;

    // MARK:- PUBLIC METHODS
    void Start()
    {
        pauseMenu = GameObject.Find("UI Manager");
    }

    // MARK:- COLLISION METHODS
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            pauseMenu.gameObject.GetComponent<UIManager>().ShowSuccessMessage();
        }
    }
}
