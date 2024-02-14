using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class Movmant : MonoBehaviour
{
    [SerializeField] CharacterController controlPlayer;
    [SerializeField] float Speed = 3;
    [SerializeField] float JumpHight = 3;
    [SerializeField] Transform GFX;
    [SerializeField] bool isGrounded;
    [SerializeField] Animator anim;
    [SerializeField] FixedJoystick fixedJoystick;

    // garvity
    private float Gravoty = -9.81f;
    private Vector3 velocity;


    private float InputX, InputY;

    private void Start()
    {
        controlPlayer = GetComponent<CharacterController>();
    }


    void Update()
    {
        /// Checks if the character is grounded by seeing if the CharacterController isGrounded.
        /// If grounded and moving downwards, set a small upward y velocity to avoid sticking to the ground.
        isGrounded = controlPlayer.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }

        /// Gets the horizontal and vertical input from the input manager multiplied 
        /// by the speed and delta time. This allows scaling movement speed and handling 
        /// time correctly.
        InputX = fixedJoystick.Horizontal * Speed * Time.deltaTime;
        InputY = fixedJoystick.Vertical * Speed * Time.deltaTime;

        Vector3 Dir = new Vector3(InputX, 0, InputY);
        anim.SetFloat("run", Dir.magnitude);

        /// Calculates the angle to rotate the character graphic towards based on horizontal and vertical input direction.
        /// Uses Mathf.Atan2 to get the angle in radians between the input direction and forward vector.
        /// Converts the angle to degrees with Mathf.Rad2Deg.
        /// Rotates the character graphic to face the input direction using the calculated angle.
        float angle = Mathf.Atan2(Dir.x, Dir.z) * Mathf.Rad2Deg;
        GFX.rotation = Quaternion.Euler(0, angle, 0);

        /// Moves the character controller in the direction specified by Dir, using the Move method.
        /// Dir is calculated based on player input, so this allows player movement.
        controlPlayer.Move(Dir);

        // jump
        /// Checks if the jump button was pressed and the character is grounded.
        /// If so, calculates a vertical velocity based on the jump height and gravity to initiate a jump.
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(JumpHight * -2 * Gravoty);
        }
        /// Applies gravity to the character's vertical velocity over time.
        /// Moves the character controller by the current velocity multiplied by delta time.
        /// This allows physics forces like gravity to accumulate into velocity over frames,
        /// and moves the character smoothly based on the resulting velocity each frame.

        velocity.y += Gravoty * Time.deltaTime;
        controlPlayer.Move(velocity * Time.deltaTime);
    }



}
