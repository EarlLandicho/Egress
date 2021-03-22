#region

using UnityEngine;

#endregion

namespace Player
{
	public class PlayerMovement : MonoBehaviour
	{
		private const float groundCheckRadius = .1f;
		[SerializeField] private float movementSpeed;
		[SerializeField] private float jumpForce;
		[SerializeField] private float airSideForce;
		[SerializeField] private GameObject groundCheck;
		private bool isGrounded;

		private Rigidbody2D playerRigidBody => GetComponent<Rigidbody2D>();

		private void Update()
		{
			JumpCheck();
		}

		private void FixedUpdate()
		{
			MoveCheck();
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.grey;
			Gizmos.DrawWireSphere(groundCheck.transform.position, groundCheckRadius);
		}

		private void JumpCheck()
		{
			isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, groundCheckRadius, 1 << LayerMask.NameToLayer("Ground"));
			if (Input.GetButtonDown("Jump"))
			{
				if (isGrounded)
				{
					playerRigidBody.AddForce(new Vector2(0, jumpForce));
				}
			}
		}

		private void MoveCheck()
		{
			Vector2 playerVelocity = playerRigidBody.velocity;
			bool playerIsMovingInAir = playerVelocity.x <= movementSpeed && playerVelocity.x >= -movementSpeed && !isGrounded;
			if (playerIsMovingInAir)
			{
				playerRigidBody.AddForce(new Vector2(Input.GetAxisRaw("Horizontal") * airSideForce, 0));
			}
			else
			{
				playerRigidBody.velocity = new Vector2(Input.GetAxis("Horizontal") * movementSpeed,
													   playerRigidBody.velocity.y);
			}
		}
	}
}