namespace FlutterSharp.UI
{
    /// An opaque object representing a batch of semantics updates.
    ///
    /// To create a SemanticsUpdate object, use a [SemanticsUpdateBuilder].
    ///
    /// Semantics updates can be applied to the system's retained semantics tree
    /// using the [Window.updateSemantics] method.
    public class SemanticsUpdate : NativeFieldWrapperClass2
    {
        /// This class is created by the engine, and should not be instantiated
        /// or extended directly.
        ///
        /// To create a SemanticsUpdate object, use a [SemanticsUpdateBuilder].
        public SemanticsUpdate()
        {
        }

        /// Releases the resources used by this semantics update.
        ///
        /// After calling this function, the semantics update is cannot be used
        /// further.
        public void Dispose()
        {
            // TODO :  native 'SemanticsUpdate_dispose';
        }
    }
}
