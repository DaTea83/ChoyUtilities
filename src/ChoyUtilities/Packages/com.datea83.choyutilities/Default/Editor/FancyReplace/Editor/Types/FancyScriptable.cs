using UnityEditor;

// ReSharper disable once CheckNamespace
namespace ChoyUtilities.Editor {
    
    [InitializeOnLoad]
    public class FancyScriptable {
        
        [MenuItem("Assets/Fancy/Scriptable Object", false, 100)]
        public static void CustomModificationMenuItem() {
            
        }
        public static bool ValidateCustomModificationMenuItem() {
            throw new System.NotImplementedException();
        }
        public static bool IsSelectedObjectScriptableObject(out string path) {
            throw new System.NotImplementedException();
        }
    }
}