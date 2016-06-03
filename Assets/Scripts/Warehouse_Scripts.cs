using UnityEngine;
using System.Collections;

public class Warehouse_Scripts : MonoBehaviour {

    [Header("Box Objects")]
    public Rigidbody Box1;
    public Rigidbody Box2, Box3, Box4, BigBox1, BigBox2;

    [Header("Audio Sources")]
    public AudioSource Narrator_Aud;
    public AudioSource WoodenBoxes_Aud, FemaleWorker_Aud;

    [Header("Animators")]
    public Animator Chuck_Anim;
    public Animator PPE_Anim;

	void Awake()
	{
	}

	void Start() 
	{
	}
	
	void Update()
	{
	}

    public void WorkersKneel()
    {
        Chuck_Anim.SetBool("KneelDown", true);
        PPE_Anim.SetBool("KneelDown", true);
    }

    public void PlayNarrator()
    {
        Narrator_Aud.Play();
    }

    public void AddForce()
    {
        //Play female worker ouch sound
        FemaleWorker_Aud.Play();

        //Remove freeze rotation from the boxes
        Box1.freezeRotation = false;
        Box2.freezeRotation = false;
        Box3.freezeRotation = false;
        Box4.freezeRotation = false;


        //Add force to the boxes
        Box1.AddForce(new Vector3(-400, 0, 0), ForceMode.Impulse);
        Box2.AddForce(new Vector3(-700, 0, 0), ForceMode.Impulse);
        Box3.AddForce(new Vector3(-700, 0, 0), ForceMode.Impulse);
        Box4.AddForce(new Vector3(-400, 0, 0), ForceMode.Impulse);

        BigBox1.AddForce(new Vector3(0, -200, 0), ForceMode.Impulse);
        BigBox2.AddForce(new Vector3(0, -200, 0), ForceMode.Impulse);

        //Stop box from moving around
        StartCoroutine(FreezeBox());
        
        //Play Wooden Boxes Falling Audio
        WoodenBoxes_Aud.Play();
    }

    IEnumerator FreezeBox()
    {
        yield return new WaitForSeconds(2f);
        Box3.freezeRotation = true;
        
    }


}
