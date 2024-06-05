using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Numerics;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : BaseManager<GameManager>
{
    [SerializeField] private Blade blade;
    [SerializeField] private Spawner spawner;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Image fadeImage;

    private int score;
    public int Score => score;

    private float notifyLoadingTime = 2.0f;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        NewGame();

        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowNotify<NotifyLoading>();
            NotifyLoading scr = UIManager.Instance.GetExistNotify<NotifyLoading>();
            if (scr != null)
            {
                scr.AnimationLoaddingText(notifyLoadingTime);
                scr.DoAnimationLoadingProgress(notifyLoadingTime,
                    OnComplete: () =>
                    {
                        scr.Hide();
                        UIManager.Instance.ShowScreen<ScreenHome>();
                    });
            }
        }
    }

    private void Update()
    {
        //ShowPopupPause();
    }

    public void RestartGame()
    {
        if (UIManager.HasInstance)
        {
            OverlapFade overlapFade = UIManager.Instance.GetExistOverlap<OverlapFade>();
            overlapFade.Show(null);
            overlapFade.Fade(2f,
                onDuringFade: () =>
                {
                    UIManager.Instance.ShowScreen<ScreenHome>();
                    LoadScene("Menu");
                },
                onFinish: () =>
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                });
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void NewGame()
    {
        Time.timeScale = 1f;

        ClearScene();

        blade.enabled = true;
        spawner.enabled = true;

        score = 0;
        scoreText.text = score.ToString();
    }

    private void ClearScene()
    {
        Fruit[] fruits = FindObjectsOfType<Fruit>();

        foreach (Fruit fruit in fruits)
        {
            Destroy(fruit.gameObject);
        }

        Bomb[] bombs = FindObjectsOfType<Bomb>();

        foreach (Bomb bomb in bombs)
        {
            Destroy(bomb.gameObject);
        }
    }

    public void Explode()
    {
        if(blade != null)
        {
            blade.enabled = false;
        }

        if(spawner != null)
        {
            spawner.enabled = false;
        }

        StartCoroutine(ExplodeSequence());
    }

    private IEnumerator ExplodeSequence()
    {
        float elapsed = 0f;
        float duration = 1.5f;
        if (fadeImage != null)
        {
            // Fade to white
            while (elapsed < duration)
            {
                float t = Mathf.Clamp01(elapsed / duration);
                fadeImage.color = Color.Lerp(Color.clear, Color.white, t);

                Time.timeScale = 1f - t;
                elapsed += Time.unscaledDeltaTime;

                yield return null;
            }

            yield return new WaitForSecondsRealtime(1f);

            elapsed = 0f;

            // Fade back in
            while (elapsed < duration)
            {
                float t = Mathf.Clamp01(elapsed / duration);
                fadeImage.color = Color.Lerp(Color.white, Color.clear, t);

                elapsed += Time.unscaledDeltaTime;

                yield return null;
            }

            NewGame();
        }

        RestartGame();
    }

    public void IncreaseScore(int points)
    {
        score += points;
        scoreText.text = score.ToString();

        float hiscore = PlayerPrefs.GetFloat("hiscore", 0);

        if (score > hiscore)
        {
            hiscore = score;
            PlayerPrefs.SetFloat("hiscore", hiscore);
        }
    }
}