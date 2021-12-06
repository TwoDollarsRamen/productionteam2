using UnityEngine;

/* This script makes it so that the player can jump through
 * the platform in one direction, but stand on it when he falls
 * down. It does this by disabling the box collider on the platform. */
public class Platform : MonoBehaviour
{
	public RedRidingHood player;

	void Start()
	{
		if (player == null)
		{
			player = GameObject.Find("Red Riding Hood")
				.GetComponent<RedRidingHood>();
		}
	}

	void Update()
	{
		/* The box collider size and centre are in local coordinates, which is
		 * why they are multiplied by the objects' scales */
		var playerHit = player.GetComponent<BoxCollider>();
		var playerHitSizeY = playerHit.size.y * player.transform.localScale.y;
		var playerHitCenterY = playerHit.center.y * player.transform.localScale.y;
		var playerY = (playerHitCenterY + player.transform.position.y)
			- (playerHitSizeY * 0.5f);

		var selfHit = GetComponent<BoxCollider>();
		var selfHitSizeY = selfHit.size.y * transform.localScale.y;
		var selfHitCenterY = selfHit.center.y * transform.localScale.y;
		var selfY = selfHitCenterY + transform.position.y
			- (selfHitSizeY * 0.5f);

		if (playerY > selfY)
		{
			selfHit.enabled = true;
		}
		else
		{
			selfHit.enabled = false;
		}
	}
}