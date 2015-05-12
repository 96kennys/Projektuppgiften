 using UnityEngine;
using System.Collections;
/*
 * Author: Kent Nystedt Björknässgymnasiet TE12 
 */
//Komponenten Controller2D KRÄVS för programmet funktionalitet
[RequireComponent (typeof(Controller2D))]
public class Player : MonoBehaviour {

	public float jumpHeight = 4;
	public float timeToJumpApex = .4f;
	float moveSpeed = 6;

	//bool gameOn = true;
	float gravity;
	float jumpVelocity;
	float deathTimer;
	float deathTimer1;
	Vector3 velocity;

	Controller2D controller;
	Player player;

	void Start(){
		//Sessionskomponenter implementeras
		controller = GetComponent<Controller2D> ();
		player = GetComponent<Player>();
		//Sessionsvariabler implementeras
		gravity = -(2 * jumpHeight)/Mathf.Pow(timeToJumpApex, 2);
		jumpVelocity = Mathf.Abs (gravity) * timeToJumpApex;
	}
	private void OnGUI(){
		if(controller.collisions.below == false){
			deathTimer1 += Time.deltaTime;
			if(deathTimer1 >= 2){
				GUI.TextArea(new Rect(1,1,250,250),"You're falling to your death...");
			}
		}else{
			deathTimer1 = 0;
		}
	}
	void Update(){
		//do{
			if(controller.collisions.above || controller.collisions.below){
				velocity.y = 0;
			}
			//Lagrar inmatning som en vektor i y- och x-led.
			Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));

			if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.below) {
				Application.ExternalCall ("MyFunction1");
				velocity.y = jumpVelocity;
			}
			//Death
			if(controller.collisions.below == false){
				deathTimer += Time.deltaTime;

				if(deathTimer >= 2){
				Player.DestroyImmediate(player);
					print ("Time froze when you died.");
				}
			}
			else if(controller.collisions.below){
					deathTimer = 0;
			}

			velocity.x = input.x * moveSpeed;
			velocity.y += gravity * Time.deltaTime;

			//Tar in information från spelaren 
			controller.Move (velocity * Time.deltaTime);
			

		//}while(gameOn == true);
	}
	}
