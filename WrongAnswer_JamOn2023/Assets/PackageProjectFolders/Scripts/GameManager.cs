using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    // TRANSITION STUFF
    [SerializeField] RectTransform left;
    [SerializeField] RectTransform right;
    [SerializeField] TMP_Text level_Text;
    [SerializeField] TMP_Text levelNum_Text;
    [SerializeField] Image icon;
    [SerializeField] GameObject gameOver_Object;
    [SerializeField] GameObject player;
    [SerializeField] List<Eye> eyeList;
    float transitionDuration = 1;

    float openValue= 700;
    float closedValue = 210;
    int eyeIndex = 0;
    bool gameOver_bool = false;
    // Devuelve true si se esta en medio de una transicion
    [HideInInspector] public bool duringTransition = false;

    public static GameManager instance;
    public static GameManager GetInstance() { return instance; }
    private void Awake()
    {
        // Singleton
        if (instance == null) instance = this;
        else { Destroy(gameObject); return; }
        DontDestroyOnLoad(gameObject);

        // Todos los elementos de la transicion empiezan inactivos
        level_Text.gameObject.SetActive(false);
        levelNum_Text.gameObject.SetActive(false);
        icon.gameObject.SetActive(false);
    }

    public void IncreaseEyeIndex()
    {
        eyeIndex++;
        if (eyeIndex >= eyeList.Count) eyeIndex = eyeList.Count;
    }
    public int GetEyeIndex()
    {
        return eyeIndex;
    }
    public List<Eye> getEyes()
    {
        return eyeList;
    }

    public void setGameOver(bool active){
        gameOver_Object.SetActive(active);
        player.GetComponent<ShipController>().enabled= !active;
        gameOver_bool= active;
    }
    public bool isGameOver() {
        return gameOver_bool;
    }
    #region Change Scene
    public void ChangeScene(string sceneName)
    {
        StartCoroutine(ChangeSceneCoroutine(-1, sceneName));
        icon.gameObject.SetActive(true);
    }

    public void ChangeScene(int level)
    {
        level_Text.gameObject.SetActive(true);
        levelNum_Text.gameObject.SetActive(true);
        levelNum_Text.text = level.ToString();

        StartCoroutine(ChangeSceneCoroutine(level, ""));
    }

    float transitionsMove;
    // Se encarga de mostrar la transicion y cambiar de escena
    IEnumerator ChangeSceneCoroutine(int level, string sceneName)
    {
        #region Transition animation
        // Close transition
        left.anchoredPosition = new Vector3(-openValue, left.anchoredPosition.y);
        //left.position = new Vector3(leftOpen, left.position.y);
        right.anchoredPosition = new Vector3(openValue, right.anchoredPosition.y);

        DOTween.To(x => transitionsMove = x, openValue, closedValue, transitionDuration)
            .OnUpdate(updateTransitionWalls).SetUpdate(true).SetEase(Ease.OutCirc);

        icon.rectTransform.anchoredPosition = new Vector2(0, 500);
        icon.rectTransform.DOAnchorPosY(0, transitionDuration)
            .SetUpdate(true).SetEase(Ease.OutCirc);
        icon.transform.DORotate(new Vector3(0, 0, 360), transitionDuration, RotateMode.FastBeyond360)
            .SetUpdate(true).SetEase(Ease.OutBack);
        //.SetUpdate(true).SetEase(Ease.OutSine);

        duringTransition = true;
        yield return new WaitForSecondsRealtime(transitionDuration+.5f);

        DOTween.To(x => transitionsMove = x, closedValue, openValue, transitionDuration)
            .OnUpdate(updateTransitionWalls).SetUpdate(true).SetEase(Ease.InCirc);

        icon.rectTransform.DOAnchorPosY(-500, transitionDuration)
            .SetUpdate(true).SetEase(Ease.InCirc);
        icon.transform.DORotate(new Vector3(0, 0, 360), transitionDuration, RotateMode.FastBeyond360)
            .SetUpdate(true).SetEase(Ease.InBack);
        //.SetUpdate(true).SetEase(Ease.InSine);

        #endregion

        Time.timeScale = 1;

        if (sceneName != "")
        {
            AudioManager_PK.GetInstance().ChangeBackgroundMusic(sceneName);
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            if (SceneManager.GetActiveScene().name == "MainMenu_Scene")
                AudioManager_PK.GetInstance().ChangeBackgroundMusic("Gameplay");
            SceneManager.LoadScene("Level_" + level);
        }

        yield return new WaitForSecondsRealtime(transitionDuration);
        duringTransition = false; // Termina la transicion

        // Poner todo de vuelta a false
        level_Text.gameObject.SetActive(false);
        levelNum_Text.gameObject.SetActive(false);
        icon.gameObject.SetActive(false);
    }

    void updateTransitionWalls()
    {
        left.anchoredPosition = new Vector3(-transitionsMove, left.anchoredPosition.y);
        right.anchoredPosition = new Vector3(transitionsMove, right.anchoredPosition.y);
    }

    #endregion
}
