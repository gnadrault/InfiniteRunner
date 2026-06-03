using Core;
using Player;
using Shared;
using UnityEngine;

namespace Gameplay.Movement
{
    /// <summary>
    /// Script to game object following the player horizontally
    /// </summary>
    public class FollowPlayerHorizontal : GameBehavior
    {
        [Header("Settings")]
        [SerializeField] private FloatValue speed;
        [SerializeField] private float leftLaneX = -4;
        [SerializeField] private float rightLaneX = 4;
        
        private float _speed;
        private Transform _transform;
		
        private void Awake()
        {
            _transform = transform;
        }
        
        private void Start()
        {
            _speed = speed.Value;
        }
        
        protected override void GameplayUpdate()
        {
            Vector3 position = _transform.position;
            float deltaX = PlayerController.Instance.transform.position.x - position.x;
            
            if (Mathf.Abs(deltaX) < 0.05f) return; // Deadzone => prevent pixel movement
            position.x = Mathf.MoveTowards(position.x, PlayerController.Instance.transform.position.x, _speed * Time.deltaTime);
            position.x = Mathf.Clamp(position.x, leftLaneX, rightLaneX);
            _transform.position = position;
        }
    }
}