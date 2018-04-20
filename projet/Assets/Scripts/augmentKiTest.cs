using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class augmentKiTest : MonoBehaviour {
	
	//taille maximum des barres de ki
		public float TailleBar;
	//position des barres de ki
		private Vector3 RefPoint;
	//barres de ki
		private GameObject Ki1;
		private GameObject Ki2;
		private GameObject Ki3;

	//initialisation des variables
		void Start () {
			//initialisation des barres de ki
				Ki1=GameObject.Find("KiNiv1");
				Ki2=GameObject.Find("KiNiv2");
				Ki3=GameObject.Find("KiNiv3");
			//initialisation de la position des barres de ki
				RefPoint=new Vector3(-10F, -1F, -4F);
		}
	
	void Update () {
		//augmentation des barres de ki
			if (Ki1.GetComponent<Transform>().localScale.x!=TailleBar){
				Ki1.GetComponent<Transform>().localScale = Ki1.GetComponent<Transform>().localScale+new Vector3(0.5F, 0, 0);
				Ki1.GetComponent<Transform>().position = RefPoint+new Vector3(Ki1.GetComponent<Transform>().localScale.x/2F, 0, 0);
				if (Ki1.GetComponent<Transform>().localScale.x==TailleBar){
					Ki1.GetComponent<Transform>().position = RefPoint+new Vector3(Ki1.GetComponent<Transform>().localScale.x/2F, -1.5F, 0);
					Ki2.GetComponent<Transform>().position = RefPoint+new Vector3(Ki2.GetComponent<Transform>().localScale.x/2F, -1F, 0);
				}
			}else if (Ki2.GetComponent<Transform>().localScale.x!=TailleBar){
				Ki2.GetComponent<Transform>().localScale = Ki2.GetComponent<Transform>().localScale+new Vector3(0.5F, 0, 0);
				Ki2.GetComponent<Transform>().position = RefPoint+new Vector3(Ki2.GetComponent<Transform>().localScale.x/2F, 0, 0);
				if (Ki2.GetComponent<Transform>().localScale.x==TailleBar){
					Ki2.GetComponent<Transform>().position = RefPoint+new Vector3(Ki1.GetComponent<Transform>().localScale.x/2F, -1.5F, 0);
					Ki3.GetComponent<Transform>().position = RefPoint+new Vector3(Ki2.GetComponent<Transform>().localScale.x/2F, -1F, 0);
				}
			}else if (Ki3.GetComponent<Transform>().localScale.x!=TailleBar){
				Ki3.GetComponent<Transform>().localScale = Ki3.GetComponent<Transform>().localScale+new Vector3(0.5F, 0, 0);
				Ki3.GetComponent<Transform>().position = RefPoint+new Vector3(Ki3.GetComponent<Transform>().localScale.x/2F, 0, 0);
			}
		
	}
}
