using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour
{
	[Header("Ability 1")]
	public Image abilityImage1;
	public float cooldown1 = 5;
	bool isOnCooldown = false;
	public KeyCode ability1;

	Vector3 position;
	public Canvas ability1Canvas;
	public Image skillshot;
	public Transform player;

	[Header("Ability 2")]
	public Image abilityImage2;
	public float cooldown2 = 3.33f;
	bool isOnCooldown2 = false;
	public KeyCode ability2;

	public Image targetCircle;
	public Image indicatorRangeCircle;
	public Canvas ability2Canvas;
	private Vector3 posUp;
	public float maxAbility2Distance;


	[Header("Ability 3")]
	public Image abilityImage3;
	public float cooldown3 = 10;
	bool isOnCooldown3 = false;
	public KeyCode ability3;

	// Start is called before the first frame update
	void Start()
	{
		abilityImage1.fillAmount = 0;
		abilityImage2.fillAmount = 0;
		abilityImage3.fillAmount = 0;

		skillshot.GetComponent<Image>().enabled = false;
		targetCircle.GetComponent<Image>().enabled = false;
		indicatorRangeCircle.GetComponent<Image>().enabled = false;
	}

	// Update is called once per frame
	void Update()
	{
		Ability1();
		Ability2();
		Ability3();

		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if(Physics.Raycast(ray, out hit, Mathf.Infinity))
		{
			position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
		}

		if(Physics.Raycast(ray, out hit, Mathf.Infinity))
		{
			if(hit.collider.gameObject != this.gameObject)
			{
				posUp = new Vector3(hit.point.x, 10f, hit.point.z);
				position = hit.point;
			}
		}

		Quaternion transRot = Quaternion.LookRotation(position - player.transform.position);
		ability1Canvas.transform.rotation = Quaternion.Lerp(transRot, ability1Canvas.transform.rotation, 0f);

		var hitPosDir = (hit.point - transform.position).normalized;
		float distance = Vector3.Distance(hit.point, transform.position);
		distance = Mathf.Min(distance, maxAbility2Distance);

		var newHitPos = transform.position + hitPosDir * distance;
		ability2Canvas.transform.position = (newHitPos);
	}

	void Ability1()
	{
		if(Input.GetKey(ability1) && isOnCooldown == false)
		{
			skillshot.GetComponent<Image>().enabled = true;

			targetCircle.GetComponent<Image>().enabled = false;
			indicatorRangeCircle.GetComponent<Image>().enabled = false;
		}

		if (skillshot.GetComponent<Image>().enabled == true && Input.GetMouseButtonDown(0) && !isOnCooldown)
		{

			isOnCooldown = true;
			abilityImage1.fillAmount = 1;
		}

		if (isOnCooldown)
		{
			abilityImage1.fillAmount -= 1 / cooldown1 * Time.deltaTime;
			skillshot.GetComponent<Image>().enabled = false;

			if (abilityImage1.fillAmount <= 0)
			{
				abilityImage1.fillAmount = 0;
				isOnCooldown = false;
			}
		}
	}

	void Ability2()
	{
		if (Input.GetKey(ability2) && isOnCooldown2 == false)
		{
			skillshot.GetComponent<Image>().enabled = false;

			targetCircle.GetComponent<Image>().enabled = true;
			indicatorRangeCircle.GetComponent<Image>().enabled = true;
		}
		if(targetCircle.GetComponent<Image>().enabled == true && indicatorRangeCircle.GetComponent<Image>().enabled == true && Input.GetMouseButtonDown(0) && !isOnCooldown2)
		{
			isOnCooldown2 = true;
			abilityImage2.fillAmount = 1;
		}

		if (isOnCooldown2)
		{
			abilityImage2.fillAmount -= 1 / cooldown2 * Time.deltaTime;
			targetCircle.GetComponent<Image>().enabled = false;
			indicatorRangeCircle.GetComponent<Image>().enabled = false;

			if (abilityImage2.fillAmount <= 0)
			{
				abilityImage2.fillAmount = 0;
				isOnCooldown2 = false;
			}
		}
	}

	void Ability3()
	{
		if (Input.GetKey(ability3) && isOnCooldown3 == false)
		{
			isOnCooldown3 = true;
			abilityImage3.fillAmount = 1;

		}

		if (isOnCooldown3)
		{
			abilityImage3.fillAmount -= 1 / cooldown3 * Time.deltaTime;

			if (abilityImage3.fillAmount <= 0)
			{
				abilityImage3.fillAmount = 0;
				isOnCooldown3 = false;
			}
		}
	}
}
