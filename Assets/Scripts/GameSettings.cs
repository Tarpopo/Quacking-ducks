
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private TriggerButton _sound;
    private Save _save;

    private void Start()
    {
        _save = Toolbox.Get<Save>();
        _sound.Start();
        if (_save._save.Volume == false)
        {
            _sound.ClickOnButton();
            _sound.OnPointerUp(default(PointerEventData));
        }
        ChangeSound();
    }

    public void ChangeSound()
    {
        _save._save.Volume = _sound._isTriggerActive==false;
        var volume = _sound._isTriggerActive == false ? 0 : -80;
        _audioMixer.SetFloat("Volume",volume);
        _audioMixer.SetFloat("MainVolume",volume);
        _save.SaveAll();
    }
}
