namespace ChoyUtilities {

    public enum EMotion : byte {

        /// <summary>
        ///     Straight forward linear motion
        /// </summary>
        Linear,

        /// <summary>
        ///     Represents a motion type where the transition follows a squared function
        ///     for speeding up progress, starting slow and increasing speed towards the end.
        /// </summary>
        SqrEaseIn,

        /// <summary>
        ///     Represents a cubic easing-in motion where the rate of change starts slowly
        ///     and speeds up progressively in a more extreme way than <see cref="SqrEaseIn"/>.
        /// </summary>
        CubeEaseIn,

        /// <summary>
        ///     Eases out motion with a square root curve, starting quickly
        ///     and slowing down toward the end.
        /// </summary>
        SqrtEaseOut,

        /// <summary>
        ///     Applies a cubic easing-out motion, starting fast
        ///     and decelerating towards the end in a more extreme way than <see cref="SqrtEaseOut"/>.
        /// </summary>
        CubedEaseOut,

        /// <summary>
        ///     Smoothly decelerates motion as it approaches the end,
        ///     following a quadratic curve.
        /// </summary>
        QuadraticEaseOut,

        /// <summary>
        ///     Represents a motion that follows a parabolic trajectory pattern.
        /// </summary>
        Parabola,

        /// <summary>
        ///     Represents a triangular motion pattern where the value
        ///     increases linearly to a peak and then decreases linearly
        ///     symmetrically.
        /// </summary>
        Triangle,

        /// <summary>
        ///     A spring-like oscillation motion that
        ///     gradually decreases in amplitude as it moves towards the target.
        /// </summary>
        ElasticOut,

        /// <summary>
        ///     Represents a motion type that simulates a bouncing effect
        ///     as it eases out towards the target.
        /// </summary>
        BounceOut,

        /// <summary>
        ///     Represents a motion type that combines <see cref="SqrEaseIn"/> and <see cref="SqrtEaseOut"/>,
        ///     creating a gradual acceleration at the beginning and a smooth deceleration
        ///     towards the end of the motion.
        /// </summary>
        SqrEaseInOut,

        /// <summary>
        ///     Motion that combines <see cref="CubeEaseIn"/> and <see cref="CubedEaseOut"/>,
        ///     with a cubic rate of change in the middle.
        /// </summary>
        CubeEaseInOut

    }

}