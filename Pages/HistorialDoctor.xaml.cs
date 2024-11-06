using MedicalUTP.ViewModel;
using MedicalUTP.DataAcess;
using Microsoft.Maui.Controls;

namespace MedicalUTP.Pages
{
    public partial class HistorialDoctor : ContentPage
    {
        private readonly MedicalUTPDbContext _context;
        private readonly HistorialDoctorViewModel _viewModel;

        public HistorialDoctor(HistorialDoctorViewModel viewModel, MedicalUTPDbContext context)
        {
            InitializeComponent();
            _context = context;
            _viewModel = viewModel;
            BindingContext = viewModel;
        }

        private void OnHistorialClicked(object sender, EventArgs e)
        {
            
            var historialEditor = new Editor
            {
                Placeholder = "Escribe aquí el historial médico...",
                HeightRequest = 200,
                BackgroundColor = Colors.LightGray
            };
            historialEditor.SetBinding(Editor.TextProperty, "HistorialTexto", mode: BindingMode.TwoWay);

            
            var saveButton = new Button
            {
                Text = "Guardar",
                HorizontalOptions = LayoutOptions.End
            };
            saveButton.Clicked += async (s, args) =>
            {
                await _viewModel.GuardarHistorialAsync();
                await DisplayAlert("Éxito", "Historial guardado correctamente.", "OK");
            };

            
            var layout = new StackLayout();
            layout.Children.Add(historialEditor);
            layout.Children.Add(saveButton);

            SidebarContent.Content = layout;
            SideBar.IsVisible = true;
        }

        private async void OnCertificadosClicked(object sender, EventArgs e)
        {
            try
            {
                
                var certificateImage = new Image
                {
                    Source = "certificado_pdf.png",
                    BackgroundColor = Colors.Transparent,
                    WidthRequest = 100, 
                    HeightRequest = 100
                };

                
                var downloadButton = new Button
                {
                    Text = "Descargar Certificado",
                    HorizontalOptions = LayoutOptions.Center,
                    BackgroundColor = Colors.LightBlue
                };

                downloadButton.Clicked += async (s, args) =>
                {
                    try
                    {
                        
                        var fileName = "Certificado de salud.pdf";
                        var filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);

                        if (!File.Exists(filePath))
                        {
                            
                            using var stream = await FileSystem.OpenAppPackageFileAsync(fileName);
                            using var newStream = File.Create(filePath);
                            await stream.CopyToAsync(newStream);
                        }

                        
                        await Launcher.Default.OpenAsync(new OpenFileRequest
                        {
                            File = new ReadOnlyFile(filePath)
                        });
                    }
                    catch (Exception ex)
                    {
                        
                        await DisplayAlert("Error", $"Hubo un problema al abrir el archivo: {ex.Message}", "OK");
                    }
                };

                
                var layout = new StackLayout
                {
                    Children = { certificateImage, downloadButton },
                    Padding = 20,
                    HorizontalOptions = LayoutOptions.CenterAndExpand, 
                    VerticalOptions = LayoutOptions.CenterAndExpand 
                };

                
                if (SidebarContent != null)
                {
                    SidebarContent.Content = layout;
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo inicializar el contenido de la barra lateral", "OK");
                }

                
                SideBar.IsVisible = true;
            }
            catch (Exception ex)
            {
                
                await DisplayAlert("Error", $"Hubo un problema al intentar mostrar el contenido: {ex.Message}", "OK");
            }
        }




        private void OnReferenciasMedicasClicked(object sender, EventArgs e)
        {
           
            var imageButton = new ImageButton
            {
                Source = "referencia_pdf.png", 
                BackgroundColor = Colors.Transparent,
                WidthRequest = 100,
                HeightRequest = 100
            };

            
            var downloadButton = new Button
            {
                Text = "Descargar Referencia Médica",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            downloadButton.Clicked += async (s, args) =>
            {
                
                var fileName = "Referencias medica.pdf"; 
                var filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);

                if (!File.Exists(filePath))
                {
                    try
                    {
                       
                        using Stream stream = await FileSystem.OpenAppPackageFileAsync(fileName);
                        using FileStream newStream = File.Create(filePath);
                        await stream.CopyToAsync(newStream);
                    }
                    catch (Exception ex)
                    {
                        
                        await Application.Current.MainPage.DisplayAlert("Error", "No se pudo cargar el archivo PDF: " + ex.Message, "OK");
                        return;
                    }
                }

                
                await Launcher.Default.OpenAsync(new OpenFileRequest
                {
                    File = new ReadOnlyFile(filePath)
                });
            };

            
            var layout = new StackLayout
            {
                Children = { imageButton, downloadButton },
                Spacing = 10,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            SidebarContent.Content = layout;
            SideBar.IsVisible = true;
        }


        private void OnCloseSidebarClicked(object sender, EventArgs e)
        {
            SideBar.IsVisible = false;
        }
    }
}








