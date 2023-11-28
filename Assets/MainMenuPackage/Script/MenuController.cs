using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject MenuParent;
    public GameObject OptionsParent;
    public GameObject LevelSelParent;
    public AudioSource RtnSource;

    private void Start()
    {
        MenuParent.SetActive(true);
        OptionsParent.SetActive(false);
        LevelSelParent.SetActive(false);

    }


    public void RtnSound()
    {
        RtnSource.Play();

    }
}
