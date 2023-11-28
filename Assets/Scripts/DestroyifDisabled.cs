using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyifDisabled : MonoBehaviour
{

    public bool SelfDestructionEnable { get; set; } = false;


    private void OnDisable()
    {
        if (SelfDestructionEnable )
        {
            Destroy(gameObject);
        }
    }
}
