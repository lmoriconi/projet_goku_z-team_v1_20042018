using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrolBG : MonoBehaviour {

	//vitesse de scroling du background
		public float scrollSpeed;
	//taille du background
		public float tileSizeZ;
	//position de départ du scroling
		private Vector3 startPosition;

    void Start () 
    {
		//initialisation de la position de départ du scroling
			startPosition = transform.position;
    }
    
    void Update ()
    {
		//nouvelle position du background
			float newPosition = Mathf.Repeat (Time.time * scrollSpeed, tileSizeZ);
		//modification de la position du background
			transform.position = startPosition + Vector3.forward * newPosition;
    }
	
}
