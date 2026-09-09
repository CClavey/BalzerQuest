using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopScript : MonoBehaviour
{
    public GameObject ShopMenu;
    public GameObject player;
    private TestAttack currentTestAttack;
    private TestAttack newTestAttack;

    void Start()
    {
        // Assume you have a way to differentiate between the two TestAttack scripts, 
        // perhaps by their order or a tag. Here, I'm just assuming the first and second components.
        var testAttacks = player.GetComponents<TestAttack>();
        if (testAttacks.Length >= 2)
        {
            currentTestAttack = testAttacks[0];
            newTestAttack = testAttacks[1];
        }
    }

    void Update()
    {
        HandleInput();
    }

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("TRYING TO TOGGLE");
            ToggleShop();
        }
        if (ShopMenu.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                BuyHealth();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                BuyWeapon();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                BuyHeal();
            }
        }
    }

    private void ToggleShop()
    {
        ShopMenu.SetActive(!ShopMenu.activeSelf);
    }

    private void BuyHeal()
    {
        Debug.Log("Option 2 selected");
        if (PlayerStats.playerStats.coins >= 20)
        {
            PlayerStats.playerStats.coins -= 20;
            PlayerStats.playerStats.HealCharacter(30);
            PlayerStats.playerStats.coinValue.text = "COINS: " + PlayerStats.playerStats.coins.ToString();
            Debug.Log("Option 2 purchased");
        }
    }

    private void BuyWeapon()
    {
        Debug.Log("Option 3 selected");
        if (PlayerStats.playerStats.coins >= 200)
        {
            PlayerStats.playerStats.coins -= 200;
            PlayerStats.playerStats.coinValue.text = "COINS: " + PlayerStats.playerStats.coins.ToString();

            // Disable the current TestAttack and enable the new TestAttack
            if (currentTestAttack != null) currentTestAttack.enabled = false;
            if (newTestAttack != null) newTestAttack.enabled = true;

            Debug.Log("Option 3 purchased and TestAttack switched");
        }
    }

    private void BuyHealth()
    {
        Debug.Log("Option 1 selected");
        if (PlayerStats.playerStats.coins >= 100)
        {
            PlayerStats.playerStats.coins -= 100;
            PlayerStats.playerStats.maxHealth += 50;
            PlayerStats.playerStats.health = PlayerStats.playerStats.maxHealth;
            PlayerStats.playerStats.SetHealthUI();
            PlayerStats.playerStats.coinValue.text = "COINS: " + PlayerStats.playerStats.coins.ToString();
            Debug.Log("Option 1 purchased");
        }
    }
}
