using Gameplay.Data;
using UnityEngine;

namespace Gameplay
{
    public class LootSystem : MonoBehaviour
    {
        [Header("Words")] 
        [SerializeField] private WordDatabase bonusWords;
        [SerializeField] private WordDatabase malusWords;
        [SerializeField] private LettersDisplay[] bonusWordsDisplays = new LettersDisplay[3];
        [SerializeField] private LettersDisplay[] malusWordsDisplays = new LettersDisplay[3];
    }
}
