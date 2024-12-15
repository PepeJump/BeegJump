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

    [Header("Animation")]
    Animator animator;

    [Header("Audio")]
    [SerializeField]private AudioClip popUpSound;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        animator = GetComponent<Animator>();
    }

    // Sandık üzerine gelince ödül ekranı açılır
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OpenPopUp();
            PlayOpenAnimation();
        }
    }

    // Sandık dışına çıkınca ödül ekranı kapanır
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ClosePopUp();
            PlayCloseAnimation();
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
        AudioManager.instance.PlaySoundSFX(popUpSound);

        EventSystem.current.SetSelectedGameObject(null);
    }

    // Ödül ekranını kapama metodu
    public void ClosePopUp()
    {
        if (rewardURLPopUp == null)
            return;

        rewardURLPopUp.SetActive(false);
    }

    private void PlayOpenAnimation()
    {
        if (animator == null)
            return;

        animator.Play("chest_animation_01");
    }

    private void PlayCloseAnimation()
    {
        if (animator == null)
            return;

        animator.Play("chest_animation_02");
    }
}