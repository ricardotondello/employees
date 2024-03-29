﻿namespace Employee.Domain;

public class Region
{
    public int Id { get; }
    public string Name { get; }
    public Region Parent { get; }

    private Region(int id, string name, Region parent)
    {
        Id = id;
        Name = name;
        Parent = parent;
    }

    public static Region Create(int id, string name, Region parent) => new(id, name, parent);
    public static Region Create(int id) => new(id, string.Empty, null!);
}