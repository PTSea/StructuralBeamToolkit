using StructuralBeamToolkit.Commands;
using StructuralBeamToolkit.Models;
using StructuralBeamToolkit.Services;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace StructuralBeamToolkit.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged, IDataErrorInfo
    {

        public double? BeamLength => TryParsePositive(BeamLengthText);
        public double? LoadMagnitude => TryParsePositive(LoadMagnitudeText);
        public double? YoungsModulusE => TryParsePositive(YoungsModulusEText);
        public double? MomentOfInertiaI => TryParsePositive(MomentOfInertiaIText);


        private string _beamLengthText = "10";
        public string BeamLengthText
        {
            get => _beamLengthText;
            set
            {
                if (_beamLengthText != value)
                {
                    _beamLengthText = value;
                    OnPropertyChanged();
                }
            }
        }


        private string _loadMagnitude = "1000"; 

        public string LoadMagnitudeText
        {
            get => _loadMagnitude;
            set
            {
                if (_loadMagnitude != value)
                {
                    _loadMagnitude = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _youngsModulusE = "200e9";

        public string YoungsModulusEText
        {
            get => _youngsModulusE;
            set
            {
                if (_youngsModulusE != value)
                {
                    _youngsModulusE = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _momentOfInertiaI = "1e-6"; 

        public string MomentOfInertiaIText
        {
            get => _momentOfInertiaI;
            set
            {
                if (_momentOfInertiaI != value)
                {
                    _momentOfInertiaI = value;
                    OnPropertyChanged();
                }
            }
        }

        private LoadType _selectedLoadType = LoadType.PointLoadCenter;

        public LoadType SelectedLoadType
        {
            get => _selectedLoadType;
            set
            {
                if (_selectedLoadType != value)
                {
                    _selectedLoadType = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _maxDeflection;

        public double MaxDeflection
        {
            get => _maxDeflection;
            set
            {
                if (_maxDeflection != value)
                {
                    _maxDeflection = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _maxMoment;

        public double MaxMoment
        {
            get => _maxMoment;
            set
            {
                if (_maxMoment != value)
                {
                    _maxMoment = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand CalculateCommand { get; }
        public ICommand ResetCommand { get; }

        private readonly BeamCalculator _beamCalculator = new BeamCalculator();

        private readonly RelayCommand _calculateCommand;

        public MainViewModel()
        {
            _calculateCommand = new RelayCommand(Calculate, CanCalculate);
            CalculateCommand = _calculateCommand;
            ResetCommand = new RelayCommand(Reset);
        }

        private void Calculate()
        {
            // CanCalculate guarantees these are non-null, but we’ll be defensive:
            var L = BeamLength ?? throw new InvalidOperationException("Invalid length");
            var P_or_w = LoadMagnitude ?? throw new InvalidOperationException("Invalid load");
            var E = YoungsModulusE ?? throw new InvalidOperationException("Invalid E");
            var I = MomentOfInertiaI ?? throw new InvalidOperationException("Invalid I");

            var input = new BeamInput
            {
                Length = L,
                Load = P_or_w,
                E = E,
                I = I,
                LoadType = SelectedLoadType
            };

            var result = _beamCalculator.Calculate(input);

            MaxMoment = result.MaxMoment;
            MaxDeflection = result.MaxDeflection;
        }


        private bool CanCalculate()
        {
            return string.IsNullOrEmpty(this[nameof(BeamLengthText)]) &&
                   string.IsNullOrEmpty(this[nameof(LoadMagnitudeText)]) &&
                   string.IsNullOrEmpty(this[nameof(YoungsModulusEText)]) &&
                   string.IsNullOrEmpty(this[nameof(MomentOfInertiaIText)]);
        }


        private void Reset()
        {
            BeamLengthText = "10";
            LoadMagnitudeText = "1000";
            YoungsModulusEText = "200e9";
            MomentOfInertiaIText = "1e-6";
            SelectedLoadType = LoadType.PointLoadCenter;
            MaxDeflection = 0.0;
            MaxMoment = 0.0;
        }

        public IReadOnlyList<LoadType> LoadTypes { get; } =
            new[] { LoadType.PointLoadCenter, LoadType.UniformLoad };

        public string Error => "";

        public string this[string columnName]
        {
            get
            {
                return columnName switch
                {
                    nameof(BeamLengthText) => BeamLength is null ? "Length must be a number greater than 0." : null,
                    nameof(LoadMagnitudeText) => LoadMagnitude is null ? "Load must be a number greater than 0." : null,
                    nameof(YoungsModulusEText) => YoungsModulusE is null ? "E must be a number greater than 0." : null,
                    nameof(MomentOfInertiaIText) => MomentOfInertiaI is null ? "I must be a number greater than 0." : null,
                    _ => null
                };
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

            CommandManager.InvalidateRequerySuggested();
            _calculateCommand.RaiseCanExecuteChanged();
        }

        private static double? TryParsePositive(string? text)
        {
            if (string.IsNullOrWhiteSpace(text)) return null;

            if (!double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out var val))
                return null;

            return val > 0 ? val : null;
        }
    }
}
