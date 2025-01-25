using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalCheck : MonoBehaviour
{
    public bool isBubbleInFinalCheck = false;
    public bool isBubbleInWinFinalCheck = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isBubbleInFinalCheck = true;
            AudioManager.instance.PlaySFX(SoundType.NextLevelSFX, 2f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isBubbleInFinalCheck = true;

            AudioManager.instance.PlaySFX(SoundType.NextLevelSFX, 2f);

            isBubbleInWinFinalCheck = true;
            WinLoseCondition.instance.WinGame();
        }
    }
}
