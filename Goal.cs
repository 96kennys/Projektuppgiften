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
	
	void onCollisionEnter(Collision other){
		//Ifall objectet som passerar har taggen "Goal" skriv ut "asd"
		print("Fungerar");
		if(other.gameObject.tag == "Cylinder"){
			print("asd");
		}
		// -||- namnet "Goal" -||- "asd"
		if(other.gameObject.name == "Cylinder"){
			print("asd");
		}
	}
}
