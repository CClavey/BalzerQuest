using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int pickupQuantity;
    public enum PickupObject { COIN };
    public PickupObject currentObject;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            if (currentObject == PickupObject.COIN)
            {
                PlayerStats.playerStats.AddCoins(pickupQuantity);
            }
            Destroy(gameObject);
        }
    }
}
