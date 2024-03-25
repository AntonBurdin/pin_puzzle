using UnityEngine;
using UnityEngine.EventSystems;

public class JointHole : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private float radius;

    [SerializeField]
    private LayerMask layersMask;

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (this.IsClickValid())
        {
            SceneRoot.JointHoleClicked(this);
        }
    }

    private bool IsClickValid()
    {
        var hits = Physics2D.CircleCastAll(this.transform.position, this.radius, Vector2.zero, 0f, this.layersMask);
        Debug.Log($"Click validation. Results {hits.Length}, Radius {this.radius}");

        for (int i = 0; i < hits.Length; i++)
        {
            var item = hits[i].rigidbody.GetComponent<PlayableItem>();
            if (!item.TryGetClosestDistanceToSocket(this.transform.position, out var distance) || distance > this.radius)
            {
                return false;
            }
        }
        return true;
    }
}
