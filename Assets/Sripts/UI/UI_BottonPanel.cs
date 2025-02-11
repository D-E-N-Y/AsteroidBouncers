using TMPro;
using UnityEngine;

public class UI_BottonPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countProjectilesText;
    
    public void Initialize()
    {
        GameSystem.current.UpdateCountProjectiles += RefreshCountProjectilesText;
    }

    private void RefreshCountProjectilesText(int count)
    {
        countProjectilesText.text = count.ToString();
    }
}
