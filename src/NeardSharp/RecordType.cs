namespace NeardSharp
{
    /// <summary>
    /// Type of the <see cref="NfcRecord"/>.
    /// </summary>
    public enum RecordType
    {
        /// <summary>
        /// Invalid record, should not be used.
        /// </summary>
        Invalid,
        /// <summary>
        /// Record is a Smart Poster.
        /// </summary>
        SmartPoster,
        /// <summary>
        /// Record is text only.
        /// </summary>
        Text,
        /// <summary>
        /// Record contains URI object
        /// </summary>
        URI,
        /// <summary>
        /// Record contains request to start handover protocol.
        /// </summary>
        HandoverRequest,
        /// <summary>
        /// Record contains reply to previously received <see cref="HandoverRequest"/>.
        /// </summary>
        HandoverSelect,
        /// <summary>
        /// Record contains unique identification of an alternative carrier technology in a <see cref="HandoverRequest"/> message.
        /// </summary>
        HandoverCarrier,
        /// <summary>
        /// Record contains android package information.
        /// </summary>
        AAR,
        /// <summary>
        /// Record contains data defined by <see cref="NfcRecord.MIMEType"/>.
        /// </summary>
        MIME
    }
}
