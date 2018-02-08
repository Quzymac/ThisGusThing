using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour {

    [SerializeField] AudioClip victorySound;
    bool win = false;


    private void OnTriggerEnter(Collider other)
    {
        if (!win)
        {
            StartCoroutine(Win());
            win = true;
        }
    }
    
    IEnumerator Win()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.PlayOneShot(victorySound);
        yield return new WaitForSeconds(victorySound.length);
        print("f");
        SceneManager.LoadScene("EndCredits");

    }
}
