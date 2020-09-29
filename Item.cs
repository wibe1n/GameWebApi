using System;
using System.ComponentModel.DataAnnotations;

namespace GameWebApi{


    public class Item
    {
        public Guid itemId {get; set;}

        [Range(0, 99)]
        public int Level { get; set; }
        public string Type{ get; set; }
        [CheckTheDate]
        public DateTime CreationDate{ get; set; }
    }
    public enum validItemType {sword, shield, potion};
}