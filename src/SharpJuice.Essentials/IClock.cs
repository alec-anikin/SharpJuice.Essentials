using System;

namespace SharpJuice.Essentials
{
    public interface IClock
    {
        DateTimeOffset Now { get; }
    }
}
