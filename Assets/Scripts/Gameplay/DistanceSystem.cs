using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class DistanceSystem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI distanceLabel;
        
        private float distance;

        private void Update()
        {
            distance += Time.deltaTime;
            distanceLabel.text = (int)distance + "m";
        }
    }
}
