using UnityEngine;
using System.Collections;

public class Office_Scripts : MonoBehaviour {

    [Header("Audio Sources")]
    public AudioSource Narration3_Aud;
    public AudioSource Narration4_Aud, Narration5_Aud;

    [Header("Animators")]
    public Animator WhiteboardText_Anim;

    [Header("Movie Texture")]
    public Material MovieTexture;

    private MovieTexture SecurityFootage;

	void Awake()
	{
	}

	void Start()
    { 
        // set the movie texture to a variable
        SecurityFootage = MovieTexture.mainTexture as MovieTexture;
	}
	
	void Update()
	{
	}

    public void PlayNarration3()
    {
        Narration3_Aud.Play();
    }

    public void PlayNarration4()
    {
        Narration4_Aud.Play();
    }

    public void PlayNarration5()
    {
        Narration5_Aud.Play();
    }

    public void PlayFootage()
    {
        SecurityFootage.Play();
    }

    public void PlayTextAnimation()
    {
        WhiteboardText_Anim.SetBool("Type", true);
    }

}
