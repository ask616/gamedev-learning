using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  public float turnSpeed = 20f;
  Vector3 m_Movement;
  Quaternion m_Rotation = Quaternion.identity;
  Animator m_Animator;
  AudioSource m_AudioSource;
  Rigidbody m_RigidBody;

  // Start is called before the first frame update
  void Start()
  {
    m_Animator = GetComponent<Animator>();
    m_RigidBody = GetComponent<Rigidbody>();
    m_AudioSource = GetComponent<AudioSource>();
  }

  void FixedUpdate()
  {
    float horizontal = Input.GetAxis("Horizontal"),
          vertical = Input.GetAxis("Vertical");

    m_Movement.Set(horizontal, 0f, vertical);
    m_Movement.Normalize();

    bool hasHorizontalMovement = !Mathf.Approximately(horizontal, 0f),
         hasVerticalMovement = !Mathf.Approximately(vertical, 0f);

    bool isWalking = hasHorizontalMovement || hasVerticalMovement;

    // Sets animator to walking if needed
    m_Animator.SetBool("IsWalking", isWalking);

    if (isWalking && !m_AudioSource.isPlaying)
    {
      m_AudioSource.Play();
    }
    else if (!isWalking && m_AudioSource.isPlaying)
    {
      m_AudioSource.Stop();
    }

    // Rotate the current forward vector towards the new direction, multiply turn speed by frame rate
    Vector3 newForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);

    // Set the new rotation to look towards the new direction
    m_Rotation = Quaternion.LookRotation(newForward);
  }

  private void OnAnimatorMove()
  {
    // Set position to the new position x delta position
    m_RigidBody.MovePosition(m_RigidBody.position + m_Movement * m_Animator.deltaPosition.magnitude);
    m_RigidBody.MoveRotation(m_Rotation);
  }
}
