using UnityEngine;
using UnityEngine.UI;

// 'Advanced' material wasn't discussed in each class
// You can check this code out if you want to learn more.
namespace Advanced
{
    //The only difference in this class from the other ResourceUI is that
    //this one refers to Advanced.Resource instead of a normal resource.
    public class AdvResourceUI : MonoBehaviour {

        public Text Value;
        public Text Label;
    
        public SOResource resource;

        private void Awake()
        {
            if (resource != null)
            {
                resource.OnValueChanged.AddListener(UpdateUI);
            }

        }

        void Start()
        {
            Label.text = resource.name;
            Value.text = resource.Amount.ToString();
        }

        public void UpdateUI()
        {
            Value.text = resource.Amount.ToString();
        }
    }

}
