using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
public class Health : MonoBehaviour
{
    public event Action<Vector2> OnDamaged;
    public event Action OnDeath;

    public int health;
    public int maxHealth;
    public Slider slider;
    public TextMeshProUGUI healthTotalText;

    private void Start()
    {
        health = maxHealth;
        slider.maxValue = maxHealth;
        slider.value = health;
    }

    public void ChangeHealth (int amount, Vector2 sourcePosition)
    {
        health += amount;
        slider.value = health;


        if (health > maxHealth)
        {
            health = maxHealth;
        }
        else if (health <= 0)
        {
            OnDeath?.Invoke();
        }
        else if(amount < 0)
        {
            OnDamaged?.Invoke(sourcePosition);

        }
    }

    public void ChangeEnemyHealth(int amount, Vector2 sourcePosition)
    {
        health += amount;


        if (health > maxHealth)
        {
            health = maxHealth;
        }
        else if (health <= 0)
        {
            OnDeath?.Invoke();
        }
        else if (amount < 0)
        {
            OnDamaged?.Invoke(sourcePosition);

        }
    }

    public void ChangeHealth(int amount)
    {
        health += amount;
        slider.maxValue = maxHealth;
        slider.value = health;


        if (health > maxHealth)
        {
            health = maxHealth;
        }
        else if (health <= 0)
        {
            OnDeath?.Invoke();
            
        }
        
    }

    public void ChangeEnemyHealth(int amount)
    {
        health += amount;

        if (health > maxHealth)
        {
            health = maxHealth;
        }
        else if (health <= 0)
        {
            OnDeath?.Invoke();

        }
    }
}
