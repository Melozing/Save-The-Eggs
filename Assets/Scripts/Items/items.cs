using System.Collections;
using UnityEngine;

public class Items : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private float rotationSpeed = 550f;
    private bool isRotating = true;
    private bool _hasCollidedWithDeathZone = false;

    private void Start()
    {
        if (Random.value > 0.5f)
        {
            rotationSpeed = -rotationSpeed;
        }

    }

    private void Update()
    {
        if (isRotating)
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!_hasCollidedWithDeathZone && col.gameObject.CompareTag(Constants.DeathZoneTag))
        {
            isRotating = false;

            _hasCollidedWithDeathZone = true;

            // Play both animation and sound at the same time
            animator.SetBool(Constants.IsDeadConditrion, true);
            SoundController.Instance.PlaySFX(Constants.EggBrokenSound);

            // Start coroutine to handle the rest after animation finishes
            StartCoroutine(HandleDeath());
        }

        if (col.gameObject.CompareTag(Constants.PlayerTag))
        {
            isRotating = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (_hasCollidedWithDeathZone) return;
        if (col.gameObject.CompareTag(Constants.PlayerTag))
        {
            isRotating = false;

            SoundController.Instance.PlaySFX(Constants.CatchItemSound);
            GameController.Instance.IncrementScore();
            Destroy(gameObject);
        }
    }

    private IEnumerator HandleDeath()
    {
        // Wait for the length of the death animation
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length - 0.8f);

        VibrationController.Instance.Vibrate();

        // Call StopGameIfOver after the wait
        GameController.Instance.StopGameIfOver();

        // Destroy the game object after the animation and delay
        Destroy(gameObject);
    }
}
