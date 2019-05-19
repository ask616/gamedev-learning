﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
  public float fadeDuration = 1f;
  public float displayImageDuration = 1f;
  public GameObject player;
  public CanvasGroup exitBackgroundImageCanvasGroup, caughtBackgroundImageCanvasGroup;
  public AudioSource exitAudio, caughtAudio;

  bool m_IsPlayerAtExit, m_IsPlayerCaught, m_HasAudioPlayed;
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
      EndLevel(exitBackgroundImageCanvasGroup, false, exitAudio);
    }
    else if (m_IsPlayerCaught)
    {
      EndLevel(caughtBackgroundImageCanvasGroup, true, caughtAudio);
    }
  }

  private void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
  {
    if (!m_HasAudioPlayed)
    {
      audioSource.Play();
      m_HasAudioPlayed = true;
    }

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
