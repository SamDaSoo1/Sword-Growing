using TMPro;
using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tmp1;
    [SerializeField] TextMeshProUGUI tmp2;
    [SerializeField] TextMeshProUGUI tmp3;
    [SerializeField] TextMeshProUGUI tmp4;
    [SerializeField] ParticleSystem effect1;
    [SerializeField] ParticleSystem effect2;
    [SerializeField] ParticleSystem effect3;
    [SerializeField] ParticleSystem effect4;
    [SerializeField] ParticleSystem effect5;
    [SerializeField] GameStartBtn gameStartBtn;

    void Start()
    {
        tmp1.enabled = false;
        tmp2.enabled = false;
        tmp3.enabled = false;
        tmp4.enabled = false;
        effect1.Stop();
        effect2.Stop();
        effect3.Stop();
        effect4.Stop();
        effect5.Stop();
    }

    public IEnumerator CoActions()
    {
        yield return new WaitForSeconds(1f);
        effect1.Play(); 
        SoundManager.Instance.PlaySFX(Sfx.Effect);

        yield return new WaitForSeconds(0.1f);
        tmp1.enabled = true;

        yield return new WaitForSeconds(0.3f);
        effect2.Play(); 
        SoundManager.Instance.PlaySFX(Sfx.Effect);

        yield return new WaitForSeconds(0.1f);
        tmp2.enabled = true;

        yield return new WaitForSeconds(0.3f);
        effect3.Play(); 
        SoundManager.Instance.PlaySFX(Sfx.Effect);

        yield return new WaitForSeconds(0.1f);
        tmp3.enabled = true;

        yield return new WaitForSeconds(0.3f);
        effect4.Play(); 
        SoundManager.Instance.PlaySFX(Sfx.Effect);

        yield return new WaitForSeconds(0.1f);
        tmp4.enabled = true;

        yield return new WaitForSeconds(0.6f);
        effect5.Play(); 
        SoundManager.Instance.PlaySFX(Sfx.Effect);

        yield return new WaitForSeconds(0.1f);
        gameStartBtn.Appear();
    }
}
