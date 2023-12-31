using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image healthImage;
    private float targetHealth = 1f;
    private float changeSpeed = 2f;
    void Start()
    {
        healthImage = gameObject.transform.Find("Health").GetComponent<Image>();
    }


    private void Update()
    {
        healthImage.fillAmount = Mathf.Lerp(healthImage.fillAmount, targetHealth, changeSpeed * Time.deltaTime);

        
    }


    public void AdjustHealth(float percentage)
    {
        //healthImage.fillAmount = percentage;
        targetHealth = percentage;
    }
}
