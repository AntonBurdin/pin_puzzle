using UnityEngine;

public class WorldEdge : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var item = collision.gameObject.GetComponent<PlayableItem>();
        if (item != null)
        {
            GameObject.Destroy(item.gameObject);
        }
    }
}
