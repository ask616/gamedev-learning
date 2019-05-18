using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
  public Transform player;
  public GameEnding gameEnding;

  bool m_IsPlayerInRange;

  private void OnTriggerEnter(Collider other)
  {
    if (other.transform == player)
    {
      m_IsPlayerInRange = true;
    }
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.transform == player)
    {
      m_IsPlayerInRange = false;
    }
  }

  private void Update()
  {
    if (m_IsPlayerInRange)
    {
      Vector3 playerDirection = player.position - transform.position + Vector3.up;
      Ray ray = new Ray(transform.position, playerDirection);

      RaycastHit raycastHit;

      if (Physics.Raycast(ray, out raycastHit))
      {
        if (raycastHit.collider.transform == player)
        {
          gameEnding.CaughtPlayer();
        }
      }
    }
  }
}
