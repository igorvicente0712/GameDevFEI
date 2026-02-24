using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    public enum GoalSide
    {
        Bottom,
        Top
    }

    public GoalSide goalSide;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Puck"))
            return;

        if (GameManager.Instance == null)
            return;

        if (goalSide == GoalSide.Bottom)
        {
            GameManager.Instance.GoalBottom();
        }
        else
        {
            GameManager.Instance.GoalTop();
        }
    }
}
