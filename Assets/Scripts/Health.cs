using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Health : NetworkBehaviour
{
    public const int maxHealth = 100;
    [SyncVar(hook = "OnChangeHealth")] public int currentHealth = maxHealth;
    public RectTransform healthbar;

    public void TakeDamage(int amount)
    {
        if (!isServer)
        { return; }

        currentHealth -= amount;
        if (currentHealth < 0)
        {
            currentHealth = 0;
            Debug.Log("Dead!");
            gameObject.SetActive(false);
        }

    }
    void OnChangeHealth(int health)
    {
        healthbar.sizeDelta = new Vector2(health * 2, healthbar.sizeDelta.y);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
