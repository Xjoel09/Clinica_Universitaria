using MedicalUTP.DataAcess;
using MedicalUTP.ViewsModel;

namespace MedicalUTP.Pages;

public partial class CRUDAdmin : ContentPage
{
	public CRUDAdmin(MedicalUTPDbContext context)
	{
		InitializeComponent();
        BindingContext = new CRUDAdminViewModel(context);
    }
}