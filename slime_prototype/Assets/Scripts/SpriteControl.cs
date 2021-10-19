using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SpriteControl : MonoBehaviour
{
    private const float splineOffset = 0.5f;

    public SpriteShapeController spriteShape;
    public Transform[] points;

    void Awake()
    {
        UpdateVerticles();
    }

    void Update()
    {
        UpdateVerticles();
    }

    public void UpdateVerticles()
    {
        for (int i = 0; i < points.Length - 1; i++)
        {
            Vector2 vertex = points[i].localPosition;
            Vector2 towardsCenter = (Vector2.zero - vertex).normalized;

            float colliderRadius = points[i].gameObject.GetComponent<CircleCollider2D>().radius;
            try
            {
                spriteShape.spline.SetPosition(i, (vertex - towardsCenter * colliderRadius));
            }
            catch
            {
                Debug.Log("an error occured");
                spriteShape.spline.SetPosition(i, (vertex - towardsCenter * (colliderRadius + splineOffset)));
            }


            Vector2 lt = spriteShape.spline.GetLeftTangent(i);

            Vector2 newRt = Vector2.Perpendicular(towardsCenter) * lt.magnitude;
            Vector2 newLt = Vector2.zero - newRt;

            spriteShape.spline.SetRightTangent(i, newRt);
            spriteShape.spline.SetLeftTangent(i, newLt);
        }
    }
}
