namespace ITHSystems.DTOs;

public record class PersonDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";
    public string FullNameNormalize => FullName.ToUpper();
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string CardName{ get; set; } = string.Empty;
    public string CardType { get;  set; } = string.Empty;
    public string CardTypeNormalized => CardType.ToUpper();
}
