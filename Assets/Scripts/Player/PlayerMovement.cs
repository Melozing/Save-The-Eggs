using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    public float padding = 0.005f; // Padding from the edges of the screen

    private float minX, maxX;

    void Start()
    {
        // Calculate the left and right screen boundaries in world coordinates, considering padding
        minX = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.z)).x + padding;
        maxX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, Camera.main.transform.position.z)).x - padding;
    }

    void Update()
    {
        if (GameController.Instance.IsGameOver() || GameController.Instance.IsGamePause())
        {
            return;
        }
        if (Application.isMobilePlatform)
        {
            HandleTouchInput();
        }
        else
        {
            HandleMouseInput();
        }
    }

    void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {

            Touch touch = Input.GetTouch(0);
            MoveToPosition(touch.position.x);
        }
        else
        {
            animator.SetBool(Constants.IsMovedConditrion, false);
        }
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButton(0))
        {
            MoveToPosition(Input.mousePosition.x);
        }
        else
        {
            animator.SetBool(Constants.IsMovedConditrion, false);
        }
    }

    void MoveToPosition(float positionX)
    {
        animator.SetBool(Constants.IsMovedConditrion, true);
        // Convert screen position to world position
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(positionX, 0, Camera.main.transform.position.z));

        // Clamp the target position's X value to stay within screen boundaries with padding
        float clampedX = Mathf.Clamp(targetPosition.x, minX, maxX);

        // Update the player's position
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}
