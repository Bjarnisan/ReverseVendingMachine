using ReverseVendingMachine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReverseVendingMachine.Interfaces
{
    internal interface IDepositMachineLogger : IDisposable
    {
        Task LogItemDeposited(ScannedItem item);
        Task LogReceptPrinted(DepositingSession depositingSession);
    }
}
