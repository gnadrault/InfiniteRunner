using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public static HUD Instance;
    
    [SerializeField] private TextMeshProUGUI virusLabel;
    [SerializeField] private GameObject virusPanel;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }
    
    public void ShowVirusPanel(string text)
    {
        virusPanel.gameObject.SetActive(true);
        virusLabel.text = text;
    }

    public void UpdateVirusLabel(string text)
    {
        virusLabel.text = text;
    }

    public void HideVirusPanel()
    {
        virusPanel.gameObject.SetActive(false);
        virusLabel.text = "";
    }
}
