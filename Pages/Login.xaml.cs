using MedicalUTP.DataAcess;
using MedicalUTP.Pages;

namespace MedicalUTP.Pages;

public partial class Login : ContentPage
{
    private readonly MedicalUTPDbContext _context;
    public Login(MedicalUTPDbContext context)
    {
        InitializeComponent();
        _context = context;
    }
    public async void IrASingUn(object sender, EventArgs e)
    {
        var dbContext = _context;
        await Navigation.PushAsync(new Register(dbContext));
    }
}