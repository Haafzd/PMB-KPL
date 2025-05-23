using PMB.Models;
using System;

public class Registration
{
    public string email { get; set; }
    public string NewPassword { get; set; }
    public BankTransferPaymentRequest PaymentRequest { get; set; }
    public RegistrationState State { get; set; } = RegistrationState.Unregistered;
    public string ReEnterPassword { get;set; }

    public Registration(string email, string NewPassword, string ReEnterPassword)
    {
        this.email = email;
        this.NewPassword = NewPassword;
        this.ReEnterPassword = ReEnterPassword;
    }
}
