using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PickUpMutationCurrency : MonoBehaviour {

    [SerializeField] GameObject gus;
    [SerializeField] GameObject mutationManager;
    [SerializeField] GameObject graficsObject1;
    [SerializeField] GameObject graficsObject2;
    [SerializeField] GameObject graficsObject3;
    [SerializeField] GameObject graficsObject4;

    [SerializeField] AudioClip pickUpSFX;
    bool pickedUp = false;
    float scaleTime = 0;
    bool scaling = false;
    float audioLenght = 0;
    
	void Update () {

		scaleTime += Time.deltaTime * audioLenght;
        graficsObject1.transform.localScale = Vector3.Lerp(graficsObject1.transform.localScale, new Vector3(0, 0, 0), scaleTime);
        graficsObject2.transform.localScale = Vector3.Lerp(graficsObject2.transform.localScale, new Vector3(0, 0, 0), scaleTime);
        graficsObject3.transform.localScale = Vector3.Lerp(graficsObject3.transform.localScale, new Vector3(0.2f, 0.2f, 0.2f), scaleTime);
        graficsObject4.transform.localScale = Vector3.Lerp(graficsObject4.transform.localScale, new Vector3(0.5f, 0.5f, 0.5f), scaleTime);


        if (scaleTime > 1)
        {
            scaling = false;
        }
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == gus && pickedUp == false)
        {
            mutationManager.GetComponent<MutationManager>().PickUpMutationCurrency();
            pickedUp = true;
            StartCoroutine(PickUp());
        }
    }
    IEnumerator PickUp()
    {
        AudioSource audio = GetComponent<AudioSource>();

        audio.clip = pickUpSFX;
        audioLenght = audio.clip.length;
        scaling = true;
        audio.Play();

        yield return new WaitForSeconds(audio.clip.length);
        Destroy(this.gameObject);
    }
}
