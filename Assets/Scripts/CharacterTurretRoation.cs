using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterTurretRoation : MonoBehaviour
{
    private CustomInput Controls;

    Vector2 rot;

    private void Awake()
    {
        Controls = new CustomInput();

        Controls.Player.Rotate.performed += cntxt => rot = cntxt.ReadValue<Vector2>();
        Controls.Player.Rotate.canceled += cntxt => rot = Vector2.zero;
    }

    private void Update()
    {
        GetComponent<Transform>().Rotate(Vector3.down * rot.y*.2f);
    }

    private void OnEnable()
    {
        Controls.Player.Enable();
    }

    private void OnDisable()
    {
        Controls.Player.Disable();
    }

}
