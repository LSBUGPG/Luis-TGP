using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStart : MonoBehaviour
{
    public void Start()
    {
        enemies.SetActive(false);
    }
    public AudioSource countdown;
    public void animStart()
    { 
        countdown.Play();
    }

    public GameObject enemies;

    public void animOver()
    {
        enemies.SetActive(true);

    }

}
