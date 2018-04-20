using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animeshot : MonoBehaviour {

	//matériaux utilisé pour l'animation du shot
		public Material shot1;
		public Material shot2;
		public Material shot3;
		public Material shot4;
	//Sprite du shot
		public GameObject SpriteShot;
	//matériel actuel du shot
		private Material[] Tab;
	//sert à savoir à quel moment de l'animation le shot en est
		private int test;
	//délai entre chaque changement de sprite
		private float attenteanime;
		private float nextsprit;
	
	//initialisation des variables
		void Start () {
			//initialisation du materiel actuel du shot
				Tab=SpriteShot.GetComponent<MeshRenderer>().materials;
			//initialisation de l'étape de l'animation
				test=4;
			//initialisation du temps d'attente
				attenteanime=0.1F;
		}
	
	void Update () {
		//si le shot en est à l'étape 4 alors...
			if (test==4 && Time.time > nextsprit){
				//...modification du sprite
					Tab[0]=shot1;
					SpriteShot.GetComponent<MeshRenderer>().materials=Tab;
				//modification de l'étape
					test=1;
				//modification du temps
					nextsprit = Time.time + attenteanime;
		//si le shot en est à l'étape 3 alors...
			}else if (test==3 && Time.time > nextsprit){
				//...modification du sprite
					Tab[0]=shot2;
					SpriteShot.GetComponent<MeshRenderer>().materials=Tab;
				//modification de l'étape
					test=4;
				//modification du temps
					nextsprit = Time.time + attenteanime;
		//si le shot en est à l'étape 2 alors...
			}else if (test==2 && Time.time > nextsprit){
				//...modification du sprite
					Tab[0]=shot3;
					SpriteShot.GetComponent<MeshRenderer>().materials=Tab;
				//modification de l'étape
					test=3;
				//modification du temps
					nextsprit = Time.time + attenteanime;
		//si le shot en est à l'étape 1 alors...
			}else if (test==1 && Time.time > nextsprit){
				//...modification du sprite
					Tab[0]=shot4;
					SpriteShot.GetComponent<MeshRenderer>().materials=Tab;
				//modification de l'étape
					test=2;
				//modification du temps
					nextsprit = Time.time + attenteanime;
			}
		
	}
}
