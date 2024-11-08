using UnityEngine;
using UnityEngine.EventSystems;

public class StartButton : MonoBehaviour, IPointerClickHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private bool isDragging = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        // Trigger StartGame when the player clicks or taps quickly
        StartGame();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Mark the start of a touch
        isDragging = false;
        StartGame();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // If this was a tap (not a drag), trigger StartGame
        if (!isDragging)
        {
            StartGame();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Mark this action as a drag
        isDragging = true;
        StartGame();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Trigger StartGame when the player finishes a swipe or drag
        StartGame();
    }

    private void StartGame()
    {
        // Call the StartGame function in the GameController singleton instance
        GameController.Instance.StartGame();
    }
}