using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SpriteRenderer))]
public class JointPin : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private float radius;

    [SerializeField]
    private LayerMask layersMask;

    private List<HingeJoint2D> joints = new List<HingeJoint2D>();

    public void SetSelection(bool selection)
    {
        var renderer = GetComponent<SpriteRenderer>();
        renderer.color = selection ? Color.green : Color.black;
    }

    public void MoveTo(Vector2 position)
    {
        this.transform.position = position;
        this.UpdateJoints();
    }

    private void Awake()
    {
        UpdateJoints();
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Pin click");
        SceneRoot.JointPinClicked(this);
    }

    private void UpdateJoints()
    {
        HashSet<HingeJoint2D> validJoints = new HashSet<HingeJoint2D>();

        var hits = Physics2D.CircleCastAll(this.transform.position, this.radius, Vector2.zero, 0f, this.layersMask);
        for (int i = 0; i < hits.Length; i++)
        {
            var hit = hits[i];
            var hitJoint = this.FindJointForTarget(hit.rigidbody);
            if (hitJoint == null)
            {
                hitJoint = this.CreateJoint(hit.rigidbody);
            }

            validJoints.Add(hitJoint);
            if (!this.joints.Contains(hitJoint))
            {
                this.joints.Add(hitJoint);
            }
        }

        this.FilterInvalidJoints(validJoints);
    }

    private void FilterInvalidJoints(HashSet<HingeJoint2D> validJoints)
    {
        int i = 0;
        while (i < this.joints.Count)
        {
            var joint = this.joints[i];
            if (!validJoints.Contains(joint))
            {
                this.joints.Remove(joint);
                GameObject.Destroy(joint);

                continue;
            }
            i++;
        }
    }

    private HingeJoint2D FindJointForTarget(Rigidbody2D target)
    {
        for (int i = 0; i < this.joints.Count; i++)
        {
            var joint = this.joints[i];
            if (joint.connectedBody == target)
            {
                return joint;
            }
        }
        return null;
    }

    private HingeJoint2D CreateJoint(Rigidbody2D body)
    {
        var result = this.AddComponent<HingeJoint2D>();
        result.connectedBody = body;

        return result;
    }
}
