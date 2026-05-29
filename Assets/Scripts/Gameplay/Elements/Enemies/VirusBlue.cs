using System.Collections.Generic;
using Data;
using Player;
using UI;
using UnityEngine;

namespace Gameplay.Elements.Enemies
{
    public class VirusBlue: Virus
    {
        [SerializeField] private int duration = 5;
        [SerializeField] private WordDatabase wordsDatabase;
        
        private PlayerController _player;
        private WordData _malusWord;
        
        public override void ApplyEffect(PlayerController player, Transform position)
        {
            _player = player;
            _malusWord = wordsDatabase.GetRandomWordExcept(new List<WordData>(), false);
            _malusWord.Effect.ApplyEffect(_player, this);
        }

        public override void RemoveEffect(PlayerController player)
        {
            AlertHUD.Instance.ForceHidePanels();
            Destroy(gameObject);
        }

        private void Update()
        {
            if (_malusWord != null && _malusWord.Effect.isComplete)
                _player.DetachVirus();
        }
    }
}