//using AndroidX.Lifecycle;
using MedicalUTP.DataAcess;
using MedicalUTP.ViewsModel;

namespace MedicalUTP.Pages;

public partial class CRUDAdmin : ContentPage
{
    private readonly MedicalUTPDbContext _context;
    public CRUDAdmin(MedicalUTPDbContext context)
	{
		InitializeComponent();
        _context = context;
        BindingContext = new CRUDAdminViewModel(_context);
    }
}