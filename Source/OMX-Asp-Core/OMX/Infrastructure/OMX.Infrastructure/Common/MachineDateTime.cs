namespace OMX.Infrastructure.Common
{
    using OMX.Common;
    using System;

    public class MachineDateTime : IDateTime
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
