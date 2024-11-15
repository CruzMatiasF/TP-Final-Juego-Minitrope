using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;
using System.Collections;

public class TestSuite
{
    private GameObject guiManagerObj;
    private GUIManager guiManager;
    private GameObject gameOverPanel;
    private GameObject gameManagerObj;
    private GameObject boardManagerObj;
    private GUISizePingPong sizePingPong;
    private SFXManager sfxManager;

    [SetUp]
    public void SetUp()
    {
        // Setup para GUIManager
        guiManagerObj = new GameObject("GUIManager");
        guiManager = guiManagerObj.AddComponent<GUIManager>();

        gameManagerObj = new GameObject("GameManager");
        gameManagerObj.AddComponent<GameManager>();
        GameManager.instance = gameManagerObj.GetComponent<GameManager>();

        boardManagerObj = new GameObject("BoardManager");
        boardManagerObj.AddComponent<BoardManager>();
        BoardManager.instance = boardManagerObj.GetComponent<BoardManager>();

        gameOverPanel = new GameObject("GameOverPanel");
        gameOverPanel.SetActive(false);
        guiManager.gameOverPanel = gameOverPanel;

        guiManager.scoreTxt = new GameObject("ScoreText").AddComponent<Text>();
        guiManager.moveCounterTxt = new GameObject("MoveCounterText").AddComponent<Text>();
        guiManager.yourScoreTxt = new GameObject("YourScoreText").AddComponent<Text>();
        guiManager.highScoreTxt = new GameObject("HighScoreText").AddComponent<Text>();

        // Setup para GUISizePingPong
        GameObject sizePingPongObj = new GameObject();
        sizePingPong = sizePingPongObj.AddComponent<GUISizePingPong>();
        sizePingPong.minScale = new Vector3(0.5f, 0.5f, 0.5f);

        // Setup para SFXManager
        GameObject sfxManagerObj = new GameObject();
        sfxManager = sfxManagerObj.AddComponent<SFXManager>();
        AudioSource audioSource1 = sfxManagerObj.AddComponent<AudioSource>();
        AudioSource audioSource2 = sfxManagerObj.AddComponent<AudioSource>();
        AudioSource audioSource3 = sfxManagerObj.AddComponent<AudioSource>();
        sfxManager.sfx = new AudioSource[] { audioSource1, audioSource2, audioSource3 };
    }

    [TearDown]
    public void TearDown()
    {
        // Destruir objetos creados
        GameObject.Destroy(guiManagerObj);
        GameObject.Destroy(gameOverPanel);
        GameObject.Destroy(gameManagerObj);
        GameObject.Destroy(boardManagerObj);
    }

    // Pruebas de GUIManager
    [Test]
    public void ActivaciónGameOver()
    {
        guiManager.GameOver();
        Assert.IsTrue(guiManager.gameOverPanel.activeSelf, "El panel de Game Over no se activó.");
    }

    [Test]
    public void ActualizaciónPuntuación()
    {
        int newScore = 100;
        guiManager.Score = newScore;
        Assert.AreEqual(newScore.ToString(), guiManager.scoreTxt.text, "El texto de puntaje no se actualizó correctamente.");
    }

    [Test]
    public void PruebaIncrementoPuntaje()
    {
        guiManager.Score = 50;
        guiManager.Score += 50;
        Assert.AreEqual("100", guiManager.scoreTxt.text);
    }

    [Test]
    public void PruebaActualizaciónMejorPuntaje()
    {
        PlayerPrefs.SetInt("HighScore", 0);
        guiManager.Score = 200;
        guiManager.GameOver();
        Assert.AreEqual("New Best: 200", guiManager.highScoreTxt.text, "El texto de HighScore no se actualizó correctamente.");
    }

    //// Pruebas de GUISizePingPong
    //[UnityTest]
    //public IEnumerator TestPingPongScaling()
    //{
    //    RectTransform rectTransform = sizePingPong.GetComponent<RectTransform>();
    //    Vector3 initialScale = rectTransform.localScale;
    //    yield return new WaitForSeconds(sizePingPong.changeDelay * 10);
    //    Assert.AreNotEqual(initialScale, rectTransform.localScale);
    //}

    // Pruebas de SFXManager
    [Test]
    public void TestPlaySFX()
    {
        Assert.DoesNotThrow(() => sfxManager.PlaySFX(SFXManager.Clip.Select));
    }
}
