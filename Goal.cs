using UnityEngine;
using System.Collections;

/*Klassen implementeras i fysikmotorn BoxCollider2D
 *och skapar "Collidern Other" som intergerar med objekt som
 *passerar ingeom den.
 */
public class Goal : MonoBehaviour {
	
	void onTriggerEnter(Collider other){
		if(other.gameObject.tag == "Goal"){
			print("asd");
		}
		if(other.gameObject.name == "Goal"){
			print("asd");
		}
	}
}
