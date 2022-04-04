using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Slider _healthBar;

    private Animator _animator;
    private Weapon _currentWeapon;

    public int Money { get; private set; }
    public int CurrentHealth { get; private set; }
    public int MinHealth { get; private set; }
    public int MaxHealth { get; private set; }

    private const float FillingTime = 0.5f;

    public void OnEnemyDied(int reward)
    {
        Money += reward;
    }

    public void ApplyDamage(int damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= MinHealth)
            Destroy(gameObject);

        ChangeHealth();
    }

    private void OnEnable()
    {
        MinHealth = 0;
        MaxHealth = 100;
    }

    private void Start()
    {
        CurrentHealth = MaxHealth;
        _currentWeapon = _weapons[0];
        _animator = GetComponent<Animator>();
        SetMaxHealth(MaxHealth);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            _currentWeapon.Shoot(_shootPoint);
    }

    private void SetMaxHealth(int health)
    {
        _healthBar.maxValue = health;
        _healthBar.value = health;
    }

    private void Heal(int bonusHealth)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + bonusHealth, MinHealth, MaxHealth);
        ChangeHealth();
    }

    private void ChangeHealth()
    {
        _healthBar.DOValue(CurrentHealth, FillingTime);
    }
}