using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class SoundController : MonoBehaviour {

    // Use this for initialization
    public AudioMixerSnapshot outOfCombat;
    public AudioMixerSnapshot inCombat;

    public AudioClip[] stings;
    public AudioSource stingSource;
    public float bpm = 128f;

    private float m_transitionIn;
    private float m_transitionOut;
    private float m_QuaterNote;

    void Start () {

        m_QuaterNote = 60 / bpm;
        m_transitionIn = m_QuaterNote;
        m_transitionOut = m_QuaterNote * 32;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CombatStart()
    {
        Debug.Log("CombatStart");
        inCombat.TransitionTo(m_transitionIn);
        PlaySting();
    }

    public void CombatOut()
    {
        Debug.Log("CombatOut");
        outOfCombat.TransitionTo(m_transitionOut);
    }

    void PlaySting()
    {
        AudioClip currentaudio = stings[Random.Range(0, stings.Length)];
        stingSource.PlayOneShot(currentaudio);
    }
}
