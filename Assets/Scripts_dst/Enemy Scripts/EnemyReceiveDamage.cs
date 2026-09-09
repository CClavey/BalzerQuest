using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyReceiveDamage : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public GameObject healthBar;
    public Slider healthBarSlider;
    private Coroutine drainHealthCoroutine;
    [SerializeField] private float drainTime = 0.25f;
    private float target = 1f;
    public Image fillImage;
    public Gradient healthGradient;

    public GameObject lootDrop;

    private void Start()
    {
        health = maxHealth;
        healthBarSlider.value = CalculateHealthPercent();
        UpdateHealthBarColor(); 
    }

    public void DealDamage(float damage)
    {
        healthBar.SetActive(true);
        health -= damage;
        CheckDeath();
        target = CalculateHealthPercent();
        if (drainHealthCoroutine != null)
        {
            StopCoroutine(drainHealthCoroutine);
        }

        drainHealthCoroutine = StartCoroutine(DrainHealth());
    }

    private void CheckOverheal()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void HealEnemy(float heal)
    {
        health += heal;
        CheckOverheal();
        healthBarSlider.value = CalculateHealthPercent();
        UpdateHealthBarColor();
    }

    private void CheckDeath()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            Instantiate(lootDrop, transform.position, Quaternion.identity);
        }
    }

    private float CalculateHealthPercent()
    {
        return (health / maxHealth);
    }

    private IEnumerator DrainHealth()
    {
        float fillAmount = healthBarSlider.value;
        float elapsedTime = 0f;
        while (elapsedTime < drainTime)
        {
            elapsedTime += Time.deltaTime;
            healthBarSlider.value = Mathf.Lerp(fillAmount, target, elapsedTime / drainTime);
            UpdateHealthBarColor();
            yield return null;
        }
    }

    private void UpdateHealthBarColor()
    {
        float healthPercent = CalculateHealthPercent();
        Color newColor = healthGradient.Evaluate(healthPercent);
        fillImage.color = newColor;
    }
}