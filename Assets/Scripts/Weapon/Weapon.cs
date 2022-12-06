using DefaultNamespace;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class Weapon: MonoBehaviour,ITick
{
    [SerializeField] private WeaponData _baseWeaponData;
    [SerializeField] private Transform _shootPos;
    [SerializeField] private TMP_Text _bulletText;
    [SerializeField] private Animator _weaponAnim;

    [SerializeField] private float _recoil;
    
    private WeaponItem _baseWeapon;
    private SoundVisitorComponent _soundVisitor;
    private Transform _transform;
    private AudioSource _audioSource;
    private WeaponItem _weapon;
    private Rigidbody2D _rigidbody;
    private float _reloadTime;
    
    
    private void Start()
    {
        ManagerUpdate.AddTo(this);
        _transform = transform;
        _soundVisitor = GetComponent<SoundVisitorComponent>();
        _audioSource = GetComponent<AudioSource>();
        _baseWeapon=GetComponentInChildren<WeaponItem>();
        _rigidbody = GetComponent<Rigidbody2D>();
        print("its start");
        //if (_shootPos == null) _shootPos = _transform;
    }

    public void SetBaseWeapon(bool changeSortCount = false)
    {
        Start();
        //_weapon = Instantiate(_baseWeapon,_transform.position,quaternion.identity,_transform);
        //Destroy(_weapon.GetComponent<Rigidbody2D>()); 
        _weapon = _baseWeapon;
        print(_weapon.WeaponData);
        //_baseWeapon = _weapon;
        //_weapon.WeaponData = _baseWeaponData;
        _weapon.OnStart();
        Destroy(_weapon.GetComponent<BoxCollider2D>());
        if (_weaponAnim) _weapon.Animator = _weaponAnim;
        else _weaponAnim = _weapon.GetWeaponAnimator();
        _reloadTime = _weapon.WeaponData.reloadTime;
        _soundVisitor.SetWeaponData(_weapon.WeaponData);
        _weaponAnim.Play(_weapon.WeaponData.idleAnim.name);
        _weapon.WeaponData.shoot.SetParameters(_weaponAnim,_weapon,_audioSource,_rigidbody);
        if(changeSortCount)_baseWeapon.ChangeSortingSprite(5);
    }

    private void SetTextBullet(int count)
    {
        if(_bulletText!=null) _bulletText.text = count.ToString();
    }

    public void Shoot(ItemsSpawner itemsSpawner)
    {
        //print(_reloadTime);
        if (_reloadTime > 0) return;
        //TakeRecoil();
        _weapon.WeaponData.shoot.Shoot(_soundVisitor,itemsSpawner);
        _reloadTime = _weapon.WeaponData.reloadTime;
        SetTextBullet(_weapon.CurrentBullet);
    }
    
    // public void MeleeShoot(Loader loader)
    // {
    //     _weaponAnim.Play(_weapon.WeaponData.shootAnim.name);
    //     _weapon.WeaponData.shoot.Shoot(_soundVisitor,loader);
    //     _audioSource.PlaySound(_weapon.WeaponData.shootSound); 
    // }

    // public void TryShoot(Loader loader)
    // {
    //     if (_weapon.CurrentBullet > 0)
    //     { 
    //         _audioSource.PlaySound(_weapon.WeaponData.shootSound);
    //         ParticleManager.PlayParticle(_weapon.WeaponData.Sleeve,_shootPos.position,scale:(int)_transform.localScale.x);
    //         ParticleManager.PlayParticle(_weapon.WeaponData.ShootParticle,_shootPos.position,scale:(int)_transform.localScale.x);
    //         _weapon.CurrentBullet--;
    //         _weaponAnim.Play(_weapon.WeaponData.shootAnim.name);
    //         _weapon.WeaponData.shoot.Shoot(_soundVisitor,loader);
    //         _audioSource.PlaySound(_weapon.WeaponData.shootSound);
    //     }
    //     _audioSource.PlaySound(_weapon.WeaponData.noAmmo);
    // }

    public bool IsBulletShoot()
    {
        return _reloadTime<=0 && _weapon.CurrentBullet != 0;
    }

    public bool TryQuitWeapon()
    {
        if (IsBaseWeapon()) return false;
        _weapon.gameObject.SetActive(true);
        //print(_transform.parent.gameObject);
        print(_transform.position);
        _weapon.QuitItem(_transform.position);
        _weapon = _baseWeapon;
        _soundVisitor.SetWeaponData(_weapon.WeaponData);
        SetTextBullet(0);
        _baseWeapon.ChangeSortingSprite(5);
        return true;
    }

    public void Tick()
    {
        if (_reloadTime > 0) _reloadTime -= Time.deltaTime;
    }

    // private void TakeRecoil()
    // {
    //     _rigidbody.AddForce(Vector2.left * (_transform.localScale.x * _weapon.WeaponData.recoil),ForceMode2D.Impulse);
    //     //_rigidbody.AddForce(Vector2.left * (_transform.localScale.x * _recoil),ForceMode2D.Impulse);
    // }

    public bool IsBaseWeapon()
    {
        return _weapon == _baseWeapon;
    }

    public void SetWeapon(WeaponItem weapon)
    {
        _weaponAnim = weapon.GetWeaponAnimator();
        _weapon = weapon;
        _audioSource.PlaySound(_weapon.WeaponData.pickUp);
        _weaponAnim.Play(_weapon.WeaponData.idleAnim.name);
        _soundVisitor.SetWeaponData(_weapon.WeaponData);
        _weapon.transform.position = _transform.position;
        _weapon.transform.localScale = _transform.localScale;
        //_shootPos.localPosition = _weapon.WeaponData.shootPos;
        _weapon.WeaponData.shoot.SetParameters(_weaponAnim,_weapon,_audioSource,_rigidbody);
        SetTextBullet(_weapon.CurrentBullet);
        _baseWeapon.ChangeSortingSprite(7);
    }
    public void SetAudioSource(AudioSource audioSource)
    {
        _audioSource = audioSource;
    }
    
    
}