using System;

namespace SharpJuice.Essentials
{
    public class Clock : IClock
    {
        public DateTimeOffset Now => DateTimeOffset.UtcNow;
    }
}
