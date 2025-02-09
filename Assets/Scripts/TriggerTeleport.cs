using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTeleport : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform firstPosition;
    public Transform secondPosition;

  
        // Start is called before the first frame update


        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("fdffffffffffffffffffffffffffffffffffffffffffffffff");
            if (collision.CompareTag("Player"))
            {

                if (gameObject.transform.position == firstPosition.position)
                {
                    collision.transform.position = secondPosition.position;
                }
                else if (gameObject.transform.position == secondPosition.position)
                {
                    collision.transform.position = firstPosition.position;
                }
            }
        }
    }

