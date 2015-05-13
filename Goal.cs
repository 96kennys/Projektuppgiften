/**
 * @author © Kent Nystedt Björknäsgymansiet TE12
 */
using UnityEngine;
using System.Collections;

/*Klassen implementeras i fysikmotorn BoxCollider2D
 *och skapar "Collidern Other" som intergerar med objekt som
 *passerar ingeom den.
 */
public class Goal : MonoBehaviour {
	
	void onCollisionEnter2D(Collider2D other){
		//Ifall objectet som passerar har taggen "Goal" skriv ut "asd"
		print("Fungerar");
		if(other.gameObject.tag == "Player"){
			print("asd");
		}
		// -||- namnet "Goal" -||- "asd"
		if(other.gameObject.name == "Cylinder"){
			print("asd");
		}
	}
}
