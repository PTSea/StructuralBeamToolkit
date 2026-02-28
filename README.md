# Structural Beam Toolkit

A WPF desktop application for performing **basic structural beam analysis** using standard closed-form equations.  
This tool is intended for **quick checks, workflow support, and educational use**, not as a replacement for full structural analysis software.

---

## 📌 Overview

Structural Beam Toolkit models a **simply supported beam** under common loading conditions and computes key response quantities engineers care about:

- Maximum bending moment  
- Maximum vertical deflection  

The project demonstrates:

- WPF desktop UI development
- MVVM architecture
- Separation of UI and calculation logic
- Unit-tested engineering calculations

---

## 🧱 Supported Load Cases

The following load cases are currently implemented:

1. **Center Point Load**
   - A single concentrated load applied at mid-span
2. **Uniformly Distributed Load**
   - A constant load applied over the full span

Both cases assume a **simply supported beam**.

---

## 🧮 Inputs

All inputs must use **consistent units** (SI, Imperial, etc.).

| Input | Description |
|------|------------|
| Beam Length (L) | Distance between supports |
| Load Type | Point load at center or uniform load |
| Load Magnitude | Force (point load) or force per length (uniform load) |
| Young’s Modulus (E) | Material stiffness |
| Moment of Inertia (I) | Section resistance to bending |

### Example (SI Units)

- `L` → meters  
- Load → Newtons (or N/m)  
- `E` → Pascals  
- `I` → m⁴  

---

## 📤 Outputs

| Output | Description |
|------|------------|
| Max Bending Moment | Peak internal bending moment (mid-span) |
| Max Deflection | Peak vertical deflection (mid-span) |

---

## ⚙️ Engineering Assumptions

This project intentionally uses **simplified beam theory** with the following assumptions:

- Simply supported beam (pin/roller idealization)
- Linear elastic material behavior
- Small deflections
- Constant material and cross-section along the span
- Closed-form Euler–Bernoulli beam equations

These assumptions are appropriate for **preliminary design checks and internal tools**, but not for detailed structural design or code-compliance verification.

---

## 📐 Formulas Used

### Center Point Load (P at mid-span)

- Maximum bending moment:
Mmax = P · L / 4

- Maximum deflection:
δmax = P · L³ / (48 · E · I)

---

### Uniformly Distributed Load (w over full span)

- Maximum bending moment:
Mmax = w · L² / 8

- Maximum deflection:
δmax = 5 · w · L⁴ / (384 · E · I)

---

## 🏗 Architecture

The application follows the **MVVM pattern**:
StructuralBeamToolkit
│
├── App.xaml
│
├──AssemblyInfo.cs│
│
├── MainWindow.xaml
│ └── MainWindow.xaml.cs
│
├── Models
│ ├── BeamInput.cs
│ ├── BeamResult.cs
│ └── LoadType.cs
│
├── Services
│ └── BeamCalculator.cs // Engineering calculations
│
├── ViewModels
│ └── MainViewModel.cs // UI state and commands only
│
├── Commands
│ └── RelayCommand.cs
│
└── StructuralBeamToolkit.Tests
└── BeamCalculatorTests.cs


### Key Design Decisions

- Calculation logic is isolated in `BeamCalculator` for **testability**
- ViewModel contains **no engineering formulas**
- UI uses bindings and commands (no business logic in code-behind)
- Engineering math is unit-tested with xUnit

---

## 🧪 Testing

The calculation engine is covered by **xUnit tests**, validating:

- Correct bending moment calculations for each load case
- Correct deflection calculations for each load case
- Input validation behavior
- Separation of UI and computation logic

Tests live in a dedicated test project and target the calculation service directly.

---

## ▶️ How to Run

1. Open the solution in **Visual Studio**
2. Build the solution
3. Run the WPF project
4. Enter beam parameters and click **Calculate**
5. View results in the UI to the right
6. Click **Reset** to clear inputs

---

## 🚀 Future Enhancements

Potential next steps:

- Additional load cases (eccentric point load, partial uniform load)
- Stress calculations
- Section property calculators
- CSV / PDF export
- Deflection curve visualization
- Material presets

---

## ⚠️ Disclaimer

This software is intended for **educational and workflow-support purposes only**.  
It is not a substitute for licensed engineering judgment or full structural analysis tools.

---


