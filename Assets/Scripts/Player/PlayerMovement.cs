using UnityEngine;

namespace Player
{
	public class PlayerMovement : MonoBehaviour
	{
		[SerializeField] private float movementSpeed;
		[SerializeField] private float jumpForce;

		private Rigidbody2D playerRigidBody => GetComponent<Rigidbody2D>();

		private void Update()
		{
			MoveCheck();
			JumpCheck();
		}

		private void JumpCheck()
		{
			if (Input.GetButtonDown("Jump"))
			{
				playerRigidBody.AddForce(new Vector2(0, jumpForce));
			}
		}

		private void MoveCheck()
		{
			playerRigidBody.velocity = new Vector2(Input.GetAxis("Horizontal") * movementSpeed,
												   playerRigidBody.velocity.y);
		}
	}
}

