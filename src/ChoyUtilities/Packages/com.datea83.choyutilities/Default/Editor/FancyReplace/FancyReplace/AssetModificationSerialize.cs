using System;

// ReSharper disable once CheckNamespace
namespace ChoyUtilities.Editor {
    
    [Serializable]
    public struct AssetModificationSerialize {
        public DoubleStringSerialize[] assetModified;
    }
    
    [Serializable]
    public struct DoubleStringSerialize {
        public string idPath;
        public string texturePath;
    }
}