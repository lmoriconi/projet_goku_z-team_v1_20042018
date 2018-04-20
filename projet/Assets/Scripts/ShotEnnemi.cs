using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotEnnemi : MonoBehaviour {

	public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    public float delay;

    private AudioSource audioSource;

    void Start ()
    {
		InvokeRepeating ("Fire", delay, fireRate);
    }

    void Fire ()
    {
		if(this.GetComponent<Transform>().position.z <= 4)
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
    }
}
