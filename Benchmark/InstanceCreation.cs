namespace Benchmark
{
    /// <summary>
    /// Type of creation instance
    /// </summary>
    public enum InstanceCreation
    {
        /// <summary>
        /// Create instance for each run of method
        /// </summary>
        Scoped,

        /// <summary>
        /// Create instance for all methods
        /// </summary>
        Transient,

        /// <summary>
        /// Create instance for all benchmark
        /// </summary>
        Singleton
    }
}
