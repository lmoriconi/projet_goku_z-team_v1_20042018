using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortieDeJeu : MonoBehaviour {
	
	//si un objet sort de la zone de jeu alors...
		void OnTriggerExit(Collider other)
		{
			//... il est détruit
				Destroy(other.gameObject);
		}
	
}
