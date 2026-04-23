using UnityEngine;
using UnityEngine.UIElements;

namespace Gameplay
{
    public class ForwardScroller : MonoBehaviour
    {
        [SerializeField] private float speed = 2f;
    
        private Transform _transform;
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _transform = transform;
        }

        // Update is called once per frame
        void Update()
        {
            _transform.Translate(Vector3.back * (speed * Time.deltaTime));
        }
    }
}
