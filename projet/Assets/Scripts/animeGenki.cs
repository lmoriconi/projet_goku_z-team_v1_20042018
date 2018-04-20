using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animeGenki : MonoBehaviour {

	//matériaux utilisé pour l'animation du Genki
		public Material Genki1;
		public Material Genki2;
	//Sprite du shot
		public GameObject SpriteShotSpeNiv3;
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
				Tab=SpriteShotSpeNiv3.GetComponent<MeshRenderer>().materials;
			//initialisation de l'étape de l'animation
				test=2;
			//initialisation du temps d'attente
				attenteanime=0.1F;
		}
	
	void Update () {
		//si le shot en est à l'étape 4 alors...
			if (test==2 && Time.time > nextsprit){
				//...modification du sprite
					Tab[0]=Genki1;
					SpriteShotSpeNiv3.GetComponent<MeshRenderer>().materials=Tab;
				//modification de l'étape
					test=1;
				//modification du temps
					nextsprit = Time.time + attenteanime;
		//si le shot en est à l'étape 3 alors...
			}else if (test==1 && Time.time > nextsprit){
				//...modification du sprite
					Tab[0]=Genki2;
					SpriteShotSpeNiv3.GetComponent<MeshRenderer>().materials=Tab;
				//modification de l'étape
					test=2;
				//modification du temps
					nextsprit = Time.time + attenteanime;
			}
		
	}
}
