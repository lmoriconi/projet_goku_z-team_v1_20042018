using System.Collections;
using UnityEngine;

public class MoveEnemy : MonoBehaviour {

    private bool isLeft = false;
	public GameObject SpriteMouvEnn;
	//varibles de matériaux pour changer le sprite de l'ennemi
		//Sprite de chaque direction
			public Material CoteG;
			public Material FaceH;
			public Material CoteD;
		//Materiel de l'ennemi actuel
			private Material[] Tab;
			
	void Start(){
		//récupération du matériel du player
			Tab=SpriteMouvEnn.GetComponent<MeshRenderer>().materials;
	}

    void Update() {
        //make the enemy go down until the enemy reached 3 on the z axis
        if (this.GetComponent<Transform>().position.z > 3)
        {
			Tab[0]=FaceH;
			SpriteMouvEnn.GetComponent<MeshRenderer>().materials=Tab;
            transform.Translate(Vector3.back * Time.deltaTime);
        }
        //once the enemy reached the limit on the z axis, it goes left until it reached -3 on the x axis
        else if (this.GetComponent<Transform>().position.x >= -6 && isLeft == false)
        {
			Tab[0]=CoteG;
			SpriteMouvEnn.GetComponent<MeshRenderer>().materials=Tab;
            transform.Translate(Vector3.left * Time.deltaTime);
            if (this.GetComponent<Transform>().position.x < -6)
                isLeft = true;      //we know the enemy reached -6 on the x axis
        }
        //once the enemy reached 6 on the x axis, it goes right until it reached 6 on the x axis
        else if (this.GetComponent<Transform>().position.x <= 6 && isLeft == true)
        {
			Tab[0]=CoteD;
			SpriteMouvEnn.GetComponent<MeshRenderer>().materials=Tab;
            transform.Translate(Vector3.right * Time.deltaTime);
            if (this.GetComponent<Transform>().position.x > 6)
                isLeft = false;     //we know the enemy reached 6 on the x axis
        }
    }
}
