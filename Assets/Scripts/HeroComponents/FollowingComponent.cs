using UnityEngine;

namespace HeroComponents
{
    public class FollowingComponent : MonoBehaviour
    {
        private Transform _followingTransform;
        private Vector3 _delta;

        void Start()
        {
            _followingTransform = GetComponentInParent<Transform>();
            _delta = _followingTransform.position - transform.position;
        }
        void Update()
        {
            transform.position = _followingTransform.position - _delta;
        }
    }
}
