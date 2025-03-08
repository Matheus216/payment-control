using System.Text.RegularExpressions;
using payment_control_domain.Exceptions;

namespace payment_control_domain.Entities;

public class ClientEntity
{
    public ClientEntity(string name, string email)
    {
        this.Name = name;
        this.Email = email;

        this.IsValid();
    }

    public ClientEntity(int id, string name, string email)
    {
        this.Id = id;
        this.Name = name;
        this.Email = email;

        this.IsValid();
    }

    private void IsValid()
    {
        if (string.IsNullOrEmpty(this.Name))
        {
            throw new ValidationEntityException("Nome inválido");
        }

        if (string.IsNullOrEmpty(this.Email) || !Regex.IsMatch(Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
        {
            throw new ValidationEntityException("Email inválido");
        }
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; } 
}