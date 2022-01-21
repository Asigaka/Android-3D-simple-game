using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;

    [Header("UI")]
    [SerializeField] private float healthPanelShowingTime = 3;
    [SerializeField] private GameObject healthPanel;
    [SerializeField] private Slider healthSlider;

    public static PlayerHealth Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance);

        Instance = this;
    }

    private void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        healthPanel.SetActive(false);
    }

    public void DamagePlayer(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
            OnPlayerDie();

        StartCoroutine(ShowHealthPanel());
    }

    public void HealPlayer(float heal)
    {
        currentHealth += heal;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        StartCoroutine(ShowHealthPanel());
    }

    private IEnumerator ShowHealthPanel()
    {
        healthSlider.value = currentHealth;
        healthPanel.SetActive(true);
        yield return new WaitForSeconds(healthPanelShowingTime);
        healthPanel.SetActive(false);
    }

    public void OnPlayerDie()
    {
        Debug.Log("Игрок умер");
    }
}
