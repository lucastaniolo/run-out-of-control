using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    public List<SpriteByInputType> SpritesByInputType;
    
    public static SpriteManager Instance;
    
    private void Awake()
    {
        if (Instance)
            Destroy(this);

        Instance = this;
    }

    public Sprite GetSprite(InputType inputType) => SpritesByInputType.First(s => s.InputType == inputType).Sprite;
}