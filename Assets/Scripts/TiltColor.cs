using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TiltColor : MonoBehaviour {

	Image selfRef;

	private void Awake() {
		selfRef = GetComponent<Image>();
		Input.gyro.enabled = true;
	}

	private void Update() {

		print(Input.gyro.attitude.eulerAngles.x + " " + Input.gyro.attitude.eulerAngles.y + " " + Input.gyro.attitude.eulerAngles.z);

		if (Input.gyro.attitude.eulerAngles.z > 180 && Input.gyro.attitude.eulerAngles.z < 360)
		{
			selfRef.color = Color.red;
		} else
		{
			selfRef.color = Color.cyan;
		}

	}

	float SuperLerp (float from,  float to, float from2, float to2, float value) {
    if (value <= from2)
        return from;
    else if (value >= to2)
        return to;
    return (to - from) * ((value - from2) / (to2 - from2)) + from;
}
}
