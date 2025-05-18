using System;

public class Registration
{
    public string name { get; set; }
    public string email { get; set; }
    public string phone { get; set; }
    public string address { get; set; }
    public string birthdate { get; set; }
    public string NewPassword { get; set; }

    public RegistrationState State { get; set; } = RegistrationState.Unregistered;
    public string ReEnterPassword { get;set; }

    public Registration(string name, string email, string phone, string address, string birthdate, string NewPassword, string ReEnterPassword)
    {
        this.name = name;
        this.email = email;
        this.phone = phone;
        this.address = address;
        this.birthdate = birthdate;
        this.NewPassword = NewPassword;
        this.ReEnterPassword = ReEnterPassword;
    }
}
