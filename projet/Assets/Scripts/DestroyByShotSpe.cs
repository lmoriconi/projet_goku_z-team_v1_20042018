using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByShotSpe : MonoBehaviour {

	//si colision alors...
		void OnTriggerEnter (Collider other)
		{
			//... si l'objet est la zone de jeu, le player ou la killzone alors...
				if (other.CompareTag ("ZoneDeJeu") || other.CompareTag ("Player") || other.CompareTag ("KillZone"))
				{
					//on ne fait rien
						return;
				}
			
			//sinon on détruit l'objet colisionné
				Destroy (other.gameObject);
		}
}
