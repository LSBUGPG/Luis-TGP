using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerDamage : MonoBehaviour
{
    public int MaxHealth = 100;
    [SerializeField] private int health;

    public int Health
    {
        get { return health; } 
        set 
        {
            health = value;
            OnHealthChange?.Invoke((float)health / MaxHealth);
        
        }

    }
    public UnityEvent OnDead;
    public UnityEvent<float> OnHealthChange;
    public UnityEvent OnHit, OnHeal;

    public void Start()
    {
        Health = MaxHealth;
    }
    public Slider slider;

    internal void Hit(int damagePoints)
    {
        Health -= damagePoints;
        if (Health <= 0)
        {
            OnDead?.Invoke();
        }
        else
        {
            OnHit?.Invoke();
            slider.value = health;
        }
    }
    public void Heal(int healthBoost)
    {
        Health += healthBoost;
        Health = Mathf.Clamp(Health, 0, MaxHealth);
        OnHeal?.Invoke();
    }




}
