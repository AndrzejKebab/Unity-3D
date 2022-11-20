using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
	public Slider playerSlider3D;
	Slider playerSlider2D;

	public int Health;

	// Start is called before the first frame update
	void Start()
	{
		playerSlider2D = GetComponent<Slider>();

		playerSlider2D.maxValue = Health;
		playerSlider3D.maxValue = Health;
	}

	// Update is called once per frame
	void Update()
	{
		playerSlider2D.value = Health;
		playerSlider3D.value = playerSlider2D.value;

		if(Health <= 0) Health = 0;
	}
}
