using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSFXManager : MonoBehaviour
{
    [SerializeField] private AudioData exhaleSound;
    [SerializeField] private AudioData footstepSound;

    [SerializeField, Range(0.2f, 1.5f)] public float footstepFrequency = 0.2f;

    private Coroutine _walkingCoroutine;

    public void Exhale()
    {
        SFXManager.PlaySoundAt(exhaleSound, transform.position);
    }

    private void Update()
    {
        if( Keyboard.current.nKey.wasPressedThisFrame )
        {
            Walk(true );
        }

        if( Keyboard.current.mKey.wasPressedThisFrame )
        {
            Walk(!true );
        }
    }

    public void Walk(bool isWalking)
    {
        if (isWalking)
        {
            _walkingCoroutine = StartCoroutine(Walking());
        }
        else
        {
            if(_walkingCoroutine != null) StopCoroutine(_walkingCoroutine);
            _walkingCoroutine = null;
        }
    }

    private IEnumerator Walking()
    {
        while (true)
        {
            SFXManager.PlaySoundAt(footstepSound, transform.position);
            yield return new WaitForSeconds(footstepFrequency);
        }
    }
}
