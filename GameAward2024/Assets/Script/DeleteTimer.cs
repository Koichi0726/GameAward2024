using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteTimer : MonoBehaviour
{
    [SerializeField] private float deleteTime;
    float countdownTimer;

    // Start is called before the first frame update
    void Start()
    {
        countdownTimer = deleteTime;
    }

    // Update is called once per frame
    void Update()
    {
        countdownTimer -= Time.deltaTime;
        if (countdownTimer < 0)
        {
            Destroy(gameObject);
        }
    }
}
