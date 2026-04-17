namespace ChoyUtilities {

    public partial class TeaMotion {

        public enum ETransformType : byte {

            /// <summary>
            /// Displace the transform
            /// </summary>
            Move = 1,
            /// <summary>
            /// Rotate the transform
            /// </summary>
            Rotate = 1 << 1,
            /// <summary>
            /// Scale the transform
            /// </summary>
            Scale = 1 << 2,
            /// <summary>
            /// Do all for <see cref="Move"/>, <see cref="Rotate"/> and <see cref="Scale"/>
            /// </summary>
            Transform = Move | Rotate | Scale
            
        }

    }

}