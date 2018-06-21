using System;

namespace SharpJuice.Essentials
{
    public class FrozenClock : IClock
    {
        public FrozenClock()
        {
            Now = DateTimeOffset.UtcNow;
        }

        public DateTimeOffset Now { get; }
    }
}
