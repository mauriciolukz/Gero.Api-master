using System;

namespace Gero.API.Controllers.Interfaces
{
    interface IArchive<T>
    {
        T Archive(Guid id);
    }
}
