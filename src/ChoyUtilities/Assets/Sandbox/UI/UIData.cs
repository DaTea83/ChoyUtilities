using Unity.Entities;

namespace ChoyUtilities.Entities {

    //TODO
    public struct UIData : IComponentData {

        public byte ParentId;
        public byte OwnId;

    }

    //TODO
    [InternalBufferCapacity(1)]
    public struct UIBuffer : IBufferElementData {

        public byte Value;

    }

}