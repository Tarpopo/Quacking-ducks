using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/ItemData")]
public class ItemData: ScriptableObject
{
    public Vector2 offsetColider;
    public Vector2 sizeColider;
    public Sprite fullSprite;
    public Sprite halfSprite;
    public AnimationClip destroyAnim;
    public int health;
    public SimpleSound full;
    public SimpleSound destroy;
}
