using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
  public float fadeDuration = 1f;
  public float displayImageDuration = 1f;
  public GameObject player;
  public CanvasGroup exitBackgroundImageCanvasGroup, caughtBackgroundImageCanvasGroup;

  bool m_IsPlayerAtExit, m_IsPlayerCaught;
  float m_Timer;

  public void CaughtPlayer()
  {
    m_IsPlayerCaught = true;
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject == player)
    {
      m_IsPlayerAtExit = true;
    }
  }

  private void Update()
  {
    if (m_IsPlayerAtExit)
    {
      EndLevel(exitBackgroundImageCanvasGroup, false);
    }
    else if (m_IsPlayerCaught)
    {
      EndLevel(caughtBackgroundImageCanvasGroup, true);
    }
  }

  private void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart)
  {
    m_Timer += Time.deltaTime;
    imageCanvasGroup.alpha = m_Timer / fadeDuration;

    if (m_Timer > fadeDuration + displayImageDuration)
    {
      if (doRestart)
      {
        SceneManager.LoadScene(0);
      }
      else
      {
        Application.Quit();
      }
    }
  }
}
