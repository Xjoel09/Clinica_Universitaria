using System.ComponentModel;
using System.Windows.Input;

namespace MedicalUTP.ViewModel;

public class ConsultaViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private string _tipoConsulta;
    public string TipoConsulta
    {
        get => _tipoConsulta;
        set
        {
            _tipoConsulta = value;
            OnPropertyChanged(nameof(TipoConsulta));
        }
    }

    private string _diaSeleccionado;
    public string DiaSeleccionado
    {
        get => _diaSeleccionado;
        set
        {
            _diaSeleccionado = value;
            OnPropertyChanged(nameof(DiaSeleccionado));
        }
    }

    private string _horaSeleccionada;
    public string HoraSeleccionada
    {
        get => _horaSeleccionada;
        set
        {
            _horaSeleccionada = value;
            OnPropertyChanged(nameof(HoraSeleccionada));
        }
    }

    public List<string> TiposConsultas { get; set; } = new List<string> { "Consulta General", "Consulta Especialista" };
    public List<string> DiasDisponibles { get; set; } = new List<string> { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes" };
    public List<string> HorasDisponibles { get; set; } = new List<string> { "10:00 AM", "11:00 AM", "12:00 PM", "1:00 PM" };

    public ICommand SubmitCommand { get; }

    public ConsultaViewModel()
    {
        SubmitCommand = new Command(OnSubmit);
    }

    private void OnSubmit()
    {
        Application.Current.MainPage.DisplayAlert("Solicitud Enviada",
            $"Consulta: {TipoConsulta}, Día: {DiaSeleccionado}, Hora: {HoraSeleccionada}",
            "OK");
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
