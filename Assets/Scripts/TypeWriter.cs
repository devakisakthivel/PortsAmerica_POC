using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
[RequireComponent(typeof(AudioSource))]
public class TypeWriter : MonoBehaviour {

	public string msg = "";
	private Text textComp;
	public float startDelay = 2f;
	public float typeDelay = 0.01f;
	public AudioClip narratorClip;
    public bool EnableTypeWriterEffect = true;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine (TypeIn ());
	}
	
	// Update is called once per frame
	void Awake ()
	{
		textComp = GetComponent<Text> ();
	}

	public IEnumerator TypeIn()
	{
		yield return new WaitForSeconds(startDelay);
        if (EnableTypeWriterEffect != false)
        {
            GetComponent<AudioSource>().clip = narratorClip;
            GetComponent<AudioSource>().Play();
            for (int i = 0; i < msg.Length; i++)
            {
                textComp.text = msg.Substring(0, i + 1);
                yield return new WaitForSeconds(typeDelay);
            }
            GetComponent<AudioSource>().Stop();
        }
        else
        {
            textComp.text = msg;
        }
	}	
}	
