using System;
using System.ComponentModel.DataAnnotations;

namespace GameWebApi{

public class NewItem
{
    [EnumDataType(typeof(validItemType))]
    public string Type { get; set; }

    [Range(0, 99)]
    public int lvl {get; set;}
}

}