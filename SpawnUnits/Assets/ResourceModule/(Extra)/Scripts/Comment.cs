using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advanced
{
    //This class is only here so I can leave a comment on a gameobject.
    public class Comment : MonoBehaviour
    {
        [TextArea(10,30)]
        public string Text;
    }

}
