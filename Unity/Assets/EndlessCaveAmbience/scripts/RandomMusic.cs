using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class RandomMusic : MonoBehaviour {

	[Tooltip("Immediately start the music when the object is loaded. If false, StartMusic() will need to be called instead.")]
	public bool playOnAwake=false;

	public AudioSource bed;
	public AudioSource melody;
	public AudioSource perc;
	public AudioSource fx;

	public AudioMixerSnapshot volUp;
	public AudioMixerSnapshot volDown;
	[Tooltip("Time, in seconds to fade in the music on Start. Set to 0 for immediate playback.")]
	public float fadeInDuration=2f;
	[Tooltip("Time, in seconds to fade out the music on Stop. Set to 0 for an immediate stop.")]
	public float fadeOutDuration=3f;

	public AudioClip[] melodyArray;
	public AudioClip[] percArray;
	public AudioClip[] fxArray;

	[Tooltip("Tempo in beats per minute (default: 95), only required if using your own music")]
	public int bpm=95;
	[Tooltip("Time signature in beats per bar (default: 4), only required if using your own music")]
	public int beatsPerBar=4;
	[Tooltip("How frequently, in whole bars, that cues can be triggered (default: 2)")]
	public int barsPerTrigger=2;

	[Tooltip("Percent chance that a melody cue wil play at the trigger point")]
	public int melodyChance=60;
	[Tooltip("Percent chance that a percussion cue wil play at the trigger point")]
	public int percChance=40;
	[Tooltip("Percent chance that an FX cue wil play at the trigger point")]
	public int fxChance=20;

	float timer;
	float triggerTime;
	bool musicPlaying=false;

	void Start () 
	{
		triggerTime = 60f / bpm * beatsPerBar * barsPerTrigger;
		if (playOnAwake ==true)
			StartMusic();
	}
		
	void Update () 
	{
		if (musicPlaying==true)
		{
			timer += Time.deltaTime;
			if (timer > triggerTime)
			{
				PlayClips();
			}
		}
	}

	public void StartMusic()
	{
		if (musicPlaying==false)
		{
			StopAllCoroutines();
			timer=0f;
			volUp.TransitionTo(fadeInDuration);
			bed.Play();
			musicPlaying=true;
		}
	}

	public void StopMusic ()
	{
		volDown.TransitionTo(fadeOutDuration);
		StartCoroutine(WaitToStop());
	}

	void PlayClips()
	{
		timer=0f;
		int melodyRoll=Random.Range(0,100);
		int percRoll=Random.Range(0,100);
		int fxRoll=Random.Range(0,100);

		if (melodyChance > melodyRoll && !melody.isPlaying)
		{
			int melodyIndex=Random.Range(0,melodyArray.Length);
			melody.clip = melodyArray[melodyIndex];
			melody.Play();
		}

		if (percChance > percRoll && !perc.isPlaying)
		{
			int percIndex=Random.Range(0,percArray.Length);
			perc.clip = percArray[percIndex];
			perc.Play();
		}

		if (fxChance > fxRoll && !fx.isPlaying)
		{
			int fxIndex = Random.Range(0,fxArray.Length);
			fx.clip = fxArray[fxIndex];
			fx.Play();
		}
	}

	IEnumerator WaitToStop()
	{
		yield return new WaitForSeconds(fadeOutDuration);
		bed.Stop();
		melody.Stop();
		perc.Stop();
		fx.Stop();
		musicPlaying=false;
		StopAllCoroutines();
	}
}
