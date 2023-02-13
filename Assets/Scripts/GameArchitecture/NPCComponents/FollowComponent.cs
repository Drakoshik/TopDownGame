using System;
using UnityEngine;

namespace GameArchitecture.NPCComponents
{
    [Serializable]
    public class FollowComponent
    {
        [SerializeField] private float _speed;


        public void Follow(Transform obj, Transform target)
        {
            Vector2 rotateVector = obj.position - target.transform.position;
            obj.position = Vector2.MoveTowards(obj.position,
                target.transform.position, _speed * Time.deltaTime);
        }
    }
}
