using UnityEngine;
using System.Collections;

//Kräver komponenten BoxCollider2D av superklassen BoxCollider2D 
[RequireComponent (typeof(BoxCollider2D))]
public class Controller2D : MonoBehaviour {

	/*Medlemsvariabler och refenser skapas med förangivna värden.
	 * Float är decimatal som kan lagra upp till 7 decimaler
	 */

	//Objekt med en Layermask kan kollidera
	public LayerMask collisionMask;

	public float JumpSpeed = 100.0f;
	const float skinWidth = .015f;

	//Stålar som skickas ut vertikalt och horisontellt
	public int horizontalRaycount = 4;
	public int verticalRaycount = 4;

	//Intervallet mellan strålarna
	float horizontalRaySpacing;
	float verticalRaySpacing;

	//Referenser till komponenterna implementeras
	BoxCollider2D collider;
	RaycastOrigins raycastOrigins;
	public CollisionInfo collisions;

	void Start () {
		//Implementerar sessionskomponenten
		collider = GetComponent<BoxCollider2D> ();
		//Beräknar strålintervallet
		CalculateRaySpacing ();
	}

	/*Rör spelaren med en hastighetsvektor mot vektornsriktningen och tilldelar hastigheten i y- och x-led en kollition
	*
	*/
	public void Move(Vector3 velocity) {
		
		UpdateRaycastOrigins ();
		collisions.Reset();

		if (velocity.x != 0) {
			horizontallCollision(ref velocity);	
		}
		if (velocity.y != 0) {
			verticalCollision (ref velocity);
		}
		transform.Translate (velocity);

	}

	void horizontallCollision(ref Vector3 velocity){
		float directionX = Mathf.Sign (velocity.x);
		float rayLength = Mathf.Abs (velocity.x) + skinWidth;
		
		for (int i = 0; i < horizontalRaycount; i++) {
			Vector2 rayOrigin = (directionX == -1)?raycastOrigins.bottomLeft:raycastOrigins.bottomRight;
			rayOrigin += Vector2.up * (horizontalRaySpacing * i);
			RaycastHit2D hit = Physics2D.Raycast (rayOrigin, Vector2.right * directionX, rayLength, collisionMask);


			if(hit){
				velocity.x = (hit.distance - skinWidth) * directionX;
				rayLength = hit.distance;

				collisions.left = directionX == -1;
				collisions.right = directionX == 1;
			}
		}
		
	}

	/* Ansvarar för vertikala kollitionen genom en referens, "ref", till hastighetsvektorn.
	 * Ifall referensen ändrar en funktion i variabeln förändras alla referenser
	 */
	void verticalCollision(ref Vector3 velocity){
		//Mathf.sign retunerar 1 ifall vektorn i y-led är större än 0 respektive mindra än 0 -> -1
		float directionY = Mathf.Sign (velocity.y);
		//Mathf.abs retunerar det absoluta värdet
		float rayLength = Mathf.Abs (velocity.y) + skinWidth;

		for (int i = 0; i < verticalRaycount; i++) {

		   /* Ifall y är -1 är sant sätter vi vektorn rayOrigin till raycastOrigins.bottomLeft.
			* Annars blir det raycastOrigins.topleft
			*/
			Vector2 rayOrigin = (directionY == -1)?raycastOrigins.bottomLeft:raycastOrigins.topLeft;
			rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
		   /* Skickar ut en stråle mot höger och ifall vi har en hastighet i y-led blir det en resultant till längden
			* och ifall strålen rör ett object som har en collisionMask uppstår en kolltion.
			*/
			RaycastHit2D hit = Physics2D.Raycast (rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

			//Ifall vi blir träffade av ett kollition ska vi inte kunna röra oss i den riktningen.
			if(hit){
			   /*Ser till att ifall spelaren skickar ut en stråle och halft är utanför en platå kommer den inte att påverkas ifall den hittar
				* en längre platå. Den kommer då stanna kvar på platån istället för att glida igenom platån den står på för nuvarandra
				* till den lägre.
				*/
				velocity.y = directionY* (hit.distance - skinWidth);
				rayLength = hit.distance;

				collisions.below = directionY == -1;
				collisions.above = directionY == 1;
			}
		}

	}
	//Kollar efter gränserna till kollitionsdektorn
	void UpdateRaycastOrigins() {
		Bounds bounds = collider.bounds;
		//Begränsningen utökas till beroende av spelarens skinWidth
		bounds.Expand (skinWidth * -2);

		//En vektor skapas som resultant till kollitionens
		raycastOrigins.bottomLeft = new Vector2 (bounds.min.x, bounds.min.y);
		raycastOrigins.bottomRight = new Vector2 (bounds.max.x, bounds.min.y);
		raycastOrigins.topLeft = new Vector2 (bounds.min.x, bounds.max.y);
		raycastOrigins.bottomRight = new Vector2 (bounds.max.x, bounds.max.y);
	}

	//Räknar ut intervallet mellan strålarna och begränsar minst anatal strålar
	void CalculateRaySpacing() {
		Bounds bounds = collider.bounds;
		bounds.Expand (skinWidth * -2);

		//Begränsar antalet strålar som kan skickas ut till minst 2
		horizontalRaycount = Mathf.Clamp (horizontalRaycount, 2, int.MaxValue);
		verticalRaycount = Mathf.Clamp (horizontalRaycount, 2, int.MaxValue);

		//Räknar ut intervallet mellan strålarna
		horizontalRaySpacing = bounds.size.y / (horizontalRaycount - 1);
		verticalRaySpacing = bounds.size.y / (verticalRaycount - 1);
	}

	//Håller reda på vektorerna som kommer i kontakt med en kollision för BoxCollider2D
	struct RaycastOrigins {
		public Vector2 topLeft, topRight;
		public Vector2 bottomLeft, bottomRight;
	}

	//Håller reda på kollisioner och kan reset:ta de ifall det inte uppstår
	public struct CollisionInfo {
		public bool above, below;
		public bool left, right;

		public void Reset() {
			above = below = false;
			left = right = false;
		}
	}
}
