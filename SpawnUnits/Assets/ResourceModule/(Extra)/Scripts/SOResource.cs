using UnityEngine;
using UnityEngine.Events;

// 'Advanced' material wasn't discussed in each class
// You can check this code out if you want to learn more.
namespace Advanced
{
    /// <summary>
    /// A ScriptableObject is a bit like a monobehaviour,
    /// but instead of existing in the scene you create
    /// them as assets in your project directory.
    /// The 'CreateAssetMenu' adds a button so you can instantiate a new asset.
    /// </summary>
    [CreateAssetMenu(menuName = "Advanced/Resource")]
    public class SOResource : ScriptableObject
    {
        // Important note! Since you are now responsible for the lifecycle of this object
        // This value will not be reset during play. You'll need to write a script to do this.
        public int Amount;

        public UnityEvent OnValueChanged = new UnityEvent(); // By setting a default value, this will never be null.
    
        public void AddAmount(int value)
        {
            if (value < 0)
            {
                Debug.LogError("Can't add negative values!");
            }
            else
            {
                Amount += value;
                updateUI();
            }
        }
    
        public void RemoveAmount(int value)
        {
            if (value < 0)
            {
                Debug.LogError("Can't remove negative values!");
            }
            else
            {
                Amount -= value;
                updateUI();
            }
        }

        void updateUI()
        {
            OnValueChanged.Invoke();
        }
    }

}

