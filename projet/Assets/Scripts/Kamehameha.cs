using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamehameha : MonoBehaviour {
	
	//le kamehameha
		public GameObject Cible;
	//le player
		private GameObject Perso;
	//la position de spawn du kamehameha
		private Transform spawnPoint;
	
	void Start(){
		//initialisation du player
			Perso=GameObject.Find("Player");
	}
	
	//si colision alors...
		void OnTriggerEnter (Collider other)
		{
			//... si l'objet est la killzone alors...
				if (other.CompareTag ("KillZone"))
				{
					//... destruction du kamehameha
						Destroy(Cible);
				}
		}
	
	void Update ()
    {
		//si le kamehameha existe alors...
			if (Cible){
				//récupération du shotspawn
					spawnPoint=Perso.GetComponent<Transform>();
				//modification du kamehameha
					Cible.GetComponent<Transform>().localScale = Cible.GetComponent<Transform>().localScale+new Vector3(0, 0, 0.1F);
				//modification de la position du kamehameha
					transform.position = spawnPoint.position-new Vector3(0, 0, Cible.GetComponent<Transform>().localScale.z/2F);
			}
    }
}
