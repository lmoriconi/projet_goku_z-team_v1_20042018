using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{
    public int scoreValue;                  //number of points gained whenever the player destroys an enemy
    private GameController gameController;   //holds a reference to the instance of our gameController, allows us to call the "AddScore" function when an enemy is destroyed
	public GameObject Cible;
	//taille maximum des barres de ki
		public float TailleBar;
	//position des barres de ki
		private Vector3 RefPoint;
	//barres de ki
		private GameObject Ki1;
		private GameObject Ki2;
		private GameObject Ki3;
    
	void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");     //each instance of enemy will find its reference to our Game Controller script
        //if we found a reference to our gameController object
        if (gameControllerObject != null)
            gameController = gameControllerObject.GetComponent<GameController>();       //set the reference to the script held in the Game Controller Object
        //if we haven't find it
        if (gameController == null){
            Debug.Log("Cannot find 'GameController' script");
		}
		//initialisation des barres de ki
			Ki1=GameObject.Find("KiNiv1");
			Ki2=GameObject.Find("KiNiv2");
			Ki3=GameObject.Find("KiNiv3");
		//initialisation de la position des barres de ki
			RefPoint=new Vector3(-6F, -1F, -4F);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ZoneDeJeu" || other.tag=="Ennemi" || other.tag == "Player" || other.tag == "ShotEnemy")
        {
            return;
        }
        gameController.AddScore(scoreValue);
		if (other.tag != "ShotSpe"){
			Destroy(other.gameObject);  //destroy the bolt
		}
        Destroy(gameObject);        //destroy the enemy
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