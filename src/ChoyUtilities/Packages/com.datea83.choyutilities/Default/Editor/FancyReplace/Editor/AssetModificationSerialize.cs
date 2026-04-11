using System;

// ReSharper disable once CheckNamespace
namespace ChoyUtilities.Editor {
    
    [Serializable]
    public struct AssetModificationSerialize {
        public MenuItemSerialize[] assetModified;
        public Floater color;
    }
    
    [Serializable]
    public struct MenuItemSerialize {
        public Floater idType;
        public string idPath;
        public string texturePath;
    }
}