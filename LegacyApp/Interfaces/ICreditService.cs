using System;

namespace LegacyApp.Interfaces;

public interface ICreditService : IDisposable
{
    int GetCreditLimit(string lastName, DateTime dateOfBirth);
}