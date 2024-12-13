using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OpenURL : MonoBehaviour
{
    [Header("Reward Pop Up")]
    [SerializeField]private GameObject rewardURLPopUp; //Ödül ekranı
    [SerializeField] private Button openURLButton; // Linke gitme tuşu
    [SerializeField] private Button closePopUpButton; // Ödül ekranınu kapama tuşu

    // Buradakı linki sandık üzerindeki inspector kısmından değiştirebilirsiniz
    [Header("Reward URL")]
    [SerializeField] private string rewardURL = "https://github.com/PepeJump/BeegJump";

    // Sandık üzerine gelince ödül ekranı açılır
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OpenPopUp();
        }
    }

    // Sandık dışına çıkınca ödül ekranı kapanır
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ClosePopUp();
        }
    }

    //Link açmak için
    public void OpenLink()
    {
        Application.OpenURL(rewardURL);
    }

    // Ödül ekranını açma metodu
    public void OpenPopUp()
    {
        if (rewardURLPopUp == null)
            return;

        rewardURLPopUp.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
    }

    // Ödül ekranını kapama metodu
    public void ClosePopUp()
    {
        if (rewardURLPopUp == null)
            return;

        rewardURLPopUp.SetActive(false);
    }
}