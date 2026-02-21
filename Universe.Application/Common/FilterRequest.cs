using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.Common;

public record FilterRequest 
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public string? SearchValue { get; init; }
    public string? SortColumn { get; init; }
    public string? SortDirection { get; init; } = "ASC";
}