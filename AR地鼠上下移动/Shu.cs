using UnityEngine;
using System.Collections;

public class Shu : MonoBehaviour
{
	private float Speed = 5f;

	private float time;

	// Use this for initialization
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		print(Time.deltaTime);
		time                    += Time.deltaTime;
		time                    =  time + Time.deltaTime;
		transform.localPosition += new Vector3(0, 0.1f, 0) * Speed * Time.deltaTime;
		if (transform.localPosition.y > 0.15f)
		{
			transform.localPosition = new Vector3(-0.1683633f, 0.15f, -0.2573629f);
		}

		if (time > 1f)
		{
			StartCoroutine(Deng());
		}
		else if (time < 0)
		{
			transform.localPosition += new Vector3(0, -0.2f, 0) * Speed * Time.deltaTime;
		}
	}

	IEnumerator Deng()
	{
		yield return new WaitForSeconds(0.5f);
		time = -0.5f;
	}
}