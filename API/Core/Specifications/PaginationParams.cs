﻿namespace Core.Specifications;

public class PaginationParams
{
    private const int MaxPageSize = 25;

    private int _pageSize = 5;
    
    public int PageIndex { get; set; } = 1;

    public int PageSize
    {
        get => _pageSize; 
        set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
    }
}