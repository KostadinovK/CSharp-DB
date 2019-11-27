using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace PetStore.Models.Enums
{
    public enum OrderStatus
    {
        Received = 0,
        Pending = 1,
        Canceled = 2,
        InProgress = 3,
        Completed = 4
    }
}
