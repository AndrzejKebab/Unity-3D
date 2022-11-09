using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public GameObject heartContainer;
    private float fillValue;

    // Update is called once per frame
    void Update()
    {
        fillValue = (float)GameManager.Health;
        fillValue = fillValue / GameManager.MaxHealth;
        heartContainer.GetComponent<Image>().fillAmount = fillValue;
    }
}
