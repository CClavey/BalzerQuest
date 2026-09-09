using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats playerStats;
    public GameObject player;
    public Text healthText;
    public Slider healthSlider;
    private Coroutine drainHealthCoroutine;
    [SerializeField] private float drainTime = 0.25f;
    private float target = 1f;
    public Image fillImage;
    public Gradient healthGradient;
    public float health;
    public float maxHealth;
    public int coins;
    public Text coinValue;
    public AudioSource audioSource;
    public AudioClip deathSound;
    

    void Awake()
    {
        if (playerStats != null)
        {
            Destroy(playerStats);
        }
        else
        {
            playerStats = this;
        }
        DontDestroyOnLoad(this);
    }



    void Start()
    {
        health = maxHealth;
        SetHealthUI();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 10)
        {
            maxHealth = 1000000;
            health = 1000000;
        }
    }

    public void DealDamage(float damage)
    {
        health -= damage;
        CheckDeath();
        SetHealthUI();
        target = CalculateHealthPercent();
        if (drainHealthCoroutine != null)
        {
            StopCoroutine(drainHealthCoroutine);
        }

        drainHealthCoroutine = StartCoroutine(DrainHealth());
    }

    public void HealCharacter(float heal)
    {
        health += heal;
        CheckOverheal();
        SetHealthUI();
    }

    public void CheckOverheal()
    {
        if(health > maxHealth)
        {
            health = maxHealth;
        }
        SetHealthUI();
    }

    private void CheckDeath()
    {
        if (health <= 0)
        {
            health = 0;
            Destroy(player);
            StartCoroutine(WaitForAudioAndQuit());
        }
    }

    private IEnumerator WaitForAudioAndQuit()
    {
        audioSource.PlayOneShot(deathSound);
        yield return new WaitForSeconds(2);
        Application.Quit();
    }


    private IEnumerator DrainHealth()
    {
        float fillAmount = healthSlider.value;
        float elapsedTime = 0f;
        while (elapsedTime < drainTime)
        {
            elapsedTime += Time.deltaTime;
            healthSlider.value = Mathf.Lerp(fillAmount, target, elapsedTime / drainTime);
            UpdateHealthBarColor();
            yield return null;
        }
    }

    public void SetHealthUI()
    {
        healthSlider.value = CalculateHealthPercent();
        healthText.text = Mathf.Ceil(health).ToString() + " / " + Mathf.Ceil(maxHealth).ToString();
    }

    private void UpdateHealthBarColor()
    {
        float healthPercent = CalculateHealthPercent();
        Color newColor = healthGradient.Evaluate(healthPercent);
        fillImage.color = newColor;
    }

    float CalculateHealthPercent()
    {
        return health / maxHealth;
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        coinValue.text = "COINS: " + coins.ToString();
    }
}
