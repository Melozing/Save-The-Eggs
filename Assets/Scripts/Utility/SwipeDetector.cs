using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    private Vector2 _startTouchPosition;
    private Vector2 _endTouchPosition;
    private float _minSwipeDistance = 50f; // Minimum swipe distance to detect a gesture

    private void Update()
    {
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            _startTouchPosition = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            _endTouchPosition = Input.GetTouch(0).position;
            ProcessSwipe();
        }
    }

    private void ProcessSwipe()
    {
        float swipeDistance = Vector2.Distance(_startTouchPosition, _endTouchPosition);

        // Check if the swipe is long enough
        if (swipeDistance >= _minSwipeDistance)
        {
            Vector2 direction = _endTouchPosition - _startTouchPosition;
            if (Mathf.Abs(direction.y) > Mathf.Abs(direction.x))
            {
                if (direction.y > 0)
                {
                    // Up Swipe Detected - Pause Game
                    GameController.Instance?.PauseGame();
                }
            }
        }
    }
}
