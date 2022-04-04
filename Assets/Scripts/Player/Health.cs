using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int CurrentValue { get; private set; }
    public int MinValue { get; private set; }
    public int MaxValue { get; private set; }

    public void TakeDamage(int damage)
    {
        CurrentValue -= damage;

        if (CurrentValue <= MinValue)
            Destroy(gameObject);
    }

    public void Heal(int bonusHealth)
    {
        CurrentValue = Mathf.Clamp(CurrentValue + bonusHealth, MinValue, MaxValue);
    }

    private void OnEnable()
    {
        MinValue = 0;
        MaxValue = 100;
    }

    private void Start()
    {
        CurrentValue = MaxValue;
    }
}