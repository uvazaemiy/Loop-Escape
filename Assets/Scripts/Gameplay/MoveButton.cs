using UnityEngine;

public class MoveButton : MonoBehaviour
{
    [SerializeField] private float value;

    public void ButtonDown()
    {
        StartCoroutine(GameManager.instacne.Player.Move(value));

        if (value != 0)
        {
            GameManager.instacne.Player.isMoving = true;

            if (!PlayerController.instance.jumping)
                GameManager.instacne.Player.skeletonAnimation.state.SetAnimation(0, "Step", true);
        }
    }

    public void ButtonUp()
    {
        if (value != 0)
        {
            GameManager.instacne.Player.isMoving = false;

            GameManager.instacne.Player.collider.sharedMaterial = GameManager.instacne.PlayerPhysMatGround;
            GameManager.instacne.Player.skeletonAnimation.state.SetAnimation(0, "animation Costs", true);
        }
    }
}