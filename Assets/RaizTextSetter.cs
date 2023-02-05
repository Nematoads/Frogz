using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaizTextSetter : MonoBehaviour
{

    public Text healthText;

    private void Awake()
    {
        EventBroker.setRaizHealth += setHealth;
    }

    public void setHealth(int health)
    {
        healthText.text = health.ToString();
    }

    private void OnDestroy()
    {
        EventBroker.setRaizHealth -= setHealth;
    }
}
