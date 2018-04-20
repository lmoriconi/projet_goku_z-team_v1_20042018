using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

//création de la classe boundary de la zone de jeu
	public class Boundary
	{
		public float xMin, xMax, zMin, zMax;
	}

public class PlayerControler : MonoBehaviour {

	//varible de vie
		public int vie;
		public Renderer rend;
		private bool isCliping;
		private float ClipTime;
		public float WaitClip;
		private float FinClip;
		public float TempClip;
		private GameController gameController;
		
		
	//varibles lié à la vitesse du player
		//vitesse de base
			public float BaseSpeed;
		//facteur du kaioken
			public float kaiokenSpeed;
		//vitesse actuelle
			private float curentSpeed;
	
	//varibles lié au personnage
		//le player
			public GameObject Cible;
		//Sprite du player
			public GameObject SpriteMouv;
		//objet portant l'effet sonor du kaioken
			public GameObject PorteurEffetKaioken;
			
	//varibles de matériaux pour changer le sprite du player
		//Sprite de chaque direction
			public Material CoteG;
			public Material FaceH;
			public Material CoteD;
			public Material FaceB;
			public Material Static;
			public Material PlayerShot;
		//Materiel du player actuel
			private Material[] Tab;
	
	//varibles d'états du player
		private bool isMouveLeft;
		private bool isMouveRight;
		private bool isMouveUp;
		private bool isMouveDown;
		private bool isKaioken;
		
	//Zone de jeu
		public Boundary ZoneDeJeu;
	
	//varibles de son du player
		public AudioClip Genkidama;
		public AudioClip KaiokenVoix;
		public AudioClip KaiokenEffet;
		public AudioClip Dash1;
		public AudioClip Dash2;
		public AudioClip Dash3;
		public AudioClip kamehameha;
		//sert pour le random des sons du dash
			private float hazard;
	
	//varibles des barres de ki
		//taille maximum des barres
			public float TailleBar;
		//barres de ki
			private GameObject Ki1;
			private GameObject Ki2;
			private GameObject Ki3;
		//position des barres de ki
			private Vector3 RefPoint;
	
	//varibles des shot
		//prefabs des shot normaux et spéciaux de niveau 2 et 3
			public GameObject shot;
			public GameObject shotSpeNiv3;
			public GameObject shotSpeNiv2;
		//position de spawn des shot
			public Transform shotSpawn;
		//vitesse de tire de base
			public float BasefireRate;
		//facteur de tire du kaioken
			public float KaiokenFireRate;
		//vitesse actuelle du player
			private float curentFireRate;
		//temps entre chaque tire
			private float nextFire;
	
	//varibles du kaioken utilisé pour laisser un temps entre chaque utilisation de la touche
		private float attenteKaioken;
		private float nextKaioken;
		private float DimKaioken;
		private float TempKaioken;
	
	//initialisation des varibles
		void Start()
		{
			GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
			if (gameControllerObject != null)
			{
				gameController = gameControllerObject.GetComponent <GameController>();
			}
			if (gameController == null)
			{
				Debug.Log ("Cannot find 'GameController' script");
			}
			isCliping=false;
			rend = SpriteMouv.GetComponent<Renderer>();
			rend.enabled = true;
			//récupération du matériel du player
				Tab=SpriteMouv.GetComponent<MeshRenderer>().materials;
				
			//initialisation des varibles d'états du player
				isMouveLeft=false;
				isMouveDown=false;
				isMouveUp=false;
				isMouveRight=false;
				isKaioken=false;
			
			//initialisation des varibles des barres de ki
				Ki1=GameObject.Find("KiNiv1");
				Ki2=GameObject.Find("KiNiv2");
				Ki3=GameObject.Find("KiNiv3");
				RefPoint=new Vector3(-6F, -1F, -4F);
				
			//initialisation des varibles de tire
				curentSpeed=BaseSpeed;
				curentFireRate=BasefireRate;
			
			//initialisation de la varible du kaioken
				attenteKaioken=1;
				TempKaioken=1;
		}

	void Update ()
    {
		if (isCliping && Time.time > ClipTime){
			ClipTime=Time.time+WaitClip;
			if(rend.enabled){
				rend.enabled = false;
			}else{
				rend.enabled = true;
			}
			if(Time.time>=FinClip){
				isCliping=false;
				rend.enabled = true;
			}
			
		}
		//shot normaux
			//si la joueur utilise Fire1 et est autorisé à tirer alors...
				if (Input.GetButton("Fire1") && Time.time > nextFire)
				{
					//...on incrémente la varible d'autorisation de tire
						nextFire = Time.time + curentFireRate;
					//changement du sprite du player
						Tab[0]=PlayerShot;
						SpriteMouv.GetComponent<MeshRenderer>().materials=Tab;
					//et on fait tirer le player
						Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
				}
		
		//shot spéciaux
			//de niveau 3
				//si le joueur utilise Fire2 et que toutes les barres de ki sont pleine alors...
					if ((Input.GetButton("Fire2")) && Ki1.GetComponent<Transform>().localScale.x==TailleBar && 
					Ki2.GetComponent<Transform>().localScale.x==TailleBar && Ki3.GetComponent<Transform>().localScale.x==TailleBar)
					{
						//...on incrémente la varible d'autorisation de tire
							nextFire = Time.time + curentFireRate;
						//on fait tirer le player
							Instantiate(shotSpeNiv3, shotSpawn.position, shotSpawn.rotation);
						//on joue l'effet sonore correspondant
							SpriteMouv.GetComponent<AudioSource>().clip=Genkidama;
							SpriteMouv.GetComponent<AudioSource>().Play();
						//on vide les barres de ki 
							Ki1.GetComponent<Transform>().localScale=new Vector3(0.5F, 0.3F, 1F);
							Ki2.GetComponent<Transform>().localScale=new Vector3(0.5F, 0.3F, 1F);
							Ki3.GetComponent<Transform>().localScale=new Vector3(0.5F, 0.3F, 1F);
						//on modifi les positions des barres de ki
							Ki1.GetComponent<Transform>().position = RefPoint+new Vector3(Ki1.GetComponent<Transform>().localScale.x/2F, -1F, 0);
							Ki3.GetComponent<Transform>().position = RefPoint+new Vector3(Ki2.GetComponent<Transform>().localScale.x/2F, -1.5F, 0);
			//de niveau 2
				//sinon si le joueur utilise Fire2 et que 2 des barres de ki sont pleine alors...
					}else if ((Input.GetButton("Fire2")) && Ki1.GetComponent<Transform>().localScale.x==TailleBar && 
					Ki2.GetComponent<Transform>().localScale.x==TailleBar)
					{
						//...on incrémente la varible d'autorisation de tire
							nextFire = Time.time + curentFireRate;
						//on fait tirer le player
							Instantiate(shotSpeNiv2, shotSpawn.position, shotSpawn.rotation);
						//on joue l'effet sonore correspondant
							SpriteMouv.GetComponent<AudioSource>().clip=kamehameha;
							SpriteMouv.GetComponent<AudioSource>().Play();
						//on enlève 2 barres de ki
							Ki1.GetComponent<Transform>().localScale=Ki3.GetComponent<Transform>().localScale;
							Ki2.GetComponent<Transform>().localScale=new Vector3(0.5F, 0.3F, 1F);
							Ki3.GetComponent<Transform>().localScale=new Vector3(0.5F, 0.3F, 1F);
						//on modifi les positions des barres de ki
							Ki1.GetComponent<Transform>().position = RefPoint+new Vector3(Ki1.GetComponent<Transform>().localScale.x/2F, -1F, 0);
							Ki2.GetComponent<Transform>().position = RefPoint+new Vector3(Ki2.GetComponent<Transform>().localScale.x/2F, -1.5F, 0);
							Ki3.GetComponent<Transform>().position = RefPoint+new Vector3(Ki2.GetComponent<Transform>().localScale.x/2F, -1.5F, 0);
					}
		
		//Kaioken
			//si le joueur utilise Fire3 que le player a du ki et qu'il est autorisé à utiliser le kaioken alors...
				if ((Input.GetButton("Fire3")) && Ki1.GetComponent<Transform>().localScale.x>0.5F && Time.time > nextKaioken){
					//...si le player n'est pas en kaioken alors...
						if (!isKaioken){
							//... on incrémente la varible d'autorisation
								nextKaioken = Time.time + attenteKaioken;
							//on joue les effet sonore correspondant
								SpriteMouv.GetComponent<AudioSource>().clip=KaiokenVoix;
								SpriteMouv.GetComponent<AudioSource>().Play();
								PorteurEffetKaioken.GetComponent<AudioSource>().clip=KaiokenEffet;
								PorteurEffetKaioken.GetComponent<AudioSource>().Play();
							//on modifi la vitesse du player
								curentSpeed=BaseSpeed*kaiokenSpeed;
							//on modifi la vitesse de tire du player
								curentFireRate=BasefireRate*KaiokenFireRate;
							//on modifi l'état du player
								isKaioken=true;
					//si il est déjà en kaioken...
						}else {
							//... on incrémente la varible d'autorisation
								nextKaioken = Time.time + attenteKaioken;
							//on réinitialise la vitesse du player
								curentSpeed=BaseSpeed;
							//on réinitialise la vitesse de tire du player
								curentFireRate=BasefireRate;
							//on change l'état du player
								isKaioken=false;
						}
				}
			
			//si le joueur est en kaioken alors...
				if (isKaioken && Time.time > DimKaioken){
						DimKaioken = Time.time + TempKaioken;
					//on diminue les barres de ki
						if(Ki3.GetComponent<Transform>().localScale.x>0.5F){
							Ki3.GetComponent<Transform>().localScale = Ki3.GetComponent<Transform>().localScale-new Vector3(0.5F, 0, 0);
							Ki3.GetComponent<Transform>().position = RefPoint+new Vector3(Ki3.GetComponent<Transform>().localScale.x/2F, 0, 0);
							if (Ki3.GetComponent<Transform>().localScale.x==0.5F){
								Ki3.GetComponent<Transform>().position = RefPoint+new Vector3(Ki3.GetComponent<Transform>().localScale.x/2F, -1.5F, 0);
								Ki2.GetComponent<Transform>().position = RefPoint+new Vector3(Ki2.GetComponent<Transform>().localScale.x/2F, -1F, 0);
							}
						}else if(Ki2.GetComponent<Transform>().localScale.x>0.5F){
							Ki2.GetComponent<Transform>().localScale = Ki2.GetComponent<Transform>().localScale-new Vector3(0.5F, 0, 0);
							Ki2.GetComponent<Transform>().position = RefPoint+new Vector3(Ki2.GetComponent<Transform>().localScale.x/2F, 0, 0);
							if (Ki2.GetComponent<Transform>().localScale.x==0.5F){
								Ki2.GetComponent<Transform>().position = RefPoint+new Vector3(Ki2.GetComponent<Transform>().localScale.x/2F, -1.5F, 0);
								Ki1.GetComponent<Transform>().position = RefPoint+new Vector3(Ki1.GetComponent<Transform>().localScale.x/2F, -1F, 0);
							}
						}else if(Ki1.GetComponent<Transform>().localScale.x>0.5F){
							Ki1.GetComponent<Transform>().localScale = Ki1.GetComponent<Transform>().localScale-new Vector3(0.5F, 0, 0);
							Ki1.GetComponent<Transform>().position = RefPoint+new Vector3(Ki1.GetComponent<Transform>().localScale.x/2F, 0, 0);
							if (Ki1.GetComponent<Transform>().localScale.x==0.5F){
								isKaioken=false;
								curentSpeed=BaseSpeed;
							}
						}
				}
    }

    void FixedUpdate ()
    {
		//varible de mouvement du personnage
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");
		
		//initialisation de hazard
			hazard=Random.Range(1,3);
		
		if (Time.time > nextFire-0.25){
		//si le joueur bouge à gauche alors...
			if (moveHorizontal<0){
				//...réinitialisation des varibles d'états du player
					isMouveDown=false;
					isMouveUp=false;
					isMouveRight=false;
				//changement du sprite du player
					Tab[0]=CoteG;
					SpriteMouv.GetComponent<MeshRenderer>().materials=Tab;
				//si il ne bougeait pas déjà à gauche alors...
					if (!isMouveLeft){
						//...on joue le son correspondant
							if (hazard==1){
								Cible.GetComponent<AudioSource>().clip=Dash1;
							}else if (hazard==3){
								Cible.GetComponent<AudioSource>().clip=Dash3;
							}else{
								Cible.GetComponent<AudioSource>().clip=Dash2;
							}
							Cible.GetComponent<AudioSource>().Play();
						//on change la varible d'état du player
							isMouveLeft=true;
					}
		//si le joueur bouge à droite alors...
			}else if (moveHorizontal>0){
				//...réinitialisation des varibles d'états du player
					isMouveDown=false;
					isMouveUp=false;
					isMouveLeft=false;
				//changement du sprite du player
					Tab[0]=CoteD;
					SpriteMouv.GetComponent<MeshRenderer>().materials=Tab;
				//si il ne bougeait pas déjà à droite alors...
					if (!isMouveRight){
						//...on joue le son correspondant
							if (hazard==1){
								Cible.GetComponent<AudioSource>().clip=Dash1;
							}else if (hazard==3){
								Cible.GetComponent<AudioSource>().clip=Dash3;
							}else{
								Cible.GetComponent<AudioSource>().clip=Dash2;
							}
							Cible.GetComponent<AudioSource>().Play();
						//on change la varible d'état du player
							isMouveRight=true;
					}
		//si le joueur bouge en bas alors...
			}else if (moveVertical<0){
				//...réinitialisation des varibles d'états du player
					isMouveLeft=false;
					isMouveUp=false;
					isMouveRight=false;
				//changement du sprite du player
					Tab[0]=FaceB;
					SpriteMouv.GetComponent<MeshRenderer>().materials=Tab;
				//si il ne bougeait pas déjà en bas alors...
					if (!isMouveDown){
						//...on joue le son correspondant
							if (hazard==1){
								Cible.GetComponent<AudioSource>().clip=Dash1;
							}else if (hazard==3){
								Cible.GetComponent<AudioSource>().clip=Dash3;
							}else{
								Cible.GetComponent<AudioSource>().clip=Dash2;
							}
							Cible.GetComponent<AudioSource>().Play();
						//on change la varible d'état du player
							isMouveDown=true;
					}
		//si le joueur bouge en haut alors...
			}else if (moveVertical>0){
				//...réinitialisation des varibles d'états du player
					isMouveDown=false;
					isMouveLeft=false;
					isMouveRight=false;
				//changement du sprite du player
					Tab[0]=FaceH;
					SpriteMouv.GetComponent<MeshRenderer>().materials=Tab;
				//si il ne bougeait pas déjà en haut alors...
					if (!isMouveUp){
						//...on joue le son correspondant
							if (hazard==1){
								Cible.GetComponent<AudioSource>().clip=Dash1;
							}else if (hazard==3){
								Cible.GetComponent<AudioSource>().clip=Dash3;
							}else{
								Cible.GetComponent<AudioSource>().clip=Dash2;
							}
							Cible.GetComponent<AudioSource>().Play();
						//on change la varible d'état du player
							isMouveUp=true;
					}
		//sinon...
			}else{
				//...réinitialisation des varibles d'états du player
					isMouveDown=false;
					isMouveLeft=false;
					isMouveRight=false;
					isMouveUp=false;
				//changement du sprite du player
					Tab[0]=Static;
					SpriteMouv.GetComponent<MeshRenderer>().materials=Tab;
			}
		}
		
		//déplacement du player
			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
			Cible.GetComponent <Rigidbody>().velocity = movement * curentSpeed;

		//repositoinnement du player pour qu'il reste dans la zone de jeu
			Cible.GetComponent <Rigidbody>().position = new Vector3 
			(
				Mathf.Clamp (Cible.GetComponent <Rigidbody>().position.x, ZoneDeJeu.xMin, ZoneDeJeu.xMax), 
				0.0f, 
				Mathf.Clamp (Cible.GetComponent <Rigidbody>().position.z, ZoneDeJeu.zMin, ZoneDeJeu.zMax)
			);
    }
	
	void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ZoneDeJeu" || other.tag=="Player" || other.tag == "PlayerShot" )
        {
            return;
        }
		if (other.tag != "ShotSpe" && other.tag != "Ennemi"){
			Destroy(other.gameObject);  //destroy the bolt
		}
		if(!isCliping && other.tag != "ShotSpe"){
			vie--;
			isCliping=true;
			FinClip=Time.time+TempClip;
		}
		if(vie == 0){
			Destroy(gameObject);        //destroy the player
			gameController.GameOver ();
		}
    }
}
