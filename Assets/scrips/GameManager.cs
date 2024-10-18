using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int coins = 0;

    private int star = 0;

    private bool isPaused;

    private bool pauseAnimation;

    [SerializeField] Text _coinText;
    [SerializeField] Text _starText;

    [SerializeField] GameObject _pauseCanvas;
    [SerializeField] Animator _pausePanelAnimation;
    [SerializeField] private Slider _healBar;


    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        _pausePanelAnimation = _pauseCanvas.GetComponentInChildren<Animator>();
    }

    public void Pause()
    {
        if(!isPaused && !pauseAnimation)
        {
            isPaused = true;
            Time.timeScale = 0;
            _pauseCanvas.SetActive(true);
        }
        else if(isPaused && !pauseAnimation)
        {
            pauseAnimation = true; 

            StartCoroutine(ClosePauseAnimation());

        }
    }

    IEnumerator ClosePauseAnimation()
    {
        _pausePanelAnimation.SetBool("close", true);

        yield return new WaitForSecondsRealtime(0.20f);

        Time.timeScale = 1;
        _pauseCanvas.SetActive(false);

        isPaused =false;
        pauseAnimation = false;

    }

    public void AddCoin()
    {
        coins++;
        _coinText.text = coins.ToString(); 
    }

    public void SetHealthBar(int maxHealth)
    {
        _healBar.maxValue = maxHealth;
        _healBar.value = maxHealth;
    }

    public void AddStar()
    {
        star++;
        _starText.text = star.ToString();

    }

    public void UpdateHealthBar(int health)
    {
        _healBar.value = health; 
    }

    public void SceneLoader(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
