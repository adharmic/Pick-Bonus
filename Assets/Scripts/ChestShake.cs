using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChestShake : MonoBehaviour
{


	private Vector3 startingPos;
	private Vector3 startingRot;
	private Vector3 originPosition;
	private Quaternion originRotation;

	private bool shook = false;
	// private bool isShaking = false;
	public float shake_decay = 0f;
	public float shake_intensity = 0f;

	private float temp_shake_intensity = 0;

	public TextMeshProUGUI winvalue;
	
	void Update (){
		if (temp_shake_intensity > 0){
			transform.position = originPosition + Random.insideUnitSphere * temp_shake_intensity * .5f;
			transform.rotation = new Quaternion(
				originRotation.x + Random.Range (-temp_shake_intensity,temp_shake_intensity) * .000005f,
				originRotation.y + Random.Range (-temp_shake_intensity,temp_shake_intensity) * .05f,
				originRotation.z + Random.Range (-temp_shake_intensity,temp_shake_intensity) * .000005f,
				originRotation.w + Random.Range (-temp_shake_intensity,temp_shake_intensity) * .000005f);
			temp_shake_intensity -= shake_decay;
		}
		else if(shook && gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Closed")) {
			gameObject.GetComponent<LootBox>().Open();
			winvalue.GetComponent<VisibilityToggle>().Toggle();
			shook = false;
		}
	}
	
	public void Shake(){
		gameObject.GetComponent<LootBox>().BounceBox(false);
		originPosition = transform.position;
		originRotation = transform.rotation;
		temp_shake_intensity = shake_intensity;
		shook = true;
		// if (isShaking) {
		// }
	}
}
