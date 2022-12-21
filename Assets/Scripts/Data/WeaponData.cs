using DefaultNamespace;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon", fileName = "SimpleWeapon")]
public class WeaponData : ItemData
{
    public Sprite weaponIcon;
    public float recoil;
    public GameObject bullet;
    public int Damage;
    public float Force;
    public float reloadTime;
    public Vector3 shootPos;
    public AnimationClip shootAnim;
    public AnimationClip reloadAnim;
    public AnimationClip idleAnim;
    public AnimationClip ShootParticle;
    public AnimationClip Sleeve;
    public ShootLogic shoot;
    public SimpleSound chest;
    public SimpleSound barrel;
    public SimpleSound shootSound;
    public SimpleSound bodySound;
    public SimpleSound pickUp;
    public SimpleSound noAmmo;
    private AudioSource _audioSource;
    public int bulletCount;
    public LayerMask HitLayer;

    // public void Visit(IChestSound sound)
    // {
    //    PlaySound(chest.audioClip,chest.volumeSound);
    // }
    //
    // public void Visit(IBarrelSound sound)
    // {
    //    PlaySound(barrel.audioClip,barrel.volumeSound);
    // }
    // private void PlaySound(AudioClip clip,float volume )
    // {
    //    _audioSource.volume = volume;
    //    _audioSource.PlayOneShot(clip);
    //    _audioSource.Play();
    // }
    // public void Visit(IBodySound sound)
    // {
    //    PlaySound(bodySound.audioClip,bodySound.volumeSound);
    // }
}