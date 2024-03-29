﻿using System.Text.Json.Serialization;

namespace Employee.Contracts.Output;

public class Employee
{
    public Guid Id { get; }
    public string Name { get; }
    public string Surname { get; }
    public Region Region { get; }

    [JsonConstructor]
    private Employee(Guid id, string name, string surname, Region region)
    {
        Id = id;
        Name = name;
        Region = region;
        Surname = surname;
    }

    public static Employee Create(Guid id, string name, string surname, Region region) =>
        new (id, name, surname, region);
}