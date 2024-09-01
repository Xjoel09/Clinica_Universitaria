namespace MedicalUTP.Pages;

public partial class FlyoutMenu : FlyoutPage
{
	public FlyoutMenu()
	{
		InitializeComponent();
		flyoutPage.collectioView.SelectionChanged += CollectionView_SelectionChanged;
	}
	private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		var item = e.CurrentSelection.FirstOrDefault() as FlyoutItems;
		if (item != null)
		{
			Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
			IsPresented = false;
		}
	}
}