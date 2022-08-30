using System.Collections.Generic;
using UnityEngine;

public class PieceCollisionUtil
{
    public static List<RaycastHit2D> GetPossibleCollisions(List<Collider2D> colliderList, Vector3 direction,
        float distance, int layer)
    {
        List<RaycastHit2D> list = new List<RaycastHit2D>();
        foreach (Collider2D collider in colliderList)
        {
            List<RaycastHit2D> list2 = CheckColliderCollision(collider, direction.normalized, distance, layer);
            foreach (RaycastHit2D item in list2)
            {
                if (!colliderList.Contains(item.collider))
                {
                    list.Add(item);
                }
            }
        }

        return list;
    }

    private static List<RaycastHit2D> CheckColliderCollision(Collider2D collider, Vector3 direction, float distance,
        int layer)
    {
        bool flag = Mathf.Abs(direction.y) > Mathf.Abs(direction.x);
        Vector2 origin = collider.bounds.min;
        if (direction.x > 0f)
        {
            origin.x = collider.bounds.max.x;
            origin.y += 0.25f;
        }

        if (direction.y > 0f)
        {
            origin.y = collider.bounds.max.y;
        }

        float x = collider.bounds.max.x;
        float num = collider.bounds.max.y - 0.25f;
        List<RaycastHit2D> list = new List<RaycastHit2D>();
        while (origin.x <= x && origin.y <= num)
        {
            RaycastHit2D[] array = Physics2D.RaycastAll(origin, direction, distance, 1 << layer);
            foreach (RaycastHit2D item in array)
            {
                list.Add(item);
            }

            if (flag)
            {
                origin.x += 0.1f;
            }
            else
            {
                origin.y += 0.1f;
            }
        }

        return list;
    }

    private const float _HORIZONTAL_RAYCAST_MARGIN = 0.25f;
    private const float _RAYCAST_STEP = 0.1f;
}