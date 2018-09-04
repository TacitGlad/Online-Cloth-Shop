using System;

namespace Obaju.Services.Enums
{
    [Flags]
    public enum IssueStatusFilter
    {
        All = 1,
        OpenedOnly = 2,
        ClosedOnly = 4,
    }
}
